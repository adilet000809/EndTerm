using System.Collections.Generic;
using EndTerm.Data;
using EndTerm.Models;

namespace EndTerm.Repository.Impl
{
    public class OblastRepository: IOblastRepository
    {

        private readonly ApplicationDbContext _context;

        public OblastRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public Oblast GetOblast(int id)
        {
            return _context.Oblasts.Find(id);
        }

        public IEnumerable<Oblast> GetAllOblasts()
        {
            return _context.Oblasts;
        }

        public Oblast Add(Oblast oblast)
        {
            _context.Oblasts.Add(oblast);
            _context.SaveChanges();
            return oblast;
        }

        public Oblast Update(Oblast oblast)
        {
            var c = _context.Oblasts.Attach(oblast);
            c.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return oblast;
        }

        public Oblast Delete(int id)
        {
            var address = _context.Oblasts.Find(id);
            if (address == null) return null;
            _context.Oblasts.Remove(address);
            _context.SaveChanges();

            return address;
        }
    }
}