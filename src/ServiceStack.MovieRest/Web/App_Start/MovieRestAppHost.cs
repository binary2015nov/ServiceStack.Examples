using Funq;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.Text;

namespace ServiceStack.MovieRest.App_Start
{
	public class MovieRestAppHost : AppHostBase
	{
		/// <summary>
		/// Initializes a new instance of your ServiceStack application, with the specified name and assembly containing the services.
		/// </summary>
		public MovieRestAppHost() : base("ServiceStack REST at the Movies!", typeof (MovieService).Assembly)
		{
			Config.DebugMode = true; //Show StackTraces for easier debugging (default auto inferred by Debug/Release builds)
			Config.UseCamelCase = true; //Set JSON web services to return idiomatic JSON camelCase properties
			JsConfig.DateHandler = DateHandler.ISO8601DateOnly;
		}

		public override void Configure(Container container)
		{
			container.Register<IDbConnectionFactory>(
				c => new OrmLiteConnectionFactory("~/App_Data/db.sqlite".MapHostAbsolutePath(), SqliteDialect.Provider));

			Plugins.Add(new CorsFeature()); //Enable CORS
		}

		protected override void OnAfterInit()
		{
			using (var resetMovies = Resolve<ResetMoviesService>())
			{
				resetMovies.Any(new ResetMovies());
			}
		}
	}
}