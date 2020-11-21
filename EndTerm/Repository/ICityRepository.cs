using System.Collections.Generic;
using EndTerm.Models;

namespace EndTerm.Repository
{
    public interface ICityRepository
    {
        City GetCity(int id);
        IEnumerable<City> GetAllCities();
        City Add(City city);
        City Update(City city);
        City Delete(int id);
    }
}