using CachingExcersize.Database;
using System.Collections.Generic;

namespace CachingExcersize.Services
{
    class CountriesService
    {
        private readonly IDatabase _db;

        public CountriesService(IDatabase db)
        {
            _db = db;
        }

        public IReadOnlyCollection<Country> GetAllCountries()
        {
            return _db.GetCountries();
        }
    }
}
