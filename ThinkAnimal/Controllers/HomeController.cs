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

        private readonly IFeaturesRepository _repository;
        public HomeController(){}
        public HomeController(IFeaturesRepository repository)
        {
            //Get feature repository object (Unity) 
            _repository = repository;
        }
        
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
            Feature feature = _repository.GetFirstFeature();
            return View(feature);
        }

        /// <summary>
        /// Get next feature after player answer as json object (AJAX)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetFeatureById(int id)
        {
            Feature nextFeature = _repository.GetFeatureById(id);
            return Json(nextFeature, JsonRequestBehavior.AllowGet);
        }
    }
}
