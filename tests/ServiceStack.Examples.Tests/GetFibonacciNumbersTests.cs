﻿using NUnit.Framework;
using ServiceStack.Examples.ServiceInterface;
using ServiceStack.Examples.ServiceModel;

namespace ServiceStack.Examples.Tests
{
	[TestFixture]
	public class GetFibonacciNumbersTests : TestHostBase
	{
		[Test]
		public void GetFibonacciNumbers_Test()
		{
			var request = new GetFibonacciNumbers { Take = 5 };

			var service = new GetFibonacciNumbersService(
				new ExampleConfig { DefaultFibonacciLimit = 10 });

			var response = (GetFibonacciNumbersResponse)service.Any(request);

			Assert.That(response.Results.Count, Is.EqualTo(request.Take));
			Assert.That(response.Results, Is.EqualTo(new[] { 1, 2, 3, 5, 8 }));
		}
	}
}