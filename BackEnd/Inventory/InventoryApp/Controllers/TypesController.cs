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
    public class TypesController : ControllerBase
    {
        private readonly InventoryContext _context;
        private readonly ITypeService typeService;
        private readonly IMapper mapper;

        public TypesController(InventoryContext context, ITypeService typeService, IMapper mapper)
        {
            this._context = context;
            this.typeService = typeService;
            this.mapper = mapper;
        }

        // GET: api/Types
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeReadOnlyDTO>>> GetTypes()
        {
            var types = await typeService.GetAllTypes();
            var typeDTO = mapper.Map<IEnumerable<TypeReadOnlyDTO>>(types);
            return Ok(typeDTO);
        }

        // GET: api/Types/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TypeReadOnlyDTO>> GetType(int id)
        {
              if (_context.Types == null)
              {
                  return NotFound();
              }

            var type = await typeService.GetTypeById(id);
            if (type == null)
            {
                return NotFound();
            }
            var typeDTO = mapper.Map<TypeReadOnlyDTO>(type);
            return Ok(typeDTO);
            
        }

        // PUT: api/Types/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<TypeUpdateDTO>> PutType(int id, TypeUpdateDTO typeDTO)
        {
            Models.Type? testType = await typeService.GetTypeById(id);
            if (testType is null)
            {
                return BadRequest();
            }

            Models.Type? typ = mapper.Map<Models.Type>(typeDTO);
            

            try
            {
                bool isUpdatedType = await typeService.UpdateType(id, typeDTO);
                if (!isUpdatedType)
                {
                    return BadRequest();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeExists(id))
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

        // POST: api/Types
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TypeCreateDTO>> PostType(TypeCreateDTO typeDTO)
        {
            try
            {
                Models.Type? insertedType = await typeService.InsertType(typeDTO);
                if (insertedType is null)
                {
                    return BadRequest();
                }
                var dto = mapper.Map<TypeCreateDTO>(insertedType);
                return CreatedAtAction(nameof(GetType), new { id = insertedType.TypeId }, dto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/Types/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteType(int id)
        {
            if (_context.Types == null)
            {
                return NotFound();
            }
            try
            {
                if (!TypeExists(id))
                {
                    return NotFound();
                }
                bool isDeletedType = await typeService.DeleteType(id);
                if (!isDeletedType)
                {
                    return BadRequest();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeExists(id))
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

        private bool TypeExists(int id)
        {
            return (_context.Types?.Any(e => e.TypeId == id)).GetValueOrDefault();
        }
    }
}
