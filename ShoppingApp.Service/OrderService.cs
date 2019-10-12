using ShoppingApp.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingApp.Data.Repositories;
using ShoppingApp.Data.Infrastructure;
using ShoppingApp.Common.ViewModels;

namespace ShoppingApp.Service
{
    public interface IOrderService
    {
        bool Create(Order order,List<OrderDetail> orderDatails);
        IEnumerable<OrderFullViewModel> GetAll();
        IEnumerable<OrderFullViewModel> GetAll(string keyword);
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

                return true;
            }
            catch (Exception)
            {

                throw ;
            }
            
        }

        public IEnumerable<OrderFullViewModel> GetAll()
        {
            try
            {

               
                var listOrderFull = new List<OrderFullViewModel>();

                var listOrder = _orderRepository.GetAll();
                if (listOrder.Count() > 0)
                {
                    foreach (var order in listOrder)
                    {
                        var orderFull = new OrderFullViewModel();
                        orderFull.Order = order;
                        orderFull.CreatedDate = order.CreatedDate;
                        var listOrderDetail = _orderDetailRepository.GetAll().Where(x => x.OrderID == order.ID).ToList();
                        orderFull.ListOrderDetail = listOrderDetail;
                        listOrderFull.Add(orderFull);
                    }
                }
               
                return listOrderFull;
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }
        public IEnumerable<OrderFullViewModel> GetAll(string keyword)
        {
            try
            {
                var query = from od in _orderRepository.GetAll()                             
                            select new OrderFullViewModel
                            {
                                CreatedDate = od.CreatedDate,
                                Order = od,
                                ListOrderDetail = (from ordt in _orderDetailRepository.GetAll()
                                                   where ordt.OrderID == od.ID
                                                   select ordt).ToList()
                            };
                var listOrderFull = query.ToList();
                //var listOrderFull = new List<OrderFullViewModel>();

                //var listOrder = _orderRepository.GetAll().ToList();
                //if (listOrder.Count() > 0)
                //{
                //    foreach (var order in listOrder)
                //    {
                //        var orderFull = new OrderFullViewModel();
                //        orderFull.Order = order;
                //        orderFull.CreatedDate = order.CreatedDate;
                //        var listOrderDetail = _orderDetailRepository.GetAll().Where(x => x.OrderID == order.ID).ToList();
                //        orderFull.ListOrderDetail = listOrderDetail;
                //        listOrderFull.Add(orderFull);
                //    }
                //}

                if (!string.IsNullOrEmpty(keyword))
                    return listOrderFull.Where(x => x.Order.CustomerName.Contains(keyword));
                else
                    return listOrderFull.AsQueryable();
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }


    }
}
