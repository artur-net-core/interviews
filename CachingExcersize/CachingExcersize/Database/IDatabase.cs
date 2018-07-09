using System.Collections.Generic;

namespace CachingExcersize.Database
{
    interface IDatabase
    {
        IReadOnlyList<Country> GetCountries();
    }
}