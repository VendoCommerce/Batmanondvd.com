using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Collections;
using System.Globalization;
using Com.ConversionSystems.DataAccess;
using Com.ConversionSystems.Utility;
using CSBusiness.OrderManagement;
using CSBusiness;
using CSCore.Utils;
using CSCore.DataHelper;

namespace Com.ConversionSystems
{
    public class Batch : Com.ConversionSystems.UI.BasePage
    {
        public bool DoBatch()
        {
            string URL = Helper.AppSettings["SiteUrl"] + "/authorizeorderbatch.aspx?code=encryptionCode";
            Console.WriteLine(URL);
            CommonHelper.HttpPost(URL, "");
            return true;
        }

        public static void Main(string[] args)
        {
            Batch StartBatch = new Batch();
            Console.WriteLine("BatmanOnDVD Batch - Started");
            Console.WriteLine("Please Wait - ");
            StartBatch.DoBatch();
            Console.WriteLine("BatmanOnDVD Batch  - End");
            Console.WriteLine("Task Completed - ");

        }
    }

}
