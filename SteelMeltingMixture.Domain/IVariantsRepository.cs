using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SteelMeltingMixture.Domain
{
    public interface IVariantsRepository
    {
        IQueryable<Variants> All { get; }

        void InsertOrUpdate(Variants variants);

        void Remove(Variants variants);

        void Save();
    }
}