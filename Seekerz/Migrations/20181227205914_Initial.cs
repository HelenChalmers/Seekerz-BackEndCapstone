using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Seekerz.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    CompanyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Location = table.Column<string>(nullable: false),
                    URL = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.CompanyId);
                    table.ForeignKey(
                        name: "FK_Company_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "TaskToDo",
                columns: table => new
                {
                    TaskToDoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NewTask = table.Column<string>(nullable: false),
                    CompleteDate = table.Column<DateTime>(nullable: false),
                    IsCompleted = table.Column<bool>(nullable: false),
                    JobId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskToDo", x => x.TaskToDoId);
                    table.ForeignKey(
                        name: "FK_TaskToDo_Job_JobId",
                        column: x => x.JobId,
                        principalTable: "Job",
                        principalColumn: "JobId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskToDo_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "d83b833c-27bf-4118-aed9-0d993bd9b7b3", 0, "b878abee-f449-4ae9-bb5c-e85533abccf0", "admin@admin.com", true, "Admina", "Straytor", false, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAEAACcQAAAAEOJ9w2MbqCz0hQUDGzJZWUaiZqUdaLaTOhb+rq9raGsYb/jAAxk9yF1bpio9HEmnSg==", null, false, "8d01b696-f106-4730-873e-3ae04824cf9b", false, "Admina" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "df7399e2-00ed-4abc-bf6c-583a79cb953a", 0, "54f32b8f-7f13-4aad-a012-91acb5a11d30", "hchalmers23@gmail.com", true, "Helen", "Chalmers", false, null, "HCHALMERS23@GMAIL.COM", "HCHALMERS23@GMAIL.COM", "AQAAAAEAACcQAAAAEDHe732ARKtMpEdkz5taNpGJCcg4FAY2H2xtD5HDaQQIJjqZQPJSSg5l2e9TkGqbag==", null, false, "7c731bf8-52d3-4b24-bb62-a11881a659c3", false, "Helen" });

            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "CompanyId", "Location", "Name", "URL", "UserId" },
                values: new object[,]
                {
                    { 3, "Nashville, TN", "Info Works", "https://www.infoworks.io/", "d83b833c-27bf-4118-aed9-0d993bd9b7b3" },
                    { 4, "Franklin, TN", "Ramsey Solutions", "", "d83b833c-27bf-4118-aed9-0d993bd9b7b3" },
                    { 5, "BelleMeade, Nashville, TN", "The Atkinsons", "", "d83b833c-27bf-4118-aed9-0d993bd9b7b3" },
                    { 1, "West End, Nashville", "Maize Analytics", "https://www.maizeanalytics.com/", "df7399e2-00ed-4abc-bf6c-583a79cb953a" },
                    { 2, "Nashville, TN", "Claris Health", "https://www.clarishealth.com/", "df7399e2-00ed-4abc-bf6c-583a79cb953a" }
                });

            migrationBuilder.InsertData(
                table: "QA",
                columns: new[] { "QAId", "Answer", "Notes", "Question", "UserId" },
                values: new object[,]
                {
                    { 1, "OhmyZsh on Mac side and GitBash on WindowsSide", "Maize Analytics asked this", "Tell me about what you use in the CommandLine", "df7399e2-00ed-4abc-bf6c-583a79cb953a" },
                    { 2, "Confidence Level", "Kyle from Infoworks - during Mock Interview", "What has been your greatest weakness", "df7399e2-00ed-4abc-bf6c-583a79cb953a" },
                    { 3, "Hard Personalities to work with ", "Claris Health", "What was your biggest challenge at NSS", "df7399e2-00ed-4abc-bf6c-583a79cb953a" }
                });

            migrationBuilder.InsertData(
                table: "Job",
                columns: new[] { "JobId", "CompanyId", "IsActive", "PersonalNotes", "Position", "ToldNss", "UserId" },
                values: new object[,]
                {
                    { 2, 3, true, "Had a mock interview that could turn into a real one.", "Software Developer", "Kristin knows about the mock interview might turn into a real one", "df7399e2-00ed-4abc-bf6c-583a79cb953a" },
                    { 3, 1, true, "Interviewed with Chase Ramsey - have a 2nd interview scheduled", "Technical Operations", "knows that I have a technical interview scheduled", "df7399e2-00ed-4abc-bf6c-583a79cb953a" },
                    { 4, 1, false, "Over It", "Executive Assistant", "They don't", "df7399e2-00ed-4abc-bf6c-583a79cb953a" },
                    { 5, 1, true, "Side Gig", "DogWalker", "They don't", "d83b833c-27bf-4118-aed9-0d993bd9b7b3" },
                    { 1, 2, true, "Liked the Company and is growing dramatically over the next year.", "Software Developer 1", "Nss KNows - employer came in to NSS to interview", "df7399e2-00ed-4abc-bf6c-583a79cb953a" }
                });

            migrationBuilder.InsertData(
                table: "TaskToDo",
                columns: new[] { "TaskToDoId", "CompleteDate", "IsCompleted", "JobId", "NewTask", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2018, 12, 18, 13, 30, 0, 0, DateTimeKind.Unspecified), false, 3, "Study-Technical Interview", "df7399e2-00ed-4abc-bf6c-583a79cb953a" },
                    { 3, new DateTime(2018, 12, 17, 12, 30, 0, 0, DateTimeKind.Unspecified), false, 3, "Follow up with Infoworks", "df7399e2-00ed-4abc-bf6c-583a79cb953a" },
                    { 4, new DateTime(2018, 12, 17, 12, 30, 0, 0, DateTimeKind.Unspecified), false, 5, "Take Pup for walk", "d83b833c-27bf-4118-aed9-0d993bd9b7b3" },
                    { 2, new DateTime(2018, 12, 17, 12, 30, 0, 0, DateTimeKind.Unspecified), false, 1, "Follow up with Claris Health", "df7399e2-00ed-4abc-bf6c-583a79cb953a" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Company_UserId",
                table: "Company",
                column: "UserId");

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
                name: "IX_TaskToDo_JobId",
                table: "TaskToDo",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskToDo_UserId",
                table: "TaskToDo",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "QA");

            migrationBuilder.DropTable(
                name: "TaskToDo");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Job");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
