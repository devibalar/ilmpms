using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Class1
/// </summary>
public interface IStudentDao
{
    Boolean AddStudent(StudentVO inStudent);
    Boolean UpdateStudent(StudentVO inStudent);
    Boolean DeleteStudent(StudentVO inStudent);
    List<StudentVO> SearchStudent();
    string GetStudentNameForId(int studentid);
    List<string> GetAllStudentIDAsList();
}