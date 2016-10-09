using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WFMVCProject.Models;
using System.Data;
using System.Data.SqlClient;

namespace WFMVCProject.Controllers
{
    public class ProductController : Controller
    {
        //Homework Question 2 
        public decimal OrderTotal(IList<Product> itemList, IList<Discount> discountList, string customerState, decimal shippingCost)
        {
            decimal orderTotal = 0.00M;

            //2. Shipping costs (free for in-state customers)
            if (customerState == "NC")
                shippingCost = 0;
            //1. Multiple quantities of different types of Hammers
            foreach (Product item in itemList)
            {
                //4. Generalize the discount mechanism to allow for different types of IDiscounts
                foreach (Discount discount in discountList)
                {
                    //Discount for specific Product where an amount is reduced
                    if (discount.ProductId == item.ProductId && discount.DiscountAmount > 0)
                        orderTotal = orderTotal + ((item.Cost * item.Quantity) - (item.Quantity * discount.DiscountAmount));
                    //Discount for specific Product where the amount is reduced by a percentage
                    else if (discount.ProductId == item.ProductId && discount.DiscountAmount > 0)
                        orderTotal = orderTotal + ((item.Cost * item.Quantity) * (1 - discount.DiscountPercentage));
                    //Volume break where an amount is reduced
                    //3. Discounts for high volume orders (50+)
                    else if (item.Quantity >= discount.VolumeBreak && discount.VolumeBreakDiscountAmount > 0)
                        orderTotal = orderTotal + ((item.Cost * item.Quantity) - (item.Quantity * discount.VolumeBreakDiscountAmount));
                    //Volumn break where the amount is reduced by a percentage
                    else if (item.Quantity >= discount.VolumeBreak && discount.VolumeBreakDiscountPercentage > 0)
                        orderTotal = orderTotal + ((item.Cost * item.Quantity) * (1 - discount.VolumeBreakDiscountPercentage));
                    //No Discount
                    else
                    {
                        orderTotal = orderTotal + (item.Cost * item.Quantity);
                    }
               }
                
            }

            orderTotal = shippingCost;

            return orderTotal;
        }

        //Homework Question 3
        public IList<Product> RecommendedProductsPurchased(IList<Product> cartList, IList<Product> recommendedList)
        {
            IEnumerable<Product> recommendedProductsPurchased = recommendedList.Where(a => cartList.Any(b => b.ProductId == a.ProductId));

            return recommendedProductsPurchased.ToList();

        }

        //Homework Question 4 - Use Parameters with Dynamic SQL
        public DataTable SearchProducts()
        {
            //string criteria = txtSearchBox.Text;
            string criteria = "Test";
            string _connection = string.Empty;
            SqlCommand cmd = new SqlCommand();
            DataSet userDataset = new DataSet();
            using (SqlConnection connection = new SqlConnection(_connection))
            {
                SqlDataAdapter myDataAdapter = new SqlDataAdapter(
                       "SELECT * FROM Hammers WHERE HammerName LIKE '%@criteria%'",
                       connection);
                cmd.Parameters.Add("@criteria", SqlDbType.VarChar, 11);
                cmd.Parameters["@au_id"].Value = criteria;
                myDataAdapter.Fill(userDataset);
            }
            DataTable dt = userDataset.Tables[0];
            return dt;
        }
    }
}