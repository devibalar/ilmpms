using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for StudentBO
/// </summary>
public class StudentBO
{
    IStudentDao studentDao = new StudentDaoImpl();
	public StudentBO()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public Boolean AddStudent(StudentVO instudent)
    {
        Boolean status=false;
        try
        {
            status = studentDao.AddStudent(instudent);
        }
        catch (CustomException e)
        {
            throw e;
        }
        return status;
    }

    public Boolean UpdateStudent(StudentVO instudent)
    { 
        Boolean status=false;
        try
        {
            status = studentDao.UpdateStudent(instudent);
        }
        catch (CustomException e)
        {
            throw e;
        }
        return status;
    }

    public Boolean DeleteStudent(StudentVO instudent)
    {
        Boolean status=false;
        try
        {
            status = studentDao.DeleteStudent(instudent);
        }
        catch (CustomException e)
        {
            throw e;
        }
        return status;
    }
   
    public string GetStudentNameForId(int studentid)
    {
        string studentName = "";
        try{
            studentName = studentDao.GetStudentNameForId(studentid);
        }
        catch (CustomException e)
        {
            throw e;
        }
        return studentName;
    }

    public List<string> GetAllStudentIDAsList()
    {
        List<string> studentIds = new List<string>();
        try
        {
            studentIds = studentDao.GetAllStudentIDAsList();
        }
        catch (CustomException e)
        {
            throw e;
        }
        return studentIds;
    }
}