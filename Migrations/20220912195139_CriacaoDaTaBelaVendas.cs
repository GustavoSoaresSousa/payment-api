using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace desafioPottencialSeguradora.Migrations
{
    public partial class CriacaoDaTaBelaVendas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contatos_Pedido_ItensVendidosId",
                table: "Contatos");

            migrationBuilder.DropForeignKey(
                name: "FK_Contatos_Vendedor_DadosDoVendedorId",
                table: "Contatos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contatos",
                table: "Contatos");

            migrationBuilder.RenameTable(
                name: "Contatos",
                newName: "Vendas");

            migrationBuilder.RenameIndex(
                name: "IX_Contatos_ItensVendidosId",
                table: "Vendas",
                newName: "IX_Vendas_ItensVendidosId");

            migrationBuilder.RenameIndex(
                name: "IX_Contatos_DadosDoVendedorId",
                table: "Vendas",
                newName: "IX_Vendas_DadosDoVendedorId");

            migrationBuilder.AddColumn<string>(
                name: "Cpf",
                table: "Vendedor",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Vendedor",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                table: "Vendedor",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vendas",
                table: "Vendas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendas_Pedido_ItensVendidosId",
                table: "Vendas",
                column: "ItensVendidosId",
                principalTable: "Pedido",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendas_Vendedor_DadosDoVendedorId",
                table: "Vendas",
                column: "DadosDoVendedorId",
                principalTable: "Vendedor",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendas_Pedido_ItensVendidosId",
                table: "Vendas");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendas_Vendedor_DadosDoVendedorId",
                table: "Vendas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vendas",
                table: "Vendas");

            migrationBuilder.DropColumn(
                name: "Cpf",
                table: "Vendedor");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Vendedor");

            migrationBuilder.DropColumn(
                name: "Telefone",
                table: "Vendedor");

            migrationBuilder.RenameTable(
                name: "Vendas",
                newName: "Contatos");

            migrationBuilder.RenameIndex(
                name: "IX_Vendas_ItensVendidosId",
                table: "Contatos",
                newName: "IX_Contatos_ItensVendidosId");

            migrationBuilder.RenameIndex(
                name: "IX_Vendas_DadosDoVendedorId",
                table: "Contatos",
                newName: "IX_Contatos_DadosDoVendedorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contatos",
                table: "Contatos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contatos_Pedido_ItensVendidosId",
                table: "Contatos",
                column: "ItensVendidosId",
                principalTable: "Pedido",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contatos_Vendedor_DadosDoVendedorId",
                table: "Contatos",
                column: "DadosDoVendedorId",
                principalTable: "Vendedor",
                principalColumn: "Id");
        }
    }
}
