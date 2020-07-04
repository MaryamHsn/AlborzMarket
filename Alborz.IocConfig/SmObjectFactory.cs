using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using StructureMap.Web;
using StructureMap;
using System.Data.Entity;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System;
using Alborz.DataLayer.Context;
using Alborz.DataLayer.IRepository;
using Alborz.DataLayer.Repository;
using Alborz.DomainLayer.Entities;
using Alborz.ServiceLayer;
using Alborz.ServiceLayer.IService;
using Alborz.ServiceLayer.Service;

namespace Alborz.IocConfig
{
    public static class SmObjectFactory
    {
        private static readonly Lazy<Container> _containerBuilder =
            new Lazy<Container>(defaultContainer, LazyThreadSafetyMode.ExecutionAndPublication);

        public static IContainer Container
        {
            get { return _containerBuilder.Value; }
        }

        private static Container defaultContainer()
        {
            return new Container(ioc =>
            {

                ioc.For<DbContext>().Use(ctx => new ApplicationDbContext());
                //ioc.For<IUnitOfWork>()
                // .HybridHttpOrThreadLocalScoped()
                // .Use<UnitOfWork>();

                //ioc.For<IRepository>().Use<BaseRepository>().WithTheSameGenericType();
                ioc.Scan(y =>
                {
                    y.TheCallingAssembly();
                    y.AddAllTypesOf(typeof(IRepository<,>));
                    y.WithDefaultConventions();
                });
                ioc.For(typeof(IRepository<,>)).DecorateAllWith(typeof(BaseRepository<,>));
                ioc.For(typeof(IRepository<,>)).Use(typeof(BaseRepository<,>));
                //ioc.Scan(x =>
                //{
                //    x.AssemblyContainingType(typeof(DriverRepository));
                //    x.AddAllTypesOf(typeof(IRepository<,>));
                //    x.ConnectImplementationsToTypesClosing(typeof(IRepository<,>));
                //});
                ioc.For<DbContext>().Use(ctx => new ApplicationDbContext());
                ioc.For(typeof(IRepository<,>)).Use(typeof(BaseRepository<,>));
                ioc.For(typeof(IRepository<,>)).DecorateAllWith(typeof(BaseRepository<,>));
                ioc.Scan(
            scan =>
            {
                scan.TheCallingAssembly();
                scan.WithDefaultConventions();
                scan.AssemblyContainingType<ApplicationDbContext>();
                scan.ConnectImplementationsToTypesClosing(typeof(IRepository<,>));


            });
                //ioc.For( typeof(BaseRepository<,>) )
                //   .HybridHttpOrThreadLocalScoped()
                //   .Use(typeof(IRepository<,>));
                ioc.For(typeof(IRepository<,>))
             .HybridHttpOrThreadLocalScoped()
             .Use(typeof(BaseRepository<,>));


                ioc.For<Microsoft.AspNet.SignalR.IDependencyResolver>()
                   .Singleton()
                   .Add<StructureMapSignalRDependencyResolver>();

                ioc.For<IIdentity>()
                   .HybridHttpOrThreadLocalScoped()
                   .Use(() => getIdentity());

                ioc.For<IUnitOfWork>()
                   .HybridHttpOrThreadLocalScoped()
                   .Use<ApplicationDbContext>();
                // Remove these 2 lines if you want to use a connection string named connectionString1, defined in the web.config file.
                //.Ctor<string>("connectionString")
                //.Is("Data Source=(local);Initial Catalog=TestDbIdentity;Integrated Security = true");

                ioc.For<ApplicationDbContext>()
                   .HybridHttpOrThreadLocalScoped()
                   .Use(context => (ApplicationDbContext)context.GetInstance<IUnitOfWork>());
                ioc.For<DbContext>()
                   .HybridHttpOrThreadLocalScoped()
                   .Use(context => (ApplicationDbContext)context.GetInstance<IUnitOfWork>());


                ioc.For<IUserStore<ApplicationUser, int>>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use<CustomUserStore>();

                ioc.For<IRoleStore<CustomRole, int>>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use<CustomRoleStore>();

                ioc.For<IAuthenticationManager>()
                   .HybridHttpOrThreadLocalScoped()
                   .Use(() => HttpContext.Current.GetOwinContext().Authentication);

                ioc.For<IApplicationSignInManager>()
                      .HybridHttpOrThreadLocalScoped()
                      .Use<ApplicationSignInManager>();

                ioc.For<IApplicationRoleManager>()
                      .HybridHttpOrThreadLocalScoped()
                      .Use<ApplicationRoleManager>();

                // map same interface to different concrete classes
                ioc.For<IIdentityMessageService>().Use<SmsService>();
                ioc.For<IIdentityMessageService>().Use<EmailService>();

                ioc.For<IApplicationUserManager>()
                   .HybridHttpOrThreadLocalScoped()
                   .Use<ApplicationUserManager>()
                   .Ctor<IIdentityMessageService>("smsService").Is<SmsService>()
                   .Ctor<IIdentityMessageService>("emailService").Is<EmailService>()
                   .Setter(userManager => userManager.SmsService).Is<SmsService>()
                   .Setter(userManager => userManager.EmailService).Is<EmailService>();

                ioc.For<ApplicationUserManager>()
                   .HybridHttpOrThreadLocalScoped()
                   .Use(context => (ApplicationUserManager)context.GetInstance<IApplicationUserManager>());

                ioc.For<ICustomRoleStore>()
                      .HybridHttpOrThreadLocalScoped()
                      .Use<CustomRoleStore>();

                ioc.For<ICustomUserStore>()
                      .HybridHttpOrThreadLocalScoped()
                      .Use<CustomUserStore>();

                //config.For<IDataProtectionProvider>().Use(() => app.GetDataProtectionProvider()); // In Startup class
                //Repository
                ioc.For<IAddressRepository>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use<AddressRepository>();

                ioc.For<ICartItemRepository>()
                         .HybridHttpOrThreadLocalScoped()
                         .Use<CartItemRepository>();

                ioc.For<ICartRepository>()
                         .HybridHttpOrThreadLocalScoped()
                         .Use<CartRepository>();

                ioc.For<ICategoryRepository>()
                         .HybridHttpOrThreadLocalScoped()
                         .Use<CategoryRepository>();
                
                ioc.For<IColorRepository>()
                         .HybridHttpOrThreadLocalScoped()
                         .Use<ColorRepository>();

                ioc.For<IFileRepository>()
                   .HybridHttpOrThreadLocalScoped()
                   .Use<FileRepository>();

                ioc.For<IPostRepository>()
                   .HybridHttpOrThreadLocalScoped()
                   .Use<PostRepository>();


                ioc.For<ICustomerRepository>()
                         .HybridHttpOrThreadLocalScoped()
                         .Use<CustomerRepository>();


                ioc.For<ICustomerTypeRepository>()
                         .HybridHttpOrThreadLocalScoped()
                         .Use<CustomerTypeRepository>();


                ioc.For<IDiscountRepository>()
                         .HybridHttpOrThreadLocalScoped()
                         .Use<DiscountRepository>();


                ioc.For<IErrorRepository>()
                         .HybridHttpOrThreadLocalScoped()
                         .Use<ErrorRepository>();


                ioc.For<IInternetPaymentGetwayRepository>()
                         .HybridHttpOrThreadLocalScoped()
                         .Use<InternetPaymentGetwayRepository>();


                ioc.For<IInvoiceProcessHistoryRepository>()
                         .HybridHttpOrThreadLocalScoped()
                         .Use<InvoiceProcessHistoryRepository>();


                ioc.For<IInvoiceProcessRepository>()
                         .HybridHttpOrThreadLocalScoped()
                         .Use<InvoiceProcessRepository>();


                ioc.For<IInvoiceRepository>()
                         .HybridHttpOrThreadLocalScoped()
                         .Use<InvoiceRepository>();


                ioc.For<IIPGHistoryRepository>()
                         .HybridHttpOrThreadLocalScoped()
                         .Use<IPGHistoryRepository>();


                ioc.For<IOrderOperationRepository>()
                         .HybridHttpOrThreadLocalScoped()
                         .Use<OrderOperationRepository>();


                ioc.For<IOrderProcessHistoryRepository>()
                         .HybridHttpOrThreadLocalScoped()
                         .Use<OrderProcessHistoryRepository>();


                ioc.For<IOrderProcessRepository>()
                         .HybridHttpOrThreadLocalScoped()
                         .Use<OrderProcessRepository>();


                ioc.For<IOrderRepository>()
                         .HybridHttpOrThreadLocalScoped()
                         .Use<OrderRepository>();


                ioc.For<IPaymentRepository>()
                         .HybridHttpOrThreadLocalScoped()
                         .Use<PaymentRepository>();


                ioc.For<IPriceRepository>()
                         .HybridHttpOrThreadLocalScoped()
                         .Use<PriceRepository>();


                ioc.For<IProductDetailRepository>()
                         .HybridHttpOrThreadLocalScoped()
                         .Use<ProductDetailRepository>();

                ioc.For<IProductRepository>()
                         .HybridHttpOrThreadLocalScoped()
                         .Use<ProductRepository>();


                ioc.For<IPropertyRepository>()
                         .HybridHttpOrThreadLocalScoped()
                         .Use<PropertyRepository>();


                ioc.For<IPropertyValueRepository>()
                         .HybridHttpOrThreadLocalScoped()
                         .Use<PropertyValueRepository>();


                ioc.For(typeof(IAddressRepository)).Use(typeof(AddressRepository));
                ioc.For(typeof(ICartItemRepository)).Use(typeof(CartItemRepository));
                ioc.For(typeof(ICartRepository)).Use(typeof(CartRepository));
                ioc.For(typeof(ICategoryRepository)).Use(typeof(CategoryRepository));
                ioc.For(typeof(IColorRepository)).Use(typeof(ColorRepository));
                ioc.For(typeof(IFileRepository)).Use(typeof(FileRepository));
                ioc.For(typeof(IPostRepository)).Use(typeof(PostRepository));
                ioc.For(typeof(ICustomerRepository)).Use(typeof(CustomerRepository));
                ioc.For(typeof(ICustomerTypeRepository)).Use(typeof(CustomerTypeRepository));
                ioc.For(typeof(IPaymentRepository)).Use(typeof(PaymentRepository));
                ioc.For(typeof(IDiscountRepository)).Use(typeof(DiscountRepository));
                ioc.For(typeof(IErrorRepository)).Use(typeof(ErrorRepository));
                ioc.For(typeof(IInternetPaymentGetwayRepository)).Use(typeof(InternetPaymentGetwayRepository));
                ioc.For(typeof(IInvoiceProcessHistoryRepository)).Use(typeof(InvoiceProcessHistoryRepository));
                ioc.For(typeof(IInvoiceProcessRepository)).Use(typeof(InvoiceProcessRepository));
                ioc.For(typeof(IInvoiceRepository)).Use(typeof(InvoiceRepository));
                ioc.For(typeof(IIPGHistoryRepository)).Use(typeof(IPGHistoryRepository));
                ioc.For(typeof(IOrderOperationRepository)).Use(typeof(OrderOperationRepository));
                ioc.For(typeof(IOrderProcessHistoryRepository)).Use(typeof(OrderProcessHistoryRepository));
                ioc.For(typeof(IOrderProcessRepository)).Use(typeof(OrderProcessRepository));
                ioc.For(typeof(IOrderRepository)).Use(typeof(OrderRepository));
                ioc.For(typeof(IProductDetailRepository)).Use(typeof(ProductDetailRepository));
                ioc.For(typeof(IProductRepository)).Use(typeof(ProductRepository));
                ioc.For(typeof(IPriceRepository)).Use(typeof(PriceRepository));
                ioc.For(typeof(IPropertyRepository)).Use(typeof(PropertyRepository));
                ioc.For(typeof(IPropertyValueRepository)).Use(typeof(PropertyValueRepository));

                /////
                ioc.For<IAddressService>()
          .HybridHttpOrThreadLocalScoped()
          .Use<AddressService>();

                ioc.For<ICartItemService>()
                         .HybridHttpOrThreadLocalScoped()
                         .Use<CartItemService>();

                ioc.For<ICartService>()
                         .HybridHttpOrThreadLocalScoped()
                         .Use<CartService>();

                ioc.For<ICategoryService>()
                         .HybridHttpOrThreadLocalScoped()
                         .Use<CategoryService>();

                ioc.For<IFileService>()
                 .HybridHttpOrThreadLocalScoped()
                 .Use<FileService>();

                ioc.For<IPostService>()
                 .HybridHttpOrThreadLocalScoped()
                 .Use<PostService>();


                ioc.For<ICustomerService>()
                         .HybridHttpOrThreadLocalScoped()
                         .Use<CustomerService>();


                ioc.For<ICustomerTypeService>()
                         .HybridHttpOrThreadLocalScoped()
                         .Use<CustomerTypeService>();

                ioc.For<IColorService>()
                         .HybridHttpOrThreadLocalScoped()
                         .Use<ColorService>();


                ioc.For<IDiscountService>()
                         .HybridHttpOrThreadLocalScoped()
                         .Use<DiscountService>();


                ioc.For<IErrorService>()
                         .HybridHttpOrThreadLocalScoped()
                         .Use<ErrorService>();


                ioc.For<IInternetPaymentGetwayService>()
                         .HybridHttpOrThreadLocalScoped()
                         .Use<InternetPaymentGetwayService>();


                ioc.For<IInvoiceProcessHistoryService>()
                         .HybridHttpOrThreadLocalScoped()
                         .Use<InvoiceProcessHistoryService>();


                ioc.For<IInvoiceProcessService>()
                         .HybridHttpOrThreadLocalScoped()
                         .Use<InvoiceProcessService>();


                ioc.For<IInvoiceService>()
                         .HybridHttpOrThreadLocalScoped()
                         .Use<InvoiceService>();


                ioc.For<IIPGHistoryService>()
                         .HybridHttpOrThreadLocalScoped()
                         .Use<IPGHistoryService>();


                ioc.For<IOrderOperationService>()
                         .HybridHttpOrThreadLocalScoped()
                         .Use<OrderOperationService>();


                ioc.For<IOrderProcessHistoryService>()
                         .HybridHttpOrThreadLocalScoped()
                         .Use<OrderProcessHistoryService>();


                ioc.For<IOrderProcessService>()
                         .HybridHttpOrThreadLocalScoped()
                         .Use<OrderProcessService>();


                ioc.For<IOrderService>()
                         .HybridHttpOrThreadLocalScoped()
                         .Use<OrderService>();


                ioc.For<IPaymentService>()
                         .HybridHttpOrThreadLocalScoped()
                         .Use<PaymentService>();


                ioc.For<IPriceService>()
                         .HybridHttpOrThreadLocalScoped()
                         .Use<PriceService>();


                ioc.For<IProductDetailService>()
                         .HybridHttpOrThreadLocalScoped()
                         .Use<ProductDetailService>();

                ioc.For<IProductService>()
                         .HybridHttpOrThreadLocalScoped()
                         .Use<ProductService>();


                ioc.For<IPropertyService>()
                         .HybridHttpOrThreadLocalScoped()
                         .Use<PropertyService>();


                ioc.For<IPropertyValueService>()
                         .HybridHttpOrThreadLocalScoped()
                         .Use<PropertyValueService>();
            });
        }

        private static IIdentity getIdentity()
        {
            if (HttpContext.Current != null && HttpContext.Current.User != null)
            {
                return HttpContext.Current.User.Identity;
            }

            return ClaimsPrincipal.Current != null ? ClaimsPrincipal.Current.Identity : null;
        }
    }
}