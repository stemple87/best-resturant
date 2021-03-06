using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ResturantNS
{
  public class CuisineTest : IDisposable
  {
    public CuisineTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=best_resturant_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_CategoriesEmptyAtFirst()
    {
      //Arrange, Act
      int result = Cuisine.GetAll().Count;

      //Assert
      Assert.Equal(0, result);

    }

    [Fact]
    public void Test_Equal_ReturnsTrueForSameName()
    {
      //Arrange, Act
      Cuisine firstCuisine = new Cuisine("Household chores");
      Cuisine secondCuisine= new Cuisine("Household chores");

      //Assert
      Assert.Equal(firstCuisine, secondCuisine);
    }

    [Fact]
    public void Test_Save_SavesCuisineToDatabase()
    {
      //Arrange
      Cuisine testCuisine = new Cuisine("Household chores");
      testCuisine.Save();

      //Act
      List<Cuisine> result = Cuisine.GetAll();
      List<Cuisine> testList = new List<Cuisine>{testCuisine};

      //AssertAssert.Equal(testList, result);
    }

    [Fact]
    public void Test_Save_AssignsIdToCuisineObject()
    {
      //Arrange
      Cuisine testCuisine = new Cuisine("Household chores");
      testCuisine.Save();

      //Act
      Cuisine savedCuisine = Cuisine.GetAll()[0];

      int result = savedCuisine.GetId();
      int testId = testCuisine.GetId();

      //AssertAssert
      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_Find_FindsCuisineInDatabase()
    {
      //Arrange
      Cuisine testCuisine = new Cuisine("Household chores");
      testCuisine.Save();

      //Act
      Cuisine foundCuisine = Cuisine.Find(testCuisine.GetId());

      //Assert
      Assert.Equal(testCuisine, foundCuisine);
    }

    [Fact]
    public void Test_GetResturants_RetrievesAllResturantsWithCuisine()
    {
      Cuisine testCuisine = new Cuisine("Household chores");
      testCuisine.Save();

      Resturant firstResturant = new Resturant("Mow the lawn", "ello", testCuisine.GetId());
      firstResturant.Save();

      Resturant secondResturant = new Resturant("Do the dishes", "ello", testCuisine.GetId());
      secondResturant.Save();

      List<Resturant> testResturantList = new List<Resturant>{firstResturant, secondResturant};
      List<Resturant> resultResturantList = testCuisine.GetResturants();

      Assert.Equal(testResturantList, resultResturantList);
    }

    [Fact]
    public void Test_Update_UpdatesCuisineInDatabase()
    {
      //Arrange
      string name = "Home stuff";
      Cuisine testCuisine = new Cuisine(name);
      testCuisine.Save();
      string newName = "Work stuff";

      //Act
      testCuisine.Update(newName);

      string result = testCuisine.GetName();

      //Assert
      Assert.Equal(newName, result);
    }

    [Fact]
    public void Test_Delete_DeletesCuisineFromDatabase()
    {
      //Arrange
      string name1 = "Home stuff";
      Cuisine testCuisine1 = new Cuisine(name1);
      testCuisine1.Save();

      string name2 = "Work stuff";
      Cuisine testCuisine2 = new Cuisine(name2);
      testCuisine2.Save();

      Resturant testResturant1 = new Resturant("Mow the lawn", "yep", testCuisine1.GetId());
      testResturant1.Save();
      Resturant testResturant2 = new Resturant("Send emails", "stuff", testCuisine2.GetId());
      testResturant2.Save();

      //Act
      testCuisine1.Delete();
      List<Cuisine> resultCategories = Cuisine.GetAll();
      List<Cuisine> testCuisineList = new List<Cuisine> {testCuisine2};

      List<Resturant> resultResturants = Resturant.GetAll();
      List<Resturant> testResturantList = new List<Resturant> {testResturant2};

      //Assert
      Assert.Equal(testCuisineList, resultCategories);
      Assert.Equal(testResturantList, resultResturants);
    }

    public void Dispose()
    {
      Resturant.DeleteAll();
      Cuisine.DeleteAll();

    }

  }
}
