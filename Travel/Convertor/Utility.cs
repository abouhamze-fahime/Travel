using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;


namespace BLL
{
    public static class Utility
    {
        #region [{GetRandomNumbers_SN}{Dejit}]

        //public static string GenerateRefrenceID(int RequestID)
        //{
        //    string RefrenceID = (RequestID + 10000).ToString();
        //    DateTime date = System.DateTime.Now;
        //    int Hour = date.Hour;
        //    int Minute = date.Minute;
        //    int Second = date.Second;
        //    int MilliSecond = date.Millisecond;
        //    int Year = date.Year;
        //    string month = date.Month.ToString("00");
        //    string day = date.Day.ToString("00");
        //    if (month.Length < 2)
        //    {
        //        month = "0" + month;
        //    }
        //    if (day.Length < 2)
        //    {
        //        day = "0" + day;
        //    }
        //    string DateTime = Year.ToString() + month.ToString() + day.ToString() + Hour.ToString() + Minute.ToString() + Second.ToString() + MilliSecond.ToString();
        //    RefrenceID = RefrenceID + DateTime ;
        //    return RefrenceID;
        //}
        public static string RandomString(int length)
        {
            const string pool = "abcdefghijklmnopqrstuvwxyz0123456789ABCDEFJHIGKLMNOPQRSTUVWXYZ123456789";
            var builder = new StringBuilder();
            Random Random = new Random();

            for (var i = 1; i < length; i++)
            {
                if (i % 6 == 0)
                {
                    var c = "-";
                    builder.Append(c);
                }
                else
                {
                    var c = pool[Random.Next(0, pool.Length)];
                    builder.Append(c);
                }
            }

            return builder.ToString();
        }
        public static string GetRandomNumbers_SN()//25 رقم
        {
            //SourceId = SourceId.PadLeft(2, '0');

            //PaymentId = PaymentId.PadLeft(8, '0');

            #region Date

            PersianCalendar pc = new PersianCalendar();

            StringBuilder sb2 = new StringBuilder();
            sb2.Append(pc.GetYear(DateTime.Now).ToString("0000"));


            sb2.Append(pc.GetMonth(DateTime.Now).ToString("00"));
            // string sb = sb2.ToString().Substring(2, 2);
            //sb2.Append("/");
            sb2.Append(pc.GetDayOfMonth(DateTime.Now).ToString("00"));

            #endregion
            string num = "";
            Random random = new Random(Convert.ToInt32(Guid.NewGuid().GetHashCode()));

            num = sb2.ToString().Substring(4, 2)//ماه دو رقم
                                                //  + PaymentId // شماره خرید (همان شماره سریال گواهی) حد اکثر 8 رقم
                + pc.GetMilliseconds(DateTime.Now).ToString("0000")//4
                + random.Next(1000, 9999)//4
                + sb2.ToString().Substring(2, 2)//سال که دو رقم
                 + pc.GetSecond(DateTime.Now).ToString("00")//دو رقم ثانیه
                + random.Next(10000, 99999) // پنج رقم رندوم
                  + sb2.ToString().Substring(6, 2) // روز دو رقم
                  + random.Next(100, 999)//سه رقم
                  ;
            int dej = Dejit(num);
            num = num + dej.ToString();
            // random.Next(10000, 99999).ToString();//
            return num;
        }

        /// <summary>
        /// جهت ارائه paymentid
        /// 20 رقم 
        /// یک رقم دیجیت که با رفرنس و اون بیست رقم ساخته میشه
        /// </summary>
        /// <param name="RefrenceId"></param>
        /// <returns></returns>
        public static string GetPaymentId(string RefrenceId)//21 رقم
        {
            //SourceId = SourceId.PadLeft(2, '0');

            //PaymentId = PaymentId.PadLeft(8, '0');

            #region Date

            PersianCalendar pc = new PersianCalendar();

            StringBuilder sb2 = new StringBuilder();
            sb2.Append(pc.GetYear(DateTime.Now).ToString("0000"));


            sb2.Append(pc.GetMonth(DateTime.Now).ToString("00"));
            // string sb = sb2.ToString().Substring(2, 2);
            //sb2.Append("/");
            sb2.Append(pc.GetDayOfMonth(DateTime.Now).ToString("00"));

            #endregion
            string num = "";
            Random random = new Random(Convert.ToInt32(Guid.NewGuid().GetHashCode()));


            num = sb2.ToString().Substring(4, 2)//ماه دو رقم
                                                //  + PaymentId // شماره خرید (همان شماره سریال گواهی) حد اکثر 8 رقم
                + pc.GetMilliseconds(DateTime.Now).ToString("0000")//4
                + random.Next(1000, 9999)//4
                + sb2.ToString().Substring(2, 2)//سال که دو رقم
                 + pc.GetSecond(DateTime.Now).ToString("00")//دو رقم ثانیه
                + random.Next(1, 9) // یک رقم رندوم
                  + sb2.ToString().Substring(6, 2) // روز دو رقم
                  + random.Next(100, 999)//سه رقم
                  ;
            int dej = Dejit(num + RefrenceId);
            num = num + dej.ToString();
            // random.Next(10000, 99999).ToString();//
            return num;
        }



        /// <summary>
        /// عدد کنترلی که بفهمیم عدد دریافتی تولید شده خودمان است یا نه
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static int Dejit(string num)
        {
            int dej = 0;
            int zarib = 2;
            int count = num.Length - 1;
            int sum = 0;
            for (int i = 0; i < num.Length; i++)
            {
                if (zarib == 8) { zarib = 2; }
                sum += Convert.ToInt32(num.Substring(count, 1)) * zarib;
                zarib++;
                count--;
            }
            dej = sum % 11;
            if (dej == 0 || dej == 1) { dej = 0; }
            else
            {
                dej = 11 - dej;
            }
            return dej;
        }

        #endregion

       

        #region Date Convertor

        private static readonly string[] Week = { "شنبه", "یکشنبه", "دوشنبه", "سه شنبه", "چهارشنبه", "پنجشنبه", "جمعه" };
        private static readonly string[] Months = { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند" };

        /// <summary>
        /// 13651114 تاریخ امروز  عددی
        /// </summary>
        /// <returns>13651114</returns>
        public static int PersianDateNow()
        {
            return ToPersianDate(DateTime.Now);
        }
        /// <summary>
        /// تاریخ شمسی
        /// </summary>
        /// <param name="data">DateTime Today</param>
        /// <param name="shortdate">IF True 1388/01/01</param>
        /// <returns></returns>
        public static string ConvertToPersianDate(this DateTime Date, Boolean ShortDate)
        {
            var date = string.Empty;
            try
            {
                var datePersian = new PersianCalendar();

                var week = new string[] { "شنبه", "یکشنبه", "دوشنبه", "سه شنبه", "چهارشنبه", "پنجشنبه", "جمعه" };
                var months = new string[] { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند" };
                var d = 0;

                var a = Date;
                var tempdayofweek = a.DayOfWeek;
                switch (tempdayofweek)
                {
                    case DayOfWeek.Saturday:
                        d = 0;
                        break;
                    case DayOfWeek.Sunday:
                        d = 1;
                        break;
                    case DayOfWeek.Monday:
                        d = 2;
                        break;
                    case DayOfWeek.Tuesday:
                        d = 3;
                        break;
                    case DayOfWeek.Wednesday:
                        d = 4;
                        break;
                    case DayOfWeek.Thursday:
                        d = 5;
                        break;
                    case DayOfWeek.Friday:
                        d = 6;
                        break;
                }
                var day = datePersian.GetDayOfMonth(a);
                var month = datePersian.GetMonth(a);
                var year = datePersian.GetYear(a);

                if (ShortDate != true)
                {
                    return (week[d] + " " + day + " " + months[month - 1] + " " + year);
                }
                else
                {
                    var smonth = string.Empty;
                    var sday = string.Empty;
                    if (month >= 1 && month <= 9)
                    {
                        smonth = "0" + month.ToString();
                    }
                    else
                    {
                        smonth = month.ToString();
                    }
                    if (day >= 1 && day <= 9)
                    {
                        sday = "0" + day.ToString();
                    }
                    else
                    {
                        sday = day.ToString();
                    }
                    date = (year + "/" + smonth + "/" + sday);
                }
            }
            catch (Exception)
            {
            }
            return date;
        }

        /// <summary>
        /// تاریخ امروز کامل  یکشنبه 25 آبان
        /// </summary>
        /// <returns>یکشنبه 25 آبان</returns>
        public static string PersianDateNowLong()
        {
            return ToPersianDateLong(DateTime.Now);
        }

        /// <summary>
        /// تاریخ کامل امروز همراه با زمان
        /// </summary>
        /// <returns></returns>
        public static string PersianDateTimeNowLong()
        {
            return ToPersianDateLong(DateTime.Now) + " " + Time();
        }

        /// <summary>
        /// 13651114 تبدیل تاریخ  میلادی به تاریخ شمسی عددی
        /// </summary>
        /// <param name="date"></param>
        /// <returns>13651114</returns>
        public static int ToPersianDate(this DateTime date)
        {
            var datePersian = new PersianCalendar();


            var day = datePersian.GetDayOfMonth(date);
            var month = datePersian.GetMonth(date);
            var year = datePersian.GetYear(date);

            var smonth = string.Empty;
            var sday = string.Empty;
            if (month >= 1 && month <= 9)
            {
                smonth = "0" + month.ToString();
            }
            else
            {
                smonth = month.ToString();
            }

            if (day >= 1 && day <= 9)
            {
                sday = "0" + day.ToString();
            }
            else
            {
                sday = day.ToString();
            }
            return int.Parse(year + string.Empty + smonth + string.Empty + sday);
        }

        /// <summary>
        /// تبدیل تاریخ میلادی به شمسی یکشنبه 28 آبان 
        /// </summary>
        /// <param name="date"></param>
        /// <returns>یکشنبه 28 آبان</returns>
        public static string ToPersianDateLong(this DateTime date)
        {
            var datePersian = new PersianCalendar();


            var d = 0;

            var a = date;
            var tempdayofweek = a.DayOfWeek;
            switch (tempdayofweek)
            {
                case DayOfWeek.Saturday:
                    d = 0;
                    break;
                case DayOfWeek.Sunday:
                    d = 1;
                    break;
                case DayOfWeek.Monday:
                    d = 2;
                    break;
                case DayOfWeek.Tuesday:
                    d = 3;
                    break;
                case DayOfWeek.Wednesday:
                    d = 4;
                    break;
                case DayOfWeek.Thursday:
                    d = 5;
                    break;
                case DayOfWeek.Friday:
                    d = 6;
                    break;
            }
            var day = datePersian.GetDayOfMonth(a);
            var month = datePersian.GetMonth(a);
            var year = datePersian.GetYear(a);


            return (Week[d] + " " + day + " " + Months[month - 1] + " " + year);
        }

        /// <summary>
        /// تبدیل تاریخ میلادی به شمسی به همراه زمان 1392/01/01 14:10:02
        /// </summary>
        /// <param name="date"></param>
        /// <returns>1392/01/01 14:10:02</returns>
        public static string ToPersianDateTimeLong(this DateTime date)
        {
            var datePersian = new PersianCalendar();

            var day = datePersian.GetDayOfMonth(date);
            var month = datePersian.GetMonth(date);
            var year = datePersian.GetYear(date);

            var smonth = string.Empty;
            var sday = string.Empty;
            if (month >= 1 && month <= 9)
            {
                smonth = "0" + month.ToString();
            }
            else
            {
                smonth = month.ToString();
            }

            if (day >= 1 && day <= 9)
            {
                sday = "0" + day.ToString();
            }
            else
            {
                sday = day.ToString();
            }
            return year + "/" + smonth + "/" + sday + " " + Time(date);
        }

        /// <summary>
        /// تبدیل تاریخ شمسی عددی به حروف یکشنبه 25 آبان
        /// </summary>
        /// <param name="date"></param>
        /// <returns>یکشنبه 25 آبان</returns>
        public static string ToPersianDate(int date)
        {
            var years = int.Parse(date.ToString().Substring(0, 4));
            var month = int.Parse(date.ToString().Substring(4, 2));
            var days = int.Parse(date.ToString().Substring(6, 2));

            return (days + " " + Months[month - 1] + " " + years);
        }

        /// <summary>
        /// Insert '/' In DateValue
        /// </summary>
        /// <param name="DateValue"></param>
        /// <returns></returns>
        public static string GetPersianDateNormal(int DateValue)
        {
            return string.Format("{0}/{1}/{2}",
                                                DateValue.ToString().Substring(0, 4),
                                                DateValue.ToString().Substring(4, 2),
                                                DateValue.ToString().Substring(6, 2));
        }

        /// <summary>
        /// زمان حال 13:50:10
        /// </summary>
        /// <returns></returns>
        public static string Time()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }

        /// <summary>
        /// زمان
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static string Time(this DateTime datetime)
        {
            return (datetime.ToString("HH:mm:ss"));
        }

        /// <summary>
        /// تبدیل تاریخ شمسی به میلادی
        /// </summary>
        /// <param name="year">1365</param>
        /// <param name="month">11</param>
        /// <param name="day">10</param>
        /// <param name="hour">0</param>
        /// <param name="min">0</param>
        /// <returns></returns>
        public static DateTime ToDateTime(int year, int month, int day, int hour = 12, int min = 0)
        {
            var datePersian = new PersianCalendar();

            return datePersian.ToDateTime(year, month, day, hour, min, 0, 0);
        }

        public static DateTime ToDateTimeString(string datevalue)
        {
            var datePersian = new PersianCalendar();
            string[] PDCBegindateCertificateSplit = datevalue.Split('/');
            return ToDateTime(
              Convert.ToInt16(PDCBegindateCertificateSplit[0])
              , Convert.ToInt16(PDCBegindateCertificateSplit[1]),
              Convert.ToInt16(PDCBegindateCertificateSplit[2]));

            // return datePersian.ToDateTime(int.Parse(datevalue.ToString().Substring(0, 4)), int.Parse(datevalue.ToString().Substring(5, 2)), int.Parse(datevalue.ToString().Substring(8, 2)), 0, 0, 0, 0);
        }

        /// <summary>
        /// تبدیلی تاریخ شمسی به میلادی
        /// </summary>
        /// <param name="datevalue">13651114</param>
        /// <returns></returns>
        public static DateTime ToDateTime(int datevalue)
        {
            var datePersian = new PersianCalendar();

            return datePersian.ToDateTime(int.Parse(datevalue.ToString().Substring(0, 4)), int.Parse(datevalue.ToString().Substring(4, 2)), int.Parse(datevalue.ToString().Substring(6, 2)), 0, 0, 0, 0);
        }


        public static int GetPersianYear(DateTime date)
        {
            return int.Parse(ToPersianDate(date).ToString().Substring(0, 4));
        }
        public static int GetPersianYear(int date)
        {
            return int.Parse(date.ToString().Substring(0, 4));
        }
        public static int GetPersianYearNow()
        {
            return int.Parse(PersianDateNow().ToString().Substring(0, 4));
        }



        public static string GetPersianMonth(DateTime date)
        {
            return ToPersianDate(date).ToString().Substring(5, 2);
        }
        public static string GetPersianMonth(string Date)
        {
            return Date.Substring(4, 2);
        }
        /// <summary>
        /// ماه شمسی . بهمن
        /// </summary>
        /// <param name="date">13651114</param>
        /// <returns>بهمن</returns>
        public static string GetPersianMonthName(int date)
        {
            var d = byte.Parse(GetPersianMonth(date));

            var months = new string[] { string.Empty, "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند" };

            return months[d];
        }

        private static string GetPersianMonth(int Date)
        {
            return Date.ToString().Substring(4, 2);
        }
        public static int GetPersianMonthNow()
        {
            return int.Parse(PersianDateNow().ToString().Substring(5, 2));
        }




        public static string GetPersianDay(DateTime date)
        {
            return ToPersianDate(date).ToString().Substring(6, 2);
        }
        public static string GetPersianDay(string Date)
        {
            return Date.Substring(6, 2);
        }
        public static string GetPersianDay(int Date)
        {
            return Date.ToString().Substring(6, 2);
        }
        public static int GetPersianDayInt(int Date)
        {
            return int.Parse(Date.ToString().Substring(6, 2));
        }
        public static int GetPersianDayNow()
        {
            return int.Parse(ToPersianDate(DateTime.Now).ToString().Substring(6, 2));
        }


        public static string TimeSpent(DateTime dateInsert)
        {
            var timestr = string.Empty;
            var date = (DateTime.Now - dateInsert);
            if (date.Days >= 1)
            {
                timestr += ((GetPersianDayInt(ToPersianDate(dateInsert)))) + " " + GetPersianMonthName(ToPersianDate(dateInsert));
                ;
            }
            else
            {
                if (date.Days < 1 && date.Hours >= 1)
                {
                    timestr += (date.Hours).ToString() + " ساعت ";
                    timestr += "قبل";
                }
                else
                {
                    if (date.Hours < 1 && date.Minutes >= 10)
                    {
                        timestr += (date.Minutes).ToString() + " دقیقه ";
                        timestr += "قبل";
                    }
                    else
                    {
                        timestr = " دقایقی ";
                        timestr += "قبل";
                    }
                }
            }
            return timestr;
        }

        public static string TimeSpentComment(DateTime dateInsert)
        {
            var timestr = string.Empty;
            var date = (DateTime.Now - dateInsert);
            if (date.Days >= 30)
            {
                timestr = dateInsert.ToPersianDateLong();
            }

            else
            {
                if (date.Days >= 1)
                {
                    timestr = ((GetPersianDayInt(ToPersianDate(dateInsert)))) + " " + GetPersianMonthName(ToPersianDate(dateInsert));
                }
                else
                {
                    if (date.Days < 1 && date.Hours >= 1)
                    {
                        timestr += (date.Hours).ToString() + " ساعت ";
                        timestr += "قبل";
                    }
                    else
                    {
                        if (date.Hours < 1 && date.Minutes >= 10)
                        {
                            timestr += (date.Minutes).ToString() + " دقیقه ";
                            timestr += "قبل";
                        }
                        else
                        {
                            timestr = " دقایقی ";
                            timestr += "قبل";
                        }
                    }
                }
            }
            return timestr;
        }


        #endregion

 



    }


}
