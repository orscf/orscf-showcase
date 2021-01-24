using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ORSCF.VisitData.Migrations
{
    public partial class V001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Visits",
                columns: table => new
                {
                    RecordId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ParticipantIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExecutingInstituteIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudyIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudyExecutionIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScheduledDateUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExecutionDateUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExecutionState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visits", x => x.RecordId);
                });

            migrationBuilder.CreateTable(
                name: "DataRecordings",
                columns: table => new
                {
                    RecordId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TaskIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortSubject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScheduledDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExecutionDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExecutionState = table.Column<int>(type: "int", nullable: false),
                    DataSchemaInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordedData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstructionsForExecution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotesRegardingOutcome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitRecordId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataRecordings", x => x.RecordId);
                    table.ForeignKey(
                        name: "FK_DataRecordings_Visits_VisitRecordId",
                        column: x => x.VisitRecordId,
                        principalTable: "Visits",
                        principalColumn: "RecordId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DrugApplyments",
                columns: table => new
                {
                    RecordId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TaskIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortSubject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScheduledDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExecutionDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExecutionState = table.Column<int>(type: "int", nullable: false),
                    DrugName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DrugDoseMgPerUnit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AppliedUnits = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InstructionsForExecution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotesRegardingOutcome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitRecordId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrugApplyments", x => x.RecordId);
                    table.ForeignKey(
                        name: "FK_DrugApplyments_Visits_VisitRecordId",
                        column: x => x.VisitRecordId,
                        principalTable: "Visits",
                        principalColumn: "RecordId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Treatments",
                columns: table => new
                {
                    RecordId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TaskIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortSubject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScheduledDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExecutionDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExecutionState = table.Column<int>(type: "int", nullable: false),
                    InstructionsForExecution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotesRegardingOutcome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitRecordId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treatments", x => x.RecordId);
                    table.ForeignKey(
                        name: "FK_Treatments_Visits_VisitRecordId",
                        column: x => x.VisitRecordId,
                        principalTable: "Visits",
                        principalColumn: "RecordId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DataRecordings_VisitRecordId",
                table: "DataRecordings",
                column: "VisitRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_DrugApplyments_VisitRecordId",
                table: "DrugApplyments",
                column: "VisitRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Treatments_VisitRecordId",
                table: "Treatments",
                column: "VisitRecordId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataRecordings");

            migrationBuilder.DropTable(
                name: "DrugApplyments");

            migrationBuilder.DropTable(
                name: "Treatments");

            migrationBuilder.DropTable(
                name: "Visits");
        }
    }
}
