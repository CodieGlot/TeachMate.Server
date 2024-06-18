namespace TeachMate.Domain.DTOs.SearchDto
{
    public class SearchUserDto
    {
        public string? DisplayName { get; set; } = string.Empty;
        public string? UserName { get; set; } = string.Empty;
        public UserRole? UserRole { get; set; } = null!;
        public bool? IsDisable { get; set; } = null!;
    }
}
