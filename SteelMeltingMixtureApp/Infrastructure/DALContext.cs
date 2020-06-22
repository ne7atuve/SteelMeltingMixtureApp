using SteelMeltingMixture.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SteelMeltingMixtureApp.Infrastructure
{
    public class DALContext : IDALContext
    {
        SteelMeltingMixtureDatabase _database;
        IUserProfileRepository _users;
        IComponentsRepository _components;
        IElementsRepository _elements;
        IComponentsElementsRepository _componentsElements;
        IVariantsRepository _variants;

        public DALContext()
        {
            _database = new SteelMeltingMixtureDatabase();
        }

        public IUserProfileRepository Users
        {
            get
            {
                if (_users == null)
                {
                    _users = new UserRespository(_database);
                }
                return _users;
            }
        }

        public IComponentsRepository Components
        {
            get
            {
                if (_components == null)
                {
                    _components = new ComponentsRepository(_database);
                }
                return _components;
            }
        }

        public IElementsRepository Elements
        {
            get
            {
                if (_elements == null)
                {
                    _elements = new ElementsRepository(_database);
                }
                return _elements ;
            }
        }

        public IComponentsElementsRepository ComponentsElements
        {
            get
            {
                if (_componentsElements == null)
                {
                    _componentsElements = new ComponentsElementsRepository(_database);
                }
                return _componentsElements;
            }
        }

        public IVariantsRepository Variants
        {
            get
            {
                if (_variants == null)
                {
                    _variants = new VariantsRepository(_database);
                }
                return _variants;
            }
        }
    }
}