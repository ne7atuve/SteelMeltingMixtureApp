using SteelMeltingMixture.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SteelMeltingMixtureApp.Infrastructure
{
    public class SteelMeltingMixtureDatabase : DbContext
    {
        public SteelMeltingMixtureDatabase()
          : base("SteelMeltingMixtureConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }

        public DbSet<Components> Components { get; set; }

        public DbSet<Elements> Elements { get; set; }

        public DbSet<ComponentsElements> ComponentsElements { get; set; }

        public DbSet<Variants> Variants { get; set; }

    }
}