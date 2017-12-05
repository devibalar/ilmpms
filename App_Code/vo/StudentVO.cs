using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for StudentVO
/// </summary>
public class StudentVO:UserVO
{
    private int studentId;
    private string firstName;
    private string lastName;
    private string status;
    private DateTime createdDTM;
    private DateTime updatedDTM;
    private List<StudentMajorVO> program = new List<StudentMajorVO>();

	public StudentVO()
	{		
	}

    public StudentVO(int studentId)
    {
        this.studentId = studentId;
    }

    public StudentVO(int studentId,string firstName, string lastName, string status, 
        DateTime createdDate, DateTime lastUpdatedDate ){
        this.studentId = studentId;
        this.firstName=firstName;
        this.lastName=lastName;
        this.status=status;
        this.createdDTM = createdDate;
        this.updatedDTM =lastUpdatedDate;
    }
    public StudentVO(int studentId, string firstName, string lastName, string reason, DateTime updatedDTM)
    {
        this.studentId = studentId;
        this.firstName = firstName;
        this.lastName = lastName;   
        this.status = reason;
        this.createdDTM = DateTime.Now;
        this.updatedDTM = updatedDTM;
    }  
    
  
    public StudentVO(int studentId, string firstName, string lastName, string programmeCode, string emailID, string active,
        string reason, DateTime updatedDTM)
    {
        this.studentId = studentId;
        this.firstName = firstName;
        this.lastName = lastName;
        this.EmailID = emailID;
        this.Active = active;
        this.status = reason;
        this.createdDTM = DateTime.Now;
        this.updatedDTM = updatedDTM;
    }  
    
    public int StudentId
    {
      get { return studentId; }
      set { studentId = value; }
    }

        public string FirstName
    {
      get { return firstName; }
      set { firstName = value; }
    }

    
    public string LastName
    {
      get { return lastName; }
      set { lastName = value; }
    }   
    
    public string Status
    {
      get { return status; }
      set { status = value; }
    }
    
    public DateTime CreatedDate
    {
        get { return createdDTM; }
        set { createdDTM = value; }
    }
    
    public DateTime LastUpdatedDate
    {
      get { return updatedDTM; }
      set { updatedDTM = value; }
    }

    public List<StudentMajorVO> Program
    {
        get { return program; }
        set { program = value; }
    }
}