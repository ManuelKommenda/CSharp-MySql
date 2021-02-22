using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlDevices.Core.Contracts;
using MySqlDevices.Core.DataTransferObject;
using MySqlDevices.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MySqlDevices.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        // GET: api/<DevicesController>
        private readonly IUnitOfWork _unitOfWork;

        public DevicesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Devices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeviceDto>>> GetDevices()
        {
            var devices = await _unitOfWork.Devices.GetAllAsync();

            return devices
                .Select(d => new DeviceDto(d))
                .ToArray();
        }

        // GET: api/Devices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DeviceDto>> GetDevice(int id)
        {
            var device = await _unitOfWork.Devices.FindByIdAsync(id);

            if (device == null)
            {
                return NotFound();
            }

            return new DeviceDto(device);
        }

        // PUT: api/Devices/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDevice(int id, DeviceDto deviceDto)
        {
            if (id != deviceDto.Id)
            {
                return BadRequest();
            }

            var dbDevice = await _unitOfWork.Devices.FindByIdAsync(id);
            if (dbDevice == null)
            {
                return NotFound();
            }

            dbDevice.Name = deviceDto.Name;
            dbDevice.SerialNumber = deviceDto.SerialNumber;
            dbDevice.DeviceType = Enum.Parse<DeviceType>(deviceDto.DeviceType);


            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            /*catch (ValidationException vaildationException)
            {
                ValidationResult validationResult = vaildationException.ValidationResult;
                return BadRequest(validationResult.ErrorMessage);
            }*/
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return NoContent();
        }

        // POST: api/Devices
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<DeviceDto>> PostDevice(DeviceDto deviceDto)
        {
            Device device = new Device();
            device.Name = deviceDto.Name;
            device.SerialNumber = deviceDto.SerialNumber;
            device.DeviceType = Enum.Parse<DeviceType>(deviceDto.DeviceType);

            await _unitOfWork.Devices.AddAsnyc(device);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            /*catch (ValidationException vaildationException)
            {
                ValidationResult validationResult = vaildationException.ValidationResult;
                return BadRequest(validationResult.ErrorMessage);
            }*/
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return CreatedAtAction("GetDevice", new { id = device.Id }, new DeviceDto(device));
        }

        // DELETE: api/Devices/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DeviceDto>> DeleteDevice(int id)
        {
            var device = await _unitOfWork.Devices.FindByIdAsync(id);
            if (device == null)
            {
                return NotFound();
            }

            _unitOfWork.Devices.Remove(device);
            await _unitOfWork.SaveChangesAsync();

            return new DeviceDto(device);
        }

        private bool DeviceExists(int id)
        {
            return _unitOfWork.Devices.Exists(id);
        }
    }
}
