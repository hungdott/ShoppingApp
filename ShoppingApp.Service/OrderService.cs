using ShoppingApp.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingApp.Data.Repositories;
using ShoppingApp.Data.Infrastructure;

namespace ShoppingApp.Service
{
    public interface IOrderService
    {
        bool Create(Order order,List<OrderDetail> orderDatails);
    }
    public class OrderService : IOrderService
    {
        IOrderRepository _orderRepository;
        IOrderDetailRepository _orderDetailRepository;
        IUnitOfWork _unitOfWork;

        public OrderService(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository,IUnitOfWork unitOfWork)
        {
            this._orderRepository = orderRepository;
            this._orderDetailRepository = orderDetailRepository;
            this._unitOfWork = unitOfWork;
        }
        public bool Create(Order order, List<OrderDetail> orderDatails)
        {
            try
            {
                _orderRepository.Add(order);

                _unitOfWork.Commit();

                foreach (var orderDeTail in orderDatails)
                {
                    orderDeTail.OrderID = order.ID;
                    _orderDetailRepository.Add(orderDeTail);
                }
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception)
            {

                throw ;
            }
            
        }
    }
}
