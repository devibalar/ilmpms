using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for IProgrammeMajorDao
/// </summary>
public interface IProgrammeMajorDao
{
	DataSet getMajorForProgramme(string programme);
}