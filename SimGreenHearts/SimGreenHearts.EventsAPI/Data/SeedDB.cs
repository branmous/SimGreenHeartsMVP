using Microsoft.EntityFrameworkCore;
using SimGreenHearts.EventsAPI.Helpers;
using SimGreenHearts.Shared.Entities;
using SimGreenHearts.Shared.Enums;

namespace SimGreenHearts.EventsAPI.Data
{
    public class SeedDB
    {
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;

        public SeedDB(DataContext dataContext, IUserHelper userHelper)
        {
            _dataContext = dataContext;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _dataContext.Database.EnsureCreatedAsync();
            await CheckEventTypes();
            await CheckRolesAsync();
            await CheckUserAsync();
        }

        private async Task<User> CheckUserAsync()
        {
            string email = "montoya@yopmail.com";
            var user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Bran",
                    LastName = "Montoya",
                    Email = email,
                    UserName = email,
                    PhoneNumber = "312 160 4754",
                    Document = "1010",
                    UserType = UserType.Provider,
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, UserType.Provider.ToString());
            }

            return user;
        }

        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.Agronomist.ToString());
            await _userHelper.CheckRoleAsync(UserType.Director.ToString());
            await _userHelper.CheckRoleAsync(UserType.Technician.ToString());
            await _userHelper.CheckRoleAsync(UserType.Provider.ToString());
        }

        private async Task CheckEventTypes()
        {
            if (!_dataContext.EventTypes.Any())
            {
                _dataContext.EventTypes.Add(new EventType
                {
                    Description = "RIEGO"
                });
                _dataContext.EventTypes.Add(new EventType
                {
                    Description = "RECOLECCIÓN"
                });
                _dataContext.EventTypes.Add(new EventType
                {
                    Description = "FUMIGACIÓN"
                });

                _dataContext.EventTypes.Add(new EventType
                {
                    Description = "FERTILLIZACION SUELO"
                });

                _dataContext.EventTypes.Add(new EventType
                {
                    Description = "PODA"
                });

                _dataContext.EventTypes.Add(new EventType
                {
                    Description = "REVISIÓN CULTIVO"
                });

                _dataContext.EventTypes.Add(new EventType
                {
                    Description = "CONTROL DE MALESA"
                });

                _dataContext.EventTypes.Add(new EventType
                {
                    Description = "SIEMBRA"
                });

                _dataContext.EventTypes.Add(new EventType
                {
                    Description = "PINTAR ARBOLES"
                });

                _dataContext.EventTypes.Add(new EventType
                {
                    Description = "DRENAJE DE SUELO"
                });

                await _dataContext.SaveChangesAsync();
            }
        }
    }
}
