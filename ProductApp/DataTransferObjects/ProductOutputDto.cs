﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductApp.Entities;

namespace ProductApp.DataTransferObjects
{
    public class ProductOutputDto
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public ICollection<TagCreationDto> Tags { get; set; } = new List<TagCreationDto>();
    }
}
