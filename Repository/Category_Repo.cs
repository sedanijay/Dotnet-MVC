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
    public class Category_Repo
    {
        private SqlConnection con;   
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["DB_conn_string"].ToString();
            con = new SqlConnection(constr);

        }


        //create
        public bool AddCategory(categoryModel obj)
        {

            connection();
            SqlCommand com = new SqlCommand("AddCategory", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@cat_name", obj.cat_name);

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

        public List<categoryModel> TotalRecords()
        {
            connection();
            con.Open();
            SqlCommand cmd = new SqlCommand("select dbo.fun_GetCategoriesCount()", con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            List<categoryModel> categories = new List<categoryModel>();

            categories.Add(
                        new categoryModel
                        {
                            PageCount = int.Parse(dt.Rows[0][0].ToString())
                        }
                    );

            con.Close();

            return categories;
        }


        //read
        public List<categoryModel> EditViewCategory()
        {
            connection();
            List<categoryModel> catList = new List<categoryModel>();


            SqlCommand com = new SqlCommand("EditViewCategory", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);
            con.Close();
            //Bind EmpModel generic list using dataRow     
            foreach (DataRow dr in dt.Rows)
            {
                catList.Add(
                    new categoryModel
                    {
                        cat_id = Convert.ToInt32(dr["cat_id"]),
                        cat_name = Convert.ToString(dr["cat_name"])
                    }
                );
            }

            return catList;
        }

        public List<categoryModel> GetAllCategories(int offset)
        {
            connection();

            List<categoryModel> catList = new List<categoryModel>();

            SqlCommand com = new SqlCommand("GetCategories", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@offset", (offset));
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);
            con.Close();
            //Bind EmpModel generic list using dataRow     
            foreach (DataRow dr in dt.Rows)
            {
                catList.Add(
                   new categoryModel
                   {
                        cat_id = Convert.ToInt32(dr["cat_id"]),
                        cat_name = Convert.ToString(dr["cat_name"])
                   }
                );
            }

            return catList;
        }

        //update
        public bool UpdateCategory(categoryModel obj)
        {

            connection();
            SqlCommand com = new SqlCommand("UpdateCategory", con);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@cat_id", obj.cat_id);
            com.Parameters.AddWithValue("@cat_name", obj.cat_name);
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
        public bool DeleteCategory(int Id)
        {

            connection();
            SqlCommand com = new SqlCommand("DeleteCategoryById", con);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@cat_id", Id);

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