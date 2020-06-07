namespace eeduca_api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GrupoMensagem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GruposMensagens",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UsuarioId = c.Int(nullable: false),
                    GrupoId = c.Int(nullable: false),
                    DataHoraCriacao = c.DateTime(nullable: false, precision: 0, null, "CURRENT_TIMESTAMP"),
                    Mensagem = c.String(nullable: false, unicode: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Grupos", t => t.GrupoId, cascadeDelete: true)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioId, cascadeDelete: true)
                .Index(t => t.UsuarioId)
                .Index(t => t.GrupoId);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GruposMensagens", "UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.GruposMensagens", "GrupoId", "dbo.Grupos");
            DropIndex("dbo.GruposMensagens", new[] { "GrupoId" });
            DropIndex("dbo.GruposMensagens", new[] { "UsuarioId" });
            DropTable("dbo.GruposMensagens");
        }
    }
}
