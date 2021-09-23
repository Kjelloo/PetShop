using System.Collections.Generic;
using CrashCourse.PetShop.Core.IServices;
using CrashCourse.PetShop.Core.Models;
using CrashCourse.PetShop.UI.WebApi.Dtos.PetTypes;
using Microsoft.AspNetCore.Mvc;

namespace CrashCourse.PetShop.UI.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetTypesController : ControllerBase
    {
        private readonly IPetTypeService _petTypeService;

        public PetTypesController(IPetTypeService petTypeService)
        {
            _petTypeService = petTypeService;
        }

        // POST api/PetTypes
        [HttpPost]
        public ActionResult<PetType> Create(PostPetTypeDto petType)
        {
            if (petType.Name == null)
                return BadRequest("Name cannot be null");

            var pet = _petTypeService.New(petType.Name);
            var petCreated = _petTypeService.Save(pet);

            return Created("Created pet type ", petCreated);
        }
        
        // GET api/PetTypes
        [HttpGet]
        public ActionResult<List<PetType>> GetAll()
        {
            if (_petTypeService.GetAll().Count == 0)
                return NotFound("There are no pet types in the database");

            return _petTypeService.GetAll();
        }
        
        // GET api/PetTypes/{id}
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<PetType> GetById(int id)
        {
            if (id == 0)
                return BadRequest("id cannot be zero");

            if (_petTypeService.GetById(id) == null)
                return NotFound("Pet type does not exist");
            
            var result = new GetPetTypeDto
            {
                name = _petTypeService.GetById(id).Name
            };

            return Ok(result);
        }

        
        // PUT api/PetTypes
        [HttpPut]
        public ActionResult<PetType> Update(int id, PutPetTypeDto petTypeDto)
        {
            if (id == 0 || _petTypeService.GetById(id) == null)
                return NotFound("Pet type does not exist...");

            var petType = new PetType
            {
                Id = id,
                Name = petTypeDto.Name
            };

            return Accepted("Pet type updated...", _petTypeService.Update(petType));
        }

        // DELETE api/PetTypes
        [HttpDelete]
        public ActionResult<PetType> Delete(int id)
        {
            if (id == 0 || _petTypeService.GetById(id) == null)
                return NotFound("Pet type by the given id does not exist");
            
            _petTypeService.Delete(id);
            
            return NoContent();
        }
    }
}