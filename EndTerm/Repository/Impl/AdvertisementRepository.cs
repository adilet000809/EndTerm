using System.Collections.Generic;
using EndTerm.Data;
using EndTerm.Models;

namespace EndTerm.Repository.Impl
{
    public class AdvertisementRepository: IAdvertisementRepository
    {

        private readonly ApplicationDbContext _context;

        public AdvertisementRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public Advertisement GetAdvertisement(int id)
        {
            return _context.Advertisements.Find(id);
        }

        public IEnumerable<Advertisement> GetAllAdvertisement()
        {
            return _context.Advertisements;
        }

        public Advertisement Add(Advertisement advertisement)
        {
            _context.Advertisements.Add(advertisement);
            _context.SaveChanges();
            return advertisement;
        }

        public Advertisement Update(Advertisement advertisement)
        {
            var c = _context.Advertisements.Attach(advertisement);
            c.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return advertisement;
        }

        public Advertisement Delete(int id)
        {
            var advertisement = _context.Advertisements.Find(id);
            if (advertisement == null) return null;
            _context.Advertisements.Remove(advertisement);
            _context.SaveChanges();

            return advertisement;
        }
    }
}