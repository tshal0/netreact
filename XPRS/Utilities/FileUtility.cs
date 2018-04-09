using System;
using System.IO;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Net.Http;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;



using XPRS.Models;
using XPRS.Models.Serialized;
using XPRS.Models.Entities;
using XPRS.Repositories;
using OfficeOpenXml;

namespace XPRS.Utilities
{
    public class FileUtility
    {
        private static string UploadPath = ConfigurationManager.AppSettings["Upload_File_Path"].ToString();
        private static string DownloadPath = ConfigurationManager.AppSettings["Download_File_Path"].ToString();
        public static void SaveFile(HttpPostedFile file, String uniqueFilename)
        {
            FileInfo fi = null;
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, UploadPath + uniqueFilename);
            try
            {
                fi = new FileInfo(path);
            } catch (Exception e) { }
            if (ReferenceEquals(fi, null)) { throw new Exception("Invalid Path"); }
            else if (fi.Exists) { throw new Exception("Filename already exists"); }
            file.SaveAs(path);
        }

        public static void SaveFile(ExcelPackage pkg, String uniqueFilename)
        {
            FileInfo fi = null;
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, UploadPath + uniqueFilename);
            try
            {
                fi = new FileInfo(path);
            }
            catch (Exception e) { }
            if (ReferenceEquals(fi, null)) { throw new Exception("Invalid Path"); }
            else if (fi.Exists) { throw new Exception("Filename already exists"); }
            pkg.SaveAs(fi);
        }

        public static HttpResponseMessage GetFile(String uniqueFilename, String originalFilename)
        {
            string filePath = DownloadPath + uniqueFilename;
            FileInfo fi = new FileInfo(filePath);
            if (!fi.Exists) { throw new Exception("File doesn't exist"); }
            byte[] filedata = File.ReadAllBytes(filePath);
            var datastream = new MemoryStream(filedata);

            HttpResponseMessage msg = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            msg.Content = new StreamContent(datastream);
            msg.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            msg.Content.Headers.ContentDisposition.FileName = originalFilename;
            msg.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
            return msg;
        }
    }
}