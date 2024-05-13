using AutoMapper;
using DTOs.DTOs.Orders;
using Models.Models;
using Repository.Repositories;

namespace Services.Services.Interface
{
    public interface IOrderServices
    {
        public Task<GetUpdatedorCreaterOrderDTO> Create(CreateOrderDTO orderDTO, string customerID);
        public Task<List<GetAllOrdersDTO>> GetAll(string? userID);
        public Task<GetAllOrdersDTO> GetOne(int ID, string? userID);
        public Task<GetUpdatedorCreaterOrderDTO> Update(UpdateOrderDTO orderDTO, int ID);
        public Task<bool> Delete(int ID, string? userID);
    }
}
