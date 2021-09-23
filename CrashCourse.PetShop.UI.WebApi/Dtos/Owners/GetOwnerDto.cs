using System.Collections.Generic;
using CrashCourse.PetShop.UI.WebApi.Dtos.PetTypes;

namespace CrashCourse.PetShop.UI.WebApi.Dtos.Owners
{
    public class GetOwnerDto
    {
        public string Name { get; set; }

        public List<GetPetTypeDto> Pets { get; set; }
    }
}