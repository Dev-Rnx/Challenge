using System.Collections.Generic;

namespace Take_Api.Models
{
    public class Avatar
    {
        public string avatar_url { get; set; }
    }
    public class Repositorio
    {
        public List<Repos> repos { get; set; }
    }

    public class Repos
    {
        public string name { get; set; }
        public string description { get; set; }
        public string language { get; set; }
        public string html_url { get; set; }
        public string created_at { get; set; }
    }

    public class Request
    {
        public string code { get; set; }
    }

    public class Response
    {
        public string avatar { get; set; }
        public List<Repos> repos { get; set; }       
    }
}