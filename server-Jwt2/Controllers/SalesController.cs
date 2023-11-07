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
    public class SalesController : ControllerBase
    {
        private readonly SalesService _service;
        private readonly ResponseDto response;
        public SalesController(SalesService service)
        {
            _service = service;
            response = new ResponseDto();
        }
        [HttpPost]
        public async Task<IActionResult> CreateSale(sales newSale)
        {
            try
            {
                var message = await _service.createOneAsync(newSale);
                if (message=="Ok")
                {
                    response.IsSuccess = true;
                    response.DisplayMessages = "New sale has created!";
                    return Ok(response);
                }
                response.Result = message;//
                response.DisplayMessages = "Error, algun producto no se puede vender";
                response.IsSuccess = false;
                return BadRequest(response);
            }
            catch (Exception e)
            {

                response.ErrorsMessages = e.Message;
                response.DisplayMessages = "Error";
                response.IsSuccess = false;
                return BadRequest(response);
            }
        }//end of post

        [HttpGet]
        public async Task<IActionResult> getAllSales()
        {
            try
            {
                var message = await _service.getAllSales();
                if (message!=null)
                {
                    response.Result = message;
                    response.IsSuccess = true;
                    response.DisplayMessages = "List of Sales";
                    return Ok(response);
                }
               
                response.IsSuccess = false;
                response.DisplayMessages = "Error,Sales not found";
                return Ok(response);
            }
            catch (Exception e)
            {

                response.ErrorsMessages = e.Message;
                response.DisplayMessages = "Error";
                response.IsSuccess = false;
                return BadRequest(response);
            }

        }//end of GetAllSales
        [HttpGet("id:length(24)")]
        public async Task<IActionResult> getSaleById(string id)
        {
            try
            {
                var message = await _service.getSaleById(id);
                if (message!=null)
                {
                    response.Result = message;
                    response.IsSuccess = true;
                    response.DisplayMessages = "Sales Details";
                    return Ok(response);

                }
                response.IsSuccess = false;
                response.DisplayMessages = "Sales not found";
                return Ok(response);
            }
            catch (Exception e)
            {

                response.ErrorsMessages = e.Message;
                response.DisplayMessages = "Error";
                response.IsSuccess = false;
                return BadRequest(response);
            }
        }
    }
}
