using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LeaveManagementSystem.Domain.Entities;
using LeaveManagementSystem.Infraestructure.Persistence.Data;
using LeaveManagementSystem.Application.Interfaces;
using LeaveManagementSystem.Application.DTOs.LeaveType;

namespace LeaveManagementSystem.Web.Controllers
{
    public class LeaveTypeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILeaveTypeService _leaveTypeService;

        public LeaveTypeController(ApplicationDbContext context, ILeaveTypeService leaveTypeService)
        {
            _context = context;
            _leaveTypeService = leaveTypeService;
        }

        // GET: LeaveType
        public async Task<IActionResult> Index()
        {
            var leaveTypeList = await _leaveTypeService.GetAllAsync();
            return View(leaveTypeList.Data);
        }

        // GET: LeaveType/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveType = await _leaveTypeService.GetByIdAsync(id.Value);
            if (leaveType.Data == null)
            {
                return NotFound();
            }

            return View(leaveType.Data);
        }

        // GET: LeaveType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LeaveType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateLeaveTypeDto dto)
        {
            if (!ModelState.IsValid)
            {
                // Aquí deberían ya estar los mensajes personalizados de FluentValidation
                return View(dto);
            }

            var leaveType = new LeaveType
            {
                Name = dto.Name,
                NumberOfDays = dto.NumberOfDays
            };
            var result  = await _leaveTypeService.AddAsync(leaveType);
            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Error!);
                return View(dto);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: LeaveType/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveType = await _leaveTypeService.GetByIdAsync(id.Value);
            if (leaveType.Data == null)
            {
                return NotFound();
            }

            var dto = new CreateLeaveTypeDto() {
                Id = leaveType.Data.Id,
                Name = leaveType.Data.Name,
                NumberOfDays = leaveType.Data.NumberOfDays
            };

            return View(dto);
        }

        // POST: LeaveType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, CreateLeaveTypeDto dto)
        {
            if (id != dto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var leaveType = new LeaveType() {
                        Id = dto.Id.Value,
                        Name = dto.Name,
                        NumberOfDays = dto.NumberOfDays
                    };
                    var result = await _leaveTypeService.UpdateAsync(leaveType);
                    if (!result.Success)
                    {
                        ModelState.AddModelError(string.Empty, result.Error!);
                        return View(dto);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await LeaveTypeExists(dto.Id.Value))
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
            return View(dto);
        }

        // GET: LeaveType/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var leaveType = await _leaveTypeService.GetByIdAsync((long)id);
            if(leaveType.Data == null)
            {
                return NotFound();
            }
            return View(leaveType.Data);
        }

        // POST: LeaveType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var leaveType = await _leaveTypeService.GetByIdAsync(id);
            if (leaveType.Data != null)
            {
                await _leaveTypeService.TerminateAsync(leaveType.Data.Id);
            }
            
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> LeaveTypeExists(long id)
        {
            bool exists =  await _leaveTypeService.GetByIdAsync(id) != null;
            return exists;
        }
    }
}
