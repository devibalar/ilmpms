using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ILMPCourseGridVO
/// </summary>
public class ILMPCourseGridVO
{
    private string courseCode;
    private string courseType;
    private string title;
    private int credits;
    private int level;
    private string prerequisites;
    private int semester;
    private int year;
    private string result;

    public string Result
    {
        get { return result; }
        set { result = value; }
    }
	public ILMPCourseGridVO()
	{
	}
    public ILMPCourseGridVO(string courseCode, string courseType, string title, int credits, int level, string prerequisites)
    {
        this.courseCode = courseCode;
        this.courseType = courseType;
        this.title = title;
        this.credits = credits;
        this.level = level;
        this.prerequisites = prerequisites;
    }
    public ILMPCourseGridVO(string courseCode, string courseType, string title, int credits, int level, string prerequisites, int semester, int year)
    {
        this.courseCode = courseCode;
        this.courseType = courseType;
        this.title = title;
        this.credits = credits;
        this.level = level;
        this.prerequisites = prerequisites;
        this.semester = semester;
        this.year = year;
    }
    public string CourseCode
    {
        get { return courseCode; }
        set { courseCode = value; }
    }
    public string CourseType
    {
        get { return courseType; }
        set { courseType = value; }
    }
    public string Title
    {
        get { return title; }
        set { title = value; }
    }
    public int Credits
    {
        get { return credits; }
        set { credits = value; }
    }
    public int Level
    {
        get { return level; }
        set { level = value; }
    }
    public string Prerequisites
    {
        get { return prerequisites; }
        set { prerequisites = value; }
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