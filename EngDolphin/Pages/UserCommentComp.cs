using EngDolphin.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EngDolphin.Pages
{
    public class UserCommentComp:ComponentBase
    {
        protected UserComment userComment = new UserComment();
        [Inject]
        protected ApplicationDbContext appDatabase { get; set; }

        [Inject]
        SignInManager<IdentityUser> signInManager { get; set; }
        [Inject]
        UserManager<IdentityUser> userManager { get; set; }
        [Inject]
        IHttpContextAccessor _httpAccessor { get; set; }
        protected List<Comment> AllComments = new List<Comment>();
        protected void HandleValidSubmit()
        {
            Console.WriteLine("OnValidSubmit");
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            var userId = _httpAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await userManager.FindByIdAsync(userId.ToString());
            if (user.Email == "tryhon@yahoo.com")
            {
                if (appDatabase.ApplicationUsers.Include(co => co.Comments).Any())
                {
                    var appUser = appDatabase.ApplicationUsers
                                  .Include(co => co.Comments)
                                  .FirstOrDefault();
                    AllComments = appUser.Comments.ToList();
                }
            }
            else
            {
                if (appDatabase.ApplicationUsers
              .Where(c => c.Uuid == Guid.Parse(userId))
              .Include(co => co.Comments).Any())
                {
                    var appUser = appDatabase.ApplicationUsers
            .Where(c => c.Uuid == Guid.Parse(userId))
            .Include(co => co.Comments)
            .FirstOrDefault();
                    AllComments = appUser.Comments.ToList();
                }

            }
        }
        protected override async Task OnInitializedAsync()
        {
            base.OnInitialized();
            var userId = _httpAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await userManager.FindByIdAsync(userId.ToString());
            if (user.Email == "tryhon@yahoo.com")
            {
                if (appDatabase.ApplicationUsers.Include(co => co.Comments).Any())
                {
                    var appUser = appDatabase.ApplicationUsers
                                  .Include(co => co.Comments)
                                  .FirstOrDefault();
                                  AllComments = appUser.Comments.ToList();
                }
            }
            else
            {
                if (appDatabase.ApplicationUsers
              .Where(c => c.Uuid == Guid.Parse(userId))
              .Include(co => co.Comments).Any())
                {
                    var appUser = appDatabase.ApplicationUsers
            .Where(c => c.Uuid == Guid.Parse(userId))
            .Include(co => co.Comments)
            .FirstOrDefault();
                    AllComments = appUser.Comments.ToList();
                }
            }
        }

        protected void SubmitComment()
        {
            Comment com1 = new Comment() { Text =userComment.Text,dateTime=DateTime.Now };
            var userId =  _httpAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(appDatabase.ApplicationUsers
                .Where(c => c.Uuid == Guid.Parse(userId))
                .Include(co => co.Comments).Any())
            {
                var appUser = appDatabase.ApplicationUsers
              .Where(c => c.Uuid == Guid.Parse(userId))
              .Include(co => co.Comments)
              .FirstOrDefault();
                appUser.Comments.Add(com1);
                AllComments = appUser.Comments.ToList();
                appDatabase.SaveChanges();
            }
            else
            {
              //  var appUser = appDatabase.ApplicationUsers
              //.Where(c => c.Uuid == Guid.Parse(userId));
                ICollection<Comment> com=new List<Comment>();
                com.Add(com1);
                ApplicationUser app = new ApplicationUser() {Uuid= Guid.Parse(userId), Comments = com };
                appDatabase.Add(app);
                //appUser.Comments = new List<Comment>();
                //appUser.Comments.Add(com1);
                appDatabase.SaveChanges();
            }
        }
        protected async Task  DeleteComment(MouseEventArgs e, int id)
        {
           
            var userId = _httpAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await userManager.FindByIdAsync(userId.ToString());
            if (user.Email == "tryhon@yahoo.com")
            {
                var appUser = appDatabase.ApplicationUsers
              .Include(co => co.Comments)
              .FirstOrDefault();
                var item = appUser.Comments.SingleOrDefault(c => c.Id == id);
                if (item != null) appUser.Comments.Remove(item);
                AllComments = appUser.Comments.ToList();
                appDatabase.SaveChanges();
            }
            else
            {
                var appUser = appDatabase.ApplicationUsers
                .Where(c => c.Uuid == Guid.Parse(userId))
                .Include(co => co.Comments)
                .FirstOrDefault();
                var item = appUser.Comments.SingleOrDefault(c => c.Id == id);
                if (item != null) appUser.Comments.Remove(item);
                AllComments = appUser.Comments.ToList();
                appDatabase.SaveChanges();
            }
           
        }
    }
}
