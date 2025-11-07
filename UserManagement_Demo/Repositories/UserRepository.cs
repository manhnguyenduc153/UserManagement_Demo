using Mapster;
using Microsoft.EntityFrameworkCore;
using UserManagement_Demo.DTOs;
using UserManagement_Demo.Entities;
using UserManagement_Demo.Repositories.IRepositories;

namespace UserManagement_Demo.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDBContext _context;

        public UserRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<User> AddAsync(User entity)
        {
            await _context.Users.AddAsync(entity);
            return entity;
        }

        public Task UpdateAsync(User entity)
        {
            _context.Users.Update(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(User entity)
        {
            _context.Users.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<IEnumerable<UserDTO>> GetListPaging(UserSearchDTO search)
        {
            var query = _context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(u =>
                    u.FullName.ToLower().Contains(search.Keyword) ||
                    u.Email.ToLower().Contains(search.Keyword) ||
                    (u.Phone != null && u.Phone.ToLower().Contains(search.Keyword)) ||
                    (u.Address != null && u.Address.ToLower().Contains(search.Keyword))
                );
            }

            if (search.DobFrom.HasValue)
                query = query.Where(u => u.Dob >= search.DobFrom.Value);

            if (search.DobTo.HasValue)
                query = query.Where(u => u.Dob <= search.DobTo.Value);

            var skip = (search.PageIndex - 1) * search.PageSize;

            var users = await query
                .OrderBy(u => u.FullName)
                .Skip(skip)
                .Take(search.PageSize)
                .ToListAsync();

            return users.Adapt<IEnumerable<UserDTO>>();
        }


        public async Task<int> GetTotalRecord(UserSearchDTO search)
        {
            var query = _context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(u =>
                    u.FullName.ToLower().Contains(search.Keyword) ||
                    u.Email.ToLower().Contains(search.Keyword) ||
                    (u.Phone != null && u.Phone.ToLower().Contains(search.Keyword)) ||
                    (u.Address != null && u.Address.ToLower().Contains(search.Keyword))
                );
            }

            if (search.DobFrom.HasValue)
                query = query.Where(u => u.Dob >= search.DobFrom.Value);

            if (search.DobTo.HasValue)
                query = query.Where(u => u.Dob <= search.DobTo.Value);

            return await query.CountAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
