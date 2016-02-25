using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;
using System;

namespace ResturantNS
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        List<Cuisine> AllCategories = Cuisine.GetAll();
        return View["index.cshtml", AllCategories];
      };
      Get["/resturants"] = _ => {
        List<Resturant> AllResturants = Resturant.GetAll();
        return View["resturants.cshtml", AllResturants];
      };
      Get["/cuisines"] = _ => {
        List<Cuisine> AllCategories = Cuisine.GetAll();
        return View["cuisines.cshtml", AllCategories];
      };
      Get["/cuisines/new"] = _ => {
        return View["cuisines_form.cshtml"];
      };
      Post["/cuisines/new"] = _ => {
        Cuisine newCuisine = new Cuisine(Request.Form["cuisine-name"]);
        newCuisine.Save();
        return View["success.cshtml"];
      };
      Get["/resturants/new"] = _ => {
        List<Cuisine> AllCategories = Cuisine.GetAll();
        return View["resturants_form.cshtml", AllCategories];
      };
      Post["/resturants/new"] = _ => {
        DateTime newDate = new DateTime(Request.Form["new-year"], Request.Form["new-month"], Request.Form["new-day"]);
        Resturant newResturant = new Resturant(Request.Form["resturant-name"], Request.Form["resturant-location"], Request.Form["cuisine-id"], newDate);
        newResturant.Save();
        return View["success.cshtml"];
      };
      Post["/resturants/delete"] = _ => {
        Resturant.DeleteAll();
        return View["cleared.cshtml"];
      };
      Get["/cuisines/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        var SelectedCuisine = Cuisine.Find(parameters.id);
        var CuisineResturants = SelectedCuisine.GetResturants();
        model.Add("cuisine", SelectedCuisine);
        model.Add("resturants", CuisineResturants);
        return View["cuisine.cshtml", model];
      };

    }
  }
}
