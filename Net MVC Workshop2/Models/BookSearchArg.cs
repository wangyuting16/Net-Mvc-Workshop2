using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Net_MVC_Workshop2.Models
{
    public class BookSearchArg
    {
        /// <summary>
        /// 書籍名稱
        /// </summary>
        [DisplayName("圖書類別")]
        [Required()]
        public string BOOK_CLASS_ID { get; set; }

        /// <summary>
        /// 書籍名稱
        /// </summary>
        [DisplayName("書名")]
        [Required()]
        public string BOOK_NAME { get; set; }


        /// <summary>
        /// 書籍名稱
        /// </summary>
        [DisplayName("借閱狀態")]
        [Required()]
        public string BOOK_STATUS { get; set; }

        /// <summary>
        /// 書籍名稱
        /// </summary>
        [DisplayName("借閱人")]
        [Required()]
        public string BOOK_KEEPER { get; set; }

        public string BOOK_BOUGHT_DATE { get; set; }
    }
}