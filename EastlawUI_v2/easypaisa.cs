using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


    public class easypaisa
    {
        public string Aut_Code {get;set;}
        public string _Amt { get; set; }
        public string _ExpiryDate { get; set; }
        public string _orderRefNum { get; set; } 
        
        /// <summary>
        /// Same as session Id (unless pulling from database)
        /// </summary>
        //public string Aut_Code
        //{
        //    get
        //    {
        //        return _Aut_Code;
        //    }
        //    set
        //    {
        //        _Aut_Code = value;
        //    }
        //}
    }
