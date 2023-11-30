using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2.Response
{
    public class Error
    {

        public Error(string status, string title, string detail)
        {
            Errors.Add("status", status);
            Errors.Add("title", title);
            Errors.Add("details", detail);
        }

        public Dictionary<string, string> Errors { get; set; } = new Dictionary<string, string>();
    }
}