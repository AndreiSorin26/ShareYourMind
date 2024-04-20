using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities.Enums
{
    public enum UserRole
    {
        [Description("User")]
        User,
        [Description("Admin")]
        Admin,
        [Description("Guest")]
        Guest
    }
}
