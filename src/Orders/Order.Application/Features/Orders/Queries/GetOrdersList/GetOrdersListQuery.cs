using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.Orders.Queries.GetOrdersList
{

    /* MediatR for CQRS Design Pattern 
     * Every MediatR [IRequest] class must have Handler class for the implementation
     */
    public class GetOrdersListQuery: IRequest<List<OrdersVm>>
    {
        public string OrderRef { get; set; }

        public GetOrdersListQuery(string orderRef)
        {
            OrderRef = orderRef ?? throw new ArgumentNullException(nameof(orderRef));
        }
    }
}
