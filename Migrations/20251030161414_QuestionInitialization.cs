using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PollingApplication.Migrations
{
    /// <inheritdoc />
    public partial class QuestionInitialization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Seed General Questions (DepartmentId = 0 for common questions)
            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "Questions", "ScopeType", "IsActive", "DepartmentId" },
                values: new object[,]
                {
                    { 1, "How satisfied are you with your overall work-life balance at the company?", "General", true, 0 },
                    { 2, "Do you feel you have the necessary tools and resources to do your job effectively?", "General", true, 0 },
                    { 3, "On a scale of 1 to 10, how likely are you to recommend our company as a great place to work?", "General", true, 0 },
                    { 4, "How well do you feel informed about company news, goals, and important changes?", "General", true, 0 },
                    { 5, "Do you feel a sense of belonging and inclusion within your team and the company as a whole?", "General", true, 0 }
                });

            // Seed Unique Questions for Engineering (DepartmentId = 1)
            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "Questions", "ScopeType", "IsActive", "DepartmentId" },
                values: new object[,]
                {
                    { 6, "How would you rate the effectiveness of our current software development and deployment processes?", "Unique", true, 1 },
                    { 7, "What is one feature or technical improvement you would most like to see prioritized in the next development cycle?", "Unique", true, 1 },
                    { 8, "Do you have sufficient time and resources to address technical debt and refactor legacy code?", "Unique", true, 1 },
                    { 9, "How well do engineering and product management collaborate on setting project scope and timelines?", "Unique", true, 1 },
                    { 10, "Do you feel that the company encourages experimentation and innovation in our technology stack?", "Unique", true, 1 }
                });

            // Seed Unique Questions for Marketing (DepartmentId = 2)
            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "Questions", "ScopeType", "IsActive", "DepartmentId" },
                values: new object[,]
                {
                    { 11, "What is your biggest challenge in generating high-quality leads for the sales team?", "Unique", true, 2 },
                    { 12, "How would you rate the effectiveness of our cross-departmental communication, particularly with Sales and Product?", "Unique", true, 2 },
                    { 13, "Do you believe our current marketing campaigns effectively communicate our brand's unique value proposition?", "Unique", true, 2 },
                    { 14, "Are you satisfied with the tools and analytics available to measure the performance of your campaigns?", "Unique", true, 2 },
                    { 15, "What new marketing channels or strategies do you think we should explore in the next year?", "Unique", true, 2 }
                });

            // Seed Unique Questions for Sales (DepartmentId = 3)
            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "Questions", "ScopeType", "IsActive", "DepartmentId" },
                values: new object[,]
                {
                    { 16, "What is the biggest obstacle you face in closing new deals with prospective customers?", "Unique", true, 3 },
                    { 17, "How accurate is the information you receive from the Marketing department regarding lead quality?", "Unique", true, 3 },
                    { 18, "Do you have the necessary training and support to effectively overcome common customer objections?", "Unique", true, 3 },
                    { 19, "How well does the current commission and incentive structure motivate you to exceed your sales targets?", "Unique", true, 3 },
                    { 20, "What information do you wish you had about our customers to help improve your sales strategy?", "Unique", true, 3 }
                });

            // Seed Unique Questions for Human Resources (DepartmentId = 4)
            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "Questions", "ScopeType", "IsActive", "DepartmentId" },
                values: new object[,]
                {
                    { 21, "How effective is our current recruitment and onboarding process for new employees?", "Unique", true, 4 },
                    { 22, "Do you feel that our company's policies are fair, transparent, and consistently applied?", "Unique", true, 4 },
                    { 23, "What resources or training would help you improve your skills in resolving employee conflicts or concerns?", "Unique", true, 4 },
                    { 24, "How effectively do we communicate important policy changes and benefit updates to employees?", "Unique", true, 4 },
                    { 25, "What changes could be made to improve employee morale and promote a more inclusive workplace culture?", "Unique", true, 4 }
                });

            // Seed Unique Questions for Finance (DepartmentId = 5)
            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "Questions", "ScopeType", "IsActive", "DepartmentId" },
                values: new object[,]
                {
                    { 26, "How satisfied are you with the efficiency and clarity of our current budgeting and financial reporting processes?", "Unique", true, 5 },
                    { 27, "What financial tools or software would help you perform your tasks more accurately or efficiently?", "Unique", true, 5 },
                    { 28, "Do you have the data and support needed to provide accurate financial forecasting for other departments?", "Unique", true, 5 },
                    { 29, "What is the biggest challenge in managing the company's financial risks and compliance requirements?", "Unique", true, 5 },
                    { 30, "How effectively do you feel the Finance department collaborates with other departments on financial planning and performance?", "Unique", true, 5 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Delete all the questions based on their IDs
            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValues: new object[] {
                    1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
                    11, 12, 13, 14, 15, 16, 17, 18, 19, 20,
                    21, 22, 23, 24, 25, 26, 27, 28, 29, 30
                });
        }
    }
}
