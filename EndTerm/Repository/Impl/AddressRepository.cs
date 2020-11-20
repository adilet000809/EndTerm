using System.Collections.Generic;
using EndTerm.Data;
using EndTerm.Models;

namespace EndTerm.Repository.Impl
{
    public class AddressRepository: IAddressRepository
    {

        private readonly ApplicationDbContext _context;

        public AddressRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public Address GetAddress(int id)
        {
            return _context.Addresses.Find(id);
        }

        public IEnumerable<Address> GetAllAddress()
        {
            return _context.Addresses;
        }

        public Address Add(Address address)
        {
            _context.Addresses.Add(address);
            _context.SaveChanges();
            return address;
        }

        public Address Update(Address address)
        {
            var c = _context.Addresses.Attach(address);
            c.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return address;
        }

        public Address Delete(int id)
        {
            var address = _context.Addresses.Find(id);
            if (address == null) return null;
            _context.Addresses.Remove(address);
            _context.SaveChanges();

            return address;
        }
    }
}