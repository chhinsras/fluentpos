using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Shared.Core.Constants
{
    public static class RoleConstants
    {
        public const string SuperAdmin = "SuperAdmin";
        public const string Admin = "Admin";
        public const string Manager = "Manager";
        public const string Accountant = "Accountant";
        public const string Cashier = "Cashier";
        public const string Staff = "Staff";
        public const string PermissionToCreate = "SuperAdmin,Admin";
    }
}
