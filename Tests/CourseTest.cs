using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Registrar.Objects
{
  public class CourseTest : IDisposable
  {
    public CourseTest()
    {
      DBConfiguration.ConnectionString = "Data Source =(localdb)\\mssqllocaldb;Initial Catalog=registrar_test;Integrated Security =SSPI;";
    }
    public void Dispose()
    {
      Course.DeleteAll();
    }

    [Fact]
    public void GetAll_StartsWithEmptyDB_true()
    {
      //Arrange
      //Act
      List<Course> allCourses = Course.GetAll();
      //Assert
      Assert.Equal(0, allCourses.Count);
    }

    [Fact]
    public void Equals_EqualsOverrideComparesObjects_true()
    {
      Course course1 = new Course("Intro", "CS101");
      Course course2 = new Course("Intro", "CS101");

      Assert.Equal(course1, course2);
    }
    [Fact]
    public void Save_SavesToDatabase_true()
    {
      Course newCourse = new Course("Intro to Programming", "PROG101");
      List<Course> testList = new List<Course>{newCourse};

      newCourse.Save();
      List<Course> result = Course.GetAll();

      Assert.Equal(result, testList);
    }
  }
}
