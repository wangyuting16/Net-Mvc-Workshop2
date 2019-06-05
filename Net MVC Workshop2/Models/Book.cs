using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Net_MVC_Workshop2.Models
{
    public class Book
    {
        public int BOOK_ID { get; set; }

        public string BOOK_NAME { get; set; }

        public string BOOK_CLASS_ID { get; set; }

        public string BOOK_AUTHOR { get; set; }

        public string BOOK_BOUGHT_DATE { get; set; }

        public string BOOK_PUBLISHER { get; set; }

        public string BOOK_NOTE { get; set; }

        public string BOOK_STATUS { get; set; }

        public string BOOK_KEEPER { get; set; }

        public DateTime CREATE_DATE { get; set; }

        public string CREATE_USER { get; set; }

        public DateTime MODIFY_DATE { get; set; }

        public string MODIFY_USER { get; set; }
    }
}