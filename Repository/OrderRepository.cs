using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class OrderRepository
    {
        public OrderRepository Retrieve(int orderId)
        {
            return new OrderRepository();
        }

        public List<OrderRepository> Retrieve()
        {
            return new List<OrderRepository>();
        }

        public void Save(OrderRepository order)
        {

        }

    }
}
