using ECommerce.Core.model.OrderAggrgate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Specifications.OrderSpec
{
    public class OrderSpecification : Specification<Order>
    {
        public OrderSpecification(string email):base(O=>O.BuyerEmail==email)
        {
            Includes.Add(O => O.Deliverymethod);
            Includes.Add(O=>O.Items);
            AddOrderByDesc(O=>O.OrderDate);
        }
        public OrderSpecification(string email , int OrderId):base(O=>O.BuyerEmail==email && O.id==OrderId)
        {
            Includes.Add(O => O.Deliverymethod);
            Includes.Add(O => O.Items);

        }

    }
}
