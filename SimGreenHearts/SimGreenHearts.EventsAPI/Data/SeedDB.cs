using Microsoft.EntityFrameworkCore;
using SimGreenHearts.Shared.Entities;
using System.Reflection;

namespace SimGreenHearts.EventsAPI.Data
{
    public class SeedDB
    {
        private readonly DataContext _dataContext;

        public SeedDB(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task SeedAsync()
        {
            await _dataContext.Database.EnsureCreatedAsync();
            await CheckEventTypes();
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
