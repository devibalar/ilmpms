using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProgrammeBO
/// </summary>
public class ProgrammeBO
{
    IProgrammeDao programmeobj = new ProgrammeDaoImpl();
	public ProgrammeBO()
	{
		//
		// TODO: Add constructor logic here
		//
	}
     	
    public DataSet getAllProgram()
    {
        DataSet dsProgram ;
        try
        {
            dsProgram = programmeobj.getAllProgram();
        }
        catch (CustomException e)
        {
            throw e;
        }
        return dsProgram;
    }
}