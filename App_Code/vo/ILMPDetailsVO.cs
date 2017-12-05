using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ILMPDetailsVO
/// </summary>
public class ILMPDetailsVO
{
    private int ilmpId;
    private int templateId;
    private string templateName;
    private string programmeId;
    private string majorId;
    private int studentId;
    public ILMPDetailsVO()
    {
    }
    public ILMPDetailsVO(int ilmpId, int templateId, string templateName, string programmeId, string majorId, int studentId)
    {
        this.ilmpId = ilmpId;
        this.templateId = templateId;
        this.templateName = templateName;
        this.programmeId = programmeId;
        this.majorId = majorId;
        this.studentId = studentId;
    }
    public int IlmpId
    {
        get { return ilmpId; }
        set { ilmpId = value; }
    }
    public int TemplateId
    {
        get { return templateId; }
        set { templateId = value; }
    }
    public string TemplateName
    {
        get { return templateName; }
        set { templateName = value; }
    }
    public string ProgrammeId
    {
        get { return programmeId; }
        set { programmeId = value; }
    }
    public string MajorId
    {
        get { return majorId; }
        set { majorId = value; }
    }
    public int StudentId
    {
        get { return studentId; }
        set { studentId = value; }
    }	
}