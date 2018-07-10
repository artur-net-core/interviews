using CachingExcersize.Database;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace CachingExcersize.Services
{
    public class CountriesService
    {
        private readonly IDatabase _db;
        private readonly MemoryCache _cache;

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
