using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PaperSchoolTrainers.Models
{
    public class Trainer
    {
        public int Id { get; set; }
        [Display(Name="Name")]
        public String FirstName { get; set; }

        [Display(Name = "Surname")]
        public String LastName { get; set; }
        public String Subject { get; set; }
        public int Salary { get; set; }

    }


}