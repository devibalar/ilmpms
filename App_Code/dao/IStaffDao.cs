using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for IStaffDao
/// </summary>
public interface IStaffDao
{
    int AddStaff(StaffVO inStaff);
    int UpdateStaff(StaffVO inStaff);
    int DeleteStaff(StaffVO inStaff);
    bool BulkAddStaff(string filePath);
    List<StaffVO> SearchStaff();
}