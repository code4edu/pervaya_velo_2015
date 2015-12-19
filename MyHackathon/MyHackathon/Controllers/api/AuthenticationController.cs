using MyHackathon.Models.ClientModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;

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

			if (LoginModel.CanLogin(model.Email, model.Password))
			{
				FormsAuthentication.SetAuthCookie(model.Email, createPersistentCookie: true);
				return Json(new { isLogged = true });
			}

			return StatusCode(HttpStatusCode.Forbidden);
		}

		public IHttpActionResult Delete()
		{
				FormsAuthentication.SignOut();
				return Ok();
		}
	}
}
