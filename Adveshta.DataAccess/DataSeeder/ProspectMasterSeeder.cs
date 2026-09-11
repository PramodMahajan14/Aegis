using Adveshta.Model.Master;
using Adveshta.Model.ProspectModel;
using Microsoft.EntityFrameworkCore;

namespace Adveshta.DataAccess.DataSeeder
{
    public static class ProspectSourceSeeder
    {
        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<ProspectSource>().HasData(

                    new ProspectSource
                    {
                        Id = Guid.Parse("a86d9b08-42d7-4e0e-aa3e-12ea6750c3be"),
                        Name = "COLD CALL",
                        Code = "COLD_CALL"
                    },
                    new ProspectSource
                    {
                        Id = Guid.Parse("4a358b8e-1517-4935-8c54-9be5f1975636"),
                        Name = "Tele Call",
                        Code = "TELE_CALL"
                    },
                    new
                    {
                        Id = Guid.Parse("96492d44-7145-4820-8e05-39be6b573a17"),
                        Name = "Referral",
                        Code = "REFERRAL"
                    },
                    new ProspectSource
                    {
                        Id = Guid.Parse("997ae18f-d94f-4b43-a39a-9049def7eb9a"),
                        Name = "Social Media",
                        Code = "SOCIAL_MEDIA "
                    },
                     new ProspectSource
                     {
                         Id = Guid.Parse("1c2d94b1-88ca-4550-b368-b6aa70ea080b"),
                         Name = "Campaign",
                         Code = "CAMPAIGN "
                     },
                    new ProspectSource
                    {
                        Id = Guid.Parse("e6f849db-fe3f-4c8d-9856-eadb6e3a356d"),
                        Name = "Web Site",
                        Code = "WEBSITE"
                    },
                    new ProspectSource
                    {
                        Id = Guid.Parse("31a6eb48-2cf6-47ff-89cc-9987dd34d0ed"),
                        Name = "Market Research",
                        Code = "MARKET_RESEARCH"
                    }
                    ,
                    new ProspectSource
                    {
                        Id = Guid.Parse("eec202b2-cf01-4520-95e1-129d5267119b"),
                        Name = "Site Visit",
                        Code = "SITE_VISIT"
                    },
                    new ProspectSource
                    {
                        Id = Guid.Parse("2388fcd1-702c-4ba9-aa26-fd47e742126f"),
                        Name = "Existing Network",
                        Code = "EXISTING_NETWORK"
                    },
                    new ProspectSource
                    {
                        Id = Guid.Parse("4db35ae1-ec62-4b61-ab21-adc069e288c9"),
                        Name = "Other",
                        Code = "OTHER"
                    }

            );
        }

    }

    public static class ProspectTemperatureSeeder
    {
        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<ProspectTemperature>().HasData(

                new ProspectTemperature
                {
                    Id = Guid.Parse("fa93281c-ce3e-4e58-965b-37e32c2c232e"),
                    Name = "Not Set",
                    Code = "NOT_SET",
                },

                new ProspectTemperature
                {
                    Id = Guid.Parse("1be20c30-5004-44bb-992d-1a8e0affd8a0"),
                    Name = "Hot",
                    Code = "HOT",
                },
                new ProspectTemperature
                {
                    Id = Guid.Parse("ddc27c6d-343b-483f-a492-62557fc694e5"),
                    Name = "Cold",
                    Code = "COLD",
                },
                new ProspectTemperature
                {
                    Id = Guid.Parse("da1b82ba-92f1-40a8-9a8c-9c317efe5c0a"),
                    Name = "Warn",
                    Code = "WARM",
                }
            );
        }
    }
}