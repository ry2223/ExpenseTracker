using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Authorization;

namespace ExpenseTracker.Controllers
{
    [Authorize]
    public class ExpenseController : Controller
    {
        private readonly ExpenseContext _context;

        public ExpenseController(ExpenseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Category> cat = new List<Category>();
            cat = (from c in _context.Categories select c).ToList();
            cat.Insert(0, new Category { CategoryId = 0, CategoryName = "-- Select Category --" });
            ViewBag.message = cat;

            return View(await _context.Expenses.ToListAsync());
        }

        public IActionResult AddOrEdit(int id = 0)
        {
            if(id == 0)
            return View(new Expense());
            else
                return View(_context.Expenses.Find(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("ExpenseId,ExpenseName,Amount,ExpenseDate,Category")] Expense expense)
        {
            if (ModelState.IsValid)
            {
                if(expense.ExpenseId == 0)
                _context.Add(expense);
                else
                    _context.Update(expense);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(expense);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpenseExists(int id)
        {
            return _context.Expenses.Any(e => e.ExpenseId == id);
        }
    }
}