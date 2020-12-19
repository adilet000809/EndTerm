using System.Collections.Generic;
using System.Net.Mime;
using System.Text.Json;
using EndTerm.Models;
using EndTerm.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EndTerm.Controllers.Api
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(
            ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        /// <summary>
        /// Fetch all categories
        /// </summary>
        /// <returns>List of category objects</returns>
        [HttpGet("categories")]
        public IEnumerable<Category> GetAllCategories()
        {
            return _categoryRepository.GetAllCategory();
        }
        
        /// <summary>
        /// Fetches single category object by id
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns>Category object</returns>
        [HttpGet("categories/{categoryId}")]
        public IActionResult GetCategory(int categoryId)
        {
            var category = _categoryRepository.GetCategory(categoryId);
            if (category == null) return NotFound("Category not found");
            return Ok(category);
        }
        
        /// <summary>
        /// Add category into database
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = 
            JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("categories/add")]
        public Category AddCategory(Category category)
        {
            var categoryName = category.Name;
            return _categoryRepository.Add(new Category {Name = categoryName});
        }
        
        /// <summary>
        /// Update category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = 
            JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("categories/update")]
        public IActionResult UpdateCategory(Category category)
        {
            var result = _categoryRepository.GetCategory(category.Id);
            if (result == null) return NotFound("Category not found");
            var categoryName = category.Name;
            result.Name = categoryName;
            _categoryRepository.Update(result);
            return Ok("Updated successfully");
        }
        
        /// <summary>
        /// Delete category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = 
            JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("categories/{categoryId}")]
        public IActionResult DeleteCategory(int categoryId)
        {
            var result = _categoryRepository.Delete(categoryId);
            if (result == null) return BadRequest("Category not found");  
            return Ok();
        }
    }
}