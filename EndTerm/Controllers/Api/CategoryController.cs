using System.Collections.Generic;
using System.Net.Mime;
using System.Text.Json;
using EndTerm.Models;
using EndTerm.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EndTerm.Controllers.Api
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public CategoryController(
            ICategoryRepository categoryRepository, 
            UserManager<IdentityUser> userManager)
        {
            _categoryRepository = categoryRepository;
            _userManager = userManager;
        }

        [HttpGet("categories")]
        public IEnumerable<Category> GetAllCategories()
        {
            return _categoryRepository.GetAllCategory();
        }
        
        [HttpGet("categories/{categoryId}")]
        public Category GetCategory(int categoryId)
        {
            return _categoryRepository.GetCategory(categoryId);
        }
        
        [HttpPost("categories/add")]
        public Category AddCategory(JsonElement request)
        {
            var categoryName = request.GetProperty("categoryName").GetString();
            return _categoryRepository.Add(new Category {Name = categoryName});
        }
        
        [HttpPost("categories/update")]
        public IActionResult UpdateCategory(JsonElement request)
        {
            var categoryId = request.GetProperty("categoryId").GetInt32();
            var categoryName = request.GetProperty("categoryName").GetString();
            var result = _categoryRepository.GetCategory(categoryId);
            if (result == null) return BadRequest("Not Found");
            result.Name = categoryName;
            _categoryRepository.Update(result);
            return Ok("Updated successfully");
        }
        
        [HttpDelete("categories/{categoryId}")]
        public IActionResult DeleteCategory(int categoryId)
        {
            var result = _categoryRepository.Delete(categoryId);
            if (result == null) return BadRequest("Item not found");  
            return Ok();
        }
    }
}