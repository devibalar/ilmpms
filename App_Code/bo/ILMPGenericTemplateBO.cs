using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ILMPGenericTemplate
/// </summary>
public class ILMPGenericTemplateBO
{
    IILMPGenericTemplateDao ilmpGenericTemplateDao = new ILMPGenericTemplateDaoImpl();

    public string AddILMPTemplate(ILMPGenericTemplateVO inIlmpGenericTemplateVO)
    {
        string status ="";
        try{
            status= ilmpGenericTemplateDao.AddILMPTemplate(inIlmpGenericTemplateVO);
        }
        catch (CustomException e)
        {
            throw e;
        }
        return status;
    }

    public DataSet GetTitleForProgrammeMajor(string programmeCode, string major)
    {
        DataSet ds;
        try{
            ds= ilmpGenericTemplateDao.GetTitleForProgrammeMajor(programmeCode, major);
        }
        catch (CustomException e)
        {
            throw e;
        }
        return ds;
    }

    public ILMPGenericTemplateVO GetGenericTemplate(string programme, string major, string title)
    {
        ILMPGenericTemplateVO ilmpGenericTemplateVO;
        try
        {
        ilmpGenericTemplateVO= ilmpGenericTemplateDao.GetGenericTemplate(programme,major,title);
        }
        catch (CustomException e)
        {
            throw e;
        }
        return ilmpGenericTemplateVO;
    }
}