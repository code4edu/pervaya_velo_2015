using System;
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
using System.Net.Http.Headers;

namespace MyHackathon.Controllers.api
{
	public class FilesController : ApiController
	{
		private DBMapper db = new DBMapper();

		// GET: api/Files/5
		[ResponseType(typeof(File))]
		public async Task<HttpResponseMessage> GetFile(int id)
		{
			File file = await db.Files.FindAsync(id);
			if (file == null)
			{
				return new HttpResponseMessage(HttpStatusCode.NotFound);
			}

			HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
			var stream = new System.IO.FileStream(file.Path, System.IO.FileMode.Open);
			result.Content = new StreamContent(stream);
			result.Content.Headers.ContentType =
				new MediaTypeHeaderValue("application/octet-stream");
			result.Content.Headers.ContentDisposition.FileName = file.Name;
			return result;
		}

		// PUT: api/Files/5
		[ResponseType(typeof(void))]
		public async Task<IHttpActionResult> PutFile(int id, File file)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != file.Id)
			{
				return BadRequest();
			}
			SaveFile(file);
			db.Entry(file).State = EntityState.Modified;

			try
			{
				await db.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!FileExists(id))
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

		// POST: api/Files
		[ResponseType(typeof(File))]
		public async Task<IHttpActionResult> PostFile(File file)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			SaveFile(file);
			db.Files.Add(file);
			await db.SaveChangesAsync();

			return CreatedAtRoute("DefaultApi", new { id = file.Id }, file);
		}

		// DELETE: api/Files/5
		[ResponseType(typeof(File))]
		public async Task<IHttpActionResult> DeleteFile(int id)
		{
			File file = await db.Files.FindAsync(id);
			if (file == null)
			{
				return NotFound();
			}

			db.Files.Remove(file);
			await db.SaveChangesAsync();

			return Ok(file);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool FileExists(int id)
		{
			return db.Files.Count(e => e.Id == id) > 0;
		}

		private	void SaveFile(File file)
		{
			var path = @"c:\tmp\code4edu\" + new Guid().ToString();
			using (var sw = new System.IO.FileStream(path, System.IO.FileMode.OpenOrCreate))
			{
				sw.Write(file.Data, 0, file.Data.Length);
			}
			file.Path = path;
		}
	}
}