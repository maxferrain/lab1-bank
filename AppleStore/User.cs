using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bank
{
    public class User
    {
        public int id { get; set; }
        public string fullName { get; set; }
        
        public List<Account> Accounts { get; set; }

        //[ForeignKey("Technologyid")]
        //public virtual Technology Technology { get; set; }
    }
}
