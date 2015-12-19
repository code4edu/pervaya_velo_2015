using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyHackathon.Models.ClientModels
{
	public class LoginModel
	{
		[DataType(DataType.EmailAddress)]
		[Required]
		public string Email { get; set; }

		[StringLength(15, MinimumLength = 6)]
		[Required]
		public string Password { get; set; }

	}
}