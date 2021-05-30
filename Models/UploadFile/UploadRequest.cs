using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels.UploadFile
{
    public class UploadRequest
    {
        public IFormFile file { get; set; }
        
    }
}
