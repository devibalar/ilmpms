using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for IPrerequisiteDao
/// </summary>
public interface IPrerequisiteDao
{
    Boolean AddPrerequisite(PrerequisiteVO inPrerequisite);
    Boolean UpdatePrerequisite(PrerequisiteVO inPrerequisite);
    Boolean DeletePrerequisite(PrerequisiteVO inPrerequisite);
    DataSet GetAllPrerequisites();
    DataSet GetAllCorequisites();
    string GetAllPrerequisiteForCourseCode(string courseCode);
}