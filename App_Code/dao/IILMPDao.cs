using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for IILMPDao
/// </summary>
public interface IILMPDao
{
    string AddILMP(ILMPVO inilmpVO);
    List<ILMPCourseGridVO> GetILMPCoursesForId(int ilmpId);
    ILMPVO GetILMPDetailsForId(int ilmpId);
    List<WorkshopVO> GetILMPWorkshopForId(int ilmpId);
    List<ILMPVO> GetILMPListForStudentId(int studentId);
    Boolean UpdateILMPStatus(int ilmpId, string isActive);
    ILMPDetailsVO GetTemplateProgrammeForIlmpId(int ilmpId);
    Boolean UpdateILMPStatusForStudent(int studentId, string isActive);
    Boolean UpdateILMP(ILMPVO inilmpVO);
    ILMPVO GetILMPCoursesWorkshopForId(int ilmpId);
    int GetActiveIlmpForStudent(int studentId);
    Boolean UpdateILMPStatusForStudent(int studentId, int ilmpId);
}