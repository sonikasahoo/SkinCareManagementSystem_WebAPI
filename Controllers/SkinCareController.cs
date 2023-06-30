using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Case_Study_SkinCareManagementSystem.Models;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Microsoft.AspNetCore.Authorization;

namespace Case_Study_SkinCareManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkinCareController : ControllerBase
    {
        private readonly string connectionString;

        public SkinCareController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAllProducts()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var skincare = connection.Query<SkinCare>("SELECT * FROM SkinCareProducts").ToList();
                return Ok(skincare);
            }
        }

        [HttpGet("{productId}")]
        [Authorize]
        public IActionResult GetProductById(int productId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var query = "SELECT * FROM SkinCareProducts WHERE ProductId= @ProductId";
                var sk = connection.Query<SkinCare>(query, new { ProductId = productId });
                if (sk == null)
                {
                    return NotFound();
                }
                return Ok(sk);
            }
        }


        [HttpPost]
        [Authorize]
        public IActionResult AddProduct(SkinCare s)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var query = "INSERT into SkinCareProducts(ProductId,Name,Brand,Price,SkinType,TargetArea) values(@ProductId,@Name,@Brand,@Price,@SkinType,@TargetArea)";
                connection.Execute(query, s);
            }
            return Ok();
        }


        [HttpPut("{productId}")]
        [Authorize]
        public IActionResult UpdateProduct(int productId, SkinCare s)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var query = "UPDATE SkinCareProducts SET Name=@Name,Brand=@Brand,Price=@Price,SkinType=@SkinType,TargetArea=@TargetArea WHERE ProductId=@ProductId";
                s.ProductId = productId;
                connection.Execute(query, s);
            }
            return Ok();
        }
        
        [HttpDelete("{productId}")]
        [Authorize]
        public IActionResult DeleteProduct(int productId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var query = "DELETE FROM SkinCareProducts WHERE ProductId=@ProductId";
                connection.Execute(query, new { ProductId = productId });
            }
            
            return Ok();
        }
    }
}
