using ServiceStack.OrmLite;
using ServiceStack.Examples.ServiceModel;
using ServiceStack.Examples.ServiceModel.Types;

namespace ServiceStack.Examples.ServiceInterface
{
    /// <summary>
    /// An example of a basic REST web service
    /// 
    /// Each operation needs to support same Request and Response DTO's so you will
    /// need to combine the types of all your operations into the same DTO as done
    /// in this example.
    /// </summary>
    public class MovieRestService : Service
    {
        public object Any(Movies request)
        {
            return Get(request);
        }

        /// <summary>
        /// GET /Movies 
        /// GET /Movies?Id={Id}
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public object Get(Movies request)
        {
            var response = new MoviesResponse();

            if (request.Id != null)
            {
                // GET /Movies?Id={request.Id}
                var movie = Db.SingleById<Movie>(request.Id);
                if (movie != null)
                {
                    response.Movies.Add(movie);
                }
            }
            else
            {
                // GET /Movies
                response.Movies = Db.Select<Movie>();
            }

            return response;
        }

        /// <summary>
        /// PUT /Movies
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public object Put(Movies request)
        {
            Db.Update(request.Movie);

            return new MoviesResponse();
        }

        /// <summary>
        /// DELETE /Movies
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public object Delete(Movies request)
        {
            Db.DeleteById<Movie>(request.Id);

            return new MoviesResponse();
        }

        /// <summary>
        /// POST /Movies
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public object Post(Movies request)
        {
            Db.Insert(request.Movie);

            return new MoviesResponse();
        }
    }

}