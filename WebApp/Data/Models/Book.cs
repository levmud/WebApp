﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Data.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int Id { get; set; }
        public string Title { get; set; }
        public Author Author { get; set; }
        public Publisher Publisher { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }


    }
}
