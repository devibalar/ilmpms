using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ILMPBO
/// </summary>
public class ILMPBO
{
    IILMPDao ilmpDao = new ILMPDaoImpl();

    public string AddILMP(ILMPVO inilmpVO)
    {
        string status ="";
        try
        {
            status = ilmpDao.AddILMP(inilmpVO);
           
        }
        catch (CustomException e)
        {
            throw e;
        }
        return status;
    }
    public List<ILMPCourseGridVO> GetILMPCoursesForId(int ilmpId)
    {
        List<ILMPCourseGridVO> ilmpCourses = new List<ILMPCourseGridVO>();
        try
        {
            ilmpCourses = ilmpDao.GetILMPCoursesForId(ilmpId);
        }
        catch (CustomException e)
        {
            throw e;
        }
        return ilmpCourses;
    }
    public ILMPVO GetILMPDetailsForId(int ilmpId)
    {
         ILMPVO ilmp = new ILMPVO();
         try
         {
             ilmp = ilmpDao.GetILMPDetailsForId(ilmpId);
         }
         catch (CustomException e)
         {
             throw e;
         }
         return ilmp;
    }
    public ILMPVO GetILMPCoursesWorkshopForId(int ilmpId)
    {
        ILMPVO ilmp = new ILMPVO();
        try
        {
            ilmp = ilmpDao.GetILMPCoursesWorkshopForId(ilmpId);
        }
        catch (CustomException e)
        {
            throw e;
        }
        return ilmp;
    }
    public List<WorkshopVO> GetILMPWorkshopForId(int ilmpId)
    {
        List<WorkshopVO> workshopNames = new List<WorkshopVO>();
        try
        {
            workshopNames = ilmpDao.GetILMPWorkshopForId(ilmpId);
        }
        catch (CustomException e)
        {
            throw e;
        }
        return workshopNames;
    }
    public List<ILMPVO> GetILMPListForStudentId(int studentId)
    {
        List<ILMPVO> ilmps = new List<ILMPVO>();
        try
        {
            ilmps = ilmpDao.GetILMPListForStudentId(studentId);
        }
        catch (CustomException e)
        {
            throw e;
        }
        return ilmps;
    }
    public Boolean UpdateILMPStatus(int ilmpId, string isActive)
    {
        Boolean status = false;
        try
        {
            status = ilmpDao.UpdateILMPStatus(ilmpId,isActive);
        }
        catch (CustomException e)
        {
            throw e;
        }
        return status;
    }
    public ILMPDetailsVO GetTemplateProgrammeForIlmpId(int ilmpId)
    {
         ILMPDetailsVO ilmpDetails = new ILMPDetailsVO();
         try
         {
             ilmpDetails = ilmpDao.GetTemplateProgrammeForIlmpId(ilmpId);
         }
         catch (CustomException e)
         {
             throw e;
         }
         return ilmpDetails;
    }
    public Boolean UpdateILMP(ILMPVO inilmpVO)
    {
        Boolean status=false;
        try
        {
            status = ilmpDao.UpdateILMP(inilmpVO);
            if (inilmpVO.Active == "Yes")
            {
                ilmpDao.UpdateILMPStatusForStudent(inilmpVO.StudentId, inilmpVO.IlmpId);
            }
        }
        catch (CustomException e)
        {
            throw e;
        }
       
        return status;
    }
    public Boolean UpdateILMPStatusForStudent(int studentId, string isActive)
    {
        Boolean status = false;
        try
        {
            status = ilmpDao.UpdateILMPStatusForStudent(studentId,isActive);
        }
        catch (CustomException e)
        {
            throw e;
        }       
        return status;
    }
    public int GetActiveIlmpForStudent(int studentId)
    {
        int ilmpId = 0;
        try
        {
            ilmpId = ilmpDao.GetActiveIlmpForStudent(studentId);
        }
        catch (CustomException e)
        {
            throw e;
        }
        
        return ilmpId;
    }
    public Boolean UpdateILMPStatusForStudent(int studentId, int ilmpId)
    {
        Boolean status = false;
        try
        {
            status = ilmpDao.UpdateILMPStatusForStudent(studentId,ilmpId);
        }
        catch (CustomException e)
        {
            throw e;
        }        
        return status;
    }
}