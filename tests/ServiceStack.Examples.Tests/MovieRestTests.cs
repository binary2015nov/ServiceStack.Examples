using System;
using System.Collections.Generic;
using NUnit.Framework;
using ServiceStack.Host;
using ServiceStack.Text;
using ServiceStack.Examples.ServiceInterface.Support;
using ServiceStack.Examples.ServiceModel;
using ServiceStack.Examples.ServiceModel.Types;

namespace ServiceStack.Examples.Tests
{
	[TestFixture]
	public class MovieRestTests : TestHostBase
	{
		[SetUp]
		public void SetUp()
		{
			ConfigureDatabase.Init(ConnectionFactory);
		}

		[Test]
		public void Can_list_all_movies()
		{
			var response = base.Send<MoviesResponse>(new Movies(), new BasicRequest { Verb = HttpMethods.Get });
			Assert.That(response.Movies, Has.Count.EqualTo(ConfigureDatabase.Top5Movies.Count));
		}

		[Test]
		public void Can_get_single_movie()
		{
			var topMovie = ConfigureDatabase.Top5Movies[0];
			var response = base.Send<MoviesResponse>(new Movies { Id = topMovie.Id }, new BasicRequest { Verb = HttpMethods.Get });
			response.PrintDump();
			Assert.That(topMovie.Equals(response.Movies[0]), Is.True);
		}

		[Test]
		public void Can_update_movie()
		{
			var topMovie = new Movie { Id = "tt0111161", Title = "The Shawshank Redemption", Rating = 9.2m, Director = "Frank Darabont", ReleaseDate = new DateTime(1995, 2, 17), TagLine = "Fear can hold you prisoner. Hope can set you free.", Genres = new List<string> { "Crime", "Drama" } };
			var updatedMovie = TypeSerializer.Clone(topMovie);
			updatedMovie.Title = "Updated Movie";
			base.Send<MoviesResponse>(new Movies { Movie = updatedMovie }, new BasicRequest { Verb = HttpMethods.Put });

			var response = base.Send<MoviesResponse>(new Movies { Id = topMovie.Id }, new BasicRequest { Verb = HttpMethods.Get });
			Assert.That(updatedMovie.Equals(response.Movies[0]), Is.True);
		}

		[Test]
		public void Can_add_movie()
		{
			var newMovie = new Movie
			{
				Id = "tt0110912",
				Title = "Pulp Fiction",
				Rating = 8.9m,
				Director = "Quentin Tarantino",
				ReleaseDate = new DateTime(1994, 10, 24),
				TagLine = "Girls like me don't make invitations like this to just anyone!",
				Genres = new List<string> { "Crime", "Drama", "Thriller" },
			};

			base.Send<MoviesResponse>(new Movies { Movie = newMovie }, new BasicRequest { Verb = HttpMethods.Post });

			var response = base.Send<MoviesResponse>(new Movies { Id = newMovie.Id }, new BasicRequest { Verb = HttpMethods.Get });
			Assert.That(newMovie.Equals(response.Movies[0]), Is.True);
		}

		[Test]
		public void Can_delete_movie()
		{
			var topMovie = ConfigureDatabase.Top5Movies[0];

			base.Send<MoviesResponse>(new Movies { Id = topMovie.Id }, new BasicRequest { Verb = HttpMethods.Delete });

			var response = base.Send<MoviesResponse>(new Movies { Id = topMovie.Id }, new BasicRequest { Verb = HttpMethods.Get});
			Assert.That(response.Movies, Has.Count.EqualTo(0));
		}
	}

}