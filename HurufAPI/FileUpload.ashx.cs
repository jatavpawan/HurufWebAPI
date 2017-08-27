using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.IO;
using System.Drawing;
using Huruf.DAL;
using HurufAPI.Api;

namespace HurufAPI
{
    /// <summary>
    /// Summary description for PicUpload
    /// </summary>
    public class FileUpload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Expires = -1;
            try
            {
                HttpPostedFile postedFile = context.Request.Files[0];

                string savepath = "";
                string tempPath = "";
                tempPath = "Uploads/AppFiles";

                string fileExt = Path.GetExtension(postedFile.FileName);

                if (fileExt.Equals(".mp3"))
                {
                    tempPath += "/Audio";
                }
                else if (fileExt.Equals(".pdf"))
                {
                    tempPath += "/PDF";
                }
                else if (fileExt.Equals(".mp4"))
                {
                    tempPath += "/Video";
                }
                else
                {
                    tempPath = "Invalid";
                }

                if (tempPath != "Invalid")
                {
                    savepath = context.Server.MapPath(tempPath);

                    string filename = Guid.NewGuid() + fileExt;

                    if (!Directory.Exists(savepath))
                        Directory.CreateDirectory(savepath);

                    Guid objguid = Guid.NewGuid();
                    postedFile.SaveAs(savepath + @"\" + filename.Replace(" ", ""));

                    ServiceController s = new ServiceController();

                    AppFile file = new AppFile();

                    file.Title = context.Request.Form["Title"];
                    file.Description = context.Request.Form["Description"];
                    file.FileType = Convert.ToInt32(context.Request.Form["FileType"]);
                    file.FileCategory = Convert.ToInt32(context.Request.Form["FileCategory"]);
                    file.IsDownloaded = false;
                    file.Timestamp = DateTime.Now;
                    file.CreatedBy = Convert.ToInt32(context.Request.Form["CreatedBy"]);
                    file.FileURL = "http://hurufwebsvc.gmcsco.com/" + tempPath + "/" + filename;
                    
                    s.SaveUserFile(file);

                    context.Response.StatusCode = 200;
                    context.Response.Write("File Uploaded successfully");
                }
                else
                {
                    context.Response.Write("Invalid File.<br/>Please try again with correct file!!!");
                }
            }
            catch (Exception ex)
            {
                context.Response.Write("Error: " + ex.Message);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}