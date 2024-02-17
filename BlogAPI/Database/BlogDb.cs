using BlogAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogAPI.Database
{
    public class BlogDb : DbContext
    {
        public BlogDb(DbContextOptions<BlogDb> options, DbSet<Author> authors, DbSet<Article> articles, DbSet<Comment> comments, DbSet<AuthorArticle> authorArticles) : base(options)
        {
            Authors = authors;
            Articles = articles;
            Comments = comments;
            AuthorArticles = authorArticles;
        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<AuthorArticle> AuthorArticles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>(ConfigureArticle);
            modelBuilder.Entity<Comment>(ConfigureComment);
            modelBuilder.Entity<AuthorArticle>(ConfigureAuthorArticle);
        }

        void ConfigureArticle(EntityTypeBuilder<Article> builder)
        {
            builder.HasKey(a => a.ArticleId);
        }

        void ConfigureComment(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(c => c.CommentId);
        }

        void ConfigureAuthorArticle(EntityTypeBuilder<AuthorArticle> builder)
        {
            builder.HasKey(aa => new { aa.AuthorId, aa.ArticleId });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=BlogAPIDb;Trusted_Connection=True");
        }
    }
}
