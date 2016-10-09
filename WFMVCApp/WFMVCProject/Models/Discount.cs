using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WFMVCProject.Models
{
    public class Discount
    {
        public string Name;
        public int ProductId;
        public decimal DiscountAmount;
        public decimal DiscountPercentage;
        public int VolumeBreak;
        public decimal VolumeBreakDiscountAmount;
        public decimal VolumeBreakDiscountPercentage;
    }
}