using MailKit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server_Jwt2.Models;
using server_Jwt2.Repository_s;
using WebApiClientes.Models.Dto;

namespace server_Jwt2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ResponseDto response;
        private readonly categoryService _service;
        public CategoryController(categoryService service)
        {
            _service = service;
            response = new ResponseDto();
        }
        [HttpGet]
        public async Task<IActionResult> getCategory()
        {
            try
            {
                var categorys = await _service.GetAsync();
                if (categorys!=null)
                {
                    response.IsSuccess = true;
                    response.Result = categorys;
                    response.DisplayMessages = "List of Category's";
                    return Ok(response);
                }
                
                response.DisplayMessages = "Category is null"; 
                return BadRequest(response);
            }
            catch (Exception e)
            {
                response.DisplayMessages = "Error";
                response.ErrorsMessages = e.Message;
                return BadRequest(response);
            }
        }//end of get
        [HttpPost]
        public async Task<IActionResult> createCategory(category newCategory)
        {
            try
            {
                var message = await _service.CreateAsync(newCategory);
                if (message== "Ok")
                {
                    response.DisplayMessages = "New Object is created";
                    return Ok(response);

                }
                response.DisplayMessages = "internal error";
                return BadRequest(response);
            }
            catch (Exception e)
            {
                response.DisplayMessages = "Error";
                response.ErrorsMessages = e.Message;
                return BadRequest(response);
            }
        }//end of post
        [HttpPut]
        public async Task<IActionResult> updateCategory(category editCategory)
        {
            try
            {
                var message = await _service.UpdateAsync(editCategory);
                if (message== "Ok")
                {
                    response.DisplayMessages = "Object is update";
                    return Ok(response);

                }
                response.DisplayMessages = "internal error";
                return BadRequest(response);
            }
            catch (Exception e)
            {
                response.DisplayMessages = "Error";
                response.ErrorsMessages = e.Message;
                return BadRequest(response);
            }
        }//end of update
        [HttpGet("id:length(24)")]
        public async Task<IActionResult> getCategoryById(string id)
        {
            try
            {
                var category = await _service.GetByIdAsync(id);
                if (category!=null)
                {
                    response.Result = category;
                    response.DisplayMessages = "Category information";
                    return Ok(response);

                }
                response.DisplayMessages = "Category not found";
                return BadRequest(response);
            }
            catch (Exception e)
            {
                response.DisplayMessages = "Error";
                response.ErrorsMessages = e.Message;
                return BadRequest(response);
            }
        }//end of categoryById

        [HttpDelete("id:length(24)")]
        public async Task<IActionResult> deleteCategoryById(string id)
        {
            try
            {
                var category = await _service.RemoveAsync(id);
                if (category =="Ok")
                {
                   
                    response.DisplayMessages = "Category is removed";
                    return Ok(response);

                }
                response.DisplayMessages = "Category not found";
                return BadRequest(response);
            }
            catch (Exception e)
            {
                response.DisplayMessages = "Error";
                response.ErrorsMessages = e.Message;
                return BadRequest(response);
            }
        }//end of categoryById

    }
}
