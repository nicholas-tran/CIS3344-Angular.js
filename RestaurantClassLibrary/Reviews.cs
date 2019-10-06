using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace RestaurantClassLibrary
{
    public class Reviews
    {
        private string restName;
        private string foodRating;
        private string priceRating;
        private string comment;

        // default constructor
        public Reviews()
        {
        }

        // parameterized constructor
        public Reviews(string restName, string foodRating, string priceRating, string comment)
        {
            this.RestName = restName;
            this.FoodRating = foodRating;
            this.PriceRating = priceRating;
            this.Comment = comment;
        }

        // getters and setters
        public string RestName { get => restName; set => restName = value; }
        public string FoodRating { get => foodRating; set => foodRating = value; }
        public string PriceRating { get => priceRating; set => priceRating = value; }
        public string Comment { get => comment; set => comment = value; }

        public Boolean AddReview(string name, string quality, string price, string comment)
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "AddReview";
            objCommand.Parameters.AddWithValue("@name", name);
            objCommand.Parameters.AddWithValue("@foodRating", quality);
            objCommand.Parameters.AddWithValue("@priceRating", price);
            objCommand.Parameters.AddWithValue("@comment", comment);
            objDB.DoUpdateUsingCmdObj(objCommand);
            int returnVal = Convert.ToInt32(objCommand.Parameters["@result"].Value);
            if (returnVal == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
