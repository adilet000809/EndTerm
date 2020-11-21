using System.Collections.Generic;
using EndTerm.Models;

namespace EndTerm.Repository
{
    public interface IOblastRepository
    {
        Oblast GetOblast(int id);
        IEnumerable<Oblast> GetAllOblasts();
        Oblast Add(Oblast oblast);
        Oblast Update(Oblast oblast);
        Oblast Delete(int id);
    }
}