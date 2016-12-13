using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace Registrar.Objects
{
  public class Student
  {
    private int _id;
    private string _name;
    private DateTime _enrollment;

    public Student(string name, DateTime enrollment, int id=0)
    {
      _id = id;
      _name = name;
      _enrollment = enrollment;
    }

    public void DeleteAll();
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM students", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
