using Alborz.DataLayer.IRepository;
using Alborz.DomainLayer.DTO;

namespace Alborz.DataLayer.IRepository
{
    public interface IOrderOperationRepository : IRepository<OrderOperationTbl, int>
    {
    }
}