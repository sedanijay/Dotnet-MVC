using InterviewTask.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InterviewTask.Repository
{
    public class Product_Repo
    {
        private SqlConnection con;
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["DB_conn_string"].ToString();
            con = new SqlConnection(constr);

        }

        public List<productModel> TotalRecords()
        {
            connection();
            con.Open();
            SqlCommand cmd = new SqlCommand("select dbo.fun_GetProductsCount()", con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            List<productModel> product = new List<productModel>();

            product.Add(
                        new productModel
                        {
                            PageCount = int.Parse(dt.Rows[0][0].ToString())
                        }
                    );

            con.Close();

            return product;
        }


        //create
        public bool AddProduct(productModel obj)
        {

            connection();
            SqlCommand com = new SqlCommand("AddProduct", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@cat_id", obj.cat_id);
            com.Parameters.AddWithValue("@prod_name", obj.prod_name);

            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {

                return true;

            }
            else
            {

                return false;
            }


        }

        //read
        public List<productModel> EditViewProduct()
        {
            connection();

            List<productModel> product = new List<productModel>();

            SqlCommand com = new SqlCommand("EditViewProduct", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);
            con.Close();
            //Bind Model generic list using dataRow     
            foreach (DataRow dr in dt.Rows)
            {

                product.Add(
                    new productModel
                    {
                        prod_id = Convert.ToInt32(dr["prod_id"]),
                        prod_name = Convert.ToString(dr["prod_name"]),
                        cat_id = Convert.ToInt32(dr["cat_id"]),
                        cat_name = Convert.ToString(dr["cat_name"])
                    }
                );
            }

            return product;
        }

        public List<productModel> GetAllProducts(int offset)
        {
            connection();

            List<productModel> product = new List<productModel>();

            SqlCommand com = new SqlCommand("GetProducts", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@offset", (offset));
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);
            con.Close();
            //Bind Model generic list using dataRow     
            foreach (DataRow dr in dt.Rows)
            {

                product.Add(
                    new productModel
                    {
                        prod_id = Convert.ToInt32(dr["prod_id"]),
                        prod_name = Convert.ToString(dr["prod_name"]),
                        cat_id = Convert.ToInt32(dr["cat_id"]),
                        cat_name = Convert.ToString(dr["cat_name"])
                    }
                );
            }

            return product;
        }

        //update
        public bool UpdateProduct(productModel obj)
        {

            connection();
            SqlCommand com = new SqlCommand("UpdateProduct", con);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@prod_id", obj.prod_id);
            com.Parameters.AddWithValue("@cat_id", obj.cat_id);
            com.Parameters.AddWithValue("@prod_name", obj.prod_name);
            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {

                return true;
            }
            else
            {
                return false;
            }
        }

        //delete
        public bool DeleteProduct(int Id)
        {

            connection();
            SqlCommand com = new SqlCommand("DeleteProductById", con);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@prod_id", Id);

            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
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