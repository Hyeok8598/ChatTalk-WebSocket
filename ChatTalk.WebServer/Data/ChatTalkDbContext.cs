using ChatTalk.Common;
using ChatTalk.Common.Utils;
using ChatTalk.WebServer.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatTalk.WebServer.Data
{
    public class ChatTalkDbContext : DbContext
    {
        public ChatTalkDbContext(DbContextOptions<ChatTalkDbContext> options) : base(options) {}

        public DbSet<ChatMessageEntity> ChatMessages => Set<ChatMessageEntity>();
        public DbSet<UsersEntity> Users => Set<UsersEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach(var entity in modelBuilder.Model.GetEntityTypes())
            {
                string tableName = entity.ClrType.Name;

                if(tableName.EndsWith("Entity"))
                {
                    tableName = tableName[..^6];
                }

                entity.SetTableName(NamingConverter.ToSnakeCase(tableName));

                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(NamingConverter.ToSnakeCase(property.Name));
                }
            }

            modelBuilder.Entity<ChatMessageEntity>(entity =>
            {
                entity.Property(x => x.Id)
                      .HasColumnName("id")
                      .ValueGeneratedOnAdd();

                entity.Property(x => x.CreateAt)
                      .HasColumnName("create_at")
                      .HasDefaultValueSql("NOW()")
                      .ValueGeneratedOnAdd();
            });


            base.OnModelCreating(modelBuilder);
        }
    }
}
