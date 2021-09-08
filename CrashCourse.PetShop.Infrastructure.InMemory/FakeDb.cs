using System;
using System.Collections.Generic;
using CrashCourse.PetShop.Core.Models;

namespace CrashCourse.PetShop.Infrastructure.InMemory
{
    public sealed class FakeDb
    {
        public int PetId { get; set; }
        public List<Pet> Pets { get;}
        
        public int PetTypeId { get; set; }
        public List<PetType> PetTypes { get; }
        
        private static readonly Random Random = new Random(23202);

        public FakeDb()
        {
            PetId = 1;
            PetTypeId = 1;
            Pets = new List<Pet>();
            PetTypes = new List<PetType>();
            setFakePets();
        }

        
        private static readonly string[] MockupPetTypes = {
            "dog",
            "cat",
            "snake",
            "bird",
            "ferret",
            "bunny",
            "salamander",
            "fish"
        };

        private static readonly string[] MockupColors =
        {
            "white",
            "black",
            "brown",
            "orange",
            "red",
            "grey",
            "blue",
            "yellow"
        };

        private static readonly string[] mockupNames =
        {
            "Pamela",
            "June",
            "Clarke",
            "Leanna",
            "Kamran",
            "Harold",
            "Karan",
            "Peter"
        };
        
        private void setFakePets()
        {
            SetFakePetTypes();
            
            for (int i = 0; i < 8; i++)
            {
                Pet petToAdd = new Pet
                {
                    Id = PetId++,
                    Name = mockupNames[i],
                    Type = PetTypes[i],
                    Color = MockupColors[i],
                    BirthDate = DateTime.Today,
                    SoldDate = DateTime.Today,
                    Price = Math.Round(1000 * Random.NextDouble(),2)
                };
                
                Pets.Add(petToAdd);
            }
        }

        private void SetFakePetTypes()
        {
            
            for (int i = 0; i < 8; i++)
            {
                PetType petTypeToAdd = new PetType
                {
                    Id = PetTypeId++,
                    Name = MockupPetTypes[i]
                };
                
                PetTypes.Add(petTypeToAdd);
            }
        }
        
    }
}