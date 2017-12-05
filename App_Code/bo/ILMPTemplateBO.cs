using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ILMPTemplateBO
/// </summary>
public class ILMPTemplateBO
{
    IILMPTemplateDao ilmpTemplateDao = new ILMPTemplateDaoImpl();
    public string AddILMPTemplate(ILMPTemplateVO inIlmpTemplateVO)
    {
        string status = "";
        try
        {
            status = ilmpTemplateDao.AddILMPTemplate(inIlmpTemplateVO);
        }
        catch (CustomException e)
        {
            throw e;
        }       
        return status;
    }
    public DataSet GetAllProgrammeInTemplate()
    {
        DataSet ds;
        try{
        ds= ilmpTemplateDao.GetAllProgrammeInTemplate();
        }
        catch (CustomException e)
        {
            throw e;
        }
        return ds;
    }
    public DataSet GetAllMajorForProgramme(string programmeId)
    {
        DataSet ds;
        try
        {
            ds= ilmpTemplateDao.GetAllMajorForProgramme(programmeId);
        }
        catch (CustomException e)
        {
            throw e;
        }
        return ds;
    }
    public DataSet GetAllTemplateName(string programmeId, string majorId, int studentId)
    {
        DataSet ds;
        try{
            ds= ilmpTemplateDao.GetAllTemplateName(programmeId,majorId,studentId);
        }
        catch (CustomException e)
        {
            throw e;
        }
        return ds;
    }
    public ILMPTemplateVO GetTemplate(ILMPTemplateVO inilmptemplateVO)
    {
        ILMPTemplateVO ilmpTemplateVO;
        try
        {
            ilmpTemplateVO= ilmpTemplateDao.GetTemplate(inilmptemplateVO);
        }
        catch (CustomException e)
        {
            throw e;
        }
        return ilmpTemplateVO;
    }
    public ILMPTemplateVO GetCutomTemplate(ILMPTemplateVO inilmptemplateVO)
    {
        ILMPTemplateVO ilmpTemplateVO;
        try
        {
            ilmpTemplateVO = ilmpTemplateDao.GetCutomTemplate(inilmptemplateVO);
        }
        catch (CustomException e)
        {
            throw e;
        }
        return ilmpTemplateVO;
    }
    public ILMPTemplateVO GetTemplateForId(int templateId)
    {
        ILMPTemplateVO ilmpTemplateVO = new ILMPTemplateVO();
        try
        {
            ilmpTemplateVO = ilmpTemplateDao.GetTemplateForId(templateId);
        }
        catch (CustomException e)
        {
            throw e;
        }        
        return ilmpTemplateVO;
    }
    public DataSet GetAllTemplateNameForStudent(int studentId)
    {
        DataSet ds;
        try
        {
            ds = ilmpTemplateDao.GetAllTemplateNameForStudent(studentId);
        }
        catch (CustomException e)
        {
            throw e;
        }
        return ds;
    }
}