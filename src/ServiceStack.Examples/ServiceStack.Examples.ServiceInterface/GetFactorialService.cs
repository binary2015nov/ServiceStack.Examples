using ServiceStack.Examples.ServiceModel;

namespace ServiceStack.Examples.ServiceInterface
{
	/// <summary>
	/// The purpose of this example is to show the minimum number and detail of classes 
	/// required in order to implement a simple service.
	/// </summary>
	public class GetFactorialService : Service
	{
		public object Any(GetFactorial request)
		{
			return new GetFactorialResponse { Result = GetFactorial(request.ForNumber) };
		}

		static long GetFactorial(long n)
		{
            var result = 1L;
            while (n >= 2)
            {
                checked { result *= n; n--; }
            }
            return result;
		}
	}
}