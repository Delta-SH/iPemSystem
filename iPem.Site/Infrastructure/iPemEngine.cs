using Autofac;
using Autofac.Integration.Mvc;
using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Data;
using iPem.Core.Enum;
using iPem.Core.NPOI;
using iPem.Data.Installation;
using iPem.Data.Repository.Am;
using iPem.Data.Repository.Cs;
using iPem.Data.Repository.Rs;
using iPem.Data.Repository.Sc;
using iPem.Services.Am;
using iPem.Services.Cs;
using iPem.Services.Rs;
using iPem.Services.Sc;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace iPem.Site.Infrastructure {
    /// <summary>
    /// iPem Engine
    /// </summary>
    public class iPemEngine : IEngine {

        #region Fields

        private ContainerManager _containerManager;
        private iPemStore _appStore;
        private IDictionary<Guid, Store> _workStores;

        #endregion

        #region Utilities

        /// <summary>
        /// Register dependencies
        /// </summary>
        protected virtual void RegisterDependencies() {
            var builder = new ContainerBuilder();
            var container = builder.Build();

            //we create new instance of ContainerBuilder
            //because Build() or Update() method can only be called once on a ContainerBuilder.

            //register engine
            builder = new ContainerBuilder();
            builder.RegisterInstance(this).As<IEngine>().SingleInstance();

            //register controllers
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            //register http context
            builder.Register(c => new HttpContextWrapper(HttpContext.Current) as HttpContextBase)
                .As<HttpContextBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Request)
                .As<HttpRequestBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Response)
                .As<HttpResponseBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Server)
                .As<HttpServerUtilityBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Session)
                .As<HttpSessionStateBase>()
                .InstancePerLifetimeScope();

            //register core class
            builder.RegisterType<WebHelper>().As<IWebHelper>().InstancePerLifetimeScope();
            builder.RegisterType<SqliteDataProvider>().As<IDataProvider>().SingleInstance();
            builder.RegisterType<SqlDbManager>().As<IDbManager>().SingleInstance();
            builder.RegisterType<DbInstaller>().As<IDbInstaller>().SingleInstance();
            builder.RegisterType<RedisCacheManager>().As<ICacheManager>().SingleInstance();
            builder.RegisterType<ExcelManager>().As<IExcelManager>().SingleInstance();
            builder.RegisterType<iPemWorkContext>().As<IWorkContext>().InstancePerLifetimeScope();

            var dbManager = new SqlDbManager(new SqliteDataProvider());
            if(dbManager.IsValid(EnmDbType.Rs)) {
                var connectionString = dbManager.CurrentConnetions[EnmDbType.Rs];

                //register repository
                builder.Register<IAreaRepository>(c => new AreaRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IBrandRepository>(c => new BrandRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IDepartmentRepository>(c => new DepartmentRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IDeviceRepository>(c => new DeviceRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IDeviceTypeRepository>(c => new DeviceTypeRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IDutyRepository>(c => new DutyRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IEmployeeRepository>(c => new EmployeeRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IEnumMethodsRepository>(c => new EnumMethodsRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IFsuRepository>(c => new FsuRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<ILogicTypeRepository>(c => new LogicTypeRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IPointRepository>(c => new PointRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IProductorRepository>(c => new ProductorRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IProtocolRepository>(c => new ProtocolRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IRoomRepository>(c => new RoomRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IRoomTypeRepository>(c => new RoomTypeRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IStationRepository>(c => new StationRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IStationTypeRepository>(c => new StationTypeRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<ISubCompanyRepository>(c => new SubCompanyRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<ISupplierRepository>(c => new SupplierRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IUnitRepository>(c => new UnitRepository(connectionString)).InstancePerLifetimeScope();

                //register service
                builder.RegisterType<AreaService>().As<IAreaService>().InstancePerLifetimeScope();
                builder.RegisterType<BrandService>().As<IBrandService>().InstancePerLifetimeScope();
                builder.RegisterType<DepartmentService>().As<IDepartmentService>().InstancePerLifetimeScope();
                builder.RegisterType<DeviceService>().As<IDeviceService>().InstancePerLifetimeScope();
                builder.RegisterType<DeviceTypeService>().As<IDeviceTypeService>().InstancePerLifetimeScope();
                builder.RegisterType<DutyService>().As<IDutyService>().InstancePerLifetimeScope();
                builder.RegisterType<EmployeeService>().As<IEmployeeService>().InstancePerLifetimeScope();
                builder.RegisterType<EnumMethodsService>().As<IEnumMethodsService>().InstancePerLifetimeScope();
                builder.RegisterType<FsuService>().As<IFsuService>().InstancePerLifetimeScope();
                builder.RegisterType<LogicTypeService>().As<ILogicTypeService>().InstancePerLifetimeScope();
                builder.RegisterType<PointService>().As<IPointService>().InstancePerLifetimeScope();
                builder.RegisterType<ProductorService>().As<IProductorService>().InstancePerLifetimeScope();
                builder.RegisterType<ProtocolService>().As<IProtocolService>().InstancePerLifetimeScope();
                builder.RegisterType<RoomService>().As<IRoomService>().InstancePerLifetimeScope();
                builder.RegisterType<RoomTypeService>().As<IRoomTypeService>().InstancePerLifetimeScope();
                builder.RegisterType<StationService>().As<IStationService>().InstancePerLifetimeScope();
                builder.RegisterType<StationTypeService>().As<IStationTypeService>().InstancePerLifetimeScope();
                builder.RegisterType<SubCompanyService>().As<ISubCompanyService>().InstancePerLifetimeScope();
                builder.RegisterType<SupplierService>().As<ISupplierService>().InstancePerLifetimeScope();
                builder.RegisterType<UnitService>().As<IUnitService>().InstancePerLifetimeScope();
            }

            if(dbManager.IsValid(EnmDbType.Cs)) {
                var connectionString = dbManager.CurrentConnetions[EnmDbType.Cs];

                //register repository
                builder.Register<IActAlmRepository>(c => new ActAlmRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IFsuKeyRepository>(c => new FsuKeyRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IHisAlmRepository>(c => new HisAlmRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IHisBatRepository>(c => new HisBatRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IHisElecRepository>(c => new HisElecRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IHisStaticRepository>(c => new HisStaticRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IHisValueRepository>(c => new HisValueRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IHisLoadRateRepository>(c => new HisLoadRateRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IHisBatTimeRepository>(c => new HisBatTimeRepository(connectionString)).InstancePerLifetimeScope();

                //register service
                builder.RegisterType<ActAlmService>().As<IActAlmService>().InstancePerLifetimeScope();
                builder.RegisterType<FsuKeyService>().As<IFsuKeyService>().InstancePerLifetimeScope();
                builder.RegisterType<HisAlmService>().As<IHisAlmService>().InstancePerLifetimeScope();
                builder.RegisterType<HisBatService>().As<IHisBatService>().InstancePerLifetimeScope();
                builder.RegisterType<HisElecService>().As<IHisElecService>().InstancePerLifetimeScope();
                builder.RegisterType<HisStaticService>().As<IHisStaticService>().InstancePerLifetimeScope();
                builder.RegisterType<HisValueService>().As<IHisValueService>().InstancePerLifetimeScope();
                builder.RegisterType<HisLoadRateService>().As<IHisLoadRateService>().InstancePerLifetimeScope();
                builder.RegisterType<HisBatTimeService>().As<IHisBatTimeService>().InstancePerLifetimeScope();
            }

            if(dbManager.IsValid(EnmDbType.Sc)) {
                var connectionString = dbManager.CurrentConnetions[EnmDbType.Sc];

                //register repository
                builder.Register<IAppointmentRepository>(c => new AppointmentRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IAreasInRoleRepository>(c => new AreasInRoleRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IDictionaryRepository>(c => new DictionaryRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IExtendAlmRepository>(c => new ExtendAlmRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IFormulaRepository>(c => new FormulaRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IMenuRepository>(c => new MenuRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IMenusInRoleRepository>(c => new MenusInRoleRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<INodesInAppointmentRepository>(c => new NodesInAppointmentRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<INoticeInUserRepository>(c => new NoticeInUserRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<INoticeRepository>(c => new NoticeRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IOperateInRoleRepository>(c => new OperateInRoleRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IProfileRepository>(c => new ProfileRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IProjectRepository>(c => new ProjectRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IRoleRepository>(c => new RoleRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IUserRepository>(c => new UserRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IWebEventRepository>(c => new WebEventRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IAmDeviceRepository>(c => new AmDeviceRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IAmStationRepository>(c => new AmStationRepository(connectionString)).InstancePerLifetimeScope();

                //register service
                builder.RegisterType<AppointmentService>().As<IAppointmentService>().InstancePerLifetimeScope();
                builder.RegisterType<AreasInRoleService>().As<IAreasInRoleService>().InstancePerLifetimeScope();
                builder.RegisterType<DictionaryService>().As<IDictionaryService>().InstancePerLifetimeScope();
                builder.RegisterType<ExtendAlmService>().As<IExtendAlmService>().InstancePerLifetimeScope();
                builder.RegisterType<FormulaService>().As<IFormulaService>().InstancePerLifetimeScope();
                builder.RegisterType<MenuService>().As<IMenuService>().InstancePerLifetimeScope();
                builder.RegisterType<MenusInRoleService>().As<IMenusInRoleService>().InstancePerLifetimeScope();
                builder.RegisterType<NodesInAppointmentService>().As<INodesInAppointmentService>().InstancePerLifetimeScope();
                builder.RegisterType<NoticeInUserService>().As<INoticeInUserService>().InstancePerLifetimeScope();
                builder.RegisterType<NoticeService>().As<INoticeService>().InstancePerLifetimeScope();
                builder.RegisterType<OperateInRoleService>().As<IOperateInRoleService>().InstancePerLifetimeScope();
                builder.RegisterType<ProfileService>().As<IProfileService>().InstancePerLifetimeScope();
                builder.RegisterType<ProjectsService>().As<IProjectService>().InstancePerLifetimeScope();
                builder.RegisterType<RoleService>().As<IRoleService>().InstancePerLifetimeScope();
                builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
                builder.RegisterType<WebLogger>().As<IWebLogger>().InstancePerLifetimeScope();
                builder.RegisterType<AmDeviceService>().As<IAmDeviceService>().InstancePerLifetimeScope();
                builder.RegisterType<AmStationService>().As<IAmStationService>().InstancePerLifetimeScope();
            }

            builder.Update(container);

            this._containerManager = new ContainerManager(container);

            //set dependency resolver
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initialize components and plugins in the nop environment.
        /// </summary>
        public void Initialize() {
            //init global fields
            _appStore = new iPemStore();
            _workStores = new Dictionary<Guid, Store>();

            //register dependencies
            RegisterDependencies();
        }

        /// <summary>
        /// Resolve dependency
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns></returns>
        public T Resolve<T>() where T : class {
            return ContainerManager.Resolve<T>();
        }

        /// <summary>
        ///  Resolve dependency
        /// </summary>
        /// <param name="type">Type</param>
        /// <returns></returns>
        public object Resolve(Type type) {
            return ContainerManager.Resolve(type);
        }

        /// <summary>
        /// Resolve dependencies
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns></returns>
        public T[] ResolveAll<T>() {
            return ContainerManager.ResolveAll<T>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the container manager
        /// </summary>
        public ContainerManager ContainerManager {
            get { return _containerManager; }
        }

        /// <summary>
        /// Gets or sets the application store
        /// </summary>
        public iPemStore AppStore {
            get { return _appStore; }
        }

        /// <summary>
        /// Gets or sets the work stores
        /// </summary>
        public IDictionary<Guid, Store> WorkStores {
            get { return _workStores; }
        }

        #endregion

    }
}