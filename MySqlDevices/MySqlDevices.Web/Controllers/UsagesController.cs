using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MySqlDevices.Core.Entities;
using MySqlDevices.Persistence;
using MySqlDevices.Core.Contracts;
using System.ComponentModel.DataAnnotations;

namespace MySqlDevices.Web.Controllers
{
    public class UsagesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsagesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Usages
        public async Task<IActionResult> Index()
        {
            //var applicationDbContext = _unitOfWork.Usages.Include(u => u.Device).Include(u => u.Person);
            // await applicationDbContext.ToListAsync()

            return View(await _unitOfWork.Usages.GetAllAsync());
        }

        // GET: Usages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usage = await _unitOfWork.Usages
                .FindByIdAsync(id.Value);
            if (usage == null)
            {
                return NotFound();
            }

            return View(usage);
        }

        // GET: Usages/Create
        public async Task<IActionResult> Create()
        {
            ViewData["DeviceId"] = new SelectList(await _unitOfWork.Devices.GetAllAsync(), "Id", "Name");
            ViewData["PersonId"] = new SelectList(await _unitOfWork.People.GetAllAsync(), "Id", "FirstName");
            return View();
        }

        // POST: Usages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RowVersion,From,To,DeviceId,PersonId")] Usage usage)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.Usages.AddAsnyc(usage);
                try
                {

                    await _unitOfWork.SaveChangesAsync();
                    return RedirectToAction(nameof(Index), "Home");
                }
                catch(ValidationException vaildationException)
                {
                    ValidationResult validationResult = vaildationException.ValidationResult;
                    ModelState.AddModelError(validationResult.MemberNames.ToArray()[0], validationResult.ToString());
                }
                catch(Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }


                //return RedirectToAction(nameof(Index));
            }
            ViewData["DeviceId"] = new SelectList(await _unitOfWork.Devices.GetAllAsync(), "Id", "Name", usage.DeviceId);
            ViewData["PersonId"] = new SelectList(await _unitOfWork.People.GetAllAsync(), "Id", "FirstName", usage.PersonId);
            
            return View(usage);
        }

        // GET: Usages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
           
            var usage = await _unitOfWork.Usages.FindByIdAsync(id.Value);
            if (usage == null)
            {
                return NotFound();
            }
            ViewData["DeviceId"] = new SelectList(await _unitOfWork.Devices.GetAllAsync(), "Id", "Name", usage.DeviceId);
            ViewData["PersonId"] = new SelectList(await _unitOfWork.People.GetAllAsync(), "Id", "FirstName", usage.PersonId);
            return View(usage);
        }

        // POST: Usages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RowVersion,From,To,DeviceId,PersonId")] Usage usage)
        {
            if (id != usage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var dbUsage = await _unitOfWork.Usages.FindByIdAsync(id);
                try
                {
                    dbUsage.From = usage.From;
                    dbUsage.To = usage.To;
                    dbUsage.DeviceId = usage.DeviceId;
                    dbUsage.PersonId = usage.PersonId;
                    dbUsage.RowVersion = usage.RowVersion;

                    _unitOfWork.Usages.Update(dbUsage);
                    await _unitOfWork.SaveChangesAsync();
                }
                catch (ValidationException vaildationException)
                {
                    ValidationResult validationResult = vaildationException.ValidationResult;
                    ModelState.AddModelError(validationResult.MemberNames.ToArray()[0], validationResult.ToString());
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsageExists(usage.Id))
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
                    ModelState.AddModelError("", e.Message);
                }
                return RedirectToAction(nameof(Index), "Home");

                //return RedirectToAction(nameof(Index));
            }
            ViewData["DeviceId"] = new SelectList(await _unitOfWork.Devices.GetAllAsync(), "Id", "Name", usage.DeviceId);
            ViewData["PersonId"] = new SelectList(await _unitOfWork.People.GetAllAsync(), "Id", "FirstName", usage.PersonId);
            return View(usage);
        }

        // GET: Usages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usage = await _unitOfWork.Usages.FindByIdAsync(id.Value);
            if (usage == null)
            {
                return NotFound();
            }

            return View(usage);
        }

        // POST: Usages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usage = await _unitOfWork.Usages.FindByIdAsync(id);
            _unitOfWork.Usages.Remove(usage);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index), "Home");

            //return RedirectToAction(nameof(Index));
        }

        private bool UsageExists(int id)
        {
            return _unitOfWork.Usages.Exists(id);
        }
    }
}
