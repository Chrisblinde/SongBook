﻿using SongBook.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SongBook.Models
{
    public class ShowCreate
    {
        [Required]
        [ForeignKey("Band")]
        [Display(Name = "Band Name")]
        public int BandId { get; set; }
        public string Venue { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }

        public virtual Band Band { get; set; }
    }
}
