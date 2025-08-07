using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodBookPro.Web.Controllers
{
    public class MenuItemsController : Controller
    {
        // GET: MenuItemController
        public ActionResult Index()
        {
            return View();
        }

        // GET: MenuItemController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MenuItemController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MenuItemController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MenuItemController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MenuItemController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MenuItemController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MenuItemController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
