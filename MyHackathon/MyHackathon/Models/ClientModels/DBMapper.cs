using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyHackathon.Models.ClientModels
{
	public partial class DBMapper : DbContext
	{
		public DBMapper()
			: base("name=default")
		{ }

		public virtual DbSet<User> Users { get; set; }
		public virtual DbSet<File> Files { get; set; }
		public virtual DbSet<Document> Documents { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>()
				.HasMany<User>(l => l.Professors)
				.WithMany(l => l.Students)
				.Map(m => m.ToTable("StudentsWithProfessors").MapLeftKey("ProfessorId").MapRightKey("StudentId"));

			modelBuilder.Entity<User>()
				.HasMany<Document>(l => l.Documents)
				.WithRequired(l => l.Author)
				.HasForeignKey(l => l.AuthorId);

			modelBuilder.Entity<User>().ToTable("Users");
			modelBuilder.Entity<Document>().ToTable("Documents");
			modelBuilder.Entity<File>().ToTable("Files");
		}
	}
}