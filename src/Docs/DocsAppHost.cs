﻿using System.Linq;
using ServiceStack;
using ServiceStack.Formats;
using Docs.Logic;

namespace Docs
{
    public class DocsAppHost : AppHostBase
    {
        public DocsAppHost() : base("ServiceStack Docs", typeof(PageService).Assembly)
        {
            Config.WebHostUrl = AppSettings.Get("WebHostUrl");
        }

        protected override void OnBeforeInit()
        {
            string filePath = WebHostPhysicalPath.CombineWith("Pages.json");
            PageManager.Default.Init(filePath, Config.WebHostUrl);
        }

        public override void Configure(Funq.Container container)
        {
            Plugins.Add(new MarkdownFormat());

            Routes
                .Add<Page>("/pages")
                .Add<Page>("/pages/{Name}")
                .Add<Category>("/category/{Name}")
                .Add<Search>("/search")
                .Add<Search>("/search/{Query}");
        }
    }
}