using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for StaffDaoImpl
/// </summary>
public class StaffDaoImpl:IStaffDao
{
	public StaffDaoImpl()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int AddStaff(StaffVO inStaff)
    {
        int staffId = 0;
        return staffId;
    }

    public int UpdateStaff(StaffVO inStaff)
    {
        int staffId = 0;
        return staffId;
    }

    public int DeleteStaff(StaffVO inStaff)
    {
        int staffId = 0;
        return staffId;
    }

    public bool BulkAddStaff(string filePath) 
    {
        return false;
    }

    public List<StaffVO> SearchStaff()
    {
        List<StaffVO> staff = new List<StaffVO>();
        return staff;
    }
}