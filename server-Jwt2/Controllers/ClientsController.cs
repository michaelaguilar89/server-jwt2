using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver.Core.WireProtocol.Messages;
using server_Jwt2.Models;
using server_Jwt2.Repository_s;
using WebApiClientes.Models.Dto;

namespace server_Jwt2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
                if (message == "true")
                {
                    response.DisplayMessages = "new Client has created";
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
        }//end of Post

        [HttpPut]
        public async Task<IActionResult> updateClient(client myClient)
        {
            try
            {
                var message = await _clientService.UpdateAsync( myClient);
                if (message == "true")
                {
                    response.IsSuccess = true;
                    response.DisplayMessages = "client has been update";
                    return Ok(response);
                }
                               
                    if (message=="false")
                    {
                        response.IsSuccess = false;
                        response.DisplayMessages = "Client not found";
                        return BadRequest(response);

                    }
                
                response.ErrorsMessages = message;
                response.DisplayMessages = "Internal error";
                return BadRequest(response);    
            }
            catch (Exception e)
            {
                response.DisplayMessages = "Error";
                response.ErrorsMessages = e.ToString();
                return BadRequest(response);

            }
        }// end of update

        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> getClientById(string id)
        {
            try
            {
                var message =await _clientService.GetByIdAsync(id);
                if (message==null)
                {
                    response.DisplayMessages = "Client not found";
                    return BadRequest(response);
                }
                response.DisplayMessages = "Client Details";
                response.Result = message;
                return Ok(response);
            }
            catch (Exception e)
            {
                response.DisplayMessages = "Error";
                response.ErrorsMessages = e.ToString();
                return BadRequest(response);

            }
        }// end of getById
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> removeClient(string id)
        {
            try
            {
                var message = await _clientService.GetByIdAsync(id);
                if (message == null)
                {
                    response.DisplayMessages = "Client not found";
                    return BadRequest(response);
                }
                var message2 =await _clientService.RemoveAsync(id);
                if (message2=="true")
                {
                    response.DisplayMessages = "Client has been removed";
                    return Ok(response);
                }
                response.ErrorsMessages = message2;
                response.DisplayMessages = "Client not found";
                return BadRequest(response);

            }
            catch (Exception e)
            {
                response.DisplayMessages = "Error";
                response.ErrorsMessages = e.ToString();
                return BadRequest(response);

            }
        }// end of remove
    }
}
