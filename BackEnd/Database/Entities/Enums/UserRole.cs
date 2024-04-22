using System.ComponentModel;

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
