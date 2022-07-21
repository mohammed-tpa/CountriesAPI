using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Xunit;

namespace UnitTests
{
    public class CountriesDataProviderTests
    {
        CountriesDataProvider dataProvider;
        public CountriesDataProviderTests()
        {
            var services = new ServiceCollection();
            services.AddScoped<CountriesDataProvider>();

            services.AddDbContext<DatabaseContext>(o =>
            {
                o.UseSqlServer(@"Server=tcp:shimjithmohammed.database.windows.net,1433;Initial Catalog=test;Persist Security Info=False;User ID=shimjithmohammed;Password=Admin@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            });

            var serviceProvider = services.BuildServiceProvider();

            dataProvider = serviceProvider.GetService<CountriesDataProvider>();
        }

        [Fact]
        public void GetCountriesTest()
        {

            var countries = dataProvider.GetCountries();
            Assert.Equal(54, countries.ToList().Count);
        }

        [Fact]
        public void GetHolidaysForYearTest()
        {

            var holidayForYears = dataProvider.GetHolidaysForYear("us", 2022);
            Assert.Equal(10, holidayForYears.ToList().Count);
        }

        [Fact]
        public void GetDayStatus()
        {

            var data = dataProvider.GetDayStatus("hun", "30-07-2022");
            Assert.False(data.IsWorkDay);
            Assert.False(data.IsPublicHoliday);

            data = dataProvider.GetDayStatus("hun", "29-07-2022");
            Assert.False(data.IsWorkDay);
            Assert.False(data.IsPublicHoliday);

            data = dataProvider.GetDayStatus("svk", "05-07-2022");
            Assert.False(data.IsWorkDay);
            Assert.True(data.IsPublicHoliday);
        }
    }
}
