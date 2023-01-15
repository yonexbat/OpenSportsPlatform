using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSportsPlatform.Lib.Model.Entities
{
    public interface IEntity
    {
        int Id { get; set; }
        string? InsertUser { get; set; }
        DateTimeOffset? InsertDate { get; set; }
        string? UpdateUser { get; set; }
        DateTimeOffset? UpdateDate { get; set; }
    }
}
