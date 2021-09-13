using System;

namespace CrashCourse.PetShop.UI.WebApi.Dtos.Pets
{
    public class PutPetDto
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime SoldDate { get; set; }
        public string Color { get; set; }
        public double Price { get; set; }
    }
}