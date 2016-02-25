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

    public void Dispose()
    {
      Resturant.DeleteAll();
      Cuisine.DeleteAll();

    }

  }
}
