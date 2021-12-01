using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClaimAuditFunction.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClaimAudit",
                columns: table => new
                {
                    ClaimId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TimeStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Operation = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimAudit", x => x.ClaimId);
                });

            migrationBuilder.InsertData(
                table: "ClaimAudit",
                columns: new[] { "ClaimId", "Operation", "TimeStamp" },
                values: new object[,]
                {
                    { "359c6bec-0563-4002-88a8-5c2e3489618d", "Create", "11/27/2021 3:13:05 PM" },
                    { "b09e007a-3cd3-4bf4-b840-a1d3f69637b6", "Create", "11/27/2021 3:13:05 PM" },
                    { "b1000827-e885-4ad0-9fa8-05e3d31dffbb", "Create", "11/27/2021 3:13:05 PM" },
                    { "e1aed24c-ce76-41a5-a075-dc2733697b60", "Create", "11/27/2021 3:13:05 PM" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClaimAudit_ClaimId",
                table: "ClaimAudit",
                column: "ClaimId")
                .Annotation("SqlServer:Clustered", false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClaimAudit");
        }
    }
}
