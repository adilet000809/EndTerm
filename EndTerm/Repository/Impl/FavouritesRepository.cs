using System;
using System.Collections.Generic;
using EndTerm.Data;
using EndTerm.Models;
using Microsoft.EntityFrameworkCore;

namespace EndTerm.Repository.Impl
{
    public class FavouritesRepository: IFavouritesRepository
    {

        private readonly ApplicationDbContext _context;

        public FavouritesRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public Favourites GetFavourites(int id)
        {
            return _context.Favourites.Find(id);
        }

        public IEnumerable<Favourites> GetAllFavourites()
        {
            return _context.Favourites;
        }

        public Favourites Add(Favourites favourites)
        {
            _context.Favourites.Add(favourites);
            _context.SaveChanges();
            return favourites;
        }

        public Favourites Update(Favourites favourites)
        {
            var c = _context.Favourites.Attach(favourites);
            c.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return favourites;
        }

        public Favourites Delete(int id)
        {
            var favourites = _context.Favourites.Find(id);
            if (favourites == null) return null;
            _context.Favourites.Remove(favourites);
            _context.SaveChanges();

            return favourites;
        }

        public void AddOrUpdate(Favourites favourites)
        {
            var entry = _context.Entry(favourites);
            switch (entry.State)  
            {  
                case EntityState.Detached:  
                    Add(favourites);  
                    break;  
                case EntityState.Modified:  
                    Update(favourites);  
                    break;  
                case EntityState.Added:  
                    Add(favourites);  
                    break;  
                case EntityState.Unchanged:  
                    break;
                case EntityState.Deleted:
                    break;
                default:  
                    throw new ArgumentOutOfRangeException();  
            }
        }
    }
}