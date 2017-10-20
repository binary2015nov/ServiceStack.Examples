using System.Linq;
using Docs.Logic;
using Funq;
using ServiceStack;
using ServiceStack.Formats;

namespace Docs
{
    public class DocsAppHost : AppHostBase
    {
        public DocsAppHost() : base("ServiceStack Docs", typeof(PageService).Assembly) { }

        protected override void OnBeforeInit()
        {
            var baseUrl = AppSettings.Get("WebHostUrl");
            PageManager.Instance.Init("~/Pages.json".MapServerPath(), baseUrl);
            Config.WebHostUrl = baseUrl; //replaces ~/ with Url
            Config.MarkdownBaseType = typeof(CustomMarkdownPage); //set custom base for all Markdown pages
        }

        public override void Configure(Container container)
        {
            container.Register(PageManager.Instance);

            Routes
                .Add<Page>("/pages")
                .Add<Page>("/pages/{Name}")
                .Add<Category>("/category/{Name}")
                .Add<Search>("/search")
                .Add<Search>("/search/{Query}");

            var plugin = (MarkdownFormat)Plugins.First(x => x is MarkdownFormat);
            var page = plugin.FindByPathInfo("/about");
        }
    }
}