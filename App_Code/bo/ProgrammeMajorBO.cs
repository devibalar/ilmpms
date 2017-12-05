using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProgrammeMajorBO
/// </summary>
public class ProgrammeMajorBO
{
    private IProgrammeMajorDao programmeMajorDaoImplobj = new ProgrammeMajorDaoImpl();
    public DataSet getMajorForProgramme(string inProgrammeId)
    {
        DataSet ds;
        try
        {
            ds= programmeMajorDaoImplobj.getMajorForProgramme(inProgrammeId);
        }
        catch (CustomException e)
        {
            throw e;
        }
        return ds;
    }
}