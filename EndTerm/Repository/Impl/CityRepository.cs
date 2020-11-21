using System.Collections.Generic;
using EndTerm.Data;
using EndTerm.Models;

namespace EndTerm.Repository.Impl
{
    public class CityRepository: ICityRepository
    {

        private readonly ApplicationDbContext _context;

        public CityRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public City GetCity(int id)
        {
            return _context.Cities.Find(id);
        }

        public IEnumerable<City> GetAllCities()
        {
            return _context.Cities;
        }

        public City Add(City city)
        {
            _context.Cities.Add(city);
            _context.SaveChanges();
            return city;
        }

        public City Update(City city)
        {
            var c = _context.Cities.Attach(city);
            c.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return city;
        }

        public City Delete(int id)
        {
            var city = _context.Cities.Find(id);
            if (city == null) return null;
            _context.Cities.Remove(city);
            _context.SaveChanges();

            return city;
        }
    }
}