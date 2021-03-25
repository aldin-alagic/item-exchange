using ItemExchange.Data;
using ItemExchange.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItemExchange.Controllers
{
    public class ExpenseTypeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ExpenseTypeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<ExpenseType> expenseTypes = _db.ExpenseTypes;
            return View(expenseTypes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ExpenseType expenseType)
        {
            if (ModelState.IsValid)
            {
                _db.ExpenseTypes.Add(expenseType);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(expenseType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int? id)
        {
            var expensType = _db.ExpenseTypes.Find(id);
            if (expensType is null) return NotFound();
            _db.ExpenseTypes.Remove(expensType);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Update(int? id)
        {
            if (id is null || id == 0) return NotFound();
            var expensType = _db.ExpenseTypes.Find(id);
            if (expensType is null) return NotFound();
           
            return View(expensType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(ExpenseType expenseType)
        {
            if (ModelState.IsValid)
            {
                _db.ExpenseTypes.Update(expenseType);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(expenseType);
        }
    }
}