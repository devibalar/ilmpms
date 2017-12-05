using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CourseOfferingVO
/// </summary>
public class CourseOfferingVO
{
    private string courseCode;
    private int semester;
    private int year;   
    private DateTime createdTime;

    public CourseOfferingVO()
    {
    }
    public CourseOfferingVO(string courseCode)
    {
        this.courseCode = courseCode;
    }
    public CourseOfferingVO(string courseCode, int semester, int year)
    {
        this.courseCode = courseCode;
        this.semester = semester;
        this.year = year;
    }
    public CourseOfferingVO(string courseCode, int semester, int year,  DateTime createdTime)
    {
        this.courseCode = courseCode;
        this.semester = semester;
        this.year = year;
        this.createdTime = createdTime;
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
    public DateTime CreatedTime
    {
        get { return createdTime; }
        set { createdTime = value; }
    }

	
}