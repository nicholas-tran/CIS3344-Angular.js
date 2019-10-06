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
    public class Restaurant
    {
        // class variables
        private string restName;
        private string phone;
        private string image;
        private string cuisine;
        private string city;
        private string state;
        private string foodRating;
        private string priceRating;

        // default constructor
        public Restaurant()
        {

        }

        public Restaurant(string restName, string phone, string image, string cuisine, string city, string state)
        {
            this.RestName = restName;
            this.Phone = phone;
            this.Image = image;
            this.Cuisine = cuisine;
            this.City = city;
            this.State = state;
        }

        public string RestName { get => restName; set => restName = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Image { get => image; set => image = value; }
        public string Cuisine { get => cuisine; set => cuisine = value; }
        public string City { get => city; set => city = value; }
        public string State { get => state; set => state = value; }
        public string FoodRating { get => foodRating; set => foodRating = value; }
        public string PriceRating { get => priceRating; set => priceRating = value; }

        public static string AddRestaurant(Restaurant restaurant)
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "AddRestaurant";
            objCommand.Parameters.AddWithValue("@name", restaurant.RestName);
            objCommand.Parameters.AddWithValue("@phone", restaurant.Phone);
            objCommand.Parameters.AddWithValue("@cuisine", restaurant.Cuisine);
            objCommand.Parameters.AddWithValue("@city", restaurant.City);
            objCommand.Parameters.AddWithValue("@state", restaurant.State);
            objCommand.Parameters.AddWithValue("@foodRating", restaurant.FoodRating);
            objCommand.Parameters.AddWithValue("@priceRating", restaurant.PriceRating);
            SqlParameter result = new SqlParameter("@result", DbType.Int32);
            result.Direction = ParameterDirection.Output;
            objCommand.Parameters.Add(result);
            objDB.DoUpdateUsingCmdObj(objCommand);

            int returnVal = Convert.ToInt32(objCommand.Parameters["@result"].Value);
            if (returnVal == 0)
            {
                return "This restaurant is already in our database!";
            }
            return "Restaurant added to database!";
        }

        public static string DeleteRestaurant(string name)
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "DeleteRestaurant";
            objCommand.Parameters.AddWithValue("@name", name);
            SqlParameter result = new SqlParameter("@result", DbType.Int32);
            result.Direction = ParameterDirection.Output;
            objCommand.Parameters.Add(result);
            objDB.DoUpdateUsingCmdObj(objCommand);

            int returnVal = Convert.ToInt32(objCommand.Parameters["@result"].Value);
            if (returnVal == 0)
            {
                return "This restaurant does not exist in our database!";
            }
            return "Restaurant deleted from database!";
        }

        public static List<Restaurant> GetRestaurantsByLocCuisine(string city, string state, string cuisine)
        {
            List<Restaurant> restList = new List<Restaurant>();

            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetLocationCuisine";
            objCommand.Parameters.AddWithValue("@city", city);
            objCommand.Parameters.AddWithValue("@state", state);
            objCommand.Parameters.AddWithValue("@cuisine", cuisine);

            DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);

            if (ds.Tables[0].Rows.Count == 0)
            {
                return null;
            }
            else
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Restaurant thisRest = new Restaurant();
                    thisRest.RestName = Convert.ToString(ds.Tables[0].Rows[i]["Name"]);
                    thisRest.Phone = Convert.ToString(ds.Tables[0].Rows[i]["Phone"]);
                    thisRest.Cuisine = Convert.ToString(ds.Tables[0].Rows[i]["Cuisine"]);
                    thisRest.City = Convert.ToString(ds.Tables[0].Rows[i]["City"]);
                    thisRest.State = Convert.ToString(ds.Tables[0].Rows[i]["State"]);
                    thisRest.FoodRating = Convert.ToString(ds.Tables[0].Rows[i]["FoodRating"]);
                    thisRest.PriceRating = Convert.ToString(ds.Tables[0].Rows[i]["PriceRating"]);
                    restList.Add(thisRest);
                }
            }
            return restList;

        }

        public static List<Restaurant> GetRestaurantsByLocRatings(string city, string state, string foodRating, string priceRating)
        {
            List<Restaurant> restList = new List<Restaurant>();

            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetLocationRatings";
            objCommand.Parameters.AddWithValue("@city", city);
            objCommand.Parameters.AddWithValue("@state", state);
            objCommand.Parameters.AddWithValue("@foodRating", foodRating);
            objCommand.Parameters.AddWithValue("@priceRating", priceRating);
            DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);

            if (ds.Tables[0].Rows.Count == 0)
            {
                return null;
            }
            else
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Restaurant thisRest = new Restaurant();
                    thisRest.RestName = Convert.ToString(ds.Tables[0].Rows[i]["Name"]);
                    thisRest.Phone = Convert.ToString(ds.Tables[0].Rows[i]["Phone"]);
                    thisRest.Cuisine = Convert.ToString(ds.Tables[0].Rows[i]["Cuisine"]);
                    thisRest.City = Convert.ToString(ds.Tables[0].Rows[i]["City"]);
                    thisRest.State = Convert.ToString(ds.Tables[0].Rows[i]["State"]);
                    thisRest.FoodRating = Convert.ToString(ds.Tables[0].Rows[i]["FoodRating"]);
                    thisRest.PriceRating = Convert.ToString(ds.Tables[0].Rows[i]["PriceRating"]);
                    restList.Add(thisRest);
                }
            }
            return restList;

        }

        public static List<Restaurant> GetRestaurantsByName(string name)
        {
            List<Restaurant> restList = new List<Restaurant>();

            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetRestaurantsByName";
            objCommand.Parameters.AddWithValue("@name", name);

            DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);

            if (ds.Tables[0].Rows.Count == 0)
            {
                return null;
            }
            else
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Restaurant thisRest = new Restaurant();
                    thisRest.RestName = Convert.ToString(ds.Tables[0].Rows[i]["Name"]);
                    thisRest.Phone = Convert.ToString(ds.Tables[0].Rows[i]["Phone"]);
                    thisRest.Cuisine = Convert.ToString(ds.Tables[0].Rows[i]["Cuisine"]);
                    thisRest.City = Convert.ToString(ds.Tables[0].Rows[i]["City"]);
                    thisRest.State = Convert.ToString(ds.Tables[0].Rows[i]["State"]);
                    thisRest.FoodRating = Convert.ToString(ds.Tables[0].Rows[i]["FoodRating"]);
                    thisRest.PriceRating = Convert.ToString(ds.Tables[0].Rows[i]["PriceRating"]);
                    restList.Add(thisRest);
                }
            }
            return restList;
        }

        public static List<Restaurant> GetAllRestaurants()
        {
            List<Restaurant> restList = new List<Restaurant>();

            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetAllRestaurants";

            DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);

            if (ds.Tables[0].Rows.Count == 0)
            {
                return null;
            }
            else
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Restaurant thisRest = new Restaurant();
                    thisRest.RestName = Convert.ToString(ds.Tables[0].Rows[i]["Name"]);
                    thisRest.Phone = Convert.ToString(ds.Tables[0].Rows[i]["Phone"]);
                    thisRest.Cuisine = Convert.ToString(ds.Tables[0].Rows[i]["Cuisine"]);
                    thisRest.City = Convert.ToString(ds.Tables[0].Rows[i]["City"]);
                    thisRest.State = Convert.ToString(ds.Tables[0].Rows[i]["State"]);
                    thisRest.FoodRating = Convert.ToString(ds.Tables[0].Rows[i]["FoodRating"]);
                    thisRest.PriceRating = Convert.ToString(ds.Tables[0].Rows[i]["PriceRating"]);
                    restList.Add(thisRest);
                }
            }
            return restList;
        }
    }
}
