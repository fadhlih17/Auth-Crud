using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Auth_Crud.Migrations
{
    /// <inheritdoc />
    public partial class initial_create : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "m_admin",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "Varchar(50)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_admin", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "m_product",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    last_admin = table.Column<int>(type: "int", nullable: true),
                    last_update = table.Column<DateTime>(type: "datetime2", nullable: false),
                    entry_admin = table.Column<int>(type: "int", nullable: true),
                    entry_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    delete_admin = table.Column<int>(type: "int", nullable: true),
                    delete_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    isDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_product", x => x.id);
                    table.ForeignKey(
                        name: "FK_m_product_m_admin_delete_admin",
                        column: x => x.delete_admin,
                        principalTable: "m_admin",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_m_product_m_admin_entry_admin",
                        column: x => x.entry_admin,
                        principalTable: "m_admin",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_m_product_m_admin_last_admin",
                        column: x => x.last_admin,
                        principalTable: "m_admin",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "t_transactional",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    action = table.Column<string>(type: "Varchar(10)", nullable: false),
                    admin = table.Column<int>(type: "int", nullable: false),
                    action_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_transactional", x => x.id);
                    table.ForeignKey(
                        name: "FK_t_transactional_m_admin_admin",
                        column: x => x.admin,
                        principalTable: "m_admin",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_transactional_m_product_product_id",
                        column: x => x.product_id,
                        principalTable: "m_product",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_m_admin_email",
                table: "m_admin",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_m_product_delete_admin",
                table: "m_product",
                column: "delete_admin");

            migrationBuilder.CreateIndex(
                name: "IX_m_product_entry_admin",
                table: "m_product",
                column: "entry_admin");

            migrationBuilder.CreateIndex(
                name: "IX_m_product_last_admin",
                table: "m_product",
                column: "last_admin");

            migrationBuilder.CreateIndex(
                name: "IX_t_transactional_admin",
                table: "t_transactional",
                column: "admin");

            migrationBuilder.CreateIndex(
                name: "IX_t_transactional_product_id",
                table: "t_transactional",
                column: "product_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_transactional");

            migrationBuilder.DropTable(
                name: "m_product");

            migrationBuilder.DropTable(
                name: "m_admin");
        }
    }
}
