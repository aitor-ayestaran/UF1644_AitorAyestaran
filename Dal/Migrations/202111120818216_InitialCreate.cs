namespace Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carritos",
                c => new
                    {
                        Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuarios", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Detalles",
                c => new
                    {
                        CarritoId = c.Long(nullable: false),
                        ProductoId = c.Long(nullable: false),
                        Cantidad = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CarritoId, t.ProductoId })
                .ForeignKey("dbo.Carritos", t => t.CarritoId, cascadeDelete: true)
                .ForeignKey("dbo.Productos", t => t.ProductoId, cascadeDelete: true)
                .Index(t => t.CarritoId)
                .Index(t => t.ProductoId);
            
            CreateTable(
                "dbo.Productos",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Precio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Foto = c.String(maxLength: 50),
                        Unidad = c.String(nullable: false),
                        PrecioUnidad = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Descuento = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 100),
                        Password = c.String(nullable: false, maxLength: 64),
                        Nombre = c.String(maxLength: 50),
                        Rol = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Carritos", "Id", "dbo.Usuarios");
            DropForeignKey("dbo.Detalles", "ProductoId", "dbo.Productos");
            DropForeignKey("dbo.Detalles", "CarritoId", "dbo.Carritos");
            DropIndex("dbo.Detalles", new[] { "ProductoId" });
            DropIndex("dbo.Detalles", new[] { "CarritoId" });
            DropIndex("dbo.Carritos", new[] { "Id" });
            DropTable("dbo.Usuarios");
            DropTable("dbo.Productos");
            DropTable("dbo.Detalles");
            DropTable("dbo.Carritos");
        }
    }
}
