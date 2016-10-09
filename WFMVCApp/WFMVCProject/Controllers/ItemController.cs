using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;

namespace WFMVCProject.Controllers
{
    public class ItemController : Controller
    {
        HammerTimeEntities ht = new HammerTimeEntities();

        //list
        public ActionResult List()
        {
            return View(ht.Items.ToList());
        }
        //details
        public ActionResult Details(int id = 0)
        {
            return View(ht.Items.Find(id));
        }
        //create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(Item item)
        {
            using (ht)
            {
                ht.Items.Add(item);
                ht.SaveChanges();
            }
            return RedirectToAction("List");
        }
        
        //Update
        public ActionResult Edit(int id=0)
        {
            return View(ht.Items.Find(id));
        }

        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult Edit(Item item)
        {
            ht.Entry(item).State = EntityState.Modified;
            ht.SaveChanges();
            return RedirectToAction("List");
        }

        //Delete
        public ActionResult Delete(int id = 0)
        {
            return View(ht.Items.Find(id));
        }

        [HttpPost,ActionName("Delete")]
        public ActionResult delete_conf(int id)
        {
            Item item = ht.Items.Find(id);
            ht.Items.Remove(item);
            ht.SaveChanges();
            return RedirectToAction("List");
        }
    }
}