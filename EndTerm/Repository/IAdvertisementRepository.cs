using System.Collections.Generic;
using EndTerm.Models;

namespace EndTerm.Repository
{
    public interface IAdvertisementRepository
    {
        Advertisement GetAdvertisement(int id);
        IEnumerable<Advertisement> GetAllAdvertisement();
        Advertisement Add(Advertisement advertisement);
        Advertisement Update(Advertisement advertisement);
        Advertisement Delete(int id);
    }
}