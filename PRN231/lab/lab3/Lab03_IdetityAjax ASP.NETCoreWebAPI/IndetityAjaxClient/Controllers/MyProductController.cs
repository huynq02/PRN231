using Microsoft.AspNetCore.Mvc;

namespace IndetityAjaxClient.Controllers
{
    public class MyProductController : Controller
    {
        // GET: PruductController
        
        // GET: PruductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PruductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PruductController/Create
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

        // GET: PruductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PruductController/Edit/5
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

        // GET: PruductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PruductController/Delete/5
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
