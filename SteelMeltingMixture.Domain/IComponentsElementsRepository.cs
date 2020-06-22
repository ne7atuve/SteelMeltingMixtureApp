using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SteelMeltingMixture.Domain
{
    public interface IComponentsElementsRepository
    {
        IQueryable<ComponentsElements> All { get; }

        void InsertOrUpdate(ComponentsElements componentsElements);

        void Remove(ComponentsElements componentsElements);

        void Save();
    }
}