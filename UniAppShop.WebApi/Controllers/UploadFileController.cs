using Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UniAppShop.WebApi.Models;
using ViewModels.UploadFile;

namespace UniAppShop.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UploadFileController : BaseController
    {
        private const string webSite = "http://localhost:54644";
        private readonly IWebHostEnvironment _webHostEnvironment;        

        public UploadFileController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="file"></param>
        private string SaveFile(IFormFile file)
        {
            string content_path = _webHostEnvironment.ContentRootPath;//D:\work\_BASE\pspb\pspb\src\pspb\
            string web_path = _webHostEnvironment.WebRootPath;//D:\work\_BASE\pspb\pspb\src\pspb\wwwroot

            string filePath = $@"\UploadFiles\{(DateTime.Now.ToString("yyyyMMddHHmmss"))}_{file.FileName}";
            using (FileStream fs = System.IO.File.Create($@"{content_path}{filePath}"))
            {
                file.CopyTo(fs);//将上传的文件文件流，复制到fs中
                fs.Flush();//清空文件流
            }
            return webSite + filePath.Replace("\\", "/");
        }

        [HttpPost]
        [Route("Upload")]
        public BaseResponse Upload([FromForm] UploadRequest request)
        {
            try
            {
                if (request == null || request.file == null)
                {
                    return BaseResponse.ToResponse(BackResult.Error, message: "参数解析错误或没有获取到文件");
                }

                //保存文件
                string url = SaveFile(request.file);

                //返回保存文件路径
                UploadResponse response = new UploadResponse()
                {
                    url = url
                };
                return BaseResponse.ToResponse<UploadResponse>(BackResult.Success, data: response);
            }
            catch(Exception exc)
            {
                return BaseResponse.ToResponse(BackResult.Exception, message: exc.Message);
            }
        }
        
        /// <summary>
        /// 上传用户头像
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ChangeHeadimg")]
        public BaseResponse ChangeHeadimg([FromForm] ChangeHeadimgRequest request)
        {
            try
            {
                if (request == null || request.headingFile == null)
                {
                    return BaseResponse.ToResponse(BackResult.Error, message: "参数解析错误或没有获取到文件");
                }               
                //保存文件
                string url = SaveFile(request.headingFile);

                //返回保存文件路径
                ChangeHeadimgResponse response = new ChangeHeadimgResponse()
                {
                    url = url
                };
                return BaseResponse.ToResponse<ChangeHeadimgResponse>(BackResult.Success, data: response);
            }
            catch(Exception exc)
            {
                return BaseResponse.ToResponse(BackResult.Exception, message: exc.Message);
            }
        }
    }
}
