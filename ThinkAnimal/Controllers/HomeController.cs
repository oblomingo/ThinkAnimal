using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ThinkAnimal.Model;
using ThinkAnimal.Repository;

namespace ThinkAnimal.Controllers
{
    public class HomeController : Controller
    {
        FeaturesRepository fRepository = new FeaturesRepository();
        
        /// <summary>
        /// First page action
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            List<Animal> animals = new AnimalsRepository().GetAnimals();
            if (animals != null)
                ViewBag.Animals = String.Join(", ", animals.Select(a => a.Title).ToArray()); 
            return View();
        }

        /// <summary>
        /// Game start action
        /// </summary>
        /// <returns></returns>
        public ActionResult Play()
        {
            ViewBag.Message = "Your contact page.";
            Feature feature = fRepository.GetFirstFeature();

            return View(feature);
        }

        /// <summary>
        /// Get next feature after player answer as json object (AJAX)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetFeatureById(int id)
        {
            Feature nextFeature = fRepository.GetFeatureById(id);
            return Json(nextFeature, JsonRequestBehavior.AllowGet);
        }
    }
}
