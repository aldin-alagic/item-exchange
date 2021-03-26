using ItemExchange.Data;
using ItemExchange.Models;
using ItemExchange.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItemExchange.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ExpenseController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Expense> expenses = _db.Expenses.Include("Type").ToList();
            return View(expenses);
        }

        public IActionResult Create()
        {
            ExpenseViewModel expenseViewModel = new ExpenseViewModel()
            {
                Expense = new Expense(),
                ExpenseTypes = _db.ExpenseTypes.Select(t => new SelectListItem
                {
                    Text = t.Name,
                    Value = t.Id.ToString(),
                }).ToList()
            };
            return View(expenseViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ExpenseViewModel expenseViewModel)
        {
            if (ModelState.IsValid)
            {
                _db.Expenses.Add(expenseViewModel.Expense);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(expenseViewModel.Expense);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int? id)
        {
            var expense = _db.Expenses.Find(id);
            if (expense is null)
            {
                return NotFound();
            }
            _db.Expenses.Remove(expense);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Update(int? id)
        {
            if (id is null || id == 0) return NotFound();
            var expense = _db.Expenses.Find(id);
            if (expense is null) return NotFound();
           
            return View(expense);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Expense expense)
        {
            if (ModelState.IsValid)
            {
                _db.Expenses.Update(expense);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(expense);
        }
    }
}