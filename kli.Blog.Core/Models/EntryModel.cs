using System;

namespace kli.Blog.Core.Models
{
	public class EntryModel
	{
		public string Header { get; set; } = string.Empty;
		public string Intro { get; set; } = string.Empty;
		public string Content { get; set; } = string.Empty;
		public DateTime Published { get; set; }
	}
}
