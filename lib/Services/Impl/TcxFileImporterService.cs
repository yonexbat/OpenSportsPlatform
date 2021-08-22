using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpenSportsPlatform.Lib.Database;
using OpenSportsPlatform.Lib.Model.Dtos.Import;
using OpenSportsPlatform.Lib.Model.Entities;
using OpenSportsPlatform.Lib.Services.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace OpenSportsPlatform.Lib.Services.Impl
{
    public class TcxFileImporterService : ITcxFileImporterService
    {

        private readonly ILogger _logger;
        private readonly OpenSportsPlatformDbContext _dbContext;
        private readonly ISecurityService _securityService;

        private Workout _workout;
        private Segment _segment;
        private Sample _sample;
        private ImportTcxState _importState;

        private string _currentElement;
        private string _currentHeartRate;

        private float? _lastElevation;
        private float _ascendInMeters;
        private float _descendInMeters;

        public TcxFileImporterService(
            ILogger<TcxFileImporterService> logger,
            ISecurityService securityService,
            OpenSportsPlatformDbContext dbContext)
        {
            _logger = logger;
            _securityService = securityService;
            _dbContext = dbContext;
        }

        public async Task ImportWorkout(Stream stream)
        {
            _sample = null;
            _segment = null;
            _workout = null;
            _currentElement = null;
            _currentHeartRate = null;
            _importState = ImportTcxState.Activity;
            _lastElevation = null;
            _ascendInMeters = 0;
            _descendInMeters = 0;
            

            string user = _securityService.GetCurrentUserid();
            _logger.LogInformation("Importing workout for user {0}", user);

            //Get User
            UserProfile userEntity = await _dbContext.UserProfile.Where(x => x.UserId == user)
                .SingleAsync();

            _workout = new Workout()
            {
                Segments = new List<Segment>(),
            };
            _workout.UserProfile = userEntity;
            await _dbContext.AddAsync(_workout);

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Async = true;

            _importState = ImportTcxState.Activity;

            using (XmlReader reader = XmlReader.Create(stream, settings))
            {
                while (await reader.ReadAsync())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            await ReadElement(reader);
                            break;
                        case XmlNodeType.Text:
                            string value = await reader.GetValueAsync();
                            SetValue(value);
                            break;
                        case XmlNodeType.EndElement:
                            EndElement(reader);
                            break;                        
                    }
                }
            }

            // Calculate values
            IEnumerable<float?> cadence = _workout
                .Segments
                .SelectMany(x => x.Samples)
                .Select(x => x.CadenceRpm)
                .Where(x => x > 0);

            _workout.CadenceMaxRpm = cadence.Max();
            _workout.CadenceAvgRpm = cadence.Average();

            IEnumerable<float?> altitude = _workout
              .Segments
              .SelectMany(x => x.Samples)
              .Select(x => x.AltitudeInMeters)
              .Where(x => x > 0);

            _workout.AltitudeMaxInMeters = altitude.Max();
            _workout.AltitudeMinInMeters = altitude.Min();

            _workout.StartTime = _workout.Segments?.First().Samples?.First().Timestamp;
            _workout.EndTime = _workout.Segments?.Last().Samples?.Last().Timestamp;

            _workout.AscendInMeters = _ascendInMeters;
            _workout.DescendInMeters = _descendInMeters;
                

            await _dbContext.SaveChangesAsync();
        }

        private void EndElement(XmlReader reader)
        {
            string localhame = reader.LocalName;
            switch(localhame)
            {
                case "HeartRateBpm":
                case "AverageHeartRateBpm":
                case "MaximumHeartRateBpm":
                    _currentHeartRate = null;
                    break;
            }
        }

        private void SetValue(string value)
        {
            switch(_currentElement)
            {
                case "Time":
                    if (_importState == ImportTcxState.Trackpoint)
                    {
                        _sample.Timestamp = DateTime.Parse(value);
                    }
                    break;
                case "AltitudeMeters":
                    if (_importState == ImportTcxState.Trackpoint)
                    {
                        _sample.AltitudeInMeters = float.Parse(value);
                        if(_lastElevation.HasValue)
                        {
                            float diff = _sample.AltitudeInMeters.Value - _lastElevation.Value;
                            if(diff > 0)
                            {
                                _ascendInMeters += diff;
                            }
                            else
                            {
                                _descendInMeters += (diff*-1);
                            }
                        }
                       
                        _lastElevation = _sample.AltitudeInMeters;                       
                    }
                    break;                
                case "DistanceMeters":
                    float dist = float.Parse(value);
                    dist = dist * 0.001f;
                    if(_importState == ImportTcxState.Trackpoint) 
                    {
                        _sample.DistanceInKm = dist;
                    }
                    else if(_importState == ImportTcxState.Lap)
                    {
                        if(!_workout.DistanceInKm.HasValue)
                        {
                            _workout.DistanceInKm = 0;
                        }
                        _workout.DistanceInKm += dist;
                    }
                    break;
                case "HeartRateBpm":
                    float heartRate = float.Parse(value);
                    if (_importState == ImportTcxState.Trackpoint)
                    {
                        _sample.HeartRateBpm = heartRate;
                    }
                    break;
                case "Speed":
                    float speedMetersPerSecond = float.Parse(value);
                    float kmH = speedMetersPerSecond * 0.001f * 60f * 60f;
                    if(_importState == ImportTcxState.Trackpoint)
                    {
                        _sample.SpeedKmh = kmH;
                    }
                    break;
                case "LatitudeDegrees":
                    if (_importState == ImportTcxState.Trackpoint)
                    {
                        _sample.Latitude = double.Parse(value);
                        AddCoordinates();
                    }
                    break;
                case "LongitudeDegrees":
                    if(_importState == ImportTcxState.Trackpoint)
                    {
                        _sample.Longitude = double.Parse(value);
                        AddCoordinates();
                    }
                    break;
                case "Value":
                    if(_currentHeartRate == "HeartRateBpm" && _importState == ImportTcxState.Trackpoint)
                    {
                        _sample.HeartRateBpm = float.Parse(value);
                    }
                    if (_currentHeartRate == "AverageHeartRateBpm" && _importState == ImportTcxState.Lap)
                    {
                        _workout.HeartRateAvgBpm= float.Parse(value);
                    }
                    if (_currentHeartRate == "MaximumHeartRateBpm" && _importState == ImportTcxState.Lap)
                    {
                        _workout.HeartRateMaxBpm = float.Parse(value);
                    }
                    break;
                case "MaximumSpeed":
                    if(_importState == ImportTcxState.Lap)
                    {
                        float speedMetersPerSecond2 = float.Parse(value);
                        float kmH2 = speedMetersPerSecond2 * 0.001f * 60f * 60f;

                        if (!_workout.SpeedMaxKmh.HasValue)
                        {
                            _workout.SpeedMaxKmh = 0;
                        }
                        _workout.SpeedMaxKmh = (float) Math.Max(_workout.SpeedMaxKmh.Value, kmH2);   
                    }
                    break;
                case "Calories":
                    if(_importState == ImportTcxState.Lap)
                    {
                        float calories = float.Parse(value);
                        if(!_workout.CaloriesInKCal.HasValue)
                        {
                            _workout.CaloriesInKCal = 0;
                        }
                        _workout.CaloriesInKCal += calories;
                    }
                    break;
                case "TotalTimeSeconds":
                    float duration = float.Parse(value);
                    if(_importState == ImportTcxState.Lap)
                    {
                        if(!_workout.DurationInSec.HasValue)
                        {
                            _workout.DurationInSec = 0;
                        }
                        _workout.DurationInSec += duration;
                    }
                    break;
                case "RunCadence":
                    float cadence = float.Parse(value);
                    if(_importState == ImportTcxState.Trackpoint)
                    {
                        _sample.CadenceRpm = cadence;
                    }
                    break;
                case "Id":
                    _workout.Name = value;
                    break;
            }
        }

        private void AddCoordinates()
        {
            if(_sample.Longitude.HasValue && _sample.Latitude.HasValue)
            {
                _sample.Location = new NetTopologySuite.Geometries.Point(_sample.Longitude.Value, _sample.Latitude.Value) { SRID = 4326 };
            }
        }

        private async Task ReadElement(XmlReader reader)
        {
            _currentElement = reader.LocalName;
            switch(_currentElement)
            {
                case "Activity":
                    await ReadElementActivity(reader);
                    break;
                case "Lap":
                    await ReadElementLap(reader);
                    break;
                case "Trackpoint":
                    await ReadElementTrackpoint(reader);
                    break;
                case "HeartRateBpm":
                    _currentHeartRate = "HeartRateBpm";
                    break;
                case "AverageHeartRateBpm":
                    _currentHeartRate = "AverageHeartRateBpm";
                    break;
                case "MaximumHeartRateBpm":
                    _currentHeartRate = "MaximumHeartRateBpm";
                    break;
            }   
        }

        private async Task ReadElementTrackpoint(XmlReader reader)
        {
            _sample = new Sample();
            _sample.Segment = _segment;
            await _dbContext.AddAsync(_sample);
            _importState = ImportTcxState.Trackpoint;
        }

        private async Task ReadElementLap(XmlReader reader)
        {
            _segment = new Segment();
            _segment.Workout = _workout;
            await _dbContext.AddAsync(_segment);
            _importState = ImportTcxState.Lap;
        }

        private async Task ReadElementActivity(XmlReader reader)
        {
            string sport = reader.GetAttribute("Sport");
            SportsCategory category = await _dbContext.SportsCategory.Where(x => x.Name == sport).SingleOrDefaultAsync();
            if(category == null)
            {
                category = new SportsCategory()
                {
                    Name = sport,
                };
                await _dbContext.AddAsync(category);
            }
            _workout.SportsCategory = category;
        }
    }
}
