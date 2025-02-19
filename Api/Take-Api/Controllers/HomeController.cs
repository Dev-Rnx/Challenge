﻿using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Take_Api.Models;

namespace Take_Api.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                Response.Clear();
                Response.Cache.SetExpires(DateTime.UtcNow.AddYears(-2));
                Response.Cache.SetNoStore();
                Response.Cache.SetValidUntilExpires(false);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ExpiresAbsolute = DateTime.Now.Subtract(new TimeSpan(1, 0, 0, 0));
                Response.Expires = -1;
                Response.CacheControl = "no-cache";
                Response.AppendHeader("Pragma", "no-cache");
                Response.Cache.SetNoServerCaching();
                Response.ContentType = "application/json";

                var avatar = CarregarAvatar();
                var repos = CarregarRepositorios();

                List<Repos> list = new List<Repos>();

                foreach (var item in repos)
                {
                    if (item.language == "C#" && list.Count <= 4)
                    {
                        list.Add(item);
                    }
                }

                Response response = new Response()
                {
                    avatar = avatar.avatar_url,
                    repos = list
                };

                var json = JsonConvert.SerializeObject(response, Formatting.Indented);
                return Content(json);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private Avatar CarregarAvatar()
        {
            var client = new RestClient("https://api.github.com/users/takenet");
            var request = new RestRequest();
            var response = client.Execute(request);
            var repos = JsonConvert.DeserializeObject<Avatar>(response.Content);
            return repos;
        }

        public List<Repos> CarregarRepositorios()
        {
            var client = new RestClient("https://api.github.com/orgs/takenet/repos");
            var request = new RestRequest();
            var response = client.Execute(request);
            var repos = JsonConvert.DeserializeObject<List<Repos>>(response.Content);
            return repos;
        }
    }
}
