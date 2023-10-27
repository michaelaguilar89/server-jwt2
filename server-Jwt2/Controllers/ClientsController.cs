using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server_Jwt2.Models;
using server_Jwt2.Repository_s;
using WebApiClientes.Models.Dto;

namespace server_Jwt2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly ResponseDto response;
        private readonly clientService _clientService;
        public ClientsController(clientService clientService)
        {
            _clientService = clientService;
            response = new ResponseDto();
        }


        [HttpGet]
        public async Task<IActionResult> getAllClients()
        {
            try
            {
                var clients = await _clientService.GetAsync();
                if (clients != null)
                {
                    response.DisplayMessages = "List of Clients";
                    response.Result = clients;
                    return Ok(response);
                }
                response.DisplayMessages = "Clients not found";
                
                return BadRequest(response);
            }
            catch (Exception e)
            {
                response.DisplayMessages = "Error";
                response.ErrorsMessages = e.ToString();
                return BadRequest(response);
                
            }
        }

        [HttpPost]
        public async Task<IActionResult> createClient(client newClient)
        {
            try
            {
                var message = await _clientService.CreateAsync(newClient);
                if (message== "true")
                {
                    response.DisplayMessages="new Client has created";
                    return Ok(response);
                }
                response.DisplayMessages = "Error internal";
                response.ErrorsMessages = message;
                return BadRequest(response);
            }
            catch (Exception e)
            {
                response.DisplayMessages = "Error";
                response.ErrorsMessages = e.ToString();
                return BadRequest(response);

            }
        }
    }
}
