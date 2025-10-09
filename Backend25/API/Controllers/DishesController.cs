using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services;
using BusinessLogicLayer.Constants;
using Core.Models;
using Core.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishesController : ControllerBase
    {
        private readonly IDishService _dishService;

        public DishesController(IDishService dishService)
        {
            _dishService = dishService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<DishListItemDto>>> Get(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] List<Category>? categories = null,
            [FromQuery] bool? isVegetarian = null,
            [FromQuery] SortBy sortBy = SortBy.NameAsc,
            [FromQuery] double? minPrice = null,
            [FromQuery] double? maxPrice = null)
        {
            var query = new DishQueryParamsDto()
            {
                Categories = categories,
                PageSize = pageSize,
                Page = page,
                SortBy = sortBy,
                IsVegetarian = isVegetarian,
                MinPrice = minPrice,
                MaxPrice = maxPrice
            };

            try
            {
                var result = await _dishService.GetDishListAsync(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DishDetailsDto>> GetDetails(Guid id)
        {
            try
            {
                var result = await _dishService.GetDishDetailsAsync(id);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Policy = ApplicationRoleNames.Manager)]
        public async Task<ActionResult<DishDetailsDto>> Create([FromBody] DishDetailsDto dishDto)
        {
            try
            {
                var result = await _dishService.CreateDishAsync(dishDto);
                return CreatedAtAction(nameof(GetDetails), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Policy = ApplicationRoleNames.Manager)]
        public async Task<ActionResult<DishDetailsDto>> Update(Guid id, [FromBody] DishDetailsDto dishDto)
        {
            try
            {
                var result = await _dishService.UpdateDishAsync(id, dishDto);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = ApplicationRoleNames.Manager)]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _dishService.DeleteDishAsync(id);
                if (result)
                {
                    return NoContent();
                }
                return NotFound($"Dish with id = {id} not found");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}