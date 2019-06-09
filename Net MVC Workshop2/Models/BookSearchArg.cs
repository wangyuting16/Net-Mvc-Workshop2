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
        public string BOOK_ID { get; set; }
        /// <summary>
        /// 圖書類別
        /// </summary>
        [DisplayName("圖書類別")]
        [Required()]
        public string BOOK_CLASS_ID { get; set; }

        /// <summary>
        /// 書名
        /// </summary>
        [DisplayName("書籍名稱")]
        [Required()]
        public string BOOK_NAME { get; set; }

        /// <summary>
        /// 購書日期
        /// </summary>
        [DisplayName("購書日期")]
        [Required()]
        public string BOOK_BOUGHT_DATE { get; set; }

        /// <summary>
        /// 借閱狀態
        /// </summary>
        [DisplayName("借閱狀態")]
        [Required()]
        public string BOOK_STATUS { get; set; }

        /// <summary>
        /// 借閱人
        /// </summary>
        [DisplayName("借閱人")]
        //[Required()]
        public string BOOK_KEEPER { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        [DisplayName("作者")]
        [Required()]
        public string BOOK_AUTHOR { get; set; }

        /// <summary>
        /// 出版社
        /// </summary>
        [DisplayName("出版社")]
        [Required()]
        public string BOOK_PUBLISHER { get; set; }

        /// <summary>
        /// 內容簡介
        /// </summary>
        [DisplayName("內容簡介")]
        [Required()]
        public string BOOK_NOTE { get; set; }

    }
}