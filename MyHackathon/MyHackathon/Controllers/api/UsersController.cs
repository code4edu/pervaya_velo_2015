﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using MyHackathon.Models.ClientModels;
using System.Web.Security;
using MyHackathon.Models;

namespace MyHackathon.Controllers.api
{
	public class UsersController : ApiController
	{
		private DBMapper db = new DBMapper();

		// GET: api/Users
		//[Authorize(Roles ="Admin")]
		public IQueryable<User> GetUsers()
		{
			var a = User.Identity.Name;
			return db.Users;
		}

		// GET: api/Users/5
		[ResponseType(typeof(User))]
		//[Authorize(Roles = "Admin")]
		public async Task<IHttpActionResult> GetUser(int id)
		{
			User user = await db.Users.FindAsync(id);
			if (user == null)
			{
				return NotFound();
			}

			return Ok(user);
		}

		// PUT: api/Users/5
		[ResponseType(typeof(void))]
		//[Authorize(Roles = "Admin")]
		public async Task<IHttpActionResult> PutUser(int id, User user)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != user.Id)
			{
				return BadRequest();
			}

			db.Entry(user).State = EntityState.Modified;

			try
			{
				await db.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!UserExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return StatusCode(HttpStatusCode.NoContent);
		}

		// POST: api/Users
		[ResponseType(typeof(User))]
		public async Task<IHttpActionResult> PostUser(User user)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			db.Users.Add(user);
			await db.SaveChangesAsync();
			FormsAuthentication.SetAuthCookie(user.Mail, createPersistentCookie: true);
			MyRoleProvider provider = new MyRoleProvider();
			var roles = provider.GetRolesForUser(user.Mail)?[0];
			return Json(new { roles });
		}

		// DELETE: api/Users/5
		[ResponseType(typeof(User))]
		//[Authorize(Roles = "Admin")]
		public async Task<IHttpActionResult> DeleteUser(int id)
		{
			User user = await db.Users.FindAsync(id);
			if (user == null)
			{
				return NotFound();
			}

			db.Users.Remove(user);
			await db.SaveChangesAsync();

			return Ok(user);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool UserExists(int id)
		{
			return db.Users.Count(e => e.Id == id) > 0;
		}
	}
}