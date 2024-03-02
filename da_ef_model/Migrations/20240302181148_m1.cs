using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace da_ef_model.Migrations
{
    /// <inheritdoc />
    public partial class m1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @" EXEC ('
                        alter table dbo.Nodes
                        add constraint chk_parentId_not_equal_id
                        check (ParentId != Id)
                ')");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @" EXEC ('

						ALTER TABLE dbo.Nodes
                        DROP CONSTRAINT chk_parentId_not_equal_id;
                ')");
        }
    }
}
