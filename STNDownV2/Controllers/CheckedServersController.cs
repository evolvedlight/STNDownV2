using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using STNDownV2.Models;
using STNDownV2.Data;
using System;

namespace STNDownV2.Controllers
{
    public class CheckedServersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CheckedServersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> DoManualCheck(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checkedServer = await _context.CheckedServer
                .SingleOrDefaultAsync(m => m.ID == id);
            if (checkedServer == null)
            {
                return NotFound();
            }

            var result = checkedServer.Check();

            var checkEvent = new ServerCheckerEvent();
            checkEvent.CheckedServer = checkedServer;
            checkEvent.Timestamp = DateTime.UtcNow;
            checkEvent.Success = result.Success;
            checkEvent.Latency = result.Latency;
            _context.Add(checkEvent);
            await _context.SaveChangesAsync();
            
            return View(result);
        }

        

        
        public async Task<IActionResult> Index()
        {
            return View(await _context.CheckedServer.ToListAsync());
        }

         
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checkedServer = await _context.CheckedServer
                .SingleOrDefaultAsync(m => m.ID == id);
            if (checkedServer == null)
            {
                return NotFound();
            }

            return View(checkedServer);
        }

        // GET: CheckedServers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CheckedServers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Location,Name")] CheckedServer checkedServer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(checkedServer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(checkedServer);
        }

        // GET: CheckedServers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checkedServer = await _context.CheckedServer.SingleOrDefaultAsync(m => m.ID == id);
            if (checkedServer == null)
            {
                return NotFound();
            }
            return View(checkedServer);
        }

        // POST: CheckedServers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Location,Name")] CheckedServer checkedServer)
        {
            if (id != checkedServer.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(checkedServer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CheckedServerExists(checkedServer.ID))
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
            return View(checkedServer);
        }

        // GET: CheckedServers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checkedServer = await _context.CheckedServer
                .SingleOrDefaultAsync(m => m.ID == id);
            if (checkedServer == null)
            {
                return NotFound();
            }

            return View(checkedServer);
        }

        // POST: CheckedServers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var checkedServer = await _context.CheckedServer.SingleOrDefaultAsync(m => m.ID == id);
            _context.CheckedServer.Remove(checkedServer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CheckedServerExists(int id)
        {
            return _context.CheckedServer.Any(e => e.ID == id);
        }
    }
}
