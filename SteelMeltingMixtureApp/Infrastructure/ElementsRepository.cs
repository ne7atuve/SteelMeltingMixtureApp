using SteelMeltingMixture.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SteelMeltingMixture.Domain;

namespace SteelMeltingMixtureApp.Infrastructure
{
    public class ElementsRepository : IElementsRepository
    {
        SteelMeltingMixtureDatabase _context;

        public ElementsRepository(SteelMeltingMixtureDatabase context)
        {
            _context = context;
        }

        IQueryable<Elements> IElementsRepository.All
        {
            get { return _context.Elements; }
        }

        void IElementsRepository.InsertOrUpdate(Elements elements)
        {
            if (elements.ID_Element == default(int))
            {
                _context.Elements.Add(elements);
            }
            else
            {
                _context.Entry(elements).State = System.Data.Entity.EntityState.Modified;
            }
        }

        void IElementsRepository.Remove(Elements elements)
        {
            _context.Entry(elements).State = System.Data.Entity.EntityState.Deleted;
        }

        void IElementsRepository.Save()
        {
            _context.SaveChanges();
        }
    }
}