using System.Web.Mvc;
using Microsoft.Practices.Unity;
using ThinkAnimal.Repository;
using Unity.Mvc4;

namespace ThinkAnimal
{
  public static class Bootstrapper
  {
    public static IUnityContainer Initialise()
    {
      var container = BuildUnityContainer();

      DependencyResolver.SetResolver(new UnityDependencyResolver(container));

      return container;
    }

    private static IUnityContainer BuildUnityContainer()
    {
      var container = new UnityContainer();
      container.RegisterType<IFeaturesRepository, FeaturesRepository>();   
      RegisterTypes(container);

      return container;
    }

    public static void RegisterTypes(IUnityContainer container)
    {
    
    }
  }
}