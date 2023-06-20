using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryApp.Data;
using InventoryApp.Models;
using InventoryApp.DTO;
using InventoryApp.Service;
using AutoMapper;
using InventoryApp.DAO;

namespace InventoryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturersController : ControllerBase
    {
        private readonly InventoryContext _context;
        private readonly IManufacturerService manufacturerService;
        private readonly IMapper mapper;
        
        public ManufacturersController(InventoryContext context, IManufacturerService manufacturerService, IMapper mapper)
        {
            this._context = context;
            this.manufacturerService = manufacturerService;
            this.mapper = mapper;
        }

        // GET: api/Manufacturers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ManufacturerReadOnlyDTO>>> GetManufacturers()
        {
            var manufacturers = await manufacturerService.GetAllManufacturers();
            /*var manufacturers = await _context.Manufacturers.ToListAsync();*/
            var manufacturerDTO = mapper.Map<IEnumerable<ManufacturerReadOnlyDTO>>(manufacturers);
            return Ok(manufacturerDTO);
        }

        // GET: api/Manufacturers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ManufacturerReadOnlyDTO>> GetManufacturer(int id)
        {
            if (_context.Manufacturers == null)
            {
                return NotFound();
            }
            var manufacturer = await manufacturerService.GetManufacturerById(id);

            if (manufacturer == null)
            {
                return NotFound();
            }
            var manufacturerDTO = mapper.Map<ManufacturerReadOnlyDTO>(manufacturer);
            return Ok(manufacturerDTO);
        }

        // PUT: api/Manufacturers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ManufacturerUpdateDTO>> PutManufacturer(int id, ManufacturerUpdateDTO manufacturerDTO)
        {
            Manufacturer? testManufacturer = await manufacturerService.GetManufacturerById(id);
            if (testManufacturer == null)
            {
                return NotFound();
            }
            
            Manufacturer? manufacturer = mapper.Map<Manufacturer>(manufacturerDTO);

            try
            {
                bool isUpdatedManufacturer = await manufacturerService.UpdateManufacturer(id, manufacturerDTO);
                if (!isUpdatedManufacturer)
                {
                    return BadRequest();
                }

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ManufacturerExists(id))
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

        // POST: api/Manufacturers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ManufacturerCreateDTO>> PostManufacturer(ManufacturerCreateDTO manufacturerDTO)
        {
            try
            {
                Manufacturer? insertedManufacturer = await manufacturerService.InsertManufacturer(manufacturerDTO);
                if (insertedManufacturer is null)
                {
                    return BadRequest();
                }
                var dto = mapper.Map<ManufacturerCreateDTO>(insertedManufacturer);
                return CreatedAtAction(nameof(GetManufacturer), new { id = insertedManufacturer.ManufacturerId }, dto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        // DELETE: api/Manufacturers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteManufacturer(int id)
        {
            if (_context.Manufacturers == null)
            {
                return NotFound();
            }
            try
            {
                if (!ManufacturerExists(id))
                {
                    return NotFound();
                }
                bool isDeletedManufacturer = await manufacturerService.DeleteManufacturer(id);
                if (!isDeletedManufacturer)
                {
                    return BadRequest();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ManufacturerExists(id))
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

        private bool ManufacturerExists(int id)
        {
            return (_context.Manufacturers?.Any(e => e.ManufacturerId == id)).GetValueOrDefault();
        }
    }
}
