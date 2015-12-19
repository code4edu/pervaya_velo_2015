using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyHackathon.Models.ClientModels
{
	public partial class File
	{
		[Key]
		[Required]
		public int Id { get; set; }

		[StringLength(100, MinimumLength = 5)]
		[Required]
		public string Name { get; set; }

		[Required]
		public string Path { get; set; }
	}
}