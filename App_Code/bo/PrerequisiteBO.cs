using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PrerequisiteBO
/// </summary>
public class PrerequisiteBO
{
    IPrerequisiteDao prerequisiteDao = new PrerequisiteDaoImpl();
    public PrerequisiteBO()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public Boolean AddPrerequisite(PrerequisiteVO inPrerequisite)
    {
        // Add business validations if any
        Boolean prerequisiteCode =false;
        try
        {
            prerequisiteCode= prerequisiteDao.AddPrerequisite(inPrerequisite);
        }
        catch (CustomException e)
        {
            throw e;
        }
        return true;
    }
    public Boolean UpdatePrerequisite(PrerequisiteVO inPrerequisite)
    {
        // Add business validations if any
        Boolean prerequisiteCode =false;
        try{
            prerequisiteCode= prerequisiteDao.UpdatePrerequisite(inPrerequisite);
        }
        catch (CustomException e)
        {
            throw e;
        }
        return true;
    }
    public Boolean DeletePrerequisite(PrerequisiteVO inPrerequisite)
    {
        Boolean prerequisiteCode = false;
        try
        {
         prerequisiteCode = prerequisiteDao.DeletePrerequisite(inPrerequisite);
        }
        catch (CustomException e)
        {
            throw e;
        }
        return true;
    }
    public DataSet GetAllPrerequisites()
    {
        DataSet ds = new DataSet();
        try
        {
            ds = prerequisiteDao.GetAllPrerequisites();
        }
        catch (CustomException e)
        {
            throw e;
        }       
        return ds;
    }
    public DataSet GetAllCorequisites()
    {
        DataSet ds = new DataSet();
        try
        {
            ds = prerequisiteDao.GetAllCorequisites();
        }
        catch (CustomException e)
        {
            throw e;
        }       
        return ds;
    }
    public string GetAllPrerequisiteForCourseCode(string courseCode)
    {
         string allprerequisite = "";
         try
         {
             allprerequisite = prerequisiteDao.GetAllPrerequisiteForCourseCode(courseCode);
         }
         catch (CustomException e)
         {
             throw e;
         }
         return allprerequisite;
    }
}