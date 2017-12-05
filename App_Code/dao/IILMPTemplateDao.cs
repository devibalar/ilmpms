using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for IIlmpTemplateDao
/// </summary>
public interface IILMPTemplateDao
{
    string AddILMPTemplate(ILMPTemplateVO inIlmpVO);
    DataSet GetAllProgrammeInTemplate();
    DataSet GetAllMajorForProgramme(string programmeId);
    DataSet GetAllTemplateName(string programmeId, string majorId, int studentId);
    ILMPTemplateVO GetTemplate(ILMPTemplateVO inilmptemplateVO);
    ILMPTemplateVO GetTemplateForId(int templateId);
    DataSet GetAllTemplateNameForStudent(int studentId);
    ILMPTemplateVO GetCutomTemplate(ILMPTemplateVO inilmptemplateVO);
}