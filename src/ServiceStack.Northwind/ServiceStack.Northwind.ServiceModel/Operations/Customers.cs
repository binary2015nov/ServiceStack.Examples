using System.Collections.Generic;
using System.Runtime.Serialization;
using ServiceStack.Northwind.ServiceModel.Types;

namespace ServiceStack.Northwind.ServiceModel.Operations
{
    [DataContract]
    [Route("/customers")]
	public class Customers { }

	[DataContract]
	public class CustomersResponse : IHasResponseStatus
	{
		public CustomersResponse()
		{
			this.ResponseStatus = new ResponseStatus();
			this.Customers = new List<Customer>();
		}

		[DataMember]
		public List<Customer> Customers { get; set; }

		[DataMember]
		public ResponseStatus ResponseStatus { get; set; }
	}
}