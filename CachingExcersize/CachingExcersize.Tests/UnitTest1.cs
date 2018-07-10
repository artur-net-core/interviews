using NUnit.Framework;
using System;
using System.Runtime.Caching;
using CachingExcersize.Services;

namespace CachingExcersize.Tests
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void TestMethod1()
        {
            // Arrange
            var db = new Database.Database();
            var service = new CountriesService(db);
            var countries = service.GetAllCountries();

            // Act
            db.Update(1, "Poland");
            var newCountres = service.GetAllCountries();

            // Assert
            Assert.AreEqual(countries, newCountres);
        }
    }
}
