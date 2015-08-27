namespace BookShop.WebApi.Controllers
{
    using System.Linq;
    using System.Web.Http;

    using Microsoft.AspNet.Identity.EntityFramework;

    using AutoMapper;

    using Data;
    using Models;

    public class UsersController : BaseApiController
    {
        public UsersController(IBookShopData data)
            : base(data)
        {
        }

        // PUT api/users/{username}/roles
        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("api/users/{username}/roles")]
        public IHttpActionResult AddRoleToUser(string username, [FromBody] RoleBindingModel role)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var user = this.data.Users.Search(u => u.UserName == username).FirstOrDefault();
            if (null == user)
            {
                return this.BadRequest(string.Format("User with username {0} does not exist.", username));
            }

            var existingRole = this.data.Roles.Search(r => r.Name == role.Name).FirstOrDefault();
            if (existingRole != null)
            {
                return this.BadRequest("Role with the requested name already exists.");
            }

            var dbRole = Mapper.Map<IdentityRole>(role);
            this.data.Roles.Add(dbRole);

            var roleUser = new IdentityUserRole() { UserId = user.Id };
            if (dbRole.Users.Any(u => u.UserId == user.Id))
            {
                return this.BadRequest("The role has already been assigned to this user.");
            }

            dbRole.Users.Add(roleUser);
            this.data.SaveChanges();

            return this.Ok();
        }

        // DELETE api/users/{username}/roles
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("api/users/{username}/roles")]
        public IHttpActionResult DeleteRoleFromUser(string username, [FromBody] RoleBindingModel roleModel)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var user = this.data.Users.Search(u => u.UserName == username).FirstOrDefault();
            if (null == user)
            {
                return this.BadRequest(string.Format("User with username {0} does not exist.", username));
            }

            var existingRole = this.data.Roles.Search(r => r.Name == roleModel.Name).FirstOrDefault();
            if (existingRole == null)
            {
                return this.BadRequest("Role with the requested name does not exist.");
            }

            var existingUserRole = this.data.UserRoles.Search(ur => ur.UserId == user.Id && ur.RoleId == existingRole.Id).FirstOrDefault();
            if (null == existingUserRole)
            {
                return this.BadRequest("The relationship between the requested user and role does not exist.");
            }

            existingRole.Users.Remove(existingUserRole);
            this.data.SaveChanges();
            return this.Ok();
        }
    }
}