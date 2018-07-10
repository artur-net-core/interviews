using System.Collections.Generic;

namespace CachingExcersize.Database
{
    public interface IDatabase
    {
        IReadOnlyList<Country> GetCountries();
        void Update(int id, string name);
    }
}