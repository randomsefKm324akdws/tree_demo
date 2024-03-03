using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace da_ef_model.Migrations
{
    /// <inheritdoc />
    public partial class m2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @" EXEC ('
                 drop index IX_Nodes_TreeName_ParentId on dbo.Nodes
                ')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @" EXEC ('
                    CREATE NONCLUSTERED INDEX [IX_Nodes_TreeName_ParentId] ON [dbo].[Nodes]
                    (
	                    [TreeName] ASC,
	                    [ParentId] ASC
                    )
                ')");
        }
    }
}
