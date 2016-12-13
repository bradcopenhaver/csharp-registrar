using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Registrar.Objects
{
  public class StudentTest : IDisposable
  {
    public StudentTest()
    {
      DBConfiguration.ConnectionString = "Data Source =(localdb)\\mssqllocaldb;Initial Catalog=registrar_test;Integrated Security =SSPI;";
    }
    public void Dispose()
    {
      Student.DeleteAll();
    }

    [Fact]
    public void GetAll_StartsWithEmptyDB_true()
    {
      //Arrange
      //Act
      List<Student> allStudents = Student.GetAll();
      //Assert
      Assert.Equal(0, allStudents.Count);
    }

    [Fact]
    public void Equals_EqualsOverrideComparesObjects_true()
    {
      Student student1 = new Student("Joe", DateTime.Today);
      Student student2 = new Student("Joe", DateTime.Today);

      Assert.Equal(student1, student2);
    }

    [Fact]
    public void Save_SavesToDatabase_true()
    {
      Student newStudent = new Student("Harry", DateTime.Today);
      List<Student> testList = new List<Student>{newStudent};

      newStudent.Save();
      List<Student> result = Student.GetAll();

      Assert.Equal(result, testList);
    }

    [Fact]
    public void Find_RetrievesStudentFromDB_()
    {
      Student newStudent = new Student("Fran", DateTime.Today);
      newStudent.Save();

      Student result = Student.Find(newStudent.GetId());

      Assert.Equal(newStudent, result);
    }

    [Fact]
    public void UpdateName_UpdateNameInDB_true()
    {
      string name = "Jesse";
      Student testStudent = new Student(name, DateTime.Today);
      testStudent.Save();

      string newName = "Jessica";

      testStudent.UpdateName(newName);
      string result = testStudent.GetName();

      Assert.Equal(newName, result);
    }

    [Fact]
    public void Delete_DeletesStudentFromDB_true()
    {
      Student student1 = new Student("Henrietta", DateTime.Today);
      student1.Save();
      Student student2 = new Student("Henry", DateTime.Today);
      student2.Save();
      List<Student> expectedResult = new List<Student>{student2};

      student1.Delete();
      List<Student> result = Student.GetAll();

      Assert.Equal(expectedResult, result);
    }
  }
}
