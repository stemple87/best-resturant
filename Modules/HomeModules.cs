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
        List<Cuisine> AllCuisines = Cuisine.GetAll();
        return View["index.cshtml", AllCuisines];
      };
      Get["/resturants"] = _ => {
        List<Resturant> AllResturants = Resturant.GetAll();
        return View["resturants.cshtml", AllResturants];
      };
      Get["/cuisines"] = _ => {
        List<Cuisine> AllCuisines = Cuisine.GetAll();
        return View["cuisines.cshtml", AllCuisines];
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
        List<Cuisine> AllCuisines = Cuisine.GetAll();
        return View["resturants_form.cshtml", AllCuisines];
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
      Post["/cuisines/clear"] = _ => {
        Cuisine.DeleteAll();
        return View["cuisines_cleared.cshtml"];
      };

      Get["/cuisines/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        var SelectedCuisine = Cuisine.Find(parameters.id);
        var CuisineResturants = SelectedCuisine.GetResturants();
        model.Add("cuisine", SelectedCuisine);
        model.Add("resturants", CuisineResturants);
        return View["cuisine.cshtml", model];
      };

      Get["cuisine/edit/{id}"] = parameters => {
        Cuisine SelectedCuisine = Cuisine.Find(parameters.id);
        return View["cuisine_edit.cshtml", SelectedCuisine];
      };

      Patch["cuisine/edit/{id}"] = parameters => {
        Cuisine SelectedCuisine = Cuisine.Find(parameters.id);
        SelectedCuisine.Update(Request.Form["cuisine-name"]);
        return View["success.cshtml"];
      };

      Get["cuisine/delete/{id}"] = parameters => {
        Cuisine SelectedCuisine = Cuisine.Find(parameters.id);
        return View["cuisine_delete.cshtml", SelectedCuisine];
      };

      Delete["cuisine/delete/{id}"] = parameters => {
        Cuisine SelectedCuisine = Cuisine.Find(parameters.id);
        SelectedCuisine.Delete();
        return View["success.cshtml"];
      };

    }
  }
}
