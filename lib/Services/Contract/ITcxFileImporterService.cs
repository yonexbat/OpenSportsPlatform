using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace OpenSportsPlatform.Lib.Services.Contract
{
    public interface ITcxFileImporterService
    {
        Task ImoportWorkout(Stream stream);
    }
}
