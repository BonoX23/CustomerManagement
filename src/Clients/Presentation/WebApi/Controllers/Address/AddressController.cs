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
        private readonly INotificationContext _notification;

        public AddressController(IAddressService service, INotificationContext notification)
        {
            _service = service;
            _notification = notification;
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
            var result = await _service.AddAddressAsync(UserLoggedId, customerId, address);

            if (_notification.HasNotifications)
                return BadRequest(_notification.Notifications);

            return Ok(result);
        }

        /// <summary>
        /// Buscar um logradouro
        /// </summary>
        /// <param name="addressId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("place/{addressId}")]
        public async Task<IActionResult> GetAddress(int addressId)
        {
            var address = await _service.GetAddressById(UserLoggedId, addressId);

            if (_notification.HasNotifications)
            {
                return BadRequest(_notification.Notifications);
            }

            if (address == null)
            {
                return NotFound();
            }

            return Ok(address);
        }

        /// <summary>
        /// Buscar logradouros de um cliente
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("places/{customerId}")]
        public async Task<IActionResult> GetAddressByCustomerId(int customerId)
        {
            var addresses = await _service.GetAddressesByCustomerIdAsync(UserLoggedId, customerId);

            if (_notification.HasNotifications)
                return BadRequest(_notification.Notifications);

            return Ok(addresses);
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

            if (_notification.HasNotifications)
                return BadRequest(_notification.Notifications);

            return Ok(new { Message = "Logradouro deletado com sucesso" });
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

            if (_notification.HasNotifications)
                return BadRequest(_notification.Notifications);

            return Ok(new { Message = "Logradouro atualizado com sucesso" });
        }
    }
}
