using System.ComponentModel.DataAnnotations;

namespace UserManagement_Demo.DTOs
{
    public class UserDTO
    {
        public int UserId { get; set; }

        public string FullName { get; set; } = null!;

        public DateOnly Dob { get; set; }

        public string Email { get; set; } = null!;

        public string? Phone { get; set; }

        public string? Address { get; set; }
    }

    public class UserSearchDTO
    {
        private string? _keyword;

        public string? Keyword
        {
            get => _keyword;
            set => _keyword = value?.Trim().ToLower();
        }
        public DateOnly? DobFrom { get; set; }
        public DateOnly? DobTo { get; set; }
        public int? Status { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class UserSaveDTO
    {
        public int UserId { get; set; }

        public string FullName { get; set; } = null!;

        public DateOnly Dob { get; set; }

        public string Email { get; set; } = null!;

        public string? Phone { get; set; }

        public string? Address { get; set; }
    }

}
