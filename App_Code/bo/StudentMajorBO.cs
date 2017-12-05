using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for StudentMajorBO
/// </summary>
public class StudentMajorBO
{
    IStudentMajorDao studentMajorDao = new StudentMajorDaoImpl();

    public Boolean AddStudent(StudentMajorVO inStudentMajor)
    {        
        Boolean studentId =false;
        try{
            studentId= studentMajorDao.AddStudentProgramme(inStudentMajor);
        }
        catch (CustomException e)
        {
            throw e;
        }
        return true;
    }

    public Boolean UpdateStudent(StudentMajorVO inStudentMajor)
    {
        Boolean studentId = false;
        try
        {
            studentId = studentMajorDao.UpdateStudentProgramme(inStudentMajor);
        }
        catch (CustomException e)
        {
            throw e;
        }
        return true;
    }

    public Boolean DeleteStudent(StudentMajorVO inStudentMajor)
    {
        Boolean studentId = false;
        try
        {
            studentId = studentMajorDao.DeleteStudentProgramme(inStudentMajor);
        }
        catch (CustomException e)
        {
            throw e;
        }
        return true;
    }
    public StudentMajorVO GetStudentMajor(int studentId)
    {
        StudentMajorVO studentMajorVO = new StudentMajorVO();
        try
        {
            studentMajorVO = studentMajorDao.GetStudentMajor(studentId);
        }
        catch (SqlException e)
        {
            throw e;
        }
        return studentMajorVO;
    }
    public List<StudentMajorVO> GetStudentMajorList(int studentId)
    {
        List<StudentMajorVO> studentMajorList = new List<StudentMajorVO>();
        try
        {
            studentMajorList = studentMajorDao.GetStudentMajorList(studentId);
        }
        catch (SqlException e)
        {
            throw e;
        }
        return studentMajorList;
    }
}