using Microsoft.AspNetCore.Identity;
using SimGreenHearts.Shared.DTO;
using SimGreenHearts.Shared.Entities;

namespace SimGreenHearts.EventsAPI.Helpers
{
    public interface IUserHelper
    {
        Task<User> GetUserAsync(string email);

        Task<IdentityResult> AddUserAsync(User user, string password);

        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(User user, string roleName);

        Task<bool> IsUserInRoleAsync(User user, string roleName);

        Task<SignInResult> LoginAsync(AuthDTO model);

        Task LogoutAsync();


    }
}
