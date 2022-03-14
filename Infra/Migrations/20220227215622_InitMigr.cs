using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    public partial class InitMigr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Administrators",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Firstname = table.Column<string>(name: "First name", type: "varchar(120)", nullable: false),
                    Lastname = table.Column<string>(name: "Last name", type: "varchar(120)", nullable: false),
                    Documenttype = table.Column<string>(name: "Document type", type: "varchar(6)", nullable: false),
                    Documentnumber = table.Column<string>(name: "Document number", type: "varchar(14)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(300)", nullable: false),
                    Phonenumber = table.Column<string>(name: "Phone number", type: "varchar(13)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Aparts",
                columns: table => new
                {
                    Apartnumber = table.Column<int>(name: "Apart number", type: "int", nullable: false),
                    Apartblock = table.Column<int>(name: "Apart block", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aparts", x => new { x.Apartnumber, x.Apartblock });
                });

            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BarCode = table.Column<string>(type: "varchar(13)", nullable: false),
                    Itemname = table.Column<string>(name: "Item name", type: "varchar(150)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Addressedto = table.Column<string>(name: "Addressed to", type: "varchar(250)", nullable: false),
                    Sender = table.Column<string>(type: "varchar(150)", nullable: false),
                    Senderaddress = table.Column<string>(name: "Sender address", type: "varchar(250)", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Block = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Packages_Aparts_Number_Block",
                        columns: x => new { x.Number, x.Block },
                        principalTable: "Aparts",
                        principalColumns: new[] { "Apart number", "Apart block" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Residents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Firstname = table.Column<string>(name: "First name", type: "varchar(120)", nullable: false),
                    Lastname = table.Column<string>(name: "Last name", type: "varchar(120)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(300)", nullable: false),
                    Phonenumber = table.Column<string>(name: "Phone number", type: "varchar(13)", nullable: false),
                    Documenttype = table.Column<string>(name: "Document type", type: "varchar(6)", nullable: false),
                    Documentnumber = table.Column<string>(name: "Document number", type: "varchar(14)", nullable: false),
                    Password = table.Column<string>(type: "varchar(100)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Block = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Residents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Residents_Aparts_Number_Block",
                        columns: x => new { x.Number, x.Block },
                        principalTable: "Aparts",
                        principalColumns: new[] { "Apart number", "Apart block" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Visitants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Firstname = table.Column<string>(name: "First name", type: "varchar(120)", nullable: false),
                    Lastname = table.Column<string>(name: "Last name", type: "varchar(120)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(300)", nullable: false),
                    Phonenumber = table.Column<string>(name: "Phone number", type: "varchar(13)", nullable: false),
                    Documenttype = table.Column<string>(name: "Document type", type: "varchar(6)", nullable: false),
                    Documentnumber = table.Column<string>(name: "Document number", type: "varchar(14)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Block = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visitants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Visitants_Aparts_Number_Block",
                        columns: x => new { x.Number, x.Block },
                        principalTable: "Aparts",
                        principalColumns: new[] { "Apart number", "Apart block" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Packages_Number_Block",
                table: "Packages",
                columns: new[] { "Number", "Block" });

            migrationBuilder.CreateIndex(
                name: "IX_Residents_Number_Block",
                table: "Residents",
                columns: new[] { "Number", "Block" });

            migrationBuilder.CreateIndex(
                name: "IX_Visitants_Number_Block",
                table: "Visitants",
                columns: new[] { "Number", "Block" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administrators");

            migrationBuilder.DropTable(
                name: "Packages");

            migrationBuilder.DropTable(
                name: "Residents");

            migrationBuilder.DropTable(
                name: "Visitants");

            migrationBuilder.DropTable(
                name: "Aparts");
        }
    }
}
