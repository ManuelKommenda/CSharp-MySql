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

namespace MySqlDevices.Web.Controllers
{
    public class PeopleController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PeopleController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: People
        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.People.GetAllAsync());
        }

        // GET: People/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _unitOfWork.People
                .FindByIdAsync(id.Value);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: People/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RowVersion,LastName,FirstName,MailAdress")] Person person)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.People.AddAsnyc(person);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(person);
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _unitOfWork.People.FindByIdAsync(id.Value);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RowVersion,LastName,FirstName,MailAdress")] Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var dbPerson = await _unitOfWork.People.FindByIdAsync(id);
                try
                {
                    dbPerson.LastName = person.LastName;
                    dbPerson.FirstName = person.FirstName;
                    dbPerson.MailAdress = person.MailAdress;
                    dbPerson.RowVersion = person.RowVersion;

                    _unitOfWork.People.Update(dbPerson);
                    await _unitOfWork.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.Id))
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
            return View(person);
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _unitOfWork.People.FindByIdAsync(id.Value);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _unitOfWork.People.FindByIdAsync(id);
            _unitOfWork.People.Remove(person);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id)
        {
            return _unitOfWork.People.Exists(id);
        }
    }
}
