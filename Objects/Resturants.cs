using System.Collections.Generic;
using System;
using System.Data.SqlClient;

namespace ResturantNS
{
  public class Resturant
  {
    private int _id;
    private string _name;
    private DateTime? _newDate;
    private string _location;
    private int _cuisine_id;

    //constructor
    public Resturant(string Name, string Location, int CuisineId, DateTime? NewDate = null, int Id = 0)
    {
      _id = Id;
      _name = Name;
      _cuisine_id = CuisineId;
      _location = Location;

      if (_newDate == null)
      {
        _newDate = new DateTime(2099, 12, 31);
      }
    }

    //getters setters
    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public void SetName(string newName)
    {
      _name = newName;
    }
    public DateTime? GetNewDate()
    {
      return _newDate;
    }
    public void SetNewDate(DateTime? newNewDate)
    {
      _newDate = newNewDate;
    }
    public string GetLocation()
    {
      return _location;
    }
    public void SetLocation(string newLocation)
    {
      _location = newLocation;
    }
    public int GetCuisineId()
    {
      return _cuisine_id;
    }
    public void SetCuisineId(int newCuisineId)
    {
      _cuisine_id = newCuisineId;
    }

    //equals override
    public override bool Equals(System.Object otherResturant)
    {
      if (!(otherResturant is Resturant))
      {
        return false;
      }
      else
      {
        Resturant newResturant = (Resturant) otherResturant;
        bool idEquality = (this.GetId() == newResturant.GetId());
        bool nameEquality = (this.GetName() == newResturant.GetName());
        bool dateTimeEquality = (this.GetNewDate() == newResturant.GetNewDate());
        bool locationEquality = (this.GetLocation() == newResturant.GetLocation());
        bool cuisineEquality = this.GetCuisineId() == newResturant.GetCuisineId();
        return (idEquality && nameEquality && cuisineEquality);
      }
    }

    //save method
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO resturants (name, date, location, cuisine_id) OUTPUT INSERTED.id VALUES (@ResturantName, @ResturantNewDate, @ResturantLocation, @ResturantCuisineId);", conn);

      SqlParameter nameParameter = new SqlParameter("@ResturantsName", this.GetName());
      nameParameter.ParameterName = "@ResturantName";
      nameParameter.Value = this.GetName();

      SqlParameter newDateParameter = new SqlParameter("@ResturantNewDate", this.GetNewDate());
      newDateParameter.ParameterName = "@ResturantNewDate";
      newDateParameter.Value = this.GetNewDate();

      SqlParameter locationParameter = new SqlParameter("@ResturantLocation", this.GetLocation());
      locationParameter.ParameterName = "@ResturantLocation";
      locationParameter.Value = this.GetLocation();

      SqlParameter cuisineIdParameter = new SqlParameter();
      cuisineIdParameter.ParameterName = "@ResturantCuisineId";
      cuisineIdParameter.Value = this.GetCuisineId();

      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(newDateParameter);
      cmd.Parameters.Add(locationParameter);
      cmd.Parameters.Add(cuisineIdParameter);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    //GetAll Method
    public static List<Resturant> GetAll()
    {
      List<Resturant> allResturants = new List<Resturant>{};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM resturants;", conn);  // ORDER BY new_date cut out of SqlCommand
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int ResturantId = rdr.GetInt32(0);
        string ResturantName = rdr.GetString(1);
        DateTime ResturantDateTime = rdr.GetDateTime(2);
        string ResturantLocation = rdr.GetString(3);
        int ResturantCuisineId = rdr.GetInt32(4);
        Resturant newResturant = new Resturant(ResturantName, ResturantLocation, ResturantCuisineId, ResturantDateTime, ResturantId);
        allResturants.Add(newResturant);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allResturants;
    }

    //Find Method
    public static Resturant Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM Resturant WHERE id = @ResturantId;", conn);
      SqlParameter resturantIdParameter = new SqlParameter();
      resturantIdParameter.ParameterName = "@ResturantId";
      resturantIdParameter.Value = id.ToString();
      cmd.Parameters.Add(resturantIdParameter);
      rdr = cmd.ExecuteReader();

      int foundResturantId = 0;
      string foundResturantName = null;
      DateTime foundResturantDateTime = new DateTime(2099, 12, 31);
      string foundResturantLocation = null;
      int foundCuisineId = 0;
      while(rdr.Read())
      {
        foundResturantId = rdr.GetInt32(0);
        foundResturantName = rdr.GetString(1);
        foundResturantDateTime = rdr.GetDateTime(2);
        foundResturantLocation = rdr.GetString(3);
        foundCuisineId = rdr.GetInt32(4);
      }
      Resturant foundResturant = new Resturant(foundResturantName, foundResturantLocation, foundCuisineId, foundResturantDateTime, foundResturantId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return foundResturant;
    }

    //DeleteAll Method
    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM resturants;", conn);
      cmd.ExecuteNonQuery();
    }

  }
}
