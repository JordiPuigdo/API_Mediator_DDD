using Database.Models;
using EntityFrameworkCore.EncryptColumn.Extension;
using EntityFrameworkCore.EncryptColumn.Interfaces;
using EntityFrameworkCore.EncryptColumn.Util;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class HubmaSoftContext : DbContext
    {
        private readonly IEncryptionProvider _provider;
        public HubmaSoftContext(DbContextOptions<HubmaSoftContext> options)
            : base(options)
        {
            this._provider = new GenerateEncryptionProvider("AAECAwQFBgcICQoLDA0ODw==");
        }

        
        [DeleteBehavior(DeleteBehavior.Cascade)]
        public DbSet<User> Users { get; set; }
        
        [DeleteBehavior(DeleteBehavior.Cascade)]
        public DbSet<Contact> Contacts { get; set; }
        [DeleteBehavior(DeleteBehavior.Cascade)]
        public DbSet<ContactSettings> ContactSettings { get; set; }
        [DeleteBehavior(DeleteBehavior.Cascade)]
        public DbSet<ExtraInformation> ExtraInformation{ get; set; }
        [DeleteBehavior(DeleteBehavior.Cascade)]
        public DbSet<Account> Account { get; set; }
        [DeleteBehavior(DeleteBehavior.Cascade)]
        public DbSet<LinkedContacts> LinkedContacts { get; set; }

        [DeleteBehavior(DeleteBehavior.Cascade)]
        public DbSet<ContactConfiguration> ContactConfigurations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseEncryption(this._provider);
            base.OnModelCreating(modelBuilder);
        }
    }
}
