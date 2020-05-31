using ChatApp.Models;
using ChatApp.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskQ.BAL.Interface
{
    public interface IUserService
    {
        Task<EntityResponseModel> CreateUser(UserDto request);
        Task<EntityResponseModel> UpdateUserDetail(UserDto request);
        Task<EntityResponseModel> DeleteUser(long id);
        List<UserDto> SearchUser(SearchUserDto request);
        Task<List<LeftUserOutput>> LeftUser(int currentUserId);

    }
}
