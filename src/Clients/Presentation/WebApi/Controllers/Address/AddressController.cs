using Domain.DTOs;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Address
{
    [Route("api/v1")]
    [ApiController]
    [Authorize]
    public class AddressController : MainController
    {
        private readonly IAddressService _service;

        public AddressController(IAddressService service)
        {
            _service = service;
        }

        /// <summary>
        /// Adicionar um novo logradouro ao cliente
        /// </summary>
        /// <param name="address"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("place/{customerId}")]
        public async Task<IActionResult> AddAddress(int customerId, [FromBody] AddressDto address)
        {
            return Ok(await _service.AddAddressAsync(UserLoggedId, customerId, address));
        }

        /// <summary>
        /// Buscar um logradouro
        /// </summary>
        /// <param name="addressId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("place/{addressId}")]
        public IActionResult GetAddress(int addressId)
        {
            return Ok(_service.GetAddressById(UserLoggedId, addressId));
        }

        /// <summary>
        /// Buscar um logradouros de um cliente
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("places/{customerId}")]
        public async Task<IActionResult> GetAddressByCustomerId(int customerId)
        {
            return Ok(await _service.GetAddressesByCustomerIdAsync(UserLoggedId, customerId));
        }

        /// <summary>
        /// Deletar um logradouro
        /// </summary>
        /// <param name="addressId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("place/{addressId}")]
        public async Task<IActionResult> DeleteAddress(int addressId)
        {
            await _service.DeleteAddressAsync(UserLoggedId, addressId);
            return Ok(new Tuple<string>("Logradouro deletado com sucesso"));
        }

        /// <summary>
        /// Atualizar um logradouro de um cliente
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="addressId"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("place/{customerId}/{addressId}")]
        public async Task<IActionResult> UpdateAddress(int customerId, int addressId, [FromBody] AddressDto address)
        {
            await _service.UpdateAddressAsync(UserLoggedId, customerId, addressId, address);
            return Ok(new Tuple<string>("Logradouro atualizado com sucesso"));
        }
    }
}