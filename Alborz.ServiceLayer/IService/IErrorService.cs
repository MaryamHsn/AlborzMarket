using Alborz.DomainLayer.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Alborz.ServiceLayer.IService
{ 
    public interface IErrorService
    {
        void AddNewError(ErrorTbl Error);
        IList<ErrorTbl> GetAllErrors();
        ErrorTbl GetError(int? id);
        //  int Delete(int id);
        bool Delete(int id);
        Task AddNewErrorAsync(ErrorTbl Error, CancellationToken ct = new CancellationToken());
        Task<IList<ErrorTbl>> GetAllErrorsAsync(CancellationToken ct = new CancellationToken());
        Task<ErrorTbl> GetErrorAsync(int? id, CancellationToken ct = new CancellationToken());
        Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken());
    }
}