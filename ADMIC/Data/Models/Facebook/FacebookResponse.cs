using System;
using System.Collections.Generic;

namespace ADMIC.Data.Models.Facebook
{

    public class From
    {
        public string name { get; set; }
        public string id { get; set; }
    }


    public class Post
    {
        public string message { get; set; }
        public string id { get; set; }
        public DateTime? created_time { get; set; }
        public string story { get; set; }
        public string full_picture { get; set; }
        public string permalink_url { get; set; }
        public From from { get; set; }

        public string TimeLabel
        {
            get
            {
                return created_time?.ToString("dddd d-M-yyyy");
            }
        }
    }

    public class Cursors
    {
        public string after { get; set; }
        public string before { get; set; }
    }

    public class Paging
    {
        public Cursors cursors { get; set; }
        public string next { get; set; }
    }

    public class FacebookResponse
    {
        public List<Post> data { get; set; }
        public Paging paging { get; set; }
    }

}
