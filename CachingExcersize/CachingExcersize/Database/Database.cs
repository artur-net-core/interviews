using System.Collections.Generic;
using System.Linq;

namespace CachingExcersize.Database
{
    public class Database : IDatabase
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
            return Countries
                .Select(c => new Country { Id = c.Id, Name = c.Name })
                .ToList()
                .AsReadOnly();
        }

        public void Update(int id, string name)
        {
            var country = Countries.FirstOrDefault(c => c.Id == id);

            if (country != null)
            {
                country.Name = name;
            }
        }
    }
}
