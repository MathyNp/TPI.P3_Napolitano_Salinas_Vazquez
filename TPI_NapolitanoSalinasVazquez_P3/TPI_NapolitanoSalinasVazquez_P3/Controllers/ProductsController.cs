using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TPI_NapolitanoSalinasVazquez_P3.Interfaces;
using TPI_NapolitanoSalinasVazquez_P3.Models;

namespace TPI_NapolitanoSalinasVazquez_P3.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }


        // Lista de productos -------------------------------------------

        [HttpGet("GetAll")]
        public IActionResult GetProducts()
        {
            var products = _productService.GetAll();

            if (!products.Any())
            {
                return Conflict("No hay productos almacenados.");
            }
            return Ok(products);
        }


        // Buscar producto por ID -------------------------------------

        [HttpGet("GetById/{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _productService.GetById(id);
            if (product == null)
            {
                return Conflict("No existe un producto con ese id.");
            };
            return Ok (product);

        }

        // Agregar producto a la lista ------------------------------

        [Authorize(Policy = "Admin")]
        [HttpPost("CreateProduct")]
        public IActionResult AddProduct(Product product)
        {
            _productService.Add(product);
            return Ok("Creado correctamente.");
        }

        // Editar disponibilidad de producto ------------------------

        [Authorize(Policy = "Admin")]
        [HttpPut("ChangeState/{id}")]
        public IActionResult ChangeState(int id, bool? newState)
        {
            try
            {
                _productService.ChangeState(id, newState);
                return Ok($"Estado del producto con ID {id} actualizado a {newState}.");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // Editar producto por ID ---------------------------------

        [Authorize(Policy = "Admin")]
        [HttpPut("ModifyProduct/{id}")]
        public IActionResult ModifyProduct(int id, Product updatedProduct)
        {
            try
            {
                _productService.Update(id, updatedProduct);
                return Ok("Actualizado correctamente.");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // Eliminar todos los productos ---------------------------

        [Authorize(Policy = "Admin")]
        [HttpDelete("DELETEALL")]
        public IActionResult DeleteProducts() 
        {
            var products = _productService.GetAll();
            if (!products.Any())
            {
                return Conflict("No se encuentran productos para eliminar.");
            }
            _productService.DeleteAll();
            return Ok("Borrado correctamente.");
        }

        // Eliminar productos por ID ------------------------------------------

        [Authorize(Policy = "Admin")]
        [HttpDelete("DELETEBYID/{id}")]
        public IActionResult DeleteProductsById(int id)
        {
            var products = _productService.GetById(id);
            if (products == null)
            {
                return Conflict("No existe un producto con ese id para eliminar.");
            }
            _productService.DeleteById(id);
            return Ok("Borrado correctamente.");
        }

        // Vender producto y modificar stock ----------------------

        [HttpPut("SellProduct/{id}/{amount}")]
        public IActionResult SellProduct(int id, int amount)
        {
            var product = _productService.GetById(id);          

            
            try
            {
                _productService.ProductSell(id, amount);
                return Ok("Actualizado");
            }            
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
