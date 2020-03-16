using System;
using kli.Blog.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace kli.Blog.Client.Pages
{
    public partial class About : ComponentBase
    {
        protected EntryModel AboutModel { get; set; } = new EntryModel
        {
            Header = "About me",
            Intro = "Software Engineer, Sports junky, Husband living and working in Hamburg, Germany",
            Published = new DateTime(2020, 02, 02)
        };
    }
}
