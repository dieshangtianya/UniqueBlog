using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace UniqueBlog.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class FileManagerController:Controller
    {
        [HttpPost]
        public ActionResult PostFileUpload(HttpPostedFileBase upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            string postFolder = "postFiles";
            string url = this.SaveFile(upload, postFolder);

            string outputContent = this.GetOutputCKEditorCallback(url, CKEditorFuncNum);
           
            return Content(outputContent);
        }

        [HttpPost]
        public ActionResult CommentFileUpload(HttpPostedFileBase upload,string CKEditorFuncNum,string CKEditor,string langCode)
        {
            return View();
        }


        private string SaveFile(HttpPostedFileBase file,string folder)
        {
            string folderPath = "~/webImages/";
            folderPath += folder + "/";

            string absoluteFolder = Server.MapPath(folderPath);

            if(!Directory.Exists(absoluteFolder))
            {
                Directory.CreateDirectory(absoluteFolder);
            }

            string fileName =this.GenerateFileName(file.FileName);
            string savePath = absoluteFolder + fileName;

            string relativeFileUrl = folderPath + fileName;

            file.SaveAs(savePath);

            return Url.Content(relativeFileUrl);
        }

        private string GetOutputCKEditorCallback(string fileUrl,string ckeditorFuncNum)
        {
            string message = "";
            if (string.IsNullOrEmpty(fileUrl))
            {
                message = "上传失败";
            }

            string outputContent = @"<html><script>window.parent.CKEDITOR.tools.callFunction(" + ckeditorFuncNum + ", \"" + fileUrl + "\", \"" + message + "\");</script></body></html></html>";
            return outputContent;
        }

        private string GenerateFileName(string fileName)
        {
            string finalName = Guid.NewGuid() + fileName.Substring(fileName.LastIndexOf('.') - 1);
            return finalName;
        }
    }
}
