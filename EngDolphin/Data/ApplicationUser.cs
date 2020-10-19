using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EngDolphin.Data
{
    public class ApplicationUser
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("CustomIdName")]

        public Guid Uuid { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
