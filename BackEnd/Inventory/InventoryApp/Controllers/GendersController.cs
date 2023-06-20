using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryApp.Data;
using InventoryApp.Models;
using InventoryApp.Service;
using AutoMapper;
using InventoryApp.DTO;
using InventoryApp.DAO;

namespace InventoryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GendersController : ControllerBase
    {
        private readonly InventoryContext _context;

        private readonly IGenderService genderService;
        private readonly IMapper mapper;

        public GendersController(InventoryContext context, IGenderService genderService, IMapper mapper)
        {
            this._context = context;
            this.genderService = genderService;
            this.mapper = mapper;
        }

        // GET: api/Genders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenderReadOnlyDTO>>> GetGenders()
        {
            var genders = await genderService.GetAllGenders();
            var genderDTO = mapper.Map<IEnumerable<GenderReadOnlyDTO>>(genders);
            return Ok(genderDTO);
        }

        // GET: api/Genders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GenderReadOnlyDTO>> GetGender(int id)
        {
            if (_context.Genders == null)
            {
                return NotFound();
            }

            var gender = await genderService.GetGenderById(id);
            if (gender == null)
            {
                return NotFound();
            }
            var genderDTO = mapper.Map<GenderReadOnlyDTO>(gender);
            return Ok(genderDTO);
        }

        // PUT: api/Genders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<GenderUpdateDTO>> PutGender(int id, GenderUpdateDTO genderDTO)
        {
            Gender? testgender = await genderService.GetGenderById(id);
            if (testgender is null)
            {
                return BadRequest();
            }

            Gender? gender = mapper.Map<Gender>(genderDTO);


            try
            {
                bool isUpdatedGender = await genderService.UpdateGender(id, genderDTO);
                if (!isUpdatedGender)
                {
                    return BadRequest();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Genders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GenderCreateDTO>> PostGender(GenderCreateDTO genderDTO)
        {
            try
            {
                Gender? insertedGender = await genderService.InsertGender(genderDTO);
                if (insertedGender is null)
                {
                    return BadRequest();
                }
                var dto = mapper.Map<GenderCreateDTO>(insertedGender);
                return CreatedAtAction(nameof(GetGender), new { id = insertedGender.GenderId }, dto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/Genders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGender(int id)
        {
            if (_context.Genders == null)
            {
                return NotFound();
            }
            try
            {
                if (!GenderExists(id))
                {
                    return NotFound();
                }
                bool isDeletedGender = await genderService.DeleteGender(id);
                if (!isDeletedGender)
                {
                    return BadRequest();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool GenderExists(int id)
        {
            return (_context.Genders?.Any(e => e.GenderId == id)).GetValueOrDefault();
        }
    }
}
