using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Travel.Convertor
{
    public class PagingModel
    {
        /// <summary>
        /// کلاس مدل کردن پیجینگ
        /// </summary>
        public sealed class Paging_Model
        {
            const int MaxPageSize = 100;
            //[Range(1, 99999, ErrorMessage = "شماره صفحه خارج از محدوده تعریف شده است")]
            public int PageNumber { get; set; }



            private int _pageSize = 10;
            //[Range(1, 1000, ErrorMessage = "تعداد آیتم در صفحه خارج از محدوده تعریف شده است")]
            public int PageSize
            {
                get
                {
                    return _pageSize;
                }
                set
                {
                    _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
                }
            }

        }
    }
}
