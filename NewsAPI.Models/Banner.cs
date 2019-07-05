﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAPI.Models
{
    public class Banner
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
