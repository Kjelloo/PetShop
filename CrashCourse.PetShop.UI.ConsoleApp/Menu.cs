using System;
using System.Collections.Generic;
using System.Threading;
using CrashCourse.PetShop.Core.IServices;

namespace CrashCourse.PetShop.UI.ConsoleApp
{
    public class Menu : IMenu
    {
        private readonly Printer _printer;
        private readonly List<string> _mainMenuList;

        public Menu(IPetService petService, IPetTypeService petTypeService)
        {
            _mainMenuList = new List<string>
            {
                "1. Add a pet entry",
                "2. Get a list of pet entries",
                "3. Edit the details of a pet entry",
                "4. Delete a pet entry",
                "0. Exit program"
            };

            _printer = new Printer(petService, petTypeService, 1500);
        }

        public void Start()
        {
            var selection = Printer.PrintMainMenuSelection(_mainMenuList);

            while (selection != 0)
            {
                switch (selection)
                {
                    case 1:
                        _printer.PrintAddPet();
                        break;
                    case 2:
                        _printer.PrintAllPetsMenu();
                        break;
                    case 3:
                        _printer.PrintUpdatePet();
                        break;
                    case 4:
                        _printer.PrintDeletePet();
                        break;
                    default:
                        _printer.PrintInvalidInput();
                        break;
                }
                selection = Printer.PrintMainMenuSelection(_mainMenuList);
            }
            
            Console.WriteLine("Exiting program...");
            Thread.Sleep(1500);
        }
    }
}