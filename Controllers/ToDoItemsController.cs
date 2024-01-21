using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication6.Data;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    public class ToDoItemsController : Controller
    {
        private readonly WebApplication6Context _context;

        public ToDoItemsController(WebApplication6Context context)
        {
            _context = context;
        }

        // GET: ToDoItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.ToDoItem.ToListAsync());
        }

        // GET: ToDoItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ToDoItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Text")] ToDoItem toDoItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(toDoItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(toDoItem);
        }

        // GET: ToDoItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoItem = await _context.ToDoItem.FindAsync(id);
            if (toDoItem == null)
            {
                return NotFound();
            }
            return View(toDoItem);
        }

        // POST: ToDoItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Text")] ToDoItem toDoItem)
        {
            if (id != toDoItem.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(toDoItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToDoItemExists(toDoItem.ID))
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
            return View(toDoItem);
        }

        // GET: ToDoItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoItem = await _context.ToDoItem
                .FirstOrDefaultAsync(m => m.ID == id);
            if (toDoItem == null)
            {
                return NotFound();
            }

            return View(toDoItem);
        }

        // POST: ToDoItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var toDoItem = await _context.ToDoItem.FindAsync(id);
            if (toDoItem != null)
            {
                _context.ToDoItem.Remove(toDoItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToDoItemExists(int id)
        {
            return _context.ToDoItem.Any(e => e.ID == id);
        }

      // POST: ToDoItems/Check/5
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Check(int id)
      {
         var toDoItem = await _context.ToDoItem.FindAsync(id);
         if (toDoItem == null)
         {
            return NotFound();
         }

         if (ModelState.IsValid)
         {
            toDoItem.IsCompleted=true;
               _context.Update(toDoItem);
               await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
         }
         return View(toDoItem);
      }

      // POST: ToDoItems/Uncheck/5
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Uncheck(int id)
      {
         var toDoItem = await _context.ToDoItem.FindAsync(id);
         if (toDoItem == null)
         {
            return NotFound();
         }

         if (ModelState.IsValid)
         {
            toDoItem.IsCompleted=false;
            _context.Update(toDoItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
         }
         return View(toDoItem);
      }
   }
}
