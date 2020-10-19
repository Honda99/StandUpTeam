using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EngDolphin.Data
{
    public class Comment
    {
       
        [Key]
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime dateTime { get;set; }
        public virtual ApplicationUser AppUser { get; set; }
      
    }
}
