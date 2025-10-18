using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForFutureSobes.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedPriorityTo_TaskEntity_Model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Priority",
                table: "TaskEntities",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Priority",
                table: "TaskEntities");
        }
    }
}
