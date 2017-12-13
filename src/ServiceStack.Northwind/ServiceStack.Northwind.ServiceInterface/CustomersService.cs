using ServiceStack.OrmLite;
using ServiceStack.Northwind.ServiceModel.Operations;
using ServiceStack.Northwind.ServiceModel.Types;

namespace ServiceStack.Northwind.ServiceInterface
{
    public class CustomersService : Service
    {
        public CustomersResponse Get(Customers request)
        {
            return new CustomersResponse { Customers = Db.Select<Customer>() };
        }
    }
}
