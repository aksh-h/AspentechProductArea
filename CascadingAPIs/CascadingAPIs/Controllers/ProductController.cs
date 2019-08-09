using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using CascadingAPIs.Models;

namespace CascadingAPIs.Controllers
{
    public class ProductController : ApiController
    {
        public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString.ToString());


        [HttpGet]
        [Route("api/family")]
        public HttpResponseMessage GetFamily()
        {
            List<Family> families = new List<Family>();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("[dbo].[usp_GetFamilies]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Family family = new Family
                {
                    ID = Convert.ToInt32(dr["Id"].ToString()),
                    FamilyName = dr["FamilyName"].ToString()
                };
                families.Add(family);
            }
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            return Request.CreateResponse(HttpStatusCode.OK, families);
        }

        [HttpGet]
        [Route("api/product/{family}")]
        public HttpResponseMessage GetProduct(string family)
        {
            ProductList ProductsList = new ProductList();
            ProductsList.Products = new List<Product>();
            if (con.State== ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("usp_GetProducts", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FamilyName", family);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Product product = new Product
                {
                    ID = Convert.ToInt32(dr["ID"].ToString()),
                    ProductName = dr["ProductName"].ToString()
                };
                ProductsList.Products.Add(product);
            }
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            return Request.CreateResponse(HttpStatusCode.OK, ProductsList);
        }

        [HttpGet]
        [Route("api/Area/{Product}")]
        public HttpResponseMessage GetArea(string Product)
        {
            AreaList AreaList = new AreaList();
            AreaList.Areas = new List<Area>();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("usp_GetAreas", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProductName", Product);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Area area = new Area
                {
                    AreaName = dr["AreaName"].ToString()
                };
                AreaList.Areas.Add(area);
            }
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            return Request.CreateResponse(HttpStatusCode.OK, AreaList);
        }

        [HttpGet]
        [Route("api/SubArea/{Area}")]
        public HttpResponseMessage GetSubArea(string Area)
        {
            SubAreaList SubAreaList = new SubAreaList();
            SubAreaList.SubAreas = new List<SubArea>();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("usp_GetSubAreas", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AreaName", Area);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                SubArea city = new SubArea
                {
                    SubAreaName = dr["SubAreaName"].ToString()
                };
                SubAreaList.SubAreas.Add(city);
            }
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            return Request.CreateResponse(HttpStatusCode.OK, SubAreaList);
        }


        [HttpGet]
        [Route("api/SubArea/{Product}/{Area}")]
        public HttpResponseMessage GetSubArea(string Area,string Product)
        {
            SubAreaList SubAreaList = new SubAreaList();
            SubAreaList.SubAreas = new List<SubArea>();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("usp_GetSubAreasFromSheet", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AreaName", Area);
            cmd.Parameters.AddWithValue("@ProductName", Product);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                SubArea city = new SubArea
                {
                    //ID = Convert.ToInt32(dr["ID"].ToString()),
                    SubAreaName = dr["SubAreaName"].ToString()
                };
                SubAreaList.SubAreas.Add(city);
            }
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            return Request.CreateResponse(HttpStatusCode.OK, SubAreaList);
        }
    }
}
