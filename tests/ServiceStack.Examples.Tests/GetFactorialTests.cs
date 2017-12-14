using NUnit.Framework;
using ServiceStack.Examples.ServiceInterface;
using ServiceStack.Examples.ServiceModel;

namespace ServiceStack.Examples.Tests
{
	[TestFixture]
	public class GetFactorialTests : TestHostBase
	{
		[Test]
		public void Get_Factorial()
		{
			var request = new GetFactorial { ForNumber = 4 };

			var service = new GetFactorialService();
			
			var response = (GetFactorialResponse)service.Any(request);

			Assert.That(response.Result, Is.EqualTo(4 * 3 * 2 * 1));
		}
	}
}