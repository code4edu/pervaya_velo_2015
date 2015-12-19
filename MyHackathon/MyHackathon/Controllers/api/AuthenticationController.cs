using MyHackathon.Models.ClientModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyHackathon.Controllers.api
{
	public class AuthenticationController : ApiController
	{
		public IHttpActionResult Post(LoginModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			return null;
		}
	}
}
