using UserManagement_Demo.DTOs;
using UserManagement_Demo.Entities;

namespace UserManagement_Demo.Repositories.IRepositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDTO>> GetListPaging(UserSearchDTO search);
        Task<User> AddAsync(User entity);
        Task<int> GetTotalRecord(UserSearchDTO search);
        Task<User> GetByIdAsync(int id);
        Task UpdateAsync(User entity);
        Task DeleteAsync(User entity);
        Task<int> SaveChangesAsync();
    }
}
