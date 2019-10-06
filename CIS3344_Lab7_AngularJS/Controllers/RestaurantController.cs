using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestaurantClassLibrary;

using System.Data;              // import needed for DataSet and other data classes
using System.Data.SqlClient;    // import needed for ADO.NET classes
using Utilities;                // import needed for DBConnect class
using Newtonsoft.Json.Serialization;

namespace CIS3344_Lab7_AngularJS.Controllers
{
    [Produces("application/json")]
    //[Route("api/Restaurant")]
    [Route("api/[controller]/")]
    public class RestaurantController : Controller
    {

        // GET: api/Restaurant
        // Gets all restaurants
        [HttpGet]
        public List<Restaurant> Get()
        {
            List<Restaurant> restList = Restaurant.GetAllRestaurants();
            return restList;
        }

        // GET: api/Restaurant/{name}
        [HttpGet("{name}")]
        public List<Restaurant> Get(string name)
        {
            List<Restaurant> restList = Restaurant.GetRestaurantsByName(name);
            return restList;
        }

        // GET: api/Restaurant/{city}/{state}/{cuisine}
        [HttpGet("{city}/{state}/{cuisine}")]
        public List<Restaurant> Get(string city, string state, string cuisine)
        {
            List<Restaurant> restList = Restaurant.GetRestaurantsByLocCuisine(city, state, cuisine);
            return restList;

        }

        // GET: api/Restaurant/{city}/{state}/{foodReview}/{priceRating}
        [HttpGet("{city}/{state}/{foodRating}/{priceRating}")]
        public List<Restaurant> Get(string city, string state, string foodRating, string priceRating)
        {
            List<Restaurant> restList = Restaurant.GetRestaurantsByLocRatings(city, state, foodRating, priceRating);
            return restList;
        }

        // POST: api/Restaurant
        [HttpPost("AddRestaurant")]
        public Boolean Post([FromBody]Restaurant restaurant)
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
                return true;
            }
            else {
                return false;
            }

        }

        // POST: api/Restaurant
        [HttpPost("AddReview")]
        public Boolean AddReview([FromBody] Reviews review)
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "AddReview";
            objCommand.Parameters.AddWithValue("@name", review.RestName);
            objCommand.Parameters.AddWithValue("@quality", review.FoodRating);
            objCommand.Parameters.AddWithValue("@price", review.PriceRating);
            objCommand.Parameters.AddWithValue("@comment", review.Comment);
            objDB.DoUpdateUsingCmdObj(objCommand);
            int returnVal = objDB.DoUpdateUsingCmdObj(objCommand);
            if (returnVal == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpPut("DeleteRestaurant")]
        public Boolean DeleteRestaurant([FromBody] Restaurant restaurant)
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "DeleteRestaurant";
            objCommand.Parameters.AddWithValue("@name", restaurant.RestName);
            int returnVal = objDB.DoUpdateUsingCmdObj(objCommand);
            if (returnVal == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    } // end class
} // end namespace

