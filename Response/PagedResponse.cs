using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Assignment2.Response
{
    public class PagedResponse<T>
    {

        public Dictionary<string, int> Meta { get; set; } = new Dictionary<string, int>();
        public List<T> Data { get; set; }
        public Dictionary<string, string> Links { get; set; } = new Dictionary<string, string>();

        public PagedResponse(List<T> data)
        {
            Data = data;
        }

        public void AddMeta(int totalPages, int totalRecords)
        {
            Meta.Add("TotalPages", totalPages);
            Meta.Add("TotalRecords", totalRecords);
        }

        public void AddLinks(string urlString, int pagenumber, int pagesize, int imagesInDb)
        {
            Links.Add("first", $"{urlString}?pagenumber=1");

            //only adds "previous" if its not the first page.
            if (pagenumber > 1)
                Links.Add("prev", $"{urlString}?pagenumber={pagenumber - 1}");

            //only adds "next" if theres more items than current page can have.
            if (imagesInDb > pagesize)
                Links.Add("next", $"{urlString}?pagenumber={pagenumber + 1}");
            Links.Add("last", $"{urlString}?pagenumber={(int)Math.Ceiling(imagesInDb * 1.0 / pagesize * 1.0)}");

        }
    }
}