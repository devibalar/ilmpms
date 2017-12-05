using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CourseProgrammeBO
/// </summary>
public class CourseProgrammeBO
{
    ICourseProgrammeDao courseProgrammeDao = new CourseProgrammeDaoImpl();
    public List<CourseProgrammeVO> GetCourseProgrammeForCourseCode(string incourseCode)
    {
        List<CourseProgrammeVO> courseProgrammeList = new List<CourseProgrammeVO>();
        try
        {
            courseProgrammeList = courseProgrammeDao.GetCourseProgrammeForCourseCode(incourseCode);
        }
        catch (CustomException e)
        {
            throw e;
        }
        return courseProgrammeList;
    }
}