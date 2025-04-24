using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using WEB_SHOW_WRIST_STRAP.Models.Entities;

namespace WEB_SHOW_WRIST_STRAP.Controllers
{
    public class ListPointsController : Controller
    {
        private readonly DataPointContext _context;

        public ListPointsController(DataPointContext context)
        {
            _context = context;
        }

        // GET: ListPoints
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index()
        {
            return _context.ListPoints != null ?
                        View(await _context.ListPoints.OrderBy(x => (x.IdLine*100 + x.IdPoint)).ToListAsync()) :
                          Problem("Entity set 'DataPointContext.ListPoints'  is null.");
        }

        // GET: ListPoints/Details/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ListPoints == null)
            {
                return NotFound();
            }

            var listPoint = await _context.ListPoints
                .FirstOrDefaultAsync(m => m.IdPoint == id);
            if (listPoint == null)
            {
                return NotFound();
            }

            return View(listPoint);
        }

        // GET: ListPoints/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: ListPoints/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([Bind("IdPoint,IdLine,NamePoint,MinSpect,MaxSpect,Addsread,Addswrite,Plc,OffsetValue,DeltaValue,Timeoff,Enstatus,UserChange,TimeChange,Change,Note,Csstop,Cssleft,Type")] ListPoint listPoint)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool exists = await _context.ListPoints
                                 .AnyAsync(lp => lp.IdPoint == listPoint.IdPoint && lp.IdLine == listPoint.IdLine);

                    if (exists)
                    {
                        // Nếu đã tồn tại, trả về lỗi
                        ModelState.AddModelError(string.Empty, "Combination of IdPoint and IdLine already exists.");
                        return View(listPoint);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");

                }

                
                _context.Add(listPoint);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(listPoint);
        }

        // GET: ListPoints/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null || _context.ListPoints == null || id == "" )
            {
                return NotFound();
            }

            string[] Arrid = id.Split('|');
            int id_point, id_line;

            if (Arrid.Length == 2 && int.TryParse(Arrid[0], out id_point) && int.TryParse(Arrid[1], out id_line))
            {
                var listPoint = await _context.ListPoints.FirstOrDefaultAsync(x => (x.IdPoint == id_point && x.IdLine == id_line));
                if (listPoint == null)
                {
                    return NotFound();
                }
                return View(listPoint);
            }
            else
            {
                return NotFound();
            }
            
        }

        // POST: ListPoints/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int idpoint, int idline, [Bind("IdPoint,IdLine,NamePoint,MinSpect,MaxSpect,Addsread,Addswrite,Plc,OffsetValue,DeltaValue,Timeoff,Enstatus,UserChange,TimeChange,Change,Note,Csstop,Cssleft,Type")] ListPoint listPoint)
        {
            if (idpoint != listPoint.IdPoint|| idline != listPoint.IdLine)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(listPoint);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ListPointExists(listPoint.IdPoint))
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
            return View(listPoint);
        }

        // GET: ListPoints/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null || _context.ListPoints == null || id == "")
            {
                return NotFound();
            }

            string[] Arrid = id.Split('|');
            int id_point, id_line;

            if (Arrid.Length == 2 && int.TryParse(Arrid[0], out id_point) && int.TryParse(Arrid[1], out id_line))
            {
                var listPoint = await _context.ListPoints.FirstOrDefaultAsync(x => (x.IdPoint == id_point && x.IdLine == id_line));
                if (listPoint == null)
                {
                    return NotFound();
                }
                return View(listPoint);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: ListPoints/Delete/5
        [HttpPost, ActionName("DeletePoint")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int idpoint, int idline)
        {
            if (_context.ListPoints == null)
            {
                return Problem("Entity set 'DataPointContext.ListPoints'  is null.");
            }
            var listPoint = await _context.ListPoints.FirstAsync(x => (x.IdPoint == idpoint && x.IdLine == idline));
            if (listPoint != null)
            {
                _context.ListPoints.Remove(listPoint);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ListPointExists(int id)
        {
          return (_context.ListPoints?.Any(e => e.IdPoint == id)).GetValueOrDefault();
        }

        [Authorize]
        [HttpPost]
        public async Task UpdateXYPoint(int idpoint, int idline, string top, string left, string width, string height)
        {
            if (idpoint >= 0 && idline >= 0)
            {
                var result = await _context.ListPoints.FirstAsync(x => (x.IdPoint == idpoint && x.IdLine == idline));
                if (result != null)
                {
                    result.Csstop = double.Parse(top.Substring(0, top.Length - 2));
                    result.Cssleft = double.Parse(left.Substring(0, left.Length - 2));
                    result.Width = double.Parse(width.Substring(0, width.Length - 2));
                    result.Height = double.Parse(height.Substring(0, height.Length - 2));
                    _context.Update(result);
                    await _context.SaveChangesAsync();
                }
            }
        }

        [Authorize()]
        [HttpPost]
        public async Task<string> GetAllpoint()
        {
            List<ListPoint> LsPointall = new List<ListPoint>();
            try
            {
                var result = await _context.ListPoints.ToListAsync();
                //var lsled = from led in result
                //            orderby led.IdLed descending
                //             select led;
                LsPointall = result;
            }
            catch { }
            return LsPointall.ToJson();
        }

        [Authorize()]
        [HttpPost]
        public async Task<string> GetAllpointNow()
        {
            List<DataNow> LsPointNow = new List<DataNow>();
            try
            {
                var result = await _context.DataNows.ToListAsync();
                //var lsled = from led in result
                //            orderby led.IdLed descending
                //             select led;
                LsPointNow = result;
            }
            catch { }
            return LsPointNow.ToJson();
        }

    }
}
