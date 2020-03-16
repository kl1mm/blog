using System;
using System.Linq.Expressions;

namespace kli.Blog.Shared.Models
{
    public class OverviewModel
    {
        public int Id { get; set; } = 0;
        public string Header { get; set; } = string.Empty;
        public string Intro { get; set; } = string.Empty;
        public DateTime Published { get; set; }
    }
}

