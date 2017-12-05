using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ILMPCustomTemplateBO
/// </summary>
public class ILMPCustomTemplateBO
{
    IILMPTemplateDao ilmpTemplateObj = new ILMPTemplateDaoImpl();

    public string AddILMPTemplate(ILMPVO inIlmpVO)
    {
        string status = "";//ilmpTemplateObj.AddILMPTemplate(inIlmpVO);
        return status;
    }
}