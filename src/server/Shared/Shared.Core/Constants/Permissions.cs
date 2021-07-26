using System.ComponentModel;

namespace FluentPOS.Shared.Core.Constants
{
    public static class Permissions
    {
        [DisplayName("Users")]
        [Description("Users Permissions")]
        public static class Users
        {
            public const string View = "Permissions.Users.View";
            public const string Create = "Permissions.Users.Create";
            public const string Edit = "Permissions.Users.Edit";
            public const string Delete = "Permissions.Users.Delete";
        }

        [DisplayName("Users Extended Attributes")]
        [Description("Users Extended Attributes Permissions")]
        public static class UsersExtendedAttributes
        {
            public const string View = "Permissions.Users.ExtendedAttributes.View";
            public const string ViewAll = "Permissions.Users.ExtendedAttributes.ViewAll";
            public const string Add = "Permissions.Users.ExtendedAttributes.Add";
            public const string Update = "Permissions.Users.ExtendedAttributes.Update";
            public const string Remove = "Permissions.Users.ExtendedAttributes.Remove";
        }

        [DisplayName("Roles")]
        [Description("Roles Permissions")]
        public static class Roles
        {
            public const string View = "Permissions.Roles.View";
            public const string Create = "Permissions.Roles.Create";
            public const string Edit = "Permissions.Roles.Edit";
            public const string Delete = "Permissions.Roles.Delete";
        }

        [DisplayName("Roles Extended Attributes")]
        [Description("Roles Extended Attributes Permissions")]
        public static class RolesExtendedAttributes
        {
            public const string View = "Permissions.Roles.ExtendedAttributes.View";
            public const string ViewAll = "Permissions.Roles.ExtendedAttributes.ViewAll";
            public const string Add = "Permissions.Roles.ExtendedAttributes.Add";
            public const string Update = "Permissions.Roles.ExtendedAttributes.Update";
            public const string Remove = "Permissions.Roles.ExtendedAttributes.Remove";
        }

        [DisplayName("Role Claims")]
        [Description("Role Claims Permissions")]
        public static class RoleClaims
        {
            public const string View = "Permissions.RoleClaims.View";
            public const string Create = "Permissions.RoleClaims.Create";
            public const string Edit = "Permissions.RoleClaims.Edit";
            public const string Delete = "Permissions.RoleClaims.Delete";
        }

        [DisplayName("Brands")]
        [Description("Brands Permissions")]
        public static class Brands
        {
            public const string View = "Permissions.Brands.View";
            public const string ViewAll = "Permissions.Brands.ViewAll";
            public const string Register = "Permissions.Brands.Register";
            public const string Update = "Permissions.Brands.Update";
            public const string Remove = "Permissions.Brands.Remove";
        }

        [DisplayName("Brands Extended Attributes")]
        [Description("Brands Extended Attributes Permissions")]
        public static class BrandsExtendedAttributes
        {
            public const string View = "Permissions.Brands.ExtendedAttributes.View";
            public const string ViewAll = "Permissions.Brands.ExtendedAttributes.ViewAll";
            public const string Add = "Permissions.Brands.ExtendedAttributes.Add";
            public const string Update = "Permissions.Brands.ExtendedAttributes.Update";
            public const string Remove = "Permissions.Brands.ExtendedAttributes.Remove";
        }

        [DisplayName("Customers")]
        [Description("Customers Permissions")]
        public static class Customers
        {
            public const string View = "Permissions.Customers.View";
            public const string ViewAll = "Permissions.Customers.ViewAll";
            public const string Register = "Permissions.Customers.Register";
            public const string Update = "Permissions.Customers.Update";
            public const string Remove = "Permissions.Customers.Remove";
        }

        [DisplayName("Customers Extended Attributes")]
        [Description("Customers Extended Attributes Permissions")]
        public static class CustomersExtendedAttributes
        {
            public const string View = "Permissions.Customers.ExtendedAttributes.View";
            public const string ViewAll = "Permissions.Customers.ExtendedAttributes.ViewAll";
            public const string Add = "Permissions.Customers.ExtendedAttributes.Add";
            public const string Update = "Permissions.Customers.ExtendedAttributes.Update";
            public const string Remove = "Permissions.Customers.ExtendedAttributes.Remove";
        }

        [DisplayName("Categories")]
        [Description("Categories Permissions")]
        public static class Categories
        {
            public const string View = "Permissions.Categories.View";
            public const string ViewAll = "Permissions.Categories.ViewAll";
            public const string Register = "Permissions.Categories.Register";
            public const string Update = "Permissions.Categories.Update";
            public const string Remove = "Permissions.Categories.Remove";
        }

        [DisplayName("Categories Extended Attributes")]
        [Description("Categories Extended Attributes Permissions")]
        public static class CategoriesExtendedAttributes
        {
            public const string View = "Permissions.Categories.ExtendedAttributes.View";
            public const string ViewAll = "Permissions.Categories.ExtendedAttributes.ViewAll";
            public const string Add = "Permissions.Categories.ExtendedAttributes.Add";
            public const string Update = "Permissions.Categories.ExtendedAttributes.Update";
            public const string Remove = "Permissions.Categories.ExtendedAttributes.Remove";
        }

        [DisplayName("Products")]
        [Description("Products Permissions")]
        public static class Products
        {
            public const string View = "Permissions.Products.View";
            public const string ViewAll = "Permissions.Products.ViewAll";
            public const string Register = "Permissions.Products.Register";
            public const string Update = "Permissions.Products.Update";
            public const string Remove = "Permissions.Products.Remove";
        }

        [DisplayName("Products Extended Attributes")]
        [Description("Products Extended Attributes Permissions")]
        public static class ProductsExtendedAttributes
        {
            public const string View = "Permissions.Products.ExtendedAttributes.View";
            public const string ViewAll = "Permissions.Products.ExtendedAttributes.ViewAll";
            public const string Add = "Permissions.Products.ExtendedAttributes.Add";
            public const string Update = "Permissions.Products.ExtendedAttributes.Update";
            public const string Remove = "Permissions.Products.ExtendedAttributes.Remove";
        }

        [DisplayName("Carts")]
        [Description("Carts Permissions")]
        public static class Carts
        {
            public const string View = "Permissions.Carts.View";
            public const string ViewAll = "Permissions.Carts.ViewAll";
            public const string Create = "Permissions.Carts.Create";
            public const string Remove = "Permissions.Carts.Remove";
        }

        [DisplayName("Cart Items")]
        [Description("Cart Items Permissions")]
        public static class CartItems
        {
            public const string View = "Permissions.CartItems.View";
            public const string ViewAll = "Permissions.CartItems.ViewAll";
            public const string Add = "Permissions.CartItems.Add";
            public const string Update = "Permissions.CartItems.Update";
            public const string Remove = "Permissions.CartItems.Remove";
        }
    }
}