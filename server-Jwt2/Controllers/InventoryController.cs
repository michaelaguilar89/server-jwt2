using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server_Jwt2.Models;
using server_Jwt2.Repository_s;
using WebApiClientes.Models.Dto;

namespace server_Jwt2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly inventoryService _service;
        
        private readonly ResponseDto response;
        public InventoryController(inventoryService service)
        {
            _service = service;
            response = new ResponseDto();
        }

        [HttpPost]
        public async Task<IActionResult> createInventory(inventory newInventory)
        {
            try
            {
                var message = await _service.CreateAsync(newInventory);
                if (message=="Ok")
                {
                    response.DisplayMessages = "New Object is created";
                    return Ok(response);
                }
                if (message== "CategoryId not found")
                {
                    response.DisplayMessages = "CategoryId not found";
                    return BadRequest(response);
                }
                response.DisplayMessages = "error";
                response.ErrorsMessages = message;
                return BadRequest(response);
            }
            catch (Exception e)
            {

                response.ErrorsMessages = e.Message;
                response.DisplayMessages = "Error";
                return BadRequest(response);
            }
        } //end of post
        [HttpGet]
        public async Task<IActionResult> getInventory()
        {
            try
            {
                var inventory = await _service.GetAsync();
                response.Result = inventory;
                response.DisplayMessages = "List of Objects in inventory";
                return Ok(response);
            }
            catch (Exception e)
            {

                response.ErrorsMessages = e.Message;
                response.DisplayMessages = "Error";
                return BadRequest(response);
            }
        }//end of get
        [HttpPut]
        public async Task<IActionResult> updateInventoryProduct( inventory editProduct)
        {
            try
            {
                var message = await _service.UpdateAsync(editProduct);
                if (message=="Ok")
                {
                    response.IsSuccess = true;
                    response.DisplayMessages = "Object is update";
                    return Ok(response);
                }
                response.IsSuccess = false;
                response.ErrorsMessages = message;
                response.DisplayMessages="Error";
                return BadRequest(response);
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.ErrorsMessages = e.Message;
                response.DisplayMessages = "Error";
                return BadRequest(response);
            }
        }//end of update
        [HttpDelete("id:length(24)")]
        public async Task<IActionResult> deteleInventory(string id)
        {
            try
            {
                var message = await _service.RemoveAsync(id);
                if (message== "Ok")
                {
                    response.DisplayMessages = "Object has been removed";
                    response.IsSuccess = true;
                    return Ok(response);
                }
                response.IsSuccess = false;
                response.ErrorsMessages = message;
                response.DisplayMessages = "Error, Object Not found";
                return BadRequest(response);
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.ErrorsMessages = e.Message;
                response.DisplayMessages = "Error";
                return BadRequest(response);
            }
        }//end of delete

        [HttpGet("id:legth(24)")]
        public async Task<IActionResult> getInventoryById(string id)
        {
            try
            {
                var message = await _service.GetByIdAsync(id);
                if (message!=null)
                {
                    response.DisplayMessages = "Product Details";
                    response.IsSuccess = true;
                    response.Result = message;
                    return Ok(response);
                }
                response.DisplayMessages = "Product not found";
                response.IsSuccess = false;
                
                return BadRequest(response);
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.ErrorsMessages = e.Message;
                response.DisplayMessages = "Error";
                return BadRequest(response);
            }
        }

    }
}
