using System.Collections.Generic;
using EndTerm.Models;

namespace EndTerm.Repository
{
    public interface IAddressRepository
    {
        Address GetAddress(int id);
        IEnumerable<Address> GetAllAddress();
        Address Add(Address address);
        Address Update(Address address);
        Address Delete(int id);
    }
}