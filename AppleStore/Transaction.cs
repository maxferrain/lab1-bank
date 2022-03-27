using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bank
{
    public class Transaction
    {
        public int id { get; set; }
        public double amount { get; set; }
        //public string reason { get; set; }
        //public DateTime date { get; set; }

        [ForeignKey("AccountIdFrom")]
        public virtual int accountIdFrom { get; set; }
        [ForeignKey("AccountIdTo")]
        public virtual int accountIdTo { get; set; }

        //public int yearIssue { get; set; }
        //public virtual List<Product> Products { get; set; }
    }
}
