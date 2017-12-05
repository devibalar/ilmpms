using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ILMPGenericTemplateCourse
/// </summary>
public class ILMPGenericTemplateCourseVO
{
  //  private int templateID;
    private int semester;
    private int courseCount;

    public ILMPGenericTemplateCourseVO()
    {
    }

	public ILMPGenericTemplateCourseVO(int semester, int courseCount)
	{
        //this.templateID = templateID;
        this.semester = semester;
        this.courseCount = courseCount;
	}

  /*  public int TemplateID
    {
        get { return templateID; }
        set { templateID = value; }
    }*/

    public int Semester
    {
        get { return semester; }
        set { semester = value; }
    }

    public int CourseCount
    {
        get { return courseCount; }
        set { courseCount = value; }
    }
}