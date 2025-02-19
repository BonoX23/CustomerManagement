using Domain.DTOs;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Cliente
{
    [Route("api/v1")]
    [ApiController]
    [Authorize]
    public class CustomerController : MainController
    {
        private readonly ICustomerService _service;

        public CustomerController(ICustomerService service)
        {
            _service = service;
        }

        /// <summary>
        /// Adicionar um novo cliente
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("customer")]
        [AllowAnonymous]
        public async Task<IActionResult> AddCliente([FromBody] CustomerDto customer)
        {
            return Ok(await _service.UpdateCustomerAsync(customer));
        }

        /// <summary>
        /// Buscar um cliente
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("customer/{customerId}")]
        public IActionResult GetClienteById(int customerId)
        {
            return Ok(_service.GetCustomerById(UserLoggedId, customerId));
        }

        /// <summary>
        /// Deletar um cliente
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("customer/{customerId}")]
        public async Task<IActionResult> DeleteCliente(int customerId)
        {
            await _service.DeleteCustomerAsync(UserLoggedId, customerId);
            return Ok(new Tuple<string>("Cliente deletado com sucesso"));
        }

        /// <summary>
        /// Atualizar um cliente
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("customer/{customerId}")]
        public async Task<IActionResult> UpdateCliente(int customerId, [FromBody] CustomerDto customer)
        {
            await _service.UpdateCustomerAsync(UserLoggedId, customerId, customer);
            return Ok(new Tuple<string>("Cliente atualizado com sucesso"));
        }
    }
}