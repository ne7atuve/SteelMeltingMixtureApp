using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SteelMeltingMixture.Domain
{
    public interface IComponentsRepository
    {
        IQueryable<Components> All { get; }

        void InsertOrUpdate(Components components);

        void Remove(Components components);

        void Save();
    }
}