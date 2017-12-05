using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for IILMPGenericTemplateDao
/// </summary>
public interface IILMPGenericTemplateDao
{
    string AddILMPTemplate(ILMPGenericTemplateVO inIlmpGenericTemplateVO);
    DataSet GetTitleForProgrammeMajor(string programmeCode, string major);
    ILMPGenericTemplateVO GetGenericTemplate(string programme, string major, string title);
}