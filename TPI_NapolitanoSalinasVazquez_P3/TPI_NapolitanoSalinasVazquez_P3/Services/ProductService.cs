using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TPI_NapolitanoSalinasVazquez_P3.Data;
using TPI_NapolitanoSalinasVazquez_P3.Interfaces;
using TPI_NapolitanoSalinasVazquez_P3.Models;
using TPI_NapolitanoSalinasVazquez_P3.Models.Responses;

namespace TPI_NapolitanoSalinasVazquez_P3.Services
{
    public class ProductService : IProductService
    {
        private readonly TPI_NapolitanoSalinasVazquez_P3Context _context;

        public ProductService(TPI_NapolitanoSalinasVazquez_P3Context context)
        {
            _context = context;
        }

        // Lista todos los productos -----------------------------------------------------------
        public IEnumerable<Product> GetAll()
        {
            return _context.Product;
        }

        // Buscar producto por ID -------------------------------------------------------------
        public Product GetById(int id)
        {
            return _context.Product.Find(id);
        }

        // Agregar producto a la DB -----------------------------------------------------------
        public void Add(Product product)
        {
            _context.Product.Add(product);
            _context.SaveChanges();
        }

        // Eliminar todos los productos ------------------------------------------------------
        public void DeleteAll()
        {
            var allProducts = _context.Product.ToList();
            _context.Product.RemoveRange(allProducts);
            _context.SaveChanges();
        }

        // Modificar disponibilidad de producto ---------------------------------------------
        public void ChangeState(int id, bool? newState)
        {

            var product = _context.Product.Find(id);

            if (product == null)
            {
                throw new ArgumentException($"No se encontró un producto con el ID {id}.");
            }

            product.productState = newState.Value;
            _context.SaveChanges();
        }

        // Modificar producto ---------------------------------------------------------------

        public void Update(int id, Product updatedProduct)
        {
            var product = _context.Product.Find(id);

            if (product == null)
            {
                throw new ArgumentException($"No se encontró un producto con el ID {id}.");
            }

            if (updatedProduct.productName == "string")
            {
                product.productName = product.productName;
            }
            else
            {
                product.productName = updatedProduct.productName;
            }

            if (updatedProduct.productPrice == 0)
            {
                product.productPrice = product.productPrice;
            }
            else
            {
                product.productPrice = updatedProduct.productPrice;
            }

            if (updatedProduct.productStock == 0)
            {
                product.productStock = product.productStock;
            }
            else
            {
                product.productStock = updatedProduct.productStock;
            }
            _context.SaveChanges();
        }

        // Calcular total carrito ------------------------------------------

        public decimal CalculateCartTotal(IEnumerable<Product> products)
        {
            return products.Sum(product => product.productPrice);
        }

        // Verificar si la venta se puede realizar (stock y disp) ---------------------------------------------

        public BaseResponse ValidateSell(Product product)
        {
            var validationResult = new BaseResponse();
            
            if (product.productStock <= 0)
                {
                    validationResult.IsSuccess = false;
                    validationResult.Message = $"El producto {product.productName} no tiene stock disponible para la venta."; 
                }

            if (!product.productState)
            {
                validationResult.IsSuccess = false;
                validationResult.Message = $"El producto {product.productName} no está disponible para la venta.";

            }
            if (product == null)
            {
                validationResult.IsSuccess = false;
                validationResult.Message = $"El producto no existe actualmente";
            }
            else
            {
                validationResult.IsSuccess = true;
                validationResult.Message = "Compra exitosa";
            }

            return validationResult;
        }

        public void ProductSell(int id, int amount)
        {
            var product = _context.Product.Find(id);

            if (product == null)
            {
                throw new InvalidOperationException("El producto no existe.");
            }
            else
            {
                if (product.productState == false)
                {
                    throw new InvalidOperationException("El producto no esta disponible para la venta");
                }
                else
                {
                    if (product.productStock - amount >= 0)
                    {
                        product.productStock = product.productStock - amount;

                        if (product.productStock == 0)
                        {
                            product.productState = false;
                        }
                        _context.SaveChanges();
                    }
                    else
                    {
                        throw new InvalidOperationException("No se posee suficiente stock");
                    }
                }
            }



        }
    }
}

    

