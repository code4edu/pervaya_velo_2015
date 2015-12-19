using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyHackathon.Models.ClientModels
{
	public partial class User
	{
		[Key]
		[Required]
		public int Id { get; set; }

		[DataType(DataType.EmailAddress)]
		[Required]
		public string Mail { get; set; }

		[Required]
		[StringLength(15, MinimumLength = 6)]
		public string Password { get; set; }

		[Required]
		public Roles role { get; set; }

		public virtual ICollection<User> Professors { get; set; }
		public virtual ICollection<User> Students { get; set; }

		public virtual ICollection<Document> Documents { get; set; }
	}
}