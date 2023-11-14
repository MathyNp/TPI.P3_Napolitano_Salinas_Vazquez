using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TPI_NapolitanoSalinasVazquez_P3.Data;
using TPI_NapolitanoSalinasVazquez_P3.Interfaces;
using TPI_NapolitanoSalinasVazquez_P3.Models;
using TPI_NapolitanoSalinasVazquez_P3.Services;

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

        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _productService.GetAll();

            if (!products.Any())
            {
                return Conflict("No hay productos almacenados.");
            }
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _productService.GetById(id);
            if (product == null)
            {
                return Conflict("No existe un producto con ese id.");
            };
            return Ok (product);

        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            _productService.Add(product);
            return Ok("Creado correctamente.");
        }

        [HttpPut("{id}")]
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

        [HttpDelete]
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


    }
}
