namespace TeachMate.Domain.DTOs.SearchDto
{
    public class SearchUserDto
    {
        public string? DisplayNameOrUsername { get; set; } = string.Empty;
        public UserRole? UserRole { get; set; } = null!;
        public bool? IsDisable { get; set; } = null!;
    }
}
