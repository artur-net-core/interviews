using System.Collections.Generic;

namespace CachingExcersize.Database
{
    class Database : IDatabase
    {
        private static readonly List<Country> Countries =
            new List<Country>
            {
                new Country { Id = 1, Name = "Belgium" },
                new Country { Id = 2, Name = "France" },
                new Country { Id = 3, Name = "England" },
                new Country { Id = 4, Name = "Croatia" }
            };

        public IReadOnlyList<Country> GetCountries()
        {
            return Countries.AsReadOnly();
        }
    }
}
