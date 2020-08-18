using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventApp.Models
{
    public class ApiLogEntry : AbstractEntity
    {
        public string UserName { get; set; }

        [Required]
        public DateTime RequestTime { get; set; }

        [Required]
        public long ResponseMillis { get; set; }

        [Required]
        public int StatusCode { get; set; }

        [Required]
        public string Method { get; set; }

        [Required]
        public string Path { get; set; }

        public string QueryString { get; set; }

        public string RequestBody { get; set; }

        public string ResponseBody { get; set; }
    }
}
