using System.Collections.Generic;
using EndTerm.Models;

namespace EndTerm.Repository
{
    public interface IFavouritesRepository
    {
        Favourites GetFavourites(int id);
        IEnumerable<Favourites> GetAllFavourites();
        Favourites Add(Favourites favourites);
        Favourites Update(Favourites favourites);
        Favourites Delete(int id);
        void AddOrUpdate(Favourites favourites);
    }
}