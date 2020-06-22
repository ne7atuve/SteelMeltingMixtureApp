using SteelMeltingMixture.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SteelMeltingMixtureApp.Infrastructure
{
    public class ComponentsRepository : IComponentsRepository
    {
        SteelMeltingMixtureDatabase _context;

        public ComponentsRepository(SteelMeltingMixtureDatabase context)
        {
            _context = context;
        }

        IQueryable<Components> IComponentsRepository.All
        {
            get { return _context.Components; }
        }

        void IComponentsRepository.InsertOrUpdate(Components components)
        {
            if (components.ID_Component == default(int))
            {
                _context.Components.Add(components);
            }
            else
            {
                _context.Entry(components).State = System.Data.Entity.EntityState.Modified;
            }
        }

        void IComponentsRepository.Remove(Components components)
        {
            _context.Entry(components).State = System.Data.Entity.EntityState.Deleted;
        }

        void IComponentsRepository.Save()
        {
            _context.SaveChanges();
        }
    }
}