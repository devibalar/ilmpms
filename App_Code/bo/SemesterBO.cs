using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SemesterBO
/// </summary>
public class SemesterBO
{
    ISemesterDao semesterobj = new SemesterDaoImpl();
	public SemesterBO()
	{
		
	}

    public DataSet getAllSemesters()
    {
        DataSet dsSemesters;
        try{
            dsSemesters= semesterobj.getAllSemesters();
        }
        catch (CustomException e)
        {
            throw e;
        }
        return dsSemesters;
    }
}