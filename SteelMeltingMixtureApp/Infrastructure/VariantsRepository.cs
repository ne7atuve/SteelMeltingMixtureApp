using SteelMeltingMixture.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SteelMeltingMixtureApp.Infrastructure
{
    public class VariantsRepository : IVariantsRepository
    {
        SteelMeltingMixtureDatabase _context;

        public VariantsRepository(SteelMeltingMixtureDatabase context)
        {
            _context = context;
        }

        IQueryable<Variants> IVariantsRepository.All
        {
            get { return _context.Variants; }
        }

        void IVariantsRepository.InsertOrUpdate(Variants variants)
        {
            if (variants.ID_Variant == default(int))
            {
                _context.Variants.Add(variants);
            }
            else
            {
                _context.Entry(variants).State = System.Data.Entity.EntityState.Modified;
            }
        }

        void IVariantsRepository.Remove(Variants variants)
        {
            _context.Entry(variants).State = System.Data.Entity.EntityState.Deleted;
        }

        void IVariantsRepository.Save()
        {
            _context.SaveChanges();
        }
    }
}