using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InterviewTask.Models
{
    public class categoryModel
    {
        [Display(Name="CategoryId")]
        public int cat_id { get; set; }

        [Display(Name ="CategoryName")]
        public string cat_name { get; set; }
        public int PageCount { get; set; }

        public virtual ICollection<productModel> Products { get; set; }

    }
}