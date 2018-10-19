using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lakehouse.Models
{
    public class LocationDescription
    {
        public int LocationDescriptionId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public ContentType Type { get; set; }
    }
}
