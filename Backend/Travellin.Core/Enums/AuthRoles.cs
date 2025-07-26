namespace Travellin.Travellin.Core.Enums
{
    [Flags]
    public enum AuthRoles
    {
        Admin = 1,
        Host = 2,
        CoHost = 4,
        Guest = 8,
    }
}
