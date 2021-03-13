using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicalResearch.VisitData.Migrations
{
    public partial class V001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VdrVisits",
                columns: table => new
                {
                    RecordId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ParticipantIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExecutingInstituteIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudyIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VisitIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudyExecutionIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScheduledDateUtc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExecutionDateUtc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExecutionState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VdrVisits", x => x.RecordId);
                });

            migrationBuilder.CreateTable(
                name: "VdrDataRecordings",
                columns: table => new
                {
                    RecordId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    VisitRecordId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VdrDataRecordings", x => x.RecordId);
                    table.ForeignKey(
                        name: "FK_VdrDataRecordings_VdrVisits_VisitRecordId",
                        column: x => x.VisitRecordId,
                        principalTable: "VdrVisits",
                        principalColumn: "RecordId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VdrDrugApplyments",
                columns: table => new
                {
                    RecordId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    VisitRecordId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VdrDrugApplyments", x => x.RecordId);
                    table.ForeignKey(
                        name: "FK_VdrDrugApplyments_VdrVisits_VisitRecordId",
                        column: x => x.VisitRecordId,
                        principalTable: "VdrVisits",
                        principalColumn: "RecordId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VdrTreatments",
                columns: table => new
                {
                    RecordId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    VisitRecordId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VdrTreatments", x => x.RecordId);
                    table.ForeignKey(
                        name: "FK_VdrTreatments_VdrVisits_VisitRecordId",
                        column: x => x.VisitRecordId,
                        principalTable: "VdrVisits",
                        principalColumn: "RecordId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VdrDataRecordings_VisitRecordId",
                table: "VdrDataRecordings",
                column: "VisitRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_VdrDrugApplyments_VisitRecordId",
                table: "VdrDrugApplyments",
                column: "VisitRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_VdrTreatments_VisitRecordId",
                table: "VdrTreatments",
                column: "VisitRecordId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VdrDataRecordings");

            migrationBuilder.DropTable(
                name: "VdrDrugApplyments");

            migrationBuilder.DropTable(
                name: "VdrTreatments");

            migrationBuilder.DropTable(
                name: "VdrVisits");
        }
    }
}
