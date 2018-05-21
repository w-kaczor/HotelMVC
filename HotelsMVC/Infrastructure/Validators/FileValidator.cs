using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelsMVC.Infrastructure.Validators
{
    public class FileValidator : ValidationAttribute
    {
        public int Size { get; set; }
        public string[] SupportedTypes { get; set; }

        public FileValidator(int size, string[] supportedTypes)
        {
            this.Size = size;
            this.SupportedTypes = supportedTypes;
        }

        public override bool IsValid(object value)
        {
            var file = value as HttpPostedFileBase;
            if (value == null)
            {
                return false;
            }
            if (file.ContentLength > Size * 1024 * 1024)
            {
                return false;
            }
            var fileExt = System.IO.Path.GetExtension(file.FileName.ToLower());
            if (!SupportedTypes.Contains(fileExt))
            {
                return false;
            }
            return true;
        }
    }
}