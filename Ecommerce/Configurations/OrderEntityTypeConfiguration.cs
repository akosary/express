using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Configurations
{
    public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            //Set Primary Key
            builder
                .HasKey(o => o.Id);

            //Add Default Value To Status Column
            builder
                .Property(o => o.Status)
                .HasDefaultValue("Pending");

            //Add Default Value To CreatedOn Column
            builder
                .Property(o => o.CreatedOn)
                .HasDefaultValueSql("GETDATE()");

            //Add Default Value To PayStatus Column
            builder
                .Property(o => o.PayStatus)
                .HasDefaultValue("Pending");

            //Add Default Value To ShipStatus Column
            builder
                .Property(o => o.ShipStatus)
                .HasDefaultValue("Processed");

            //Add Default Value To ShipTrackNo Column
            builder
                .Property(o => o.ShipTrackNo)
                .HasDefaultValueSql("NEWID()");

            //Configure Relation Between Users & Orders Tables
            builder
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(u => u.UserId);
        }
    }
}
