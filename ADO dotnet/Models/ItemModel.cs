using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RLG_Project_ADO_1.Models
{
    public class ItemModel
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Only Alphabets Allowed.")]
        public string Name { get; set; }

        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Only Numbers Allowed.")]
        public int Cost { get; set; }
    }
}