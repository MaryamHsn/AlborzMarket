using Alborz.DataLayer.Context;
using Alborz.DomainLayer.DTO; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Alborz.ServiceLayer.Service
{ 
    public class InternetPaymentGetwayService : IInternetPaymentGetwayService
    {
        IUnitOfWork _uow;
        DateTime _now;
        public InternetPaymentGetwayService(IUnitOfWork uow)
        {
            _now = DateTime.Now;
            _uow = uow;
        }
        public void AddNewInternetPaymentGetway(InternetPaymentGetwayTbl InternetPaymentGetway)
        {
            _uow.InternetPaymentGetwayRepository.Add(InternetPaymentGetway);
            _uow.SaveAllChanges();
        }
        public IList<InternetPaymentGetwayTbl> GetAllInternetPaymentGetwaies()
        {
            return _uow.InternetPaymentGetwayRepository.GetAll().ToList();
        }
        public InternetPaymentGetwayTbl GetInternetPaymentGetway(int? id)
        {
            return _uow.InternetPaymentGetwayRepository.GetAll(x => x.Id == id).SingleOrDefault();
        }
        public bool Delete(int id)
        {
            InternetPaymentGetwayTbl InternetPaymentGetway = _uow.InternetPaymentGetwayRepository.Get(id);
            var t = _uow.InternetPaymentGetwayRepository.SoftDelete(InternetPaymentGetway);
            _uow.SaveAllChanges();
            return t;
        }
        ////Async 
        public async Task AddNewInternetPaymentGetwayAsync(InternetPaymentGetwayTbl InternetPaymentGetway, CancellationToken ct = new CancellationToken())
        {
            await _uow.InternetPaymentGetwayRepository.AddAsync(InternetPaymentGetway, ct);
            _uow.SaveAllChanges();
        }
        public async Task<IList<InternetPaymentGetwayTbl>> GetAllInternetPaymentGetwaiesAsync(CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.InternetPaymentGetwayRepository.GetAllAsync(ct);
            //return obj.Select(PropertyKeyMapper.Map).Where(x => x.IsActive == true).ToList();
            return obj.ToList();
        }
        public async Task<InternetPaymentGetwayTbl> GetInternetPaymentGetwayAsync(int? id, CancellationToken ct = new CancellationToken())
        {
            var obj = await _uow.InternetPaymentGetwayRepository.GetAllAsync(x => x.Id == id);
            return obj.FirstOrDefault();
        }
        public async Task<bool> DeleteAsync(int id, CancellationToken ct = new CancellationToken())
        {
            var InternetPaymentGetway = await _uow.InternetPaymentGetwayRepository.GetAsync(id, ct);
            var obj = await _uow.InternetPaymentGetwayRepository.SoftDeleteAsync(InternetPaymentGetway);
            _uow.SaveAllChanges();
            return obj;
        }
    }
}

