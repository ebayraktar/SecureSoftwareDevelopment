using SSDWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace SSDWebService.Filters
{
    public class SSDAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.RequestUri.AbsolutePath.EndsWith("/login") ||
                actionContext.Request.RequestUri.AbsolutePath.EndsWith("/register") ||
                (actionContext.Request.RequestUri.AbsolutePath.EndsWith("/roles") && actionContext.Request.Method == HttpMethod.Get)
                )
            {
                Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity("anonymous"), null);
                return;
            }
            //if (actionContext.Request.Headers.Authorization == null)
            //{
            //    actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized);
            //}
            //else
            //{
            //    var tokenKey = actionContext.Request.Headers.Authorization.Parameter;
            //    //System.Web.HttpContext.Current.User = "admin";
            //    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity("admin"), null);
            //}

            if (GetUserNameAndPassword(actionContext, out string username, out string password))
            {
                if (Membership.ValidateUser(username, password))
                {
                    if (!isUserAuthorized(username))
                        actionContext.Response =
                            new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
                }
                else
                {
                    HandleUnauthorizedRequest(actionContext);
                    //actionContext.Response =
                    //    new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                }
            }
            else
            {
                actionContext.Response =
                    new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
        }
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            //base.HandleUnauthorizedRequest(actionContext);
            if (((System.Web.HttpContext.Current.User).Identity).IsAuthenticated)
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
            }
            else
            {
                base.HandleUnauthorizedRequest(actionContext);
            }
        }

        bool isUserAuthorized(string username)
        {
            return Constants.Connection.Table<Users>().Where(x => x.UserName.Equals(username)).FirstOrDefault().RoleId == 4 ? false : true;
        }
        bool GetUserNameAndPassword(HttpActionContext actionContext, out string username, out string password)
        {
            LoginModel lModel;
            username = "";
            password = "";
            try
            {
                string parameter = $@"{actionContext.Request.Headers.Authorization?.Parameter}";
                if (string.IsNullOrEmpty(parameter))
                {
                    return false;
                }
                string token = FTH.Extension.Encrypter.Decrypt(parameter, Constants.FTHPassword);
                lModel = Newtonsoft.Json.JsonConvert.DeserializeObject<LoginModel>(token);
            }
            catch
            {
                return false;
            }
            username = lModel.Username;
            password = lModel.Password;
            return true;
        }

        public static class Membership
        {
            public static bool ValidateUser(string username, string password)
            {
                string pwd = Cryptohraphy.Crypthography.EncrypteData(password);
                return Constants.Connection.Table<Users>().Where(x => x.UserName.Equals(username) &
                x.Password.Equals(pwd)).FirstOrDefault() != null ? true : false;
            }
        }
    }
}