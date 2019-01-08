namespace Market.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class final : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Fisler",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GenelToplam = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OdemeYontemi = c.Byte(nullable: false),
                        FisTarihi = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Satislar",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UrunDetayId = c.Int(nullable: false),
                        SatisAdeti = c.Int(nullable: false),
                        FisId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Fisler", t => t.FisId, cascadeDelete: true)
                .ForeignKey("dbo.UrunDetaylar", t => t.UrunDetayId, cascadeDelete: true)
                .Index(t => t.UrunDetayId)
                .Index(t => t.FisId);
            
            CreateTable(
                "dbo.UrunDetaylar",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UrunId = c.Guid(nullable: false),
                        KoliIciAdet = c.Int(nullable: false),
                        Aciklama = c.String(maxLength: 50),
                        KoliAdet = c.Int(nullable: false),
                        UrunAdet = c.Int(nullable: false),
                        AlisFiyat = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SatisFiyat = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Urunler", t => t.UrunId, cascadeDelete: true)
                .Index(t => t.UrunId);
            


        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Satislar", "UrunDetayId", "dbo.UrunDetaylar");
            DropForeignKey("dbo.UrunDetaylar", "UrunId", "dbo.Urunler");
            DropForeignKey("dbo.Satislar", "FisId", "dbo.Fisler");
            DropIndex("dbo.UrunDetaylar", new[] { "UrunId" });
            DropIndex("dbo.Satislar", new[] { "FisId" });
            DropIndex("dbo.Satislar", new[] { "UrunDetayId" });
            DropTable("dbo.UrunDetaylar");
            DropTable("dbo.Satislar");
            DropTable("dbo.Fisler");
        }
    }
}
