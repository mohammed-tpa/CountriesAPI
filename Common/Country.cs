using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    [Table("Countries")]
    public class Country
    {
        [Key]
        public string CountryCode { get; set; }

        [NotMapped]
        public string[] Regions { get; set; }

        [NotMapped]
        public string[] HolidayTypes { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public string InternalRegions
        {
            get => string.Join(",", Regions);
            set
            {
                Regions = value.Split(",");
            }
        }

        [System.Text.Json.Serialization.JsonIgnore]
        public string InternalHolidayTypes
        {
            get => string.Join(",", HolidayTypes);
            set
            {
                HolidayTypes = value.Split(",");
            }
        }

        public string FullName { get; set; }

        public HolidayDate FromDate { get; set; }

        public HolidayDate ToDate { get; set; }
    }

    public class HolidayDate
    {
        [Key]
        [System.Text.Json.Serialization.JsonIgnore]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public int Day { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public int  DayOfWeek { get; set; }
    }

    public class HolidayName
    {
        [Key]
        [System.Text.Json.Serialization.JsonIgnore]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Lang { get; set; }

        public string Text { get; set; }
    }

    public class Day
    {
        [Key]
        [System.Text.Json.Serialization.JsonIgnore]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public HolidayDate Date { get; set; }

        public string Country { get; set; }

        public bool IsWorkingDay { get; set; }

        public bool IsPublicHoliday { get; set; }
    }

    [Table("HolidayForYears")]
    public class HolidayForYear
    {
        [Key]
        [System.Text.Json.Serialization.JsonIgnore]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public HolidayDate Date { get; set; }

        [JsonProperty("Name")]
        public List<HolidayName> HolidayNames { get; set; }

        public string HolidayType { get; set; }

        public string Country { get; set; }
    }

    public class MonthHoliday
    {
        [Key]
        [System.Text.Json.Serialization.JsonIgnore]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public int Month { get; set; }

        public Holiday[] Holidays { get; set; }
    }

    public class Holiday
    {
        [Key]
        [System.Text.Json.Serialization.JsonIgnore]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }

        public string Type { get; set; }
    }

    public class DayStatus
    {
        [Key]
        [System.Text.Json.Serialization.JsonIgnore]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public bool IsPublicHoliday { get; set; }
        public bool IsWorkDay { get; set; }
        public bool IsFreeDay { get; set; }
    }
}
