using AutoMapper;
using DTOs.DTOs.Orders;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Repository.Contract;
using Services.Services.Interface;


namespace Services.Services.Classes
{
    public class OrderServices : IOrderServices
    {
        protected IOrderRepository orderRepository;
        protected IProductRepository productRepository;
        protected IMapper mapper;
        public OrderServices(IOrderRepository orderRepository, IProductRepository productRepository, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        // _________________________ 1.Create new Order _________________________
        public async Task<GetUpdatedorCreaterOrderDTO> Create(CreateOrderDTO orderDTO, string customerID)
        {
            var orderedProducts = orderDTO.Products;
            foreach (var product in orderedProducts)
            {
                var findProduct = await productRepository.GetOneByID(product.ProductID);
                if (findProduct == null)
                {
                    return null;
                }
            }
            var createOrder = mapper.Map<Order>(orderDTO);
            createOrder.CustomerID = customerID;
            await orderRepository.Create(createOrder);
            var getCreatedOrder = await orderRepository.GetLastOrder();
            var mappingOrder = mapper.Map<GetUpdatedorCreaterOrderDTO>(getCreatedOrder);
            return mappingOrder;
        }
        // ___________________________ 2.Get All  ___________________________
        public async Task<List<GetAllOrdersDTO>> GetAll(string? userID)
        {
            var orders = await orderRepository.GetAll();
            List<GetAllOrdersDTO> ordersList;
            if (userID != null)
            {
                orders = orders.Where(o => o.CustomerID == userID);
            }
            var printOrders = orders
                .Include(o => o.Products)
                .Select(o => mapper.Map<GetAllOrdersDTO>(o));
            ordersList = printOrders.ToList();
            return ordersList;

        }
        // ___________________________ 3.Get One  ___________________________
        public async Task<GetAllOrdersDTO> GetOne(int ID, string? userID)
        {
            var findOrder = await orderRepository.GetOneByID(ID);
            var mappingOrder = mapper.Map<GetAllOrdersDTO>(findOrder);
            if (userID == null)
            {
                return mappingOrder;
            }
            else
            {
                if (findOrder != null && findOrder.CustomerID == userID)
                {
                    return mappingOrder;
                }
                else
                {
                    return null;
                }
            }
        }
        // __________________________ 4.Update Order ___________________________
        public async Task<GetUpdatedorCreaterOrderDTO> Update(UpdateOrderDTO orderDTO, int ID)
        {
            var findOrder = await orderRepository.GetOneByID(ID);
            if (findOrder != null)
            {
                orderDTO.Status = char.ToUpper(orderDTO.Status[0]) + orderDTO.Status.Substring(1);
                mapper.Map(orderDTO, findOrder);
                await orderRepository.Update();
                var printUpdatedOrder = mapper.Map<GetUpdatedorCreaterOrderDTO>(findOrder);
                return printUpdatedOrder;
            }
            else
            {
                return null;
            }

        }
        // __________________________ 5.Delete Order ___________________________
        public async Task<bool> Delete(int ID, string? userID)
        {
            var findOrder = await orderRepository.GetOneByID(ID);
            if (findOrder != null)
            {
                if (userID == null)
                {
                    await orderRepository.Delete(findOrder);
                    return true;
                }
                else
                {
                    if (findOrder.CustomerID == userID)
                    {
                        await orderRepository.Delete(findOrder);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }
    }
}
