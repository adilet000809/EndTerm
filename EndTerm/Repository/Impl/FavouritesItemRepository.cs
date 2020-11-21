using System.Collections.Generic;
using EndTerm.Data;
using EndTerm.Models;

namespace EndTerm.Repository.Impl
{
    public class FavouritesItemRepository: IFavouritesItemRepository
    {

        private readonly ApplicationDbContext _context;

        public FavouritesItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public FavouritesItem GetFavouritesItem(int id)
        {
            return _context.FavouritesItems.Find(id);
        }

        public IEnumerable<FavouritesItem> GetAllFavouritesItems()
        {
            return _context.FavouritesItems;
        }

        public FavouritesItem Add(FavouritesItem favouritesItem)
        {
            _context.FavouritesItems.Add(favouritesItem);
            _context.SaveChanges();
            return favouritesItem;
        }

        public FavouritesItem Update(FavouritesItem favouritesItem)
        {
            var c = _context.FavouritesItems.Attach(favouritesItem);
            c.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return favouritesItem;
        }

        public FavouritesItem Delete(int id)
        {
            var favouritesItem = _context.FavouritesItems.Find(id);
            if (favouritesItem == null) return null;
            _context.FavouritesItems.Remove(favouritesItem);
            _context.SaveChanges();

            return favouritesItem;
        }
    }
}