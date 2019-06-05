using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Net_MVC_Workshop2.Models
{
    public class BookServices
    {
        private string GetDBConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString.ToString();
        }

        public List<Models.Book> GetBookByCondtioin(Models.BookSearchArg book)
        {

            DataTable dt = new DataTable();
            string sql = @"Select BOOK_CLASS_ID,BOOK_NAME,BOOK_STATUS,BOOK_KEEPER,BOOK_BOUGHT_DATE
                           From [dbo].[BOOK_DATA] as b
                           Where(b.BOOK_CLASS_ID = @Book_Class_Id OR @Book_Class_Id='') AND
                           (b.BOOK_NAME  LIKE ('%'+ @Book_Name +'%') OR @Book_Name='') AND
                           (b.BOOK_STATUS = @Book_Status OR @Book_Status='') AND
                           (b.BOOK_KEEPER = @Book_Keeper OR @Book_Keeper='')
                           ";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@Book_Class_Id", book.BOOK_CLASS_ID == null ? string.Empty : book.BOOK_CLASS_ID));
                cmd.Parameters.Add(new SqlParameter("@Book_Name", book.BOOK_NAME == null ? string.Empty : book.BOOK_NAME));
                cmd.Parameters.Add(new SqlParameter("@Book_Status", book.BOOK_STATUS == null ? string.Empty : book.BOOK_STATUS));
                cmd.Parameters.Add(new SqlParameter("@Book_Keeper", book.BOOK_KEEPER == null ? string.Empty : book.BOOK_KEEPER));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapBookDataToList(dt);
        }
        /// <summary>
        /// Map資料進List
        /// </summary>
        /// <param name="employeeData"></param>
        /// <returns></returns>

        private List<Models.Book> MapBookDataToList(DataTable bookData)
        {
            List<Models.Book> result = new List<Book>();
            foreach (DataRow row in bookData.Rows)
            {
                result.Add(new Book()
                {
                    BOOK_CLASS_ID = row["BOOK_CLASS_ID"].ToString(),
                    BOOK_NAME = row["BOOK_NAME"].ToString(),
                    BOOK_BOUGHT_DATE = row["BOOK_BOUGHT_DATE"].ToString(),
                    BOOK_STATUS = row["BOOK_STATUS"].ToString(),
                    BOOK_KEEPER = row["BOOK_KEEPER"].ToString(),

                });
            }

            return result;
        }
    }
}