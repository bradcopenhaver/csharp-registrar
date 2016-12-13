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
    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public DateTime GetEnrollment()
    {
      return _enrollment;
    }

    public override bool Equals(Object otherStudent)
    {
      if(!(otherStudent is Student))
      {
        return false;
      }
      else
      {
        Student newStudent = (Student) otherStudent;
        bool idEquality = (this._id == newStudent.GetId());
        bool nameEquality = (this._name == newStudent.GetName());
        bool enrollmentEquality = (this._enrollment == newStudent.GetEnrollment());
        return (idEquality && nameEquality && enrollmentEquality);
      }
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM students", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }

    public static List<Student> GetAll()
    {
      List<Student> allStudents= new List<Student>{};
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM students;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        DateTime enrollment = rdr.GetDateTime(2);

        Student newStudent = new Student(name, enrollment, id);
        allStudents.Add(newStudent);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allStudents;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO students (name, enrollment) OUTPUT INSERTED.id VALUES (@Name, @Enrollment);", conn);

      cmd.Parameters.AddWithValue("@Name", _name);
      cmd.Parameters.AddWithValue("@Enrollment", _enrollment);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        _id = rdr.GetInt32(0);
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

    public void UpdateName(string newName)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand ("UPDATE students SET name = @NewName OUTPUT INSERTED.name WHERE id = @StudentId;", conn);

      cmd.Parameters.AddWithValue("@NewName", newName);
      cmd.Parameters.AddWithValue("@StudentId", _id);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._name = rdr.GetString(0);
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

    public static Student Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM students WHERE id = @StudentId;", conn);
      cmd.Parameters.AddWithValue("@StudentId", id);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundId = 0;
      string name = "";
      DateTime enrollment = DateTime.Today;

      while(rdr.Read())
      {
        foundId = rdr.GetInt32(0);
        name = rdr.GetString(1);
        enrollment = rdr.GetDateTime(2);
      }
      Student foundStudent = new Student(name, enrollment, foundId);
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundStudent;
    }

    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM students WHERE id = @StudentId;", conn);
      cmd.Parameters.AddWithValue("@StudentId", _id);
      cmd.ExecuteNonQuery();
      if (conn != null)
      {
        conn.Close();
      }
    }
  }
}
