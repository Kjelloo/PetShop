using System;

namespace CrashCourse.PetShop.UI.WebApi.Dtos.Pets
{
    public class GetPetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime SoldDate { get; set; }
        public string Color { get; set; }
        public double Price { get; set; } 
        public string Owner {get; set; }
    }
}