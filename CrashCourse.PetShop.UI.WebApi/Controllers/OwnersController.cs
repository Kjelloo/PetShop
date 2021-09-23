using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrashCourse.PetShop.Core.IServices;
using CrashCourse.PetShop.Core.Models;
using CrashCourse.PetShop.Infrastructure.Data.Entities;
using CrashCourse.PetShop.UI.WebApi.Dtos.Owners;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrashCourse.PetShop.UI.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnersController : ControllerBase
    {
        private readonly IPetTypeService _petTypeService;
        private readonly IOwnerService _ownerService;
        private readonly IPetService _petService;

        public OwnersController(IOwnerService ownerService, IPetService petService, IPetTypeService petTypeService)
        {
            _ownerService = ownerService;
            _petService = petService;
            _petTypeService = petTypeService;
        }

        [HttpPost]
        public ActionResult<Owner> Create([FromBody] PostOwnerDto ownerDto)
        {
            try
            {
                return Created("Owner created... ",_ownerService.Save(new Owner {Name = ownerDto.Name}));
            }
            catch (ArgumentException argumentException)
            {
                return BadRequest(argumentException.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
            
        }

        [HttpGet]
        public ActionResult<List<Owner>> GetAll()
        {
            if (_ownerService.GetAll().Count == 0)
                return NotFound("No owners found");
            
            return Ok(_ownerService.GetAll());
        }

        [HttpPut]
        public ActionResult<Owner> Update(int id, PutOwnerDto ownerDto)
        {
            if (id == 0 || _ownerService.GetById(id) == null)
                return NotFound("Owner does not exist...");
            
            var owner = new Owner
            {
                Id = id,
                Name = ownerDto.Name
            };
            
            return Accepted("Owner updated...", _ownerService.Update(owner) );
        }

        [HttpDelete]
        public ActionResult<Owner> Delete(int id)
        {
            if (id == 0 || _ownerService.GetById(id) == null)
                return NotFound("Owner does not exist...");
            
            _ownerService.Delete(id);
            
            return NoContent();
        }

    }
}