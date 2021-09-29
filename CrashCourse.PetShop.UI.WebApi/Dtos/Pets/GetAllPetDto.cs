using System.Collections.Generic;

namespace CrashCourse.PetShop.UI.WebApi.Dtos.Pets
{
    public class GetAllPetDto
    {
        public List<GetPetDto> List { get; set; }
        public int TotalCount { get; set; }
    }
}