using Alborz.DataLayer.Context;
using Alborz.DomainLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Alborz.ServiceLayer.IService;

namespace Alborz.ServiceLayer.Service
{
    public class ErrorService : IErrorService
    {
        IUnitOfWork _uow;
        DateTime _now;
        public ErrorService(IUnitOfWork uow)
        {
            _now = DateTime.Now;
            _uow = uow;
        }
        public void AddNewError(ErrorTbl Error)
        {
            _uow.ErrorRepository.Add(Error);
            _uow.SaveAllChanges();
        }
        public IList<ErrorTbl> GetAllErrors()
        {
            return _uow.ErrorRepository.GetAll().ToList();
        }
        public ErrorTbl GetError(int? id)
        {
            return _uow.ErrorRepository.GetAll(x => x.Id == id).SingleOrDefault();
        }
        public bool Delete(int id)
        {
            ErrorTbl Error = _uow.ErrorRepository.Get(id);
            var t = _uow.ErrorRepository.SoftDelete(Error);
            _uow.SaveAllChanges();
            return t;
        }
        ////Async 
        public async Task AddNewErrorAsync(ErrorTbl Error, CancellationToken ct = new CancellationToken())
        {
            await _uow.ErrorRepository.AddAsync(Error, ct);
            _uow.SaveAllChanges();
        }
        public async Task<IList<ErrorTbl>> GetAllErrorsAsync(CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.ErrorRepository.GetAllAsync(ct);
            //return obj.Select(PropertyKeyMapper.Map).Where(x => x.IsActive == true).ToList();
            return obj.ToList();
        }
        public async Task<ErrorTbl> GetErrorAsync(int? id, CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.ErrorRepository.GetAllAsync(x => x.Id == id);
            return obj.FirstOrDefault();
        }
        public async Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken())
        {
            var Error = await _uow.ErrorRepository.GetAsync(id, ct);
            var obj = await _uow.ErrorRepository.SoftDeleteAsync(Error);
            _uow.SaveAllChanges();
            return obj;
        }
    }
}
