using ClassLibrary1;
using Microsoft.AspNet.Identity;
using SongBook.Data;
using SongBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SongBook.WebMVC.Controllers
{
    [Authorize]
    public class SongController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Song
        public ActionResult Index(string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            var service = CreateSongService();
            var model = service.GetSongs(sortOrder);
            return View(model);
        }

        public ActionResult Create()
        {
            ViewBag.BandId = new SelectList(db.Bands, "BandId", "Name");
            ViewBag.ShowId = new SelectList(db.Shows, "ShowId", "Date");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create (SongCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateSongService();
            if(service.CreateSong(model))
            {
                TempData["SaveResult"] = "Song Created!";
                return RedirectToAction("Index");
               
            }

            ModelState.AddModelError("", "Song Was Not Created!" );

            return View(model);
        }

        public ActionResult Details (int id)
        {
            var svc = CreateSongService();
            var model = svc.GetSongById(id);

            return View(model);
        }

        public ActionResult Edit (int id)
        {
            var service = CreateSongService();
            var detail = service.GetSongById(id);
            var model =
                new SongEdit
                {
                    SongId = detail.SongId,
                    BandID = detail.BandID,
                    ShowId = detail.ShowId,
                    Name = detail.Name,
                    Length = detail.Length,
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, SongEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if(model.SongId != id)
            {
                ModelState.AddModelError("", "Id Does Not Match!");
                return View(model);
            }

            var service = CreateSongService();

            if(service.UpdateSong(model))
            {
                TempData["SaveResult"] = "Song Updated!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Could Not Update Song!");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateSongService();
            var model = svc.GetSongById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateSongService();
            service.DeleteSong(id);
            TempData["SaveResult"] = "Song Deleted!";
            return RedirectToAction("Index");
        }


        private SongService CreateSongService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new SongService(userId);
            return service;
        }

    }
}
       
            
        

            

