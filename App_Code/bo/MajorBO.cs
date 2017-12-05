using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MajorBO
/// </summary>
public class MajorBO
{
    MajorDaoImpl majorobj = new MajorDaoImpl();
	public MajorBO()
	{		
	}

    public DataSet getAllMajor()
    {
        DataSet dsMajor;
        try{
            dsMajor= majorobj.getAllMajor();
        }
        catch (CustomException e)
        {
            throw e;
        }
        return dsMajor;
    }
}