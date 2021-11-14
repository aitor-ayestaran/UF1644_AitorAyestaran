namespace Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UniquesMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Productos", "Nombre", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("dbo.Productos", "Nombre", unique: true);
            CreateIndex("dbo.Usuarios", "Email", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Usuarios", new[] { "Email" });
            DropIndex("dbo.Productos", new[] { "Nombre" });
            AlterColumn("dbo.Productos", "Nombre", c => c.String(nullable: false));
        }
    }
}
