using Alborz.DataLayer.IRepository;
using Alborz.DataLayer.Repository;
using Alborz.DomainLayer.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alborz.DataLayer.Context
{
    public class ApplicationDbContext :
        IdentityDbContext<ApplicationUser, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>
        , IUnitOfWork
    {
        /// <summary>
        /// It looks for a connection string named connectionString1 in the web.config file.
        /// </summary>
        public ApplicationDbContext()
            : base("AlborzConnection")
        {
            //this.Database.Log = data => System.Diagnostics.Debug.WriteLine(data);
        }

        #region Dbset
        public virtual DbSet<AddressTbl> AddressTbls { get; set; }
        public virtual DbSet<CartItemTbl> CartItemTbls { get; set; }
        public virtual DbSet<CartTbl> CartTbls { get; set; }
        public virtual DbSet<CategoryTbl> CategoryTbls { get; set; }
        public virtual DbSet<ImageTbl> ImageTbls { get; set; }
        public virtual DbSet<PostTbl> PostTbls{ get; set; }
        public virtual DbSet<CustomerTbl> CustomerTbls { get; set; }
        public virtual DbSet<CustomerTypeTbl> CustomerTypeTbls { get; set; }
        public virtual DbSet<DiscountTbl> DiscountTbls { get; set; }
        public virtual DbSet<ErrorTbl> ErrorTbls { get; set; }
        public virtual DbSet<InternetPaymentGetwayTbl> InternetPaymentGetwayTbls { get; set; }
        public virtual DbSet<InvoiceProcessHistoryTbl> InvoiceProcessHistoryTbls { get; set; }
        public virtual DbSet<InvoiceProcessTbl> InvoiceProcessTbls { get; set; }
        public virtual DbSet<InvoiceTbl> InvoiceTbls { get; set; }
        public virtual DbSet<IPGHistoryTbl> IPGHistoryTbls { get; set; }
        public virtual DbSet<OrderOperationTbl> OrderOperationTbls { get; set; }
        public virtual DbSet<OrderProcessHistoryTbl> OrderProcessHistoryTbls { get; set; }
        public virtual DbSet<OrderProcessTbl> OrderProcessTbls { get; set; }
        public virtual DbSet<OrderTbl> OrderTbls { get; set; }
        public virtual DbSet<PaymentTbl> PaymentTbls { get; set; }
        public virtual DbSet<PriceTbl> PriceTbls { get; set; }
        public virtual DbSet<ProductTbl> ProductTbls { get; set; }
        public virtual DbSet<PropertyTbl> PropertyTbls { get; set; }
        public virtual DbSet<PropertyValueTbl> PropertyValueTbls { get; set; }
        #endregion

        #region OnModelCreating

        /// <summary>
        /// To change the connection string at runtime. See the SmObjectFactory class for more info.
        /// </summary>
        //public ApplicationDbContext(string connectionString)
        //    : base(connectionString)
        //{
        //    //Note: defaultConnectionFactory in the web.config file should be set.
        //}

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<CustomRole>().ToTable("Roles");
            builder.Entity<CustomUserClaim>().ToTable("UserClaims");
            builder.Entity<CustomUserRole>().ToTable("UserRoles");
            builder.Entity<CustomUserLogin>().ToTable("UserLogins");

            builder.Entity<CartItemTbl>()
              .Property(e => e.UnitPrice)
              .HasPrecision(18, 0);

            builder.Entity<CartItemTbl>()
                .Property(e => e.LineTotal)
                .HasPrecision(18, 0);

            builder.Entity<CartItemTbl>()
                .Property(e => e.Discount)
                .HasPrecision(18, 0);

            builder.Entity<CartItemTbl>()
                .Property(e => e.FinalPrice)
                .HasPrecision(18, 0);

            builder.Entity<CartTbl>()
                .Property(e => e.TotalAmount)
                .HasPrecision(18, 0);

            builder.Entity<CartTbl>()
                .Property(e => e.TotalDiscountAmount)
                .HasPrecision(18, 0);

            builder.Entity<CartTbl>()
                .Property(e => e.FinalAmount)
                .HasPrecision(18, 0);

            builder.Entity<CartTbl>()
                .HasMany(e => e.CartItemTbls)
                .WithRequired(e => e.CartTbl)
                .HasForeignKey(e => e.CartId)
                .WillCascadeOnDelete(false);

            builder.Entity<CartTbl>()
                .HasMany(e => e.InvoiceTbls)
                .WithRequired(e => e.CartTbl)
                .HasForeignKey(e => e.CartId)
                .WillCascadeOnDelete(false);

            //builder.Entity<CategoryTbl>()
            //    .HasMany(e => e.CategoryTbl1)
            //    .WithOptional(e => e.CategoryTbl2)
            //    .HasForeignKey(e => e.ParentCategoryId);

            builder.Entity<CategoryTbl>()
                .HasMany(e => e.ProductTbls)
                .WithRequired(e => e.CategoryTbl)
                .HasForeignKey(e => e.CategoryId)
                .WillCascadeOnDelete(false);

            builder.Entity<CategoryTbl>()
                .HasMany(e => e.PropertyTbls)
                .WithRequired(e => e.CategoryTbl)
                .HasForeignKey(e => e.CategoryId)
                .WillCascadeOnDelete(false);

            builder.Entity<CustomerTypeTbl>()
                .HasMany(e => e.CustomerTbls)
                .WithOptional(e => e.CustomerTypeTbl)
                .HasForeignKey(e => e.CustomerTypeId);

            builder.Entity<DiscountTbl>()
                .Property(e => e.PriceDiscount)
                .HasPrecision(18, 0);

            builder.Entity<DiscountTbl>()
                .Property(e => e.PerecentDiscount)
                .HasPrecision(18, 0);

            builder.Entity<ErrorTbl>()
                .HasMany(e => e.CartTbls)
                .WithOptional(e => e.ErrorTbl)
                .HasForeignKey(e => e.CancelReasonErrorID);

            builder.Entity<ErrorTbl>()
                .HasMany(e => e.OrderTbls)
                .WithOptional(e => e.ErrorTbl)
                .HasForeignKey(e => e.CancelReasonErrorID);

            builder.Entity<InvoiceProcessTbl>()
                .HasMany(e => e.InvoiceProcessHistoryTbls)
                .WithRequired(e => e.InvoiceProcessTbl)
                .HasForeignKey(e => e.InvoiceProcessId)
                .WillCascadeOnDelete(false);

            builder.Entity<InvoiceProcessTbl>()
                .HasMany(e => e.InvoiceTbls)
                .WithOptional(e => e.InvoiceProcessTbl)
                .HasForeignKey(e => e.CurrentProcessId);

            builder.Entity<InvoiceTbl>()
                .Property(e => e.TotalAmount)
                .HasPrecision(18, 0);

            builder.Entity<InvoiceTbl>()
                .Property(e => e.TotalDiscount)
                .HasPrecision(18, 0);

            builder.Entity<InvoiceTbl>()
                .Property(e => e.FinalPrice)
                .HasPrecision(18, 0);

            builder.Entity<InvoiceTbl>()
                .HasMany(e => e.InvoiceProcessHistoryTbls)
                .WithRequired(e => e.InvoiceTbl)
                .HasForeignKey(e => e.InvoiceId)
                .WillCascadeOnDelete(false);

            builder.Entity<InvoiceTbl>()
                .HasMany(e => e.OrderTbls)
                .WithRequired(e => e.InvoiceTbl)
                .HasForeignKey(e => e.InvoiceId)
                .WillCascadeOnDelete(false);

            builder.Entity<OrderOperationTbl>()
                .Property(e => e.Sum)
                .HasPrecision(18, 0);

            builder.Entity<OrderProcessTbl>()
                .HasMany(e => e.OrderProcessHistoryTbls)
                .WithRequired(e => e.OrderProcessTbl)
                .HasForeignKey(e => e.OrderProcessId)
                .WillCascadeOnDelete(false);

            builder.Entity<OrderProcessTbl>()
                .HasMany(e => e.OrderTbls)
                .WithRequired(e => e.OrderProcessTbl)
                .HasForeignKey(e => e.CurrentOrderProcessId)
                .WillCascadeOnDelete(false);

            builder.Entity<OrderTbl>()
                .Property(e => e.UnitPrice)
                .HasPrecision(18, 0);

            builder.Entity<OrderTbl>()
                .Property(e => e.Discount)
                .HasPrecision(18, 0);

            builder.Entity<OrderTbl>()
                .HasMany(e => e.OrderProcessHistoryTbls)
                .WithRequired(e => e.OrderTbl)
                .HasForeignKey(e => e.OrderId)
                .WillCascadeOnDelete(false);

            builder.Entity<PaymentTbl>()
                .Property(e => e.Amount)
                .HasPrecision(18, 0);

            builder.Entity<ProductTbl>()
                .HasMany(e => e.PropertyTbls)
                .WithRequired(e => e.ProductTbl)
                .HasForeignKey(e => e.productId)
                .WillCascadeOnDelete(false);

            builder.Entity<PropertyTbl>()
                .HasMany(e => e.PropertyValueTbls)
                .WithOptional(e => e.PropertyTbl)
                .HasForeignKey(e => e.PropertyId);
        }
        #endregion

        #region defineRepository


        private BaseRepository<AddressTbl, int> _addresssRepository;
        public IRepository<AddressTbl, int> AddressRepository
        {
            get
            {
                return _addresssRepository ??
                    (_addresssRepository = new BaseRepository<AddressTbl, int>(this));
            }
        }
        private BaseRepository<CartItemTbl, int> _cartItemsRepository;
        public IRepository<CartItemTbl, int> CartItemRepository
        {
            get
            {
                return _cartItemsRepository ??
                    (_cartItemsRepository = new BaseRepository<CartItemTbl, int>(this));
            }
        }
        private BaseRepository<CartTbl, int> _cartsRepository;
        public IRepository<CartTbl, int> CartRepository
        {
            get
            {
                return _cartsRepository ??
                    (_cartsRepository = new BaseRepository<CartTbl, int>(this));
            }
        }
        private BaseRepository<CategoryTbl, int> _categorysRepository;
        public IRepository<CategoryTbl, int> CategoryRepository
        {
            get
            {
                return _categorysRepository ??
                    (_categorysRepository = new BaseRepository<CategoryTbl, int>(this));
            }
        }
        private BaseRepository<ImageTbl, int> _imageRepository;
        public IRepository<ImageTbl, int> ImageRepository
        {
            get
            {
                return _imageRepository ??
                    (_imageRepository = new BaseRepository<ImageTbl, int>(this));
            }
        }
        private BaseRepository<PostTbl, int> _postsRepository;
        public IRepository<PostTbl, int> PostRepository
        {
            get
            {
                return _postsRepository ??
                    (_postsRepository = new BaseRepository<PostTbl, int>(this));
            }
        }
        private BaseRepository<CustomerTbl, int> _customersRepository;
        public IRepository<CustomerTbl, int> CustomerRepository
        {
            get
            {
                return _customersRepository ??
                    (_customersRepository = new BaseRepository<CustomerTbl, int>(this));
            }
        }
        private BaseRepository<CustomerTypeTbl, int> _customerTypesRepository;
        public IRepository<CustomerTypeTbl, int> CustomerTypeRepository
        {
            get
            {
                return _customerTypesRepository ??
                    (_customerTypesRepository = new BaseRepository<CustomerTypeTbl, int>(this));
            }
        }
        private BaseRepository<DiscountTbl, int> _discountsRepository;
        public IRepository<DiscountTbl, int> DiscountRepository
        {
            get
            {
                return _discountsRepository ??
                    (_discountsRepository = new BaseRepository<DiscountTbl, int>(this));
            }
        }
        private BaseRepository<ErrorTbl, int> _errorsRepository;
        public IRepository<ErrorTbl, int> ErrorRepository
        {
            get
            {
                return _errorsRepository ??
                    (_errorsRepository = new BaseRepository<ErrorTbl, int>(this));
            }
        }
        private BaseRepository<InternetPaymentGetwayTbl, int> _internetPaymentGetwayiesRepository;
        public IRepository<InternetPaymentGetwayTbl, int> InternetPaymentGetwayRepository
        {
            get
            {
                return _internetPaymentGetwayiesRepository ??
                    (_internetPaymentGetwayiesRepository = new BaseRepository<InternetPaymentGetwayTbl, int>(this));
            }
        }
        private BaseRepository<InvoiceProcessHistoryTbl, int> _invoiceProcessHistoriesRepository;
        public IRepository<InvoiceProcessHistoryTbl, int> InvoiceProcessHistoryRepository
        {
            get
            {
                return _invoiceProcessHistoriesRepository ??
                    (_invoiceProcessHistoriesRepository = new BaseRepository<InvoiceProcessHistoryTbl, int>(this));
            }
        }
        private BaseRepository<InvoiceProcessTbl, int> _invoiceProcesssRepository;
        public IRepository<InvoiceProcessTbl, int> InvoiceProcessRepository
        {
            get
            {
                return _invoiceProcesssRepository ??
                    (_invoiceProcesssRepository = new BaseRepository<InvoiceProcessTbl, int>(this));
            }
        }
        private BaseRepository<InvoiceTbl, int> _invoicesRepository;
        public IRepository<InvoiceTbl, int> InvoiceRepository
        {
            get
            {
                return _invoicesRepository ??
                    (_invoicesRepository = new BaseRepository<InvoiceTbl, int>(this));
            }
        }
        private BaseRepository<IPGHistoryTbl, int> _iPGHistoriesRepository;
        public IRepository<IPGHistoryTbl, int> IPGHistoryRepository
        {
            get
            {
                return _iPGHistoriesRepository ??
                    (_iPGHistoriesRepository = new BaseRepository<IPGHistoryTbl, int>(this));
            }
        }
        private BaseRepository<OrderOperationTbl, int> _orderOperationsRepository;
        public IRepository<OrderOperationTbl, int> OrderOperationRepository
        {
            get
            {
                return _orderOperationsRepository ??
                    (_orderOperationsRepository = new BaseRepository<OrderOperationTbl, int>(this));
            }
        }
        private BaseRepository<OrderProcessHistoryTbl, int> _orderProcessHistoriesRepository;
        public IRepository<OrderProcessHistoryTbl, int> OrderProcessHistoryRepository
        {
            get
            {
                return _orderProcessHistoriesRepository ??
                    (_orderProcessHistoriesRepository = new BaseRepository<OrderProcessHistoryTbl, int>(this));
            }
        }
        private BaseRepository<OrderProcessTbl, int> _orderProcesssRepository;
        public IRepository<OrderProcessTbl, int> OrderProcessRepository
        {
            get
            {
                return _orderProcesssRepository ??
                    (_orderProcesssRepository = new BaseRepository<OrderProcessTbl, int>(this));
            }
        }
        private BaseRepository<OrderTbl, int> _ordersRepository;
        public IRepository<OrderTbl, int> OrderRepository
        {
            get
            {
                return _ordersRepository ??
                    (_ordersRepository = new BaseRepository<OrderTbl, int>(this));
            }
        }
        private BaseRepository<PaymentTbl, int> _paymentsRepository;
        public IRepository<PaymentTbl, int> PaymentRepository
        {
            get
            {
                return _paymentsRepository ??
                    (_paymentsRepository = new BaseRepository<PaymentTbl, int>(this));
            }
        }
        private BaseRepository<PriceTbl, int> _pricesRepository;
        public IRepository<PriceTbl, int> PriceRepository
        {
            get
            {
                return _pricesRepository ??
                    (_pricesRepository = new BaseRepository<PriceTbl, int>(this));
            }
        }
        private BaseRepository<ProductTbl, int> _productsRepository;
        public IRepository<ProductTbl, int> ProductRepository
        {
            get
            {
                return _productsRepository ??
                    (_productsRepository = new BaseRepository<ProductTbl, int>(this));
            }
        }
        private BaseRepository<PropertyTbl, int> _propertysRepository;
        public IRepository<PropertyTbl, int> PropertyRepository
        {
            get
            {
                return _propertysRepository ??
                    (_propertysRepository = new BaseRepository<PropertyTbl, int>(this));
            }
        }
        private BaseRepository<PropertyValueTbl, int> _propertyValuesRepository;
        public IRepository<PropertyValueTbl, int> PropertyValueRepository
        {
            get
            {
                return _propertyValuesRepository ??
                    (_propertyValuesRepository = new BaseRepository<PropertyValueTbl, int>(this));
            }
        }
        #endregion
        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public int SaveAllChanges()
        {
            return base.SaveChanges();
        }

        public IEnumerable<TEntity> AddThisRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            return ((DbSet<TEntity>)this.Set<TEntity>()).AddRange(entities);
        }

        public void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class
        {
            base.Entry(entity).State = EntityState.Modified;
        }

        public IList<T> GetRows<T>(string sql, params object[] parameters) where T : class
        {
            return base.Database.SqlQuery<T>(sql, parameters).ToList();
        }

        public void ForceDatabaseInitialize()
        {
            base.Database.Initialize(force: true);
        }
         
    }
}
