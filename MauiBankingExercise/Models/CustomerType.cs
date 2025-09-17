using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBankingExercise.Models
{
    public class CustomerType
    {
        
        public int CustomerTypeId { get; set; }
        public string Name { get; set; }


        public ICollection<Customer> Customers { get; set; }
    }

}

