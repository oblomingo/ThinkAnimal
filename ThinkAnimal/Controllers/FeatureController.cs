using System.Collections.Generic;
using System.Web.Mvc;
using ThinkAnimal.Model;
using ThinkAnimal.Repository;

namespace ThinkAnimal.Controllers
{
    public class FeatureController : Controller
    {
        private readonly IFeaturesRepository _repository;

        public FeatureController(IFeaturesRepository repository) {
            //Get feature repository object (Unity) 
            _repository = repository;
        }

        /// <summary>
        /// Manage features and animals 
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Administrator")]
        public ActionResult Manage()
        {
            List<Feature> features = _repository.GetAllFeatures();
            return View(features);
        }
        
        /// <summary>
        /// Add new feature and animal
        /// </summary>
        /// <param name="parentFeatureId"></param>
        /// <param name="isYes"></param>
        /// <returns></returns>
        public ActionResult Add(int parentFeatureId, bool isYes)
        {
            var newFeature = new Feature {ParentFeatureId = parentFeatureId, IsYes = isYes};
            return View(newFeature);
        }

        /// <summary>
        /// Add new feature and animal
        /// </summary>
        /// <param name="feature"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Add(Feature feature)
        {
            if (ModelState.IsValid)
            {
                if (_repository.AddFeature(feature))
                    return RedirectToAction("Manage");
            }
            return View(feature);
        }

        /// <summary>
        /// Delete feature and animal
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id)
        {
            _repository.DeleteFeatureAndAnimal(id);
            return RedirectToAction("Manage");
        }

        /// <summary>
        /// Edit animal and feature texts
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id)
        {
            return View(_repository.GetFeatureById(id));
        }

        /// <summary>
        /// Edit animal and feature texts
        /// </summary>
        /// <param name="feature"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(Feature feature)
        {
            if (ModelState.IsValid)
            {
                if (_repository.SaveFeatureChanges(feature))
                    return RedirectToAction("Manage");
            }
            return View(feature);
        }
    }
}