using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Linq;
using System.Configuration;

namespace Quigley_Report
{
    class Batch
    {

        static string CreateReport(DateTime startDate, DateTime endDate)
        {
            string targetPath = ConfigurationManager.AppSettings["filesPath"];
            string fileNameFormat = ConfigurationManager.AppSettings["fileNameFormat"];
            DataTable Dt1 = DAL.GetDataTableByDate(startDate, endDate);

            if (Dt1.Rows.Count > 0)
            {
                Console.WriteLine("Total Rows: " + Dt1.Rows.Count.ToString());
                string excelFileName = string.Format(fileNameFormat, startDate.AddDays(1).ToString("MM-dd-yy"), endDate.ToString("MM-dd-yy"));
                string FUllPAthwithFileName = targetPath + excelFileName;

                CsvFileCreator.CreateCsvFromDataTable(Dt1, FUllPAthwithFileName);
                return FUllPAthwithFileName;
            }
            else
            {
                Console.WriteLine("No found.");
            }
            return string.Empty;
        }

        static void Main(string[] args)
        {
            DateTime startDate =  DateTime.Today.AddDays(-7).AddHours(-3);
            DateTime endDate = DateTime.Today.AddDays(0).AddHours(-3);
            
            Console.WriteLine("startDate " + startDate);
            Console.WriteLine("endDate " + endDate);

            Console.WriteLine("Start BatmanOnDvd Quigley Report");

            Logger.LogToFile("Start BatmanOnDvd Quigley Report");
            try
            {
                string reportFile = CreateReport(startDate, endDate);
                if (reportFile.Length > 0)
                    EmailSender.SendFileasAttachment(reportFile);
            }
            catch (Exception ex)
            {
                Logger.LogToFile(ex.Message + " StackTrace:: " + ex.StackTrace);
                //SendExceptionEmail(ex);
            }

            Logger.LogToFile("End BatmanOnDvd Quigley Report");
            Console.WriteLine("End BatmanOnDvd Quigley Report");
        }
    }
}
