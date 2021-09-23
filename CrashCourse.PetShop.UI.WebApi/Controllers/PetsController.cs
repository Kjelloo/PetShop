using System.Collections.Generic;
using CrashCourse.PetShop.Core.IServices;
using CrashCourse.PetShop.Core.Models;
using CrashCourse.PetShop.UI.WebApi.Dtos.Pets;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        [HttpPost]
        public ActionResult<Pet> Create(PostPetDto pet)
        {
            if (pet == null)
                return BadRequest("Pet cannot be null");

            if (_petTypeService.GetById(pet.Type) == null)
                return BadRequest("Pet type by this id does not exist");

            var petType = _petTypeService.GetById(pet.Type);
            
            var petCreated = _petService.CreateAndSave(pet.Name, petType, pet.BirthDate, pet.SoldDate, pet.Color, pet.Price);
            return Created("Pet created...", petCreated);
        }
        
        // GET api/Pets
        [HttpGet]
        public ActionResult<List<Pet>> GetALl()
        {
            if (_petService.GetAll().Count == 0)
                return NotFound("No pets found");
            
            return _petService.GetAll();
        }

        // GET api/Pets/Cheapest
        [HttpGet]
        [Route("Cheapest")]
        public ActionResult<List<Pet>> GetFiveCheapest()
        {
            if (_petService.GetFiveCheapest().Count == 0)
                return NotFound("No pets found");
            
            return _petService.GetFiveCheapest();
        }

        // PUT api/Pets/{id}
        [HttpPut("{id:int}")]
        public ActionResult<Pet> Update(int id, PutPetDto petDto)
        {
            if (id == 0 || _petService.GetById(id) == null)
                return NotFound("Pet does not exist...");

            var pet = new Pet
            {
                Id = id,
                BirthDate = petDto.BirthDate,
                Color = petDto.Color,
                Name = petDto.Name,
                Price = petDto.Price,
                SoldDate = petDto.SoldDate,
                Type = _petTypeService.GetById(petDto.Type)
            };
            
            return Accepted("Pet updated...", _petService.Update(pet));
        }
        
        // DELETE api/Pets
        [HttpDelete]
        public ActionResult<Pet> Delete(int id)
        {
            if (id == 0 || _petService.GetById(id) == null)
                return NotFound("Pet does not exist...");

            _petService.Delete(id);
            
            return NoContent();
        } 
    }
}