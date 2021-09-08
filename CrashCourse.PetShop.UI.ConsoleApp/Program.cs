using CrashCourse.PetShop.Core.IRepositories;
using CrashCourse.PetShop.Core.IServices;
using CrashCourse.PetShop.Domain.Services;
using CrashCourse.PetShop.Infrastructure.InMemory;
using CrashCourse.PetShop.Infrastructure.InMemory.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CrashCourse.PetShop.UI.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<FakeDb>();
            serviceCollection.AddScoped<IPetRepository, PetRepository>();
            serviceCollection.AddScoped<IPetService, PetService>();
            serviceCollection.AddScoped<IPetTypeRepository, PetTypeRepository>();
            serviceCollection.AddScoped<IPetTypeService, PetTypeService>();
            serviceCollection.AddScoped<IMenu, Menu>();
            
            var serviceProvider = serviceCollection.BuildServiceProvider();
            // var servicePet = serviceProvider.GetRequiredService<IPetService>();
            // var servicePetType = serviceProvider.GetRequiredService<IPetTypeService>();
            var menu = serviceProvider.GetRequiredService<IMenu>();
            menu.Start();
        }
    }
}