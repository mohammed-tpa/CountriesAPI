using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer.Controllers
{
    [ApiController]
    public class CountriesController : Controller
    {
        readonly CountriesDataProvider _countriesDataProvider;
        public CountriesController(CountriesDataProvider countriesDataProvider)
        {
            _countriesDataProvider = countriesDataProvider;
        }

        [HttpGet]
        [Route("/api/GetCountries")]
        public IEnumerable<Country> GetCountries()
        {
            return _countriesDataProvider.GetCountries();
        }

        [HttpGet]
        [Route("/api/GetAllMonthHolidays")]
        public IEnumerable<MonthHoliday> GetAllMonthHolidays(string countryCode, int year)
        {
            var holidaysForYear = _countriesDataProvider.GetHolidaysForYear(countryCode, year);
            return holidaysForYear.GroupBy(s => s.Date.Month).Select(s => new MonthHoliday()
            {
                Month = s.Key,
                Holidays = s.Select(holiday => new Holiday()
                {
                    Name = holiday.HolidayNames.FirstOrDefault(name => name.Lang == "en")?.Text,
                    Type = holiday.HolidayType
                }).ToArray()
            });
        }

        [HttpGet]
        [Route("/api/GetMaximumNumberOfFreeDays")]
        public int GetMaximumNumberOfFreeDays(string countryCode, int year)
        {
            //Need to implement Logic to calculate the maximum number of free days in a row 
            return 5;
        }

        [HttpGet]
        [Route("/api/GetDayStatus")]
        public DayStatus GetDayStatus(string countryCode, string date)
        {
            return _countriesDataProvider.GetDayStatus(countryCode, date);
        }
    }
}
