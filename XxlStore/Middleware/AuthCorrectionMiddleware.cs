using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Text;
using XxlStore.Infrastructure;
using XxlStore.Models;

namespace XxlStore.Middleware
{
    public class AuthCorrectionMiddleware
    {
        private RequestDelegate next;

        public AuthCorrectionMiddleware(RequestDelegate nextDelegate)
        {
            next = nextDelegate;
        }
        public async Task Invoke(HttpContext context)
        {
            var identity = context.User?.Identity as ClaimsIdentity;
            var claims = identity?.Claims as List<Claim>;
            claims.RemoveAll(x => x.Type == identity.RoleClaimType);

            string UserName = context.User.Identity.Name;

            var DbUsers = Data.GetAllUsers();

            if (UserName != null) {

                
                var userDB = DbUsers.SingleOrDefault(x => x.Name.ToLower() == UserName.ToLower());

                
                //////////////////////////////////////////////
                StringBuilder SB = new StringBuilder();
                for (int i = 0; i < userDB.Roles.Count; i++) { 
                        SB.Append(userDB.Roles[i]);
                        if (i < userDB.Roles.Count - 1) {
                            SB.Append(", ");
                        }
                }
                await Console.Out.WriteLineAsync("UserName: " + UserName + "; Roles: " + SB);
                //////////////////////////////////////////////

                if (userDB is User user) {
                    context.Items[nameof(User)] = user;
                    foreach (var role in userDB.Roles) claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }

            await next(context);
        }
    }
}
