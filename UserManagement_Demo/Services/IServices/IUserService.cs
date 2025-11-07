using UserManagement_Demo.DTOs;

namespace UserManagement_Demo.Services.IServices
{
    public interface IUserService
    {
        Task<PagedResult<UserDTO>> GetListPaging(UserSearchDTO search);
        Task<bool> AddAsync(UserSaveDTO model);

        Task<bool> UpdateAsync(UserSaveDTO model);

        Task<bool> DeleteAsync(int id);
        Task<UserDTO?> GetByIdAsync(int id);
    }
}
