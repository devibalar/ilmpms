using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ILMPCourseVO
/// </summary>
public class ILMPCourseVO
{
    private string courseCode;
    private int semester;
    private int year;
    private string result;
    private string courseType;

    public ILMPCourseVO()
    {        
    }

    public ILMPCourseVO(string courseCode, int semester, int year, string result)
    {
        this.courseCode = courseCode;
        this.semester = semester;
        this.year = year;
        this.result = result;
    }
    public string CourseCode
    {
        get { return courseCode; }
        set { courseCode = value; }
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
    public string Result
    {
        get { return result; }
        set { result = value; }
    }
    public string CourseType
    {
        get { return courseType; }
        set { courseType = value; }
    }
}