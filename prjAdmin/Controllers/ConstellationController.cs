using Microsoft.AspNetCore.Mvc;
using prjAdmin.Models;
using prjAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prjAdmin.Controllers
{
    public class ConstellationController : Controller
    {
        private readonly CoffeeContext _context;

        public ConstellationController(CoffeeContext context)
        {
            _context = context;
        }
        public IActionResult ConstellationList()
        {
            IEnumerable<CConstellationViewModel> datas = null;
            var list = _context.Constellations.Select(a => new CConstellationViewModel()
            {
                ConstellationId = a.ConstellationId,
                ConstellationName = a.ConstellationName,
                ConstellationProductId = a.ConstellationProductId
            });
            if (list != null)
            {
                datas = list;
            }
            return View(datas);
        }

        public IActionResult EditConstellation(int? id)
        {
            CoffeeContext db = new CoffeeContext();
            Constellation con = _context.Constellations.FirstOrDefault(a => a.ConstellationId == id);
            if (con == null)
            {
                return RedirectToAction("ConstellationList");
            }
            return View(con);
        }
        [HttpPost]
        public IActionResult EditConstellation(CConstellationViewModel c)
        {
            Constellation con = _context.Constellations.FirstOrDefault(constellation => constellation.ConstellationId == c.ConstellationId);
            if(con != null)
            {
                con.ConstellationProductId = c.ConstellationProductId;
            }
            _context.SaveChanges();
            return RedirectToAction("ConstellationList");
        }

    }
}
