﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalMediaGuide.DAL.Entities
{
    public class QuizType
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
      

    }
}
