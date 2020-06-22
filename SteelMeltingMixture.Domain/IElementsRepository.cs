using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SteelMeltingMixture.Domain
{
    public interface IElementsRepository
    {
        IQueryable<Elements> All { get; }

        void InsertOrUpdate(Elements elements);

        void Remove(Elements elements);

        void Save();
    }
}