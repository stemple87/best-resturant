using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ResturantNS
{
  public class ResturantTest : IDisposable
  {
    public ResturantTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=best_resturant_test;Integrated Security=SSPI;";
    }


    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      int result = Resturant.GetAll().Count;

      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Save_AssignsIdToObject()
    {
      //Arrange
      Resturant testResturant = new Resturant("Mow the lawn", 1);

      //Act
      testResturant.Save();
      Resturant savedResturant = Resturant.GetAll()[0];

      int result = savedResturant.GetId();
      int testId = testResturant.GetId();

      //Assert
      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_Find_FindsResturantInDatabase()
    {
      //Arrange
      Resturant testResturant = new Resturant("Mow the lawn", 1);
      testResturant.Save();

      //Act
      Resturant foundResturant = Resturant.Find(testResturant.GetId());

      //Assert
      Assert.Equal(testResturant, foundResturant);
    }




    public void Dispose()
    {
      Resturant.DeleteAll();
    }

  }
}
