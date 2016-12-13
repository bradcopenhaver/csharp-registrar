using Nancy;
using System.Collections.Generic;
using System;
using Registrar.Objects;
using Nancy.ViewEngines.Razor;

namespace Registrar
{
	public class HomeModule : NancyModule
	{
		public HomeModule()
    {
      Get["/"] = _ => {
        return View["index.cshtml"];
      };
      Get["/course/new"] = _ => {
        return View["new-course-form.cshtml"];
      };
      Post["/course/new"] = _ => {
        Course newCourse = new Course(Request.Form["courseName"], Request.Form["courseNumber"]);
        newCourse.Save();
        List<Course> allCourses = Course.GetAll();
        return View["courses.cshtml", allCourses];
      };
      Get["/courses"] = _ => {
        List<Course> allCourses = Course.GetAll();
        return View["courses.cshtml", allCourses];
      };
      Get["/student/new"] = _ => {
        return View["new-student-form.cshtml"];
      };
      Post["/student/new"] = _ => {
        Student newStudent = new Student(Request.Form["studentName"], Request.Form["enrollmentDate"]);
        newStudent.Save();
        List<Student> allStudents = Student.GetAll();
        return View["students.cshtml", allStudents];
      };
      Get["/course/{id}"] = parameters => {
        Course currentCourse = Course.Find(parameters.id);
        Dictionary<string, object> model = new Dictionary<string, object>();
        List<Student> allStudents = Student.GetAll();
        List<Student> courseStudents = currentCourse.GetAllStudents();
        model.Add("course", currentCourse);
        model.Add("allStudents", allStudents);
        model.Add("courseStudents", courseStudents);
        return View["course.cshtml", model];
      };
      Patch["/course/update/{id}"] = parameters => {
        Course currentCourse = Course.Find(parameters.id);
        currentCourse.UpdateName(Request.Form["newName"]);
        Dictionary<string, object> model = new Dictionary<string, object>();
        List<Student> allStudents = Student.GetAll();
        List<Student> courseStudents = currentCourse.GetAllStudents();
        model.Add("course", currentCourse);
        model.Add("allStudents", allStudents);
        model.Add("courseStudents", courseStudents);
        return View["course.cshtml", model];
      };
      Delete["/course/delete/{id}"] = parameters => {
        Course currentCourse = Course.Find(parameters.id);
        currentCourse.Delete();
        List<Course> allCourses = Course.GetAll();
        return View["courses.cshtml", allCourses];
      };
      Post["/course/add_student"] = _ => {
        Course currentCourse = Course.Find(Request.Form["courseId"]);
        currentCourse.AddStudent(Request.Form["studentId"]);

        Dictionary<string, object> model = new Dictionary<string, object>();
        List<Student> allStudents = Student.GetAll();
        List<Student> courseStudents = currentCourse.GetAllStudents();
        model.Add("course", currentCourse);
        model.Add("allStudents", allStudents);
        model.Add("courseStudents", courseStudents);
        return View["course.cshtml", model];
      };
      Get["/student/{id}"] = parameters => {
        Student currentStudent = Student.Find(parameters.id);
        Dictionary<string, object> model = new Dictionary<string, object>();
        List<Course> allCourses = Course.GetAll();
        List<Course> studentCourses = currentStudent.GetAllCourses();
        model.Add("student", currentStudent);
        model.Add("allCourses", allCourses);
        model.Add("studentCourses", studentCourses);
        return View["student.cshtml", model];
      };
      Post["/student/add_course"] = _ => {
        Student currentStudent = Student.Find(Request.Form["studentId"]);
        currentStudent.AddCourse(Request.Form["courseId"]);
        Dictionary<string, object> model = new Dictionary<string, object>();
        List<Course> allCourses = Course.GetAll();
        List<Course> studentCourses = currentStudent.GetAllCourses();
        model.Add("student", currentStudent);
        model.Add("allCourses", allCourses);
        model.Add("studentCourses", studentCourses);
        return View["student.cshtml", model];
      };
      Delete["/student/delete/{id}"] = parameters => {
        Student currentStudent = Student.Find(parameters.id);
        currentStudent.Delete();
        List<Student> allStudents = Student.GetAll();
        return View["students.cshtml", allStudents];
      };
      Get["/students"] = _ => {
        List<Student> allStudents = Student.GetAll();
        return View["students.cshtml", allStudents];
      };
      Patch["/student/update/{id}"] = parameters => {
        Student currentStudent = Student.Find(parameters.id);
        currentStudent.UpdateName(Request.Form["newName"]);
        Dictionary<string, object> model = new Dictionary<string, object>();
        List<Course> allCourses = Course.GetAll();
        List<Course> studentCourses = currentStudent.GetAllCourses();
        model.Add("student", currentStudent);
        model.Add("allCourses", allCourses);
        model.Add("studentCourses", studentCourses);
        return View["student.cshtml", model];
      };
    }
  }
}
