using SteelMeltingMixture.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SteelMeltingMixture.Domain;

namespace SteelMeltingMixtureApp.Infrastructure
{
    public class ComponentsElementsRepository : IComponentsElementsRepository
    {
        SteelMeltingMixtureDatabase _context;

        public ComponentsElementsRepository(SteelMeltingMixtureDatabase context)
        {
            _context = context;
        }

        IQueryable<ComponentsElements> IComponentsElementsRepository.All
        {
            get { return _context.ComponentsElements; }
        }

        void IComponentsElementsRepository.InsertOrUpdate(ComponentsElements componentsElements)
        {
            if (componentsElements.ID_ComponentElements == default(int))
            {
                _context.ComponentsElements.Add(componentsElements);
            }
            else
            {
                _context.Entry(componentsElements).State = System.Data.Entity.EntityState.Modified;
            }
        }

        void IComponentsElementsRepository.Remove(ComponentsElements componentsElements)
        {
            _context.Entry(componentsElements).State = System.Data.Entity.EntityState.Deleted;
        }

        void IComponentsElementsRepository.Save()
        {
            _context.SaveChanges();
        }
    }
}