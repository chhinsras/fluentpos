using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FluentPOS.Shared.Core.Constants
{
    public static class Permissions
    {
        public static class Brands
        {
            public const string View = "Permissions.Brands.View";
            public const string ViewAll = "Permissions.Brands.ViewAll";
            public const string Register = "Permissions.Brands.Register";
            public const string Update = "Permissions.Brands.Update";
            public const string Remove = "Permissions.Brands.Remove";
        }

        public static class BrandsExtendedAttributes
        {
            public const string View = "Permissions.Brands.ExtendedAttributes.View";
            public const string ViewAll = "Permissions.Brands.ExtendedAttributes.ViewAll";
            public const string Add = "Permissions.Brands.ExtendedAttributes.Add";
            public const string Update = "Permissions.Brands.ExtendedAttributes.Update";
            public const string Remove = "Permissions.Brands.ExtendedAttributes.Remove";
        }

        public static class Customers
        {
            public const string View = "Permissions.Customers.View";
            public const string ViewAll = "Permissions.Customers.ViewAll";
            public const string Register = "Permissions.Customers.Register";
            public const string Update = "Permissions.Customers.Update";
            public const string Remove = "Permissions.Customers.Remove";
        }

        public static class Categories
        {
            public const string View = "Permissions.Categories.View";
            public const string ViewAll = "Permissions.Categories.ViewAll";
            public const string Register = "Permissions.Categories.Register";
            public const string Update = "Permissions.Categories.Update";
            public const string Remove = "Permissions.Categories.Remove";
        }

        public static class CategoriesExtendedAttributes
        {
            public const string View = "Permissions.Categories.ExtendedAttributes.View";
            public const string ViewAll = "Permissions.Categories.ExtendedAttributes.ViewAll";
            public const string Add = "Permissions.Categories.ExtendedAttributes.Add";
            public const string Update = "Permissions.Categories.ExtendedAttributes.Update";
            public const string Remove = "Permissions.Categories.ExtendedAttributes.Remove";
        }

        public static class Products
        {
            public const string View = "Permissions.Products.View";
            public const string ViewAll = "Permissions.Products.ViewAll";
            public const string Register = "Permissions.Products.Register";
            public const string Update = "Permissions.Products.Update";
            public const string Remove = "Permissions.Products.Remove";
        }

        public static List<string> GetRegisteredPermissions()
        {
            var permssions = new List<string>();
            foreach (var prop in typeof(Permissions).GetNestedTypes().SelectMany(c => c.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)))
            {
                var propertyValue = prop.GetValue(null);
                if (propertyValue is not null)
                    permssions.Add(propertyValue.ToString());
            }
            return permssions;
        }
    }
}