namespace StansAssociates_Backend.Concrete.IServices
{
    public interface ICurrentUserServices
    {
        string Email { get; }
        bool IsAuthenticated { get; }
        string Name { get; }
        string PhoneNumber { get; }
        string Pincode { get; }
        List<int> RoleIds { get; }
        string RoleType { get; }
        int UserId { get; }
        bool IsAdmin { get; }
        int? SchoolId { get; }
    }
}