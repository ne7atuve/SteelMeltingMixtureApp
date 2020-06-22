using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SteelMeltingMixture.Domain
{
    public interface IDALContext
    {
        IUserProfileRepository Users { get; }

        IComponentsRepository Components { get; }

        IElementsRepository Elements { get; }

        IComponentsElementsRepository ComponentsElements { get; }

        IVariantsRepository Variants { get; }

    }
}