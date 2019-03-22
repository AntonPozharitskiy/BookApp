using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Requests
{
    public class RequestBookModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DateOfRelease { get; set; }
        public string AuthorId { get; set; }
        public string Content { get; set; }
    }
}
