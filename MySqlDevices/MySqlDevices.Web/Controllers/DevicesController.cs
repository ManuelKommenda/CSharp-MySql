using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MySqlDevices.Core.Contracts;
using MySqlDevices.Core.Entities;
using MySqlDevices.Persistence;

namespace MySqlDevices.Web.Controllers
{
    public class DevicesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DevicesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Devices
        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.Devices.GetAllAsync());
        }

        // GET: Devices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = await _unitOfWork.Devices
                .FindByIdAsync(id.Value);

            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }

        // GET: Devices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Devices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RowVersion,SerialNumber,Name,DeviceType")] Device device)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.Devices.AddAsnyc(device);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(device);
        }

        // GET: Devices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = await _unitOfWork.Devices.FindByIdAsync(id.Value);
            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }

        // POST: Devices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RowVersion,SerialNumber,Name,DeviceType")] Device device)
        {
            if (id != device.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var dbDevice = await _unitOfWork.Devices.FindByIdAsync(id);
                try
                {
                    dbDevice.SerialNumber = device.SerialNumber;
                    dbDevice.Name = device.Name;
                    dbDevice.DeviceType = device.DeviceType;
                    dbDevice.RowVersion = device.RowVersion;

                    _unitOfWork.Devices.Update(dbDevice);
                    await _unitOfWork.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeviceExists(device.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(device);
        }

        // GET: Devices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = await _unitOfWork.Devices.FindByIdAsync(id.Value);
            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }

        // POST: Devices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var device = await _unitOfWork.Devices.FindByIdAsync(id);
            _unitOfWork.Devices.Remove(device);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeviceExists(int id)
        {
            return _unitOfWork.Devices.Exists(id);
        }
    }
}
