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

    [Fact]
    public void Find_RetrievesCourseFromDB_()
    {
      Course newCourse = new Course("Math", "MTH101");
      newCourse.Save();

      Course result = Course.Find(newCourse.GetId());

      Assert.Equal(newCourse, result);
    }

    [Fact]
    public void UpdateName_UpdateNameInDB_true()
    {
      string name = "Intro to Programming";
      Course testCourse = new Course(name, "PROG101");
      testCourse.Save();

      string newName = "Intro to Computers";

      testCourse.UpdateName(newName);
      string result = testCourse.GetName();

      Assert.Equal(newName, result);
    }

    [Fact]
    public void Delete_DeletesCourseFromDB_true()
    {
      Course course1 = new Course("Math Party", "MTH505");
      course1.Save();
      Course course2 = new Course("CS Party", "CS502");
      course2.Save();
      List<Course> expectedResult = new List<Course>{course2};

      course1.Delete();
      List<Course> result = Course.GetAll();

      Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void AddStudent_AddsStudentToCourse_true()
    {
      Course newCourse = new Course("Intro", "CS101");
      Student student1 = new Student("James", DateTime.Today);
      Student student2 = new Student("Sarah", DateTime.Today);
      Student student3 = new Student("Grace", DateTime.Today);
      newCourse.Save();
      student1.Save();
      student2.Save();
      student3.Save();
      List<Student> expectedList = new List<Student> {student1, student3};

      newCourse.AddStudent(student1.GetId());
      newCourse.AddStudent(student3.GetId());
      List<Student> result = newCourse.GetAllStudents();

      Assert.Equal(expectedList, result);
    }
  }
}
