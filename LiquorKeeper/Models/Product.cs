using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiquorKeeper.Models
{
    public class Product
    {
        public int ID { get; set; }
        //type
        public string Name { get; set; }
        public string Description { get; set; }
    }
}