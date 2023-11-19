using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WishListApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Login = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Login);
                });

            migrationBuilder.CreateTable(
                name: "Friend",
                columns: table => new
                {
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId2 = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    User1Login = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    User2Login = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsUser1Accept = table.Column<bool>(type: "bit", nullable: false),
                    IsUser2Accept = table.Column<bool>(type: "bit", nullable: false),
                    AcceptDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friend", x => new { x.UserId1, x.UserId2 });
                    table.ForeignKey(
                        name: "FK_Friend_User_User1Login",
                        column: x => x.User1Login,
                        principalTable: "User",
                        principalColumn: "Login");
                    table.ForeignKey(
                        name: "FK_Friend_User_User2Login",
                        column: x => x.User2Login,
                        principalTable: "User",
                        principalColumn: "Login");
                });

            migrationBuilder.CreateTable(
                name: "Wish",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deadline = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    IsMaxOne = table.Column<bool>(type: "bit", nullable: false),
                    IsComplete = table.Column<bool>(type: "bit", nullable: false),
                    IsClosed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wish", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wish_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Login",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WishUser",
                columns: table => new
                {
                    AssignedWishId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AssignedUserLogin = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WishId = table.Column<long>(type: "bigint", nullable: false),
                    UserLogin = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishUser", x => new { x.AssignedWishId, x.AssignedUserLogin });
                    table.ForeignKey(
                        name: "FK_WishUser_User_UserLogin",
                        column: x => x.UserLogin,
                        principalTable: "User",
                        principalColumn: "Login");
                    table.ForeignKey(
                        name: "FK_WishUser_Wish_WishId",
                        column: x => x.WishId,
                        principalTable: "Wish",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Friend_User1Login",
                table: "Friend",
                column: "User1Login");

            migrationBuilder.CreateIndex(
                name: "IX_Friend_User2Login",
                table: "Friend",
                column: "User2Login");

            migrationBuilder.CreateIndex(
                name: "IX_Wish_UserId",
                table: "Wish",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WishUser_UserLogin",
                table: "WishUser",
                column: "UserLogin");

            migrationBuilder.CreateIndex(
                name: "IX_WishUser_WishId",
                table: "WishUser",
                column: "WishId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Friend");

            migrationBuilder.DropTable(
                name: "WishUser");

            migrationBuilder.DropTable(
                name: "Wish");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
