using DataLayer.Models;
using DataLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class CountriesDataProvider
    {
        const string BASE_URL = "https://kayaposoft.com/enrico/json/v2.0";
        DatabaseContext _dataContext;
        public CountriesDataProvider(DatabaseContext databaseContext)
        {
            _dataContext = databaseContext;
        }
        private string GetURL(string action)
        {
            return $"{BASE_URL}?action={action}";
        }

        private string GetResponse(string url)
        {
            HttpClient client = new HttpClient();
            return client.GetStringAsync(url).Result;
        }

        public IEnumerable<Country> GetCountries()
        {
            // get the countries from the database
            var countries = _dataContext.Countries.ToArray();
            if (countries.Length == 0)
            {
                var result = GetResponse(GetURL("getSupportedCountries"));
                countries = JsonConvert.DeserializeObject<Country[]>(result);

                // save it in database
                _dataContext.Countries.AddRange(countries);
                _dataContext.SaveChanges();
            }
            return countries;


        }

        public IEnumerable<HolidayForYear> GetHolidaysForYear(string countryCode, int year)
        {
            IEnumerable<HolidayForYear> holidayForYear = _dataContext.HolidayForYears
                .Include(b => b.Date)
                .Include(b => b.HolidayNames)
                .Where(s => s.Country == countryCode && s.Date.Year == year);
            if (holidayForYear.Count() == 0)
            {
                var url = GetURL($"getHolidaysForYear&year={year}&country={countryCode}");
                var result = GetResponse(url);
                holidayForYear = JsonConvert.DeserializeObject<HolidayForYear[]>(result);

                foreach (var holiday in holidayForYear)
                {
                    holiday.Country = countryCode;
                }

                _dataContext.HolidayForYears.AddRange(holidayForYear);
                _dataContext.SaveChanges();
            }

            return holidayForYear;
        }

        public DayStatus GetDayStatus(string countryCode, string date)
        {

            if (DateTime.TryParseExact(date, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime dateTime))
            {
                var day = _dataContext.Days.Include(s => s.Date).FirstOrDefault(s =>
                s.Country == countryCode && s.Date.Day == dateTime.Day && s.Date.Month == dateTime.Month && s.Date.Year == dateTime.Year);
                if (day == null)
                {
                    day = new Day()
                    {
                        Country = countryCode,
                        Date = new HolidayDate()
                        {
                            Day = dateTime.Day,
                            Month = dateTime.Month,
                            Year = dateTime.Year
                        }
                    };

                    var url = GetURL($"isWorkDay&date={date}&country={countryCode}");
                    var result = GetResponse(url);
                    var data = JsonConvert.DeserializeAnonymousType(result, new { isWorkingDay = true });

                    url = GetURL($"isPublicHoliday&date={date}&country={countryCode}");
                    result = GetResponse(url);
                    var publicHolidayData = JsonConvert.DeserializeAnonymousType(result, new { isPublicHoliday = true });
                    day.IsPublicHoliday = publicHolidayData.isPublicHoliday;

                    _dataContext.Days.Add(day);
                    _dataContext.SaveChanges();

                }

                return new DayStatus()
                {
                    IsPublicHoliday = day.IsPublicHoliday,
                    IsFreeDay = !day.IsPublicHoliday && !day.IsWorkingDay,
                    IsWorkDay = day.IsWorkingDay
                };
            }

            throw new Exception("Invalid date.");
        }
    }
}
