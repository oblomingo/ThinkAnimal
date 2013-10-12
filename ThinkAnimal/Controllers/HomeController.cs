using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using ThinkAnimal.Models;
using ThinkAnimal.Repository;

namespace ThinkAnimal.Controllers
{
    public class HomeController : Controller
    {
        FeaturesRepository fRepository = new FeaturesRepository();
        
        /// <summary>
        /// Start page action
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
        /// Think animal start page
        /// </summary>
        /// <returns></returns>
        public ActionResult Play()
        {
            ViewBag.Message = "Your contact page.";
            Feature feature = fRepository.GetFeature();

            return View(feature);
        }

        /// <summary>
        /// Get next feature after player answer as json object (AJAX)
        /// </summary>
        /// <param name="currentFeature"></param>
        /// <returns></returns>
        public JsonResult GetNextFeature(Feature currentFeature)
        {
            Feature nextFeature = fRepository.GetNextFeature(currentFeature);
            return Json(nextFeature);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Manage()
        {
            List<Feature> features = fRepository.GetAllFeatures();
            return View(features);
        }

        
        [Authorize(Roles = "Administrator")]
        public ActionResult AddAnimal(int parentFeatureId, bool isYes)
        {
            Feature newFeature = new Feature();
            newFeature.ParentFeatureId = parentFeatureId;
            newFeature.ParentFeatureAnswerIsYes = isYes;
            return View(newFeature);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult AddAnimal(Feature feature)
        {
            if (ModelState.IsValid)
            {
                if (fRepository.SaveFeature(feature))
                    return RedirectToAction("Manage");
            }
            return View(feature);
        }
    }
}
