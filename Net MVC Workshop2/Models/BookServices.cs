﻿using System;
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
        public List<Models.Book> GetBookByCondtioin(Models.BookSearchArg Book)
        {

            DataTable dt = new DataTable();
            string sql = @"Select  BOOKCLASS.BOOK_CLASS_NAME,BOOK_NAME,BOOK_BOUGHT_DATE,MEMBER.USER_ENAME,CODE.CODE_NAME
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
        private List<Models.Book> MapBookDataToList(DataTable BookData)
        {
            List<Models.Book> result = new List<Book>();
            foreach (DataRow row in BookData.Rows)
            {
                result.Add(new Book()
                {
                    BOOK_CLASS_ID = row["BOOK_CLASS_NAME"].ToString(),
                    BOOK_NAME = row["BOOK_NAME"].ToString(),
                    BOOK_BOUGHT_DATE = row["BOOK_BOUGHT_DATE"].ToString(),
                    BOOK_STATUS = row["CODE_NAME"].ToString(),
                    BOOK_KEEPER = row["USER_ENAME"].ToString(),

                });
            }

            return result;
        }
    }
}