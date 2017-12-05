using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for StaffVO
/// </summary>
public class StaffVO:UserVO
{
    private int staffId;
    private string name;
    private DateTime createdDTM;
    private DateTime updatedDTM;

    public int StaffId
    {
        get { return staffId; }
        set { staffId = value; }
    }
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    public DateTime CreatedDTM
    {
        get { return createdDTM; }
        set { createdDTM = value; }
    }
    public DateTime UpdatedDTM
    {
        get { return updatedDTM; }
        set { updatedDTM = value; }
    }
	public StaffVO()
	{
	}
    public StaffVO(int staffId, string name, DateTime createdDTM, DateTime updatedDTM)
    {
        this.staffId = staffId;
        this.name = name;
        this.createdDTM = createdDTM;
        this.updatedDTM = updatedDTM;
    }
    public StaffVO(int staffId, string name, DateTime updatedDTM)
    {
        this.staffId = staffId;
        this.name = name;
        this.updatedDTM = updatedDTM;
    }
}