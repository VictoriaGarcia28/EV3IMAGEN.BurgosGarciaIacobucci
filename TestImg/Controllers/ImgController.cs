using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestImg.Models;
using static System.Net.Mime.MediaTypeNames;

namespace TestImg.Controllers
{
    public class ImgController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Index(TBLimg tblimg)
        {
            string fileName = Path.GetFileNameWithoutExtension(tblimg.ImageFile.FileName);
            string extension = Path.GetExtension(tblimg.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            tblimg.img = "~/Img/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Img/"), fileName);
            tblimg.ImageFile.SaveAs(fileName);
            using (DBmodels dBmodel = new DBmodels())
            {
                dBmodel.TBLimg.Add(tblimg);
                dBmodel.SaveChanges();

            }
            ModelState.Clear();
            return View();
        }

        [HttpGet]

        public ActionResult View (int id)
        {
            Imagen imageModel = new Imagen(); //

            using(DBmodels db = new DBmodels())
            {
                imageModel = db.DBImage.Where(x => x.Id == id).FirstOrDefault();

            }
            return View(imageModel);

        }

            
    }

    internal class Imagen
    {
    }
}