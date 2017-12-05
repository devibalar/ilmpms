using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for IStudentMajorDao
/// </summary>
public interface IStudentMajorDao
{
    Boolean AddStudentProgramme(StudentMajorVO inStudentMajor);
    Boolean UpdateStudentProgramme(StudentMajorVO inStudentMajor);
    Boolean DeleteStudentProgramme(StudentMajorVO inStudentMajor);
    StudentMajorVO GetStudentMajor(int studentId);
    List<StudentMajorVO> GetStudentMajorList(int studentId);
}