using System;
using System.Collections.Generic;
using CrashCourse.PetShop.Core.IServices;
using CrashCourse.PetShop.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CrashCourse.PetShop.UI.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PetsController : ControllerBase
    {
        private readonly IPetService _petService;
        private readonly IPetTypeService _petTypeService;

        public PetsController(IPetService petService, IPetTypeService petTypeService)
        {
            _petService = petService;
            _petTypeService = petTypeService;
        }

        // POST
        [HttpPost]
        public ActionResult<Pet> Create(Pet pet)
        {
            if (pet == null)
                return BadRequest("Pet cannot be null");

            var petCreated = _petService.CreateAndSavePet(pet.Name, pet.Type, pet.BirthDate, pet.SoldDate, pet.Color, pet.Price);
            return Created("Pet created...", petCreated);
        }
        
        // GET
        [HttpGet]
        public ActionResult<List<Pet>> GetALl()
        {
            if (_petService.GetAllPets().Count == 0)
                return NotFound();
            
            return _petService.GetAllPets();
        }

        // GET 5 Cheapest
        [HttpGet]
        [Route("Cheapest")]
        public ActionResult<List<Pet>> GetFiveCheapest()
        {
            if (_petService.GetFiveCheapestPets().Count == 0)
                return NotFound();
            
            return _petService.GetFiveCheapestPets();
        }

        [HttpPut("{id}")]
        public ActionResult<Pet> Update(int id, Pet pet)
        {
            if (id != pet.Id)
                return BadRequest("Id to change does not match pet id");

            var petUpdate = _petService.UpdatePet(pet);

            if (petUpdate != null)
                return Ok(pet);

            return BadRequest("Cannot update pet");
        }
        
        // DELETE
        [HttpDelete]
        public ActionResult<Pet> Delete(int id)
        {
            if (_petService.GetPetById(id) == null)
                return NotFound();

            _petService.DeletePet(id);
            
            return NoContent();
        } 
    }
}