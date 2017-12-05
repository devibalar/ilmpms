using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TemplateCourseVO
/// </summary>
public class TemplateCourseVO
{
    private int templateCourseId;
    private string courseCode;
    private string courseType;
    private int semester;
    private int year;
    private int workshopId;
    public TemplateCourseVO()
    {
    }
    public TemplateCourseVO(string courseCode, string courseType,  int workshopId)
    {
        this.courseCode = courseCode;
        this.courseType = courseType;
        this.workshopId = workshopId;
    }
    public TemplateCourseVO( string courseCode, string courseType)
    {
        this.courseCode = courseCode;
        this.courseType = courseType;       
    }
    public TemplateCourseVO(string courseCode,string courseType,int semester, int year, int workshopId)
    {
        this.courseCode = courseCode;
        this.courseType = courseType;
        this.semester = semester;
        this.year = year;
        this.workshopId = workshopId;
    }
    public int TemplateCourseId
    {
        get { return templateCourseId; }
        set { templateCourseId = value; }
    }
    public string CourseCode
    {
        get { return courseCode; }
        set { courseCode = value; }
    }
    public int WorkshopId
    {
        get { return workshopId; }
        set { workshopId = value; }
    }
    public string CourseType
    {
        get { return courseType; }
        set { courseType = value; }
    }
    public int Semester
    {
        get { return semester; }
        set { semester = value; }
    }
    public int Year
    {
        get { return year; }
        set { year = value; }
    }
}