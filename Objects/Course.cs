using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace Registrar.Objects
{
  public class Course
  {
    private int _id;
    private string _name;
    private string _number;

    public Course(string name, string number, int id=0)
    {
      _id = id;
      _name = name;
      _number = number;
    }

    public void DeleteAll();
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM courses", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
