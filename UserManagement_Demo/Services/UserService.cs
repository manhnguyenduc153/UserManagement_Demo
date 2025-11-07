using Mapster;
using UserManagement_Demo.DTOs;
using UserManagement_Demo.Entities;
using UserManagement_Demo.Repositories.IRepositories;
using UserManagement_Demo.Services.IServices;

namespace UserManagement_Demo.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<bool> AddAsync(UserSaveDTO model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.FullName))
                {
                    return false;
                }

                var entity = model.Adapt<User>();

                await _userRepository.AddAsync(entity);

                var result = await _userRepository.SaveChangesAsync();

                return result > 0;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateAsync(UserSaveDTO model)
        {
            try
            {
                var entity = await _userRepository.GetByIdAsync(model.UserId);
                if (entity == null)
                    return false;

                model.Adapt(entity);

                var result = await _userRepository.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }


        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var entity = await _userRepository.GetByIdAsync(id);
                if (entity == null)
                {
                    return false;
                }

                await _userRepository.DeleteAsync(entity);
                var result = await _userRepository.SaveChangesAsync();

                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public async Task<PagedResult<UserDTO>> GetListPaging(UserSearchDTO search)
        {
            try
            {
                var totalRecord = await _userRepository.GetTotalRecord(search);
                var data = Enumerable.Empty<UserDTO>();

                if (totalRecord > 0)
                {
                    data = await _userRepository.GetListPaging(search);
                }

                return new PagedResult<UserDTO>
                {
                    Items = data,
                    TotalRecords = totalRecord,
                    PageIndex = search.PageIndex,
                    PageSize = search.PageSize
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return new PagedResult<UserDTO>
                {
                    Items = Enumerable.Empty<UserDTO>(),
                    TotalRecords = 0,
                    PageIndex = search.PageIndex,
                    PageSize = search.PageSize
                };
            }
        }

        public async Task<UserDTO?> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _userRepository.GetByIdAsync(id);
                if (entity == null)
                    return null;

                var dto = entity.Adapt<UserDTO>();
                return dto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }
    }
}
