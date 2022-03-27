using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Bank

{
    public class Account
    {
        [Key]
        public int id { get; set; }
        public double totalSum { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
