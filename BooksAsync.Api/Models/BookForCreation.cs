﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAsync.Api.Models
{
    public class BookForCreation
    {
        public string Author { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}