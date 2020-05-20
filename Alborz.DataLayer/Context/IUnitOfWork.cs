using Alborz.DataLayer.IRepository;
using Alborz.DomainLayer.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alborz.DataLayer.Context
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<AddressTbl, int> AddressRepository { get; }
        IRepository<CartItemTbl, int> CartItemRepository { get; }
        IRepository<CartTbl, int> CartRepository { get; }
        IRepository<CategoryTbl, int> CategoryRepository { get; }
        IRepository<CustomerTbl, int> CustomerRepository { get; }
        IRepository<CustomerTypeTbl, int> CustomerTypeRepository { get; }
        IRepository<DiscountTbl, int> Discounts { get; }
        IRepository<ErrorTbl, int> ErrorRepository { get; }
        IRepository<InternetPaymentGetwayTbl, int> InternetPaymentGetwayRepository { get; }
        IRepository<InvoiceProcessHistoryTbl, int> InvoiceProcessHistoryRepository { get; }
        IRepository<InvoiceProcessTbl, int> InvoiceProcessRepository { get; }
        IRepository<InvoiceTbl, int> InvoiceRepository { get; }
        IRepository<IPGHistoryTbl, int> IPGHistoryRepository { get; }
        IRepository<OrderOperationTbl, int> OrderOperations { get; }
        IRepository<OrderProcessHistoryTbl, int> OrderProcessHistoryRepository { get; }
        IRepository<OrderProcessTbl, int> OrderProcessRepository { get; }
        IRepository<OrderTbl, int> OrderRepository { get; }
        IRepository<PaymentTbl, int> PaymentRepository { get; }
        IRepository<PriceTbl, int> PriceRepository { get; }
        IRepository<ProductTbl, int> ProductRepository { get; }
        IRepository<PropertyTbl, int> PropertyRepository { get; }
        IRepository<PropertyValueTbl, int> PropertyValueRepository { get; }
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveAllChanges();
        void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class;
        IList<T> GetRows<T>(string sql, params object[] parameters) where T : class;
        IEnumerable<TEntity> AddThisRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
        void ForceDatabaseInitialize();
        // Task<TEntity> GetAsync<TEntity, TU>(TU id, CancellationToken ct = new CancellationToken()) where TEntity : class where TU : struct;


    }
}
