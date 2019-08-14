using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CascadingAPIs.Models
{
    public class FamilytList
    {
        public List<Family> Families { get; set; }
    }

    public class ProductList
    {
        public List<Product> Products { get; set; }
    }

    public class AreaList
    {
        public List<Area> Areas { get; set; }
    }

    public class SubAreaList
    {
        public List<SubArea> SubAreas { get; set; }
    }
    public class Product
    {
        public int ID { get; set; }
        public string ProductName { get; set; }
    }

    public class Area
    {
        public int ID { get; set; }
        public string AreaName { get; set; }

    }

    public class SubArea
    {
        public int ID { get; set; }
        public string SubAreaName { get; set; }

    }
    public class Family
    {
        public int ID { get; set; }
        public string FamilyName { get; set; }

    }
    public class SuiteAndProductLine
    {
        public string SuiteName { get; set; }
        public string ProductLine { get; set; }
    }
}