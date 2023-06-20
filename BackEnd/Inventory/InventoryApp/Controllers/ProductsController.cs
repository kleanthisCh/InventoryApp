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
using System.Runtime.CompilerServices;
using AutoMapper;
using InventoryApp.DTO;
using InventoryApp.DAO;

namespace InventoryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly InventoryContext _context;
        private readonly IProductService productService;
        private readonly IMapper mapper;

        public ProductsController(InventoryContext context, IProductService productService, IMapper mapper)
        {
            this._context = context;
            this.productService = productService;
            this.mapper = mapper;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductReadOnlyDTO>>> GetProducts()
        {
            var products = await productService.GetAllProducts();
            var productDTO = mapper.Map<IEnumerable<ProductReadOnlyDTO>>(products);
            return Ok(productDTO);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductReadOnlyDTO>> GetProduct(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await productService.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }
            var productDTO = mapper.Map<ProductReadOnlyDTO>(product);
            return Ok(productDTO);
        }
        // GET: api/Products/model
        [HttpGet("GetProductByModel/{model}")]
        public async Task<ActionResult<ProductReadOnlyDTO>> GetProductByModel(string model)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await productService.GetProductByName(model);

            if (product == null)
            {
                return NotFound();
            }
            var productDTO = mapper.Map<ProductReadOnlyDTO>(product);
            return Ok(productDTO);
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductUpdateDTO>> PutProduct(int id, ProductUpdateDTO productDTO)
        {
            Product? testProduct = await productService.GetProductById(id);
            if (testProduct == null)
            {
                return NotFound();
            }

            Product? product = mapper.Map<Product>(productDTO);

            try
            {
                bool isUpdatedProduct = await productService.UpdateProduct(id, productDTO);
                if (!isUpdatedProduct)
                {
                    return BadRequest();
                }

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductCreateDTO>> PostProduct(ProductCreateDTO productDTO)
        {
            try
            {
                Product? insertedProduct = await productService.InsertProduct(productDTO);
                if (insertedProduct is null)
                {
                    return BadRequest();
                }
                var dto = mapper.Map<ProductCreateDTO>(insertedProduct);
                return CreatedAtAction(nameof(GetProduct), new { id = insertedProduct.ProductId }, dto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            try
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                bool isDeletedProduct = await productService.DeleteProduct(id);
                if (!isDeletedProduct)
                {
                    return BadRequest();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
