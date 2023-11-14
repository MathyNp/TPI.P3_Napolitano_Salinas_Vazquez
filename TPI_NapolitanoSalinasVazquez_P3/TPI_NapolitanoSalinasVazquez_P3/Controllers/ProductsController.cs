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

        [HttpPost("CreateProduct")]
        public IActionResult AddProduct(Product product)
        {
            _productService.Add(product);
            return Ok("Creado correctamente.");
        }

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

        [HttpPut("SellProduct/{id}")]
        public IActionResult SellProduct(int id)
        {
            var product = _productService.GetById(id);

            if (product.productState == false)
            {
                return Conflict("Producto no disponible para la venta.");
            }

            _productService.ProductSell(id);
            return Ok("Actualizado");
        }


    }
}
