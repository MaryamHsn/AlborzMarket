using Alborz.DataLayer.IRepository;
using Alborz.DomainLayer.Entities;

namespace Alborz.DataLayer.IRepository
{
    public interface IOrderProcessHistoryRepository : IRepository<OrderProcessHistoryTbl, int>
    {
    }
}