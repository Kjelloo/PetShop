using System.Collections.Generic;
using System.Linq;
using CrashCourse.PetShop.Core.IServices;
using CrashCourse.PetShop.Core.Models;
using CrashCourse.PetShop.UI.WebApi.Dtos.Pets;
using Microsoft.AspNetCore.Mvc;

namespace CrashCourse.PetShop.UI.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetsController : ControllerBase
    {
        private readonly IPetService _petService;
        private readonly IPetTypeService _petTypeService;

        public PetsController(IPetService petService, IPetTypeService petTypeService)
        {
            _petService = petService;
            _petTypeService = petTypeService;
        }

        // POST api/Pets
        [HttpPost]
        public ActionResult<Pet> Create(PostPetDto pet)
        {
            if (pet == null)
                return BadRequest("Pet cannot be null");
            
            var petType = _petTypeService.SavePetType(_petTypeService.NewPetType(pet.Type.ToLower()));

            var petCreated = _petService.CreateAndSavePet(pet.Name, petType, pet.BirthDate, pet.SoldDate, pet.Color, pet.Price);
            return Created("Pet created...", petCreated);
        }
        
        // GET api/Pets
        [HttpGet]
        public ActionResult<List<GetPetDto>> GetALl()
        {
            if (_petService.GetAllPets().Count == 0)
                return NotFound("No pets found");
            
            var pets = _petService.GetAllPets();
            
            var petsDto = pets.Select(pet => new GetPetDto
                {
                    Name = pet.Name,
                    BirthDate = pet.BirthDate,
                    SoldDate = pet.SoldDate,
                    Color = pet.Color,
                    Price = pet.Price,
                    Type = pet.Type.Name
                })
                .ToList();

            return petsDto;
        }

        // GET api/Pets/Cheapest
        [HttpGet]
        [Route("Cheapest")]
        public ActionResult<List<Pet>> GetFiveCheapest()
        {
            if (_petService.GetFiveCheapestPets().Count == 0)
                return NotFound("No pets found");
            
            return _petService.GetFiveCheapestPets();
        }

        // PUT api/Pets/{id}
        [HttpPut("{id:int}")]
        public ActionResult<Pet> Update(int id, PutPetDto petDto)
        {
            if (id == 0 || _petService.GetPetById(id) == null)
                return NotFound("Pet does not exist...");

            var pet = new Pet
            {
                Id = id,
                BirthDate = petDto.BirthDate,
                Color = petDto.Color,
                Name = petDto.Name,
                Price = petDto.Price,
                SoldDate = petDto.SoldDate,
                Type = _petTypeService.NewPetType(petDto.Type)
            };
            
            return Accepted("Pet updated...", _petService.UpdatePet(pet));
        }
        
        // DELETE api/Pets
        [HttpDelete]
        public ActionResult<Pet> Delete(int id)
        {
            if (id == 0 || _petService.GetPetById(id) == null)
                return NotFound("Pet by the given id does not exist");

            _petService.DeletePet(id);
            
            return NoContent();
        } 
    }
}