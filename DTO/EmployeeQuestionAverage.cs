namespace PollingApplication.DTO
{
    public class EmployeeQuestionAverage
    {
        public string QuestionText { get; set; } = string.Empty;
        public int AverageScore { get; set; }

        public string EmployeeName { get; set; } = string.Empty;
        public int EmployeeId { get; set; }

        public string DepartmentName { get; set; } = string.Empty;

        public string DepartmentType { get; set; } = string.Empty;


    }
}
