﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace ChildCareLicensingScraper
{
    public class Program
    {
        static void Main(string[] args)
        {
            int processed = 0;
            int not_processed = 0;
            int startID = 1;
            int endID = 40000;

            for (int x = startID; x < endID; x++)
            {
                String page = GetWebPage("http://www.dss.virginia.gov/printer/facility/search/cc.cgi?rm=Details;ID=" + x);
                    
                if (page.Length > 7289)
                {
                    StorePage(x, page);
                    processed++;
                }
                else
                    not_processed++;

                if (x % 10 == 0)
                    Console.WriteLine("reviewed: " + ((x+1)-startID).ToString() + " processed: " + processed + " , not processed: " + not_processed);
            }

            Console.WriteLine("total processed: " + processed + " , total not processed: " + not_processed);

            //string outPage = UnStorePage(19128);
            //Console.WriteLine(outPage);
        }

        public static string GetWebPage(string url)
        {
            var request = WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response.Close();
            return responseFromServer;
            
        }

        public static void StorePage(int pageID, string page)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString)) 
            {
                conn.Open();
                string queryString = "INSERT INTO dbo.pages (PageID, Page) "
                                   + "VALUES (@PageID, @Page)";
                SqlCommand command = new SqlCommand(queryString, conn);
                command.Parameters.Add("@Page", SqlDbType.Text);
                command.Parameters["@Page"].Value = page;
                command.Parameters.Add("@PageID", SqlDbType.Int);
                command.Parameters["@PageID"].Value = pageID;
                var rows = command.ExecuteNonQuery();
            }

        }

        static string UnStorePage(int pageID)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString))
            {
                conn.Open();
                string queryString = "SELECT Page FROM dbo.pages "
                                   + "WHERE PageID = " + pageID;
                SqlCommand command = new SqlCommand(queryString, conn);
                var reader = command.ExecuteReader();
                string page = "";
                while (reader.Read())
                {
                    page = reader.GetString(0);
                }
                return page;
            }
        }
    }
}
