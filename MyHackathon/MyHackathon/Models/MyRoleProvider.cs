using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace MyHackathon.Models
{
	public class MyRoleProvider : RoleProvider
	{
		// TODO: refactor
		public override string[] GetAllRoles() { return new[] { Models.ClientModels.Roles.Admin.ToString(), Models.ClientModels.Roles.Professor.ToString(), Models.ClientModels.Roles.Student.ToString() }; }
		public override string[] GetRolesForUser(string mail)
		{
			using (var db = new Models.ClientModels.DBMapper())
			{
				var user = db.Users.FirstOrDefault(l => l.Mail == mail);
				if (user != null)
				{
					var list = new List<string>();
					if (user.Role == ClientModels.Roles.Admin)
					{
						list.Add(Models.ClientModels.Roles.Admin.ToString());
					}
					if (user.Role == ClientModels.Roles.Professor)
					{
						list.Add(Models.ClientModels.Roles.Professor.ToString());
					}
					if (user.Role == ClientModels.Roles.Student)
					{
						list.Add(Models.ClientModels.Roles.Student.ToString());
					}
					return list.ToArray();
				}
			}
			return new string[0];
		}

		#region not implemented
		public override string ApplicationName
		{
			get
			{
				throw new NotImplementedException();
			}

			set
			{
				throw new NotImplementedException();
			}
		}

		public override void AddUsersToRoles(string[] usernames, string[] roleNames)
		{
			throw new NotImplementedException();
		}

		public override void CreateRole(string roleName)
		{
			throw new NotImplementedException();
		}

		public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
		{
			throw new NotImplementedException();
		}

		public override string[] FindUsersInRole(string roleName, string usernameToMatch)
		{
			throw new NotImplementedException();
		}

		public override string[] GetUsersInRole(string roleName)
		{
			throw new NotImplementedException();
		}

		public override bool IsUserInRole(string username, string roleName)
		{
			throw new NotImplementedException();
		}

		public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
		{
			throw new NotImplementedException();
		}

		public override bool RoleExists(string roleName)
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}