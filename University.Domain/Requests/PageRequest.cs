﻿using System.ComponentModel.DataAnnotations;

namespace University.Domain.Requests
{
    public class PageRequest
    {
        [Range(1, int.MaxValue)]
        public int Page { get; set; } = 1;
        [Range(1, 100)]
        public int PageSize { get; set; } = 10;
    }
}
