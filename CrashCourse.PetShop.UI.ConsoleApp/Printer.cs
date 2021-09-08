using System;
using System.Collections.Generic;
using System.Threading;
using CrashCourse.PetShop.Core.IServices;
using CrashCourse.PetShop.Core.Models;

namespace CrashCourse.PetShop.UI.ConsoleApp
{
    public class Printer
    {
        private static int _sleepTime;
        private static IPetService _petService;
        private readonly IPetTypeService _petTypeService;

        public Printer(IPetService petService,IPetTypeService petTypeService, int sleepTime)
        {
            _sleepTime = sleepTime; // Time the program sleeps between actions
            _petService = petService;
            _petTypeService = petTypeService;
        }
        
        public static int PrintMainMenuSelection(List<string> mainMenuItems)
        {
            Clear();
            foreach (var item in mainMenuItems)
                Println(item);

            var selectionString = AskQuestion("Select one: ");

            if (int.TryParse(selectionString, out var selection))
            {
                return selection;
            }
            
            return -1;
        }

        // Create pet
        public void PrintAddPet()
        {
            Clear();
            var name = AskQuestionClear("What is the pets name?: ");
            var petType = AskQuestionClear("What species is the pet? (e.g dog, cat, rabbit...): ");
            DateTime birthDate = DateTime.Parse(AskQuestionClear("When was the pet born? (dd/mm/yy): "));
            DateTime soldDate = DateTime.Parse(AskQuestionClear("When was the pet sold?: "));
            var color = AskQuestionClear("What color is the pet?: ");
            double price;
            while (!double.TryParse(AskQuestionClear("What is the price of the pet?: "), out  price))
            {
                break;
            }
            
            _petService.CreateAndSavePet(name, _petTypeService.NewPetType(petType), birthDate, soldDate, color, price);
            
            Clear();
            Println("Pet added...");
            Thread.Sleep(_sleepTime);
            Clear();
        }

        // Read pets
        public void PrintAllPetsMenu()
        {
            while (true)
            { 
                Clear();
                Println("Type 1 to see all pets");
                Println("Type 2 to sort the list by ascending price");
                Println("Type 3 to see the five cheapest pets");
                Println("Type 4 to search pets by pet type");
                Println("Type 0 to go back");
                string selection = AskQuestion("Select: ");

                switch (selection)
                {
                    case "1":
                        PrintGetAllPetsSelection();
                        break;
                    case "2":
                        PrintPetsByAscendingOrderSelection();
                        break;
                    case "3":
                        PrintFiveCheapestPetsSelection();
                        break;
                    case "4":
                        PrintGetPetsByPetType();
                        break;
                    case "0":
                        Clear();
                        return;
                }
            }
        }
        
        // Update pet
        public void PrintUpdatePet()
        {
            while (true)
            {
                var selection = AskQuestionClear("Type the id of the pet you want to update (type 0 to go back): ");

                if (!int.TryParse(selection, out var selectionInt)) continue;
                
                if (_petService.GetPetById(selectionInt) != null)
                {
                    var updateName = AskQuestionClear("New name: ");
                    var updateTypeString = AskQuestionClear("New pet type: ");
                    var updateBirthdate = DateTime.Parse(AskQuestionClear("New birth date: "));
                    var updateSoldDate = DateTime.Parse(AskQuestionClear("New Sold date: "));
                    var updateColor = AskQuestionClear("New color: ");
                    var updatePrice = double.Parse(AskQuestionClear("New price: "));
                    
                    Clear();

                    var updateType = _petTypeService.NewPetType(updateTypeString);

                    var petUpdate = new Pet
                    {
                        Id = selectionInt,
                        Name = updateName,
                        Type = updateType,
                        BirthDate = updateBirthdate,
                        SoldDate = updateSoldDate,
                        Color = updateColor,
                        Price = updatePrice
                    };

                    _petService.UpdatePet(petUpdate);
                    
                    Println("Pet updated...");
                    Thread.Sleep(_sleepTime);
                    return;
                }
                
                PrintInvalidInput();

            }
        }
        
        // Delete pet
        public void PrintDeletePet()
        {
            while (int.TryParse(AskQuestionClear("Delete a pet by their id: "), out var selection))
            {
                if (_petService.DeletePet(selection) != null)
                {
                    Clear();
                    Println("Pet deleted...");
                    Thread.Sleep(_sleepTime);
                    return;
                }
                
                PrintInvalidInput();
            }
        }
        
        public void PrintInvalidInput()
        {
            Clear();
            Println("Invalid input...");
            Thread.Sleep(_sleepTime);
            Clear();
        }

        private void PrintGetAllPetsSelection()
        {
            while (true)
            {
                Clear();
                PrintAllPets();
                if (PrintGetAllMenuSelection()) break;
            }
        }
        
        private void PrintPetsByAscendingOrderSelection()
        {
            while (true)
            {
                Clear();
                PrintPetsByAscendingOrder();
                if (PrintGetAllMenuSelection()) break;
            }
        }
        
        /*
         * Menu selection for the five cheapest pets
         */
        private void PrintFiveCheapestPetsSelection()
        {
            while (true)
            {
                Clear();
                PrintFiveCheapest();
                if (PrintGetAllMenuSelection()) break; // breaks the while loop if the method returns true
            }
        }

        /*
         * Asks the user if they want to go back and waits for their answer
         * Prints an invalid input message if the user types anything other than 0
         */
        private bool PrintGetAllMenuSelection()
        {
            var selection = AskQuestion("Type 0 to go back: ");
            if (selection == "0")
            {
                return true;
            }

            PrintInvalidInput();
            return false;
        }

        private void PrintGetPetsByPetType()
        {
            Clear();

            var selection = AskQuestion("Pet type: ");

            var petType = _petTypeService.GetPetTypeByName(selection);

            if (petType == null)
            {
                Clear();
                Println("Could not find any pets by that name");
                Thread.Sleep(_sleepTime);
                return;
            }

            Clear();
            
            foreach (var pet in _petService.GetPetsByType(petType))
            {
                Println(pet.ToString());
            }

            while (int.TryParse(AskQuestion("Type 0 to go back: "), out int selectionInt))
            {
                if (selectionInt == 0)
                    return;
                
                PrintInvalidInput();
            }
        }
        
        /*
         * Prints an unsorted list of all pets
         */
        private void PrintAllPets()
        {
            Clear();
            foreach (var pet in _petService.GetAllPets())
            {
                Println(pet.ToString());
            }
        }
        
        /*
         * Prints the five cheapest pets
         */
        private void PrintFiveCheapest()
        {
            Clear();
            foreach (var pet in _petService.GetFiveCheapestPets())
            {
                Println(pet.ToString());
            }
        }

        /*
         * Prints a list of pets in an ascending order
         */
        private void PrintPetsByAscendingOrder()
        {
            Clear();
            foreach (var pet in _petService.SortPetsByAscendingPrice())
            {
                Println(pet.ToString());
            }
        }

        /*
         * Clears the console and asks for an input from the user from a given question or set of text
         */
        private static string AskQuestionClear(string question)
        {
            Clear();
            Print(question);
            var input = Console.ReadLine();
            return input;
        }
        
        /*
         * Asks for an input from the user from a given question or set of text
         */
        private static string AskQuestion(string question)
        {
            NewLine();
            Print(question);
            var input = Console.ReadLine();
            return input;
        }
        
        /*
         * Prints text with new line
         */
        private static void Println(string text)
        {
            Console.WriteLine(text);
        }

        /*
         * Prints text
         */
        private static void Print(string text)
        {
            Console.Write(text);
        }

        /*
         * Prints a new line
         */
        private static void NewLine()
        {
            Console.WriteLine("");
        }

        /*
         * Clears the console
         */
        private static void Clear()
        {
            Console.Clear();
        }
    }
}