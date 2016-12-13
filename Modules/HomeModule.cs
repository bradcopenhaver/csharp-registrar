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
    }
  }
}
