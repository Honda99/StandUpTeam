using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EngDolphin.Data
{
    public class UserComment
    {
        [Required]
        public string Text { get; set; }
    }
}
