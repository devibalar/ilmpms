using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CourseVO
/// </summary>
public class CourseVO
{
    private string courseCode;
    private string title;
    private int credits;
    private int level;
    private string offeredFrequency;    
    private string active;    
    private DateTime createdTime;    
    private DateTime updatedTime;
    private CoursePrerequisiteVO prequisites = new CoursePrerequisiteVO();
    private List<CourseProgrammeVO> program = new List<CourseProgrammeVO>();

    public CourseVO()
    {
    }
    public CourseVO(string courseCode)
    {
        this.courseCode = courseCode;
    }

    public CourseVO(string courseCode, string title, int credits, int level, string offeredFrequency, string active, DateTime updatedDateTime)
    {
        this.courseCode = courseCode;
        this.title = title;
        this.credits = credits;
        this.level = level;
        this.offeredFrequency = offeredFrequency;
        this.active = active;
        this.updatedTime = updatedDateTime;
    }

    public CourseVO(string courseCode, string title, int credits, int level, string offeredFrequency, string active,
        DateTime createdDateTime, DateTime updatedDateTime)
    {
        this.courseCode = courseCode;
        this.title = title;
        this.credits = credits;
        this.level = level;
        this.offeredFrequency = offeredFrequency;
        this.active = active;
        this.updatedTime = updatedDateTime;
    }

    public string CourseCode
    {
        get { return courseCode; }
        set { courseCode = value; }
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
    public string OfferedFrequency
    {
        get { return offeredFrequency; }
        set { offeredFrequency = value; }
    }
    public string Active
    {
        get { return active; }
        set { active = value; }
    }
    public DateTime CreatedTime
    {
        get { return createdTime; }
        set { createdTime = value; }
    }
    public DateTime UpdatedTime
    {
        get { return updatedTime; }
        set { updatedTime = value; }
    }
    public CoursePrerequisiteVO Prerequisites
    {
        get { return prequisites; }
        set { prequisites = value; }
    }
    public List<CourseProgrammeVO> Program
    {
        get { return program; }
        set { program = value; }
    }
}