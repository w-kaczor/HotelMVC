using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace HotelsMVC.Infrastructure.Validators
{
    public class FilesValidator : ValidationAttribute
    {
        public int ImagesCount { get; set; }
        public int Size { get; set; }
        public string[] SupportedTypes { get; set; }

        public FilesValidator(int imagesCount, int size, string[] supportedTypes)
        {
            this.ImagesCount = imagesCount;
            this.Size = size;
            this.SupportedTypes = supportedTypes;
        }

        public override bool IsValid(object values)
        {
            IEnumerable<HttpPostedFileBase> files = values as IEnumerable<HttpPostedFileBase>;
            if (files.ElementAt(0) == null)
            {
                return false;
            }
            if (files.Count() > ImagesCount)
            {
                return false;
            }
            int sum = 0;
            foreach (var item in files)
            {
                sum += item.ContentLength;
            }
            if (sum > Size * 1024 * 1024)
            {
                return false;
            }
            foreach (var item in files)
            {
                if (!SupportedTypes.Contains(Path.GetExtension(item.FileName.ToLower())))
                {
                    return false;
                }

            }
            return true;
        }
    }
}