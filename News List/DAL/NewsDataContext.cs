namespace News_List.DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class NewsDataContext : DbContext
    {
        public NewsDataContext()
            : base("name=NewsDataContext")
        {
        }

        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<News_Files> News_Files { get; set; }
        public virtual DbSet<Source> Sources { get; set; }
        public virtual DbSet<Theme> Themes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<File>()
                .HasMany(e => e.News_Files)
                .WithRequired(e => e.File)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<News>()
                .HasMany(e => e.News_Files)
                .WithRequired(e => e.News)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Source>()
                .HasMany(e => e.News)
                .WithRequired(e => e.Source)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Theme>()
                .HasMany(e => e.News)
                .WithRequired(e => e.Theme)
                .WillCascadeOnDelete(false);
        }
    }
}
