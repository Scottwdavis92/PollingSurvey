namespace PollingApplication.DTO
{
    public class EmployeeDepartmentSurveyResults
    {


        public string EmployeeName { get; set; } = string.Empty;
        public int EmployeeId { get; set; }
        public string EmployeeEmail { get; set; } = string.Empty;

        public string DepartmentName { get; set; } = string.Empty;

        public string IsCompleted { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
