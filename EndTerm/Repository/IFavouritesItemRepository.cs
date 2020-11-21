using System.Collections.Generic;
using EndTerm.Models;

namespace EndTerm.Repository
{
    public interface IFavouritesItemRepository
    {
        FavouritesItem GetFavouritesItem(int id);
        IEnumerable<FavouritesItem> GetAllFavouritesItems();
        FavouritesItem Add(FavouritesItem favouritesItem);
        FavouritesItem Update(FavouritesItem favouritesItem);
        FavouritesItem Delete(int id);
    }
}