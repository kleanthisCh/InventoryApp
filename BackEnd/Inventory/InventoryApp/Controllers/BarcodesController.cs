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
    public class BarcodesController : ControllerBase
    {
        private readonly InventoryContext _context;
        private readonly IBarcodeService barcodeService;
        private readonly IMapper mapper;

        public BarcodesController(InventoryContext context, IBarcodeService barcodeService, IMapper mapper)
        {
            this._context = context;
            this.barcodeService = barcodeService;
            this.mapper = mapper;
        }

        // GET: api/Barcodes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BarcodeReadOnlyDTO>>> GetBarcodes()
        {
            var barcodes = await barcodeService.GetAllBarcodes();
            var barcodeDTO = mapper.Map<IEnumerable<BarcodeReadOnlyDTO>>(barcodes);
            return Ok(barcodeDTO);
        }

        // GET: api/Barcodes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BarcodeReadOnlyDTO>> GetBarcode(string id)
        {
            if (_context.Barcodes == null)
            {
                return NotFound();
            }
            var barcode = await barcodeService.GetBarcodeByBarcodeId(id);

            if (barcode == null)
            {
                return NotFound();
            }
            var barcodeDTO = mapper.Map<BarcodeReadOnlyDTO>(barcode);
            return Ok(barcodeDTO);
        }

        // PUT: api/Barcodes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<BarcodeUpdateDTO>> PutBarcode(string id, BarcodeUpdateDTO barcodeDTO)
        {
            Barcode? testBarcode = await barcodeService.GetBarcodeByBarcodeId(id);
            if (testBarcode == null)
            {
                return NotFound();
            }

            Barcode? barcode = mapper.Map<Barcode>(barcodeDTO);

            try
            {
                bool isUpdatedBarcode = await barcodeService.UpdateBarcode(id, barcodeDTO);
                if (!isUpdatedBarcode)
                {
                    return BadRequest();
                }

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BarcodeExists(id))
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

        // PUT: api/Barcodes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("AddOne/{id}")]
        public async Task<ActionResult<BarcodeUpdateDTO>> AddOneToBarcode(string id)
        {
            Barcode? testBarcode = await barcodeService.GetBarcodeByBarcodeId(id);
            if (testBarcode == null)
            {
                return NotFound();
            }

            try
            {
                bool isUpdatedBarcode = await barcodeService.AddOne(id);
                if (!isUpdatedBarcode)
                {
                    return BadRequest();
                }

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BarcodeExists(id))
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

        // PUT: api/Barcodes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("SubOne/{id}")]
        public async Task<ActionResult<BarcodeUpdateDTO>> SubOneFromBarcode(string id)
        {
            Barcode? testBarcode = await barcodeService.GetBarcodeByBarcodeId(id);
            if (testBarcode == null)
            {
                return NotFound();
            }

            try
            {
                bool isUpdatedBarcode = await barcodeService.SubstractOne(id);
                if (!isUpdatedBarcode)
                {
                    return BadRequest();
                }

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BarcodeExists(id))
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
        // POST: api/Barcodes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BarcodeCreateDTO>> PostBarcode(BarcodeCreateDTO barcodeDTO)
        {
            try
            {
                Barcode? insertedBarcode = await barcodeService.InsertBarcode(barcodeDTO);
                if (insertedBarcode is null)
                {
                    return BadRequest();
                }
                var dto = mapper.Map<BarcodeCreateDTO>(insertedBarcode);
                return CreatedAtAction(nameof(GetBarcode), new { id = insertedBarcode.BarcodeId }, dto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/Barcodes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBarcode(string id)
        {
            if (_context.Barcodes == null)
            {
                return NotFound();
            }
            try
            {
                if (!BarcodeExists(id))
                {
                    return NotFound();
                }
                bool isDeletedBarcode = await barcodeService.DeleteBarcode(id);
                if (!isDeletedBarcode)
                {
                    return BadRequest();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BarcodeExists(id))
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

        private bool BarcodeExists(string id)
        {
            return (_context.Barcodes?.Any(e => e.BarcodeId == id)).GetValueOrDefault();
        }
    }
}
