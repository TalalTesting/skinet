using Skinet.Core.Entities.OrderAggregate;
using System;
using System.Linq.Expressions;

namespace Skinet.Core.Specifications
{
    public class OrderByPaymentIntentIdSpecification : BaseSpecification<Order>
    {
        public OrderByPaymentIntentIdSpecification(string paymentIntentId) 
            : base(o => o.PaymentIntentId == paymentIntentId)
        {
        }
    }
}
