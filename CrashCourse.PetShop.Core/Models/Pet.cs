using System;

namespace CrashCourse.PetShop.Core.Models
{
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public PetType Type { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime SoldDate { get; set; }
        public string Color { get; set; }
        public double Price { get; set; }
        
        public int OwnerId { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Type)}: {Type}, {nameof(BirthDate)}: {BirthDate}, {nameof(SoldDate)}: {SoldDate}, {nameof(Color)}: {Color}, {nameof(Price)}: {Price}";
        }
    }
}