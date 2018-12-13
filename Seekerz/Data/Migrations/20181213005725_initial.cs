using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Seekerz.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    CompanyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Location = table.Column<string>(nullable: false),
                    URL = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.CompanyId);
                });

            migrationBuilder.CreateTable(
                name: "QA",
                columns: table => new
                {
                    QAId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Question = table.Column<string>(nullable: false),
                    Answer = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QA", x => x.QAId);
                    table.ForeignKey(
                        name: "FK_QA_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Job",
                columns: table => new
                {
                    JobId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Position = table.Column<string>(nullable: false),
                    PersonalNotes = table.Column<string>(nullable: true),
                    ToldNss = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    CompanyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job", x => x.JobId);
                    table.ForeignKey(
                        name: "FK_Job_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Job_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Task",
                columns: table => new
                {
                    TaskToDoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NewTask = table.Column<string>(nullable: false),
                    CompleteDate = table.Column<DateTime>(nullable: false),
                    IsCompleted = table.Column<bool>(nullable: false),
                    JobId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task", x => x.TaskToDoId);
                    table.ForeignKey(
                        name: "FK_Task_Job_JobId",
                        column: x => x.JobId,
                        principalTable: "Job",
                        principalColumn: "JobId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "3a8ae9d3-15cd-4cac-aeb9-c2896c20acde", 0, "2027f8ee-cb98-4a8d-97e7-1436e5b8ac69", "admin@admin.com", true, "Admina", "Straytor", false, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAEAACcQAAAAEOtuz7pi+Tn5MmjKXer5I8AFd+C9u9VIJZTLHQxDEF6QFSL5ju7eoRRHqm4MwuMWuQ==", null, false, "01a3cecd-d8ec-4eb4-8516-8476818331ae", false, "Admina" },
                    { "0336c1eb-6efa-454e-b688-72e39b410077", 0, "fde66f63-5dce-4a8d-8da8-6b79b9c8866b", "hchalmers23@gmail.com", true, "Helen", "Chalmers", false, null, "HCHALMERS23@GMAIL.COM", "HChALMERS23@GMAIL.COM", null, null, false, "099f54fa-8bb8-4f60-8a08-ad2a20112e05", false, "Helen" }
                });

            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "CompanyId", "Location", "Name", "URL" },
                values: new object[,]
                {
                    { 1, "West End, Nashville", "Maize Analytics", "https://www.maizeanalytics.com/" },
                    { 2, "Nashville, TN", "Claris Health", "https://www.clarishealth.com/" },
                    { 3, "Nashville, TN", "Info Works", "https://www.infoworks.io/" },
                    { 4, "Franklin, TN", "Ramsey Solutions", "" }
                });

            migrationBuilder.InsertData(
                table: "Job",
                columns: new[] { "JobId", "CompanyId", "IsActive", "PersonalNotes", "Position", "ToldNss", "UserId" },
                values: new object[,]
                {
                    { 3, 1, true, "Interviewed with Chase Ramsey - have a 2nd interview scheduled", "Technical Operations", "knows that I have a technical interview scheduled", "0336c1eb-6efa-454e-b688-72e39b410077" },
                    { 1, 2, true, "Liked the Company and is growing dramatically over the next year.", "Software Developer 1", "Nss KNows - employer came in to NSS to interview", "0336c1eb-6efa-454e-b688-72e39b410077" },
                    { 2, 3, true, "Had a mock interview that could turn into a real one.", "Software Developer", "Kristin knows about the mock interview might turn into a real one", "0336c1eb-6efa-454e-b688-72e39b410077" }
                });

            migrationBuilder.InsertData(
                table: "QA",
                columns: new[] { "QAId", "Answer", "Notes", "Question", "UserId" },
                values: new object[,]
                {
                    { 1, "OhmyZsh on Mac side and GitBash on WindowsSide", "Maize Analytics asked this", "Tell me about what you use in the CommandLine", "0336c1eb-6efa-454e-b688-72e39b410077" },
                    { 2, "Confidence Level", "Kyle from Infoworks - during Mock Interview", "What has been your greatest weakness", "0336c1eb-6efa-454e-b688-72e39b410077" },
                    { 3, "Hard Personalities to work with ", "Claris Health", "What was your biggest challenge at NSS", "0336c1eb-6efa-454e-b688-72e39b410077" }
                });

            migrationBuilder.InsertData(
                table: "Task",
                columns: new[] { "TaskToDoId", "CompleteDate", "IsCompleted", "JobId", "NewTask" },
                values: new object[] { 1, new DateTime(2018, 12, 18, 13, 30, 0, 0, DateTimeKind.Unspecified), false, 3, "Study-Technical Interview" });

            migrationBuilder.InsertData(
                table: "Task",
                columns: new[] { "TaskToDoId", "CompleteDate", "IsCompleted", "JobId", "NewTask" },
                values: new object[] { 3, new DateTime(2018, 12, 17, 12, 30, 0, 0, DateTimeKind.Unspecified), false, 3, "Follow up with Infoworks" });

            migrationBuilder.InsertData(
                table: "Task",
                columns: new[] { "TaskToDoId", "CompleteDate", "IsCompleted", "JobId", "NewTask" },
                values: new object[] { 2, new DateTime(2018, 12, 17, 12, 30, 0, 0, DateTimeKind.Unspecified), false, 1, "Follow up with Claris Health" });

            migrationBuilder.CreateIndex(
                name: "IX_Job_CompanyId",
                table: "Job",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Job_UserId",
                table: "Job",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_QA_UserId",
                table: "QA",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_JobId",
                table: "Task",
                column: "JobId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QA");

            migrationBuilder.DropTable(
                name: "Task");

            migrationBuilder.DropTable(
                name: "Job");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "0336c1eb-6efa-454e-b688-72e39b410077", "fde66f63-5dce-4a8d-8da8-6b79b9c8866b" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "3a8ae9d3-15cd-4cac-aeb9-c2896c20acde", "2027f8ee-cb98-4a8d-97e7-1436e5b8ac69" });

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");
        }
    }
}
