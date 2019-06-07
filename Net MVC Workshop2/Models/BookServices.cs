using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
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
        //圖書類別下拉式
        public List<Models.BookClass> GetBookClass(Models.BookClass BookClass)
        {
            DataTable dt = new DataTable();
            string sql = @"Select  *
                           From [dbo].[BOOK_CLASS] ";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapBookClassDataToList(dt);
        }
        //借閱人下拉式
        public List<Models.BookKeeper> GetBookKeeper(Models.BookKeeper BookKeeper)
        {
            DataTable dt = new DataTable();
            string sql = @"Select  *
                           From [dbo].[MEMBER_M] ";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapBookKeeperDataToList(dt);
        }
        //借閱狀態下拉式
        public List<Models.BookStatus> GetBookStatus(Models.BookStatus BookStatus)
        {
            DataTable dt = new DataTable();
            string sql = @"Select  *
                           From [dbo].[BOOK_CODE] ";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapBookStatusDataToList(dt);
        }
        //查詢
        public List<Models.BookSearchArg> GetBookByCondtioin(Models.BookSearchArg Book)
        {

            DataTable dt = new DataTable();
            string sql = @"Select  BOOK_ID,
                           BOOKCLASS.BOOK_CLASS_NAME,
                           BOOK_NAME,
                           BOOK_BOUGHT_DATE,
                           MEMBER.USER_CNAME,
                           CODE.CODE_NAME
                           From [dbo].[BOOK_DATA] as b
                           LEFT JOIN [dbo].[BOOK_CLASS] AS BOOKCLASS　ON b.BOOK_CLASS_ID=BOOKCLASS.BOOK_CLASS_ID
                           LEFT JOIN [dbo].[MEMBER_M] AS MEMBER　ON b.BOOK_KEEPER=MEMBER.USER_ID
                           LEFT JOIN [dbo].[BOOK_CODE] AS CODE　ON b.BOOK_STATUS=CODE.CODE_ID
                           Where(b.BOOK_CLASS_ID = @Book_Class_Id OR @Book_Class_Id='') AND
                           (b.BOOK_NAME  LIKE ('%'+ @Book_Name +'%') OR @Book_Name='') AND
                           (b.BOOK_STATUS = @Book_Status OR @Book_Status='') AND
                           (b.BOOK_KEEPER = @Book_Keeper OR @Book_Keeper='')";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@Book_Class_Id", Book.BOOK_CLASS_ID == null ? string.Empty : Book.BOOK_CLASS_ID));
                cmd.Parameters.Add(new SqlParameter("@Book_Name", Book.BOOK_NAME == null ? string.Empty : Book.BOOK_NAME));
                cmd.Parameters.Add(new SqlParameter("@Book_Status", Book.BOOK_STATUS == null ? string.Empty : Book.BOOK_STATUS));
                cmd.Parameters.Add(new SqlParameter("@Book_Keeper", Book.BOOK_KEEPER == null ? string.Empty : Book.BOOK_KEEPER));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapBookDataToList(dt);
        }
        //新增
        public int InsertBook(Models.BookSearchArg Book)
        {
            string sql = @" INSERT INTO [dbo].[BOOK_DATA]
						 (
							BOOK_NAME,
							BOOK_AUTHOR,
							BOOK_PUBLISHER,
							BOOK_NOTE,
							BOOK_BOUGHT_DATE,
							BOOK_CLASS_ID,
							BOOK_STATUS,
                            BOOK_KEEPER
						 )
						VALUES
						(
							 @BOOK_NAME,@BOOK_AUTHOR, @BOOK_PUBLISHER, @BOOK_NOTE, @BOOK_BOUGHT_DATE, @BOOK_CLASS_ID, 
                             @BOOK_STATUS,@BOOK_KEEPER
						)";
            int BookId;
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@BOOK_NAME", Book.BOOK_NAME));
                cmd.Parameters.Add(new SqlParameter("@BOOK_AUTHOR", Book.BOOK_AUTHOR));
                cmd.Parameters.Add(new SqlParameter("@BOOK_PUBLISHER", Book.BOOK_PUBLISHER));
                cmd.Parameters.Add(new SqlParameter("@BOOK_NOTE", Book.BOOK_NOTE));
                cmd.Parameters.Add(new SqlParameter("@BOOK_BOUGHT_DATE", Book.BOOK_BOUGHT_DATE));
                cmd.Parameters.Add(new SqlParameter("@BOOK_CLASS_ID", Book.BOOK_CLASS_ID));
                cmd.Parameters.Add(new SqlParameter("@BOOK_STATUS", Book.BOOK_STATUS));
                cmd.Parameters.Add(new SqlParameter("@BOOK_KEEPER", Book.BOOK_KEEPER));
                BookId = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
            }
            return BookId;
        }
        //刪除
        public void DeleteBookById(string BookId)
        {
            try
            {
                string sql = "Delete FROM [dbo].[BOOK_DATA] Where BOOK_ID = @BOOK_ID";
                using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add(new SqlParameter("@BOOK_ID", BookId));
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //修改(抓取資料)
        public List<Models.BookSearchArg> GetBookUpdateByCondtioin(string BookId)
        {

            DataTable dt = new DataTable();
            string sql = @"Select  
                           BOOK_ID,
                           BOOKCLASS.BOOK_CLASS_ID,
                           BOOK_NAME,
                           BOOK_BOUGHT_DATE,
                           MEMBER.USER_ID,
                           CODE.CODE_ID,
                           BOOK_AUTHOR,
                           BOOK_PUBLISHER,
                           BOOK_NOTE
                           From [dbo].[BOOK_DATA] as b
                           LEFT JOIN [dbo].[BOOK_CLASS] AS BOOKCLASS　ON b.BOOK_CLASS_ID=BOOKCLASS.BOOK_CLASS_ID
                           LEFT JOIN [dbo].[MEMBER_M] AS MEMBER　ON b.BOOK_KEEPER=MEMBER.USER_ID
                           LEFT JOIN [dbo].[BOOK_CODE] AS CODE　ON b.BOOK_STATUS=CODE.CODE_ID
                           Where(b.BOOK_ID = @Book_Id ) ";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@Book_Id", BookId));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.MapBookDataUpdateToList(dt);
        }


        //Map資料進List   

        //圖書類別
        private List<Models.BookClass> MapBookClassDataToList(DataTable BookClass)
        {
            List<Models.BookClass> result = new List<BookClass>();
            foreach (DataRow row in BookClass.Rows)
            {
                result.Add(new BookClass()
                {
                    BOOK_CLASS_ID = row["BOOK_CLASS_ID"].ToString(),
                    BOOK_CLASS_NAME = row["BOOK_CLASS_NAME"].ToString(),
                });
            }

            return result;
        }
        //借閱人
        private List<Models.BookKeeper> MapBookKeeperDataToList(DataTable BookKeeper)
        {
            List<Models.BookKeeper> result = new List<BookKeeper>();
            foreach (DataRow row in BookKeeper.Rows)
            {
                result.Add(new BookKeeper()
                {
                    USER_ID = row["USER_ID"].ToString(),
                    USER_CNAME = row["USER_CNAME"].ToString(),
                });
            }

            return result;
        }
        //借閱狀態
        private List<Models.BookStatus> MapBookStatusDataToList(DataTable BookStatus)
        {
            List<Models.BookStatus> result = new List<BookStatus>();
            foreach (DataRow row in BookStatus.Rows)
            {
                result.Add(new BookStatus()
                {
                    CODE_ID = row["CODE_ID"].ToString(),
                    CODE_NAME = row["CODE_NAME"].ToString(),
                });
            }

            return result;
        }
        //查詢
        private List<Models.BookSearchArg> MapBookDataToList(DataTable BookData)
        {

            List<Models.BookSearchArg> result = new List<BookSearchArg>();
            foreach (DataRow row in BookData.Rows)
            {
                result.Add(new BookSearchArg()
                {
                    BOOK_ID = row["BOOK_ID"].ToString(),
                    BOOK_CLASS_ID = row["BOOK_CLASS_NAME"].ToString(),
                    BOOK_NAME = row["BOOK_NAME"].ToString(),
                    BOOK_BOUGHT_DATE = Convert.ToDateTime(row["BOOK_BOUGHT_DATE"]).ToString("yyyy/MM/dd"),
                    BOOK_STATUS = row["CODE_NAME"].ToString(),
                    BOOK_KEEPER = row["USER_CNAME"].ToString()
                   
                });
            }
     
            return result;
        }
        //修改(抓取資料)
        private List<Models.BookSearchArg> MapBookDataUpdateToList(DataTable BookData)
        {

            List<Models.BookSearchArg> result = new List<BookSearchArg>();
            foreach (DataRow row in BookData.Rows)
            {
                result.Add(new BookSearchArg()
                {
                    BOOK_ID = row["BOOK_ID"].ToString(),
                    BOOK_CLASS_ID = row["BOOK_CLASS_ID"].ToString(),
                    BOOK_NAME = row["BOOK_NAME"].ToString(),
                    BOOK_BOUGHT_DATE = Convert.ToDateTime(row["BOOK_BOUGHT_DATE"]).ToString("yyyy/MM/dd"),
                    BOOK_STATUS = row["CODE_ID"].ToString(),
                    BOOK_KEEPER = row["USER_ID"].ToString(),
                    BOOK_AUTHOR = row["BOOK_AUTHOR"].ToString(),
                    BOOK_PUBLISHER = row["BOOK_PUBLISHER"].ToString(),
                    BOOK_NOTE = row["BOOK_NOTE"].ToString()
                });
            }

            return result;
        }
    }
}