namespace eeduca_api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Grupos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UsuarioId = c.Int(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 60, storeType: "nvarchar"),
                        Descricao = c.String(maxLength: 500, storeType: "nvarchar"),
                        DataHoraCriacao = c.DateTime(nullable: false, precision: 0, null, "CURRENT_TIMESTAMP"),
                        Chave = c.String(maxLength: 6, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioId, cascadeDelete: true)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 60, storeType: "nvarchar"),
                        Email = c.String(nullable: false, maxLength: 120, storeType: "nvarchar"),
                        Senha = c.Binary(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Grupos", "UsuarioId", "dbo.Usuarios");
            DropIndex("dbo.Grupos", new[] { "UsuarioId" });
            DropTable("dbo.Usuarios");
            DropTable("dbo.Grupos");
        }
    }
}
