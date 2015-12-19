using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyHackathon.Models.ClientModels
{
	public partial class Document
	{
		[Key]
		[Required]
		public int Id { get; set; }

		[StringLength(100, MinimumLength = 10)]
		[Required]
		public string Title { get; set; }

		[StringLength(500, MinimumLength = 50)]
		[Required]
		public string Description { get; set; }

		[Required]
		public DocumentTypes Type { get; set; }

		[Required]
		public User Author { get; set; }
		public int AuthorId { get; set; }

		public User Professor { get; set; }

		[Required]
		public File FileEntity { get; set; }

		public DateTime CreationDate { get; set; }
		public DateTime EditDate { get; set; }
		public DateTime GraduationDate { get; set; }
	}
}