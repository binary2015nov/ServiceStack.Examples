using System.Collections.Generic;
using ServiceStack.Northwind.ServiceModel.Types;

namespace ServiceStack.Northwind.ServiceModel.Operations
{
    [Route("/orders")]
	[Route("/orders/page/{Page}")]
	[Route("/customers/{CustomerId}/orders")]
	public class Orders
	{
		public int? Page { get; set; }

		public string CustomerId { get; set; }
	}

	public class OrdersResponse : IHasResponseStatus
	{
		public OrdersResponse()
		{
			this.ResponseStatus = new ResponseStatus();
			this.Results = new List<CustomerOrder>();
		}

		public List<CustomerOrder> Results { get; set; }

		public ResponseStatus ResponseStatus { get; set; }
	}
}