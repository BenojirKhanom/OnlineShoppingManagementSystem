using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AspNetCoreProject.Models
{
    public class CommonActionFilter : IActionFilter
    {
        public IConfiguration _configuration;
        public CommonActionFilter(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
           

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var dbconOptions = new DbContextOptionsBuilder<ApplicationDbContext>();

            dbconOptions.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            ApplicationDbContext db = new ApplicationDbContext(dbconOptions.Options);

            var MenuPermiBasedOnRoll = from mm in db.MenuModel
                                       join mhm in db.MenuHelperModel on mm.MenuHelperModelId equals mhm.Id
                                       join ro in db.Roles.ToList() on mm.RollId equals ro.Id
                                       join mmm in db.MenuModelManage on mm.Id equals mmm.MenuModelId
                                       select new
                                       {
                                           ControllerName = mhm.ControllerName,
                                           ActionName = mhm.ActionName,
                                           RollName = ro.Name,
                                           Insert = mmm.Insert,
                                           Delete = mmm.Delete,
                                           Update = mmm.Update,
                                           Retrive = mmm.Retrive
                                       };
            var contName = context.RouteData.Values["controller"];
            var actName = context.RouteData.Values["action"];
            var metName = context.HttpContext.Request.Method;

            bool Retrive = false;
            bool Insert = false;
            bool Delete = false;
            bool Update = false;

            if (actName.ToString() == "Details" && metName.ToString() == "GET")
            {
                Retrive = true;
            }

            if (actName.ToString() == "Index" && metName.ToString() == "GET")
            {
                Retrive = true;
            }

            if (actName.ToString() == "Create" && metName.ToString() == "POST")
            {
                Insert = true;
            }

            if (actName.ToString() == "Edit" && metName.ToString() == "POST")
            {
                Update = true;
            }

            if (actName.ToString() == "Delete" && metName.ToString() == "POST")
            {
                Delete = true;
            }


            var allRollInClaims = context.HttpContext.User.Claims.Where(w => w.Type == ClaimTypes.Role).ToList();
            bool permitted = false;
            foreach (var loopClaimRoll in allRollInClaims)
            {
                if (Retrive)
                {
                    permitted = MenuPermiBasedOnRoll.Where(w => w.RollName == loopClaimRoll.Value && w.Retrive == Retrive && w.ControllerName == contName.ToString() && w.ActionName == actName.ToString()).Any();
                    if (permitted)
                    {
                        break;
                    }
                }
                else if (Insert)
                {
                    permitted = MenuPermiBasedOnRoll.Where(w => w.RollName == loopClaimRoll.Value && w.Insert == Insert && w.ControllerName == contName.ToString() && w.ActionName == actName.ToString()).Any();
                    if (permitted)
                    {
                        break;
                    }

                }
                else if (Delete)
                {
                    permitted = MenuPermiBasedOnRoll.Where(w => w.RollName == loopClaimRoll.Value && w.Delete == Delete && w.ControllerName == contName.ToString() && w.ActionName == actName.ToString()).Any();
                    if (permitted)
                    {
                        break;
                    }


                }
                else
                {
                    permitted = MenuPermiBasedOnRoll.Where(w => w.RollName == loopClaimRoll.Value && w.Update == Update && w.ControllerName == contName.ToString() && w.ActionName == actName.ToString()).Any();
                    if (permitted)
                    {
                        break;
                    }
                }
            }


            if (!permitted)
            {
                Controller controller = context.Controller as Controller;

                context.Result = controller.RedirectToAction("Login", "Account");

            }




        }
    }
}
