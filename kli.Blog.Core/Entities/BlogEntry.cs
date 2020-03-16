using System;

namespace kli.Blog.Core.Entities
{
    public class BlogEntry
    {
        public int Id { get; set; } = 0;
        public string Header { get; set; } = string.Empty;
        public string Intro { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public bool IsPublished { get; set; }
        public DateTime Published { get; set; }
    }
}
