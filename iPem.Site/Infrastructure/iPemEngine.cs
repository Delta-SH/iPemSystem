using Autofac;
using Autofac.Integration.Mvc;
using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Data;
using iPem.Core.Enum;
using iPem.Core.NPOI;
using iPem.Data.Installation;
using MsData = iPem.Data.Repository.Master;
using MsSrv = iPem.Services.Master;
using HsData = iPem.Data.Repository.History;
using HsSrv = iPem.Services.History;
using RsData = iPem.Data.Repository.Resource;
using RsSrv = iPem.Services.Resource;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web;
using System.Web.Mvc;


namespace iPem.Site.Infrastructure {
    /// <summary>
    /// Engine
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
            if(dbManager.IsValid(EnmDatabaseType.Master)) {
                var connectionString = dbManager.CurrentConnetions[EnmDatabaseType.Master];

                //register repository
                builder.Register<MsData.IWebEventRepository>(c => new MsData.WebEventRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<MsData.IRoleRepository>(c => new MsData.RoleRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<MsData.IUserRepository>(c => new MsData.UserRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<MsData.IMenuRepository>(c => new MsData.MenuRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<MsData.IMenusInRoleRepository>(c => new MsData.MenusInRoleRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<MsData.INoticeRepository>(c => new MsData.NoticeRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<MsData.INoticeInUserRepository>(c => new MsData.NoticeInUserRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<MsData.IOperateInRoleRepository>(c => new MsData.OperateInRoleRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<MsData.IAreaRepository>(c => new MsData.AreaRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<MsData.IAreasInRoleRepository>(c => new MsData.AreasInRoleRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<MsData.IStationRepository>(c => new MsData.StationRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<MsData.IRoomRepository>(c => new MsData.RoomRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<MsData.IDeviceRepository>(c => new MsData.DeviceRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<MsData.IPointRepository>(c => new MsData.PointRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<MsData.IProtocolRepository>(c => new MsData.ProtocolRepository(connectionString)).InstancePerLifetimeScope();

                //register service
                builder.RegisterType<MsSrv.WebLogger>().As<MsSrv.IWebLogger>().InstancePerLifetimeScope();
                builder.RegisterType<MsSrv.RoleService>().As<MsSrv.IRoleService>().InstancePerLifetimeScope();
                builder.RegisterType<MsSrv.UserService>().As<MsSrv.IUserService>().InstancePerLifetimeScope();
                builder.RegisterType<MsSrv.MenuService>().As<MsSrv.IMenuService>().InstancePerLifetimeScope();
                builder.RegisterType<MsSrv.MenusInRoleService>().As<MsSrv.IMenusInRoleService>().InstancePerLifetimeScope();
                builder.RegisterType<MsSrv.NoticeService>().As<MsSrv.INoticeService>().InstancePerLifetimeScope();
                builder.RegisterType<MsSrv.NoticeInUserService>().As<MsSrv.INoticeInUserService>().InstancePerLifetimeScope();
                builder.RegisterType<MsSrv.OperateInRoleService>().As<MsSrv.IOperateInRoleService>().InstancePerLifetimeScope();
                builder.RegisterType<MsSrv.AreaService>().As<MsSrv.IAreaService>().InstancePerLifetimeScope();
                builder.RegisterType<MsSrv.AreasInRoleService>().As<MsSrv.IAreasInRoleService>().InstancePerLifetimeScope();
                builder.RegisterType<MsSrv.StationService>().As<MsSrv.IStationService>().InstancePerLifetimeScope();
                builder.RegisterType<MsSrv.RoomService>().As<MsSrv.IRoomService>().InstancePerLifetimeScope();
                builder.RegisterType<MsSrv.DeviceService>().As<MsSrv.IDeviceService>().InstancePerLifetimeScope();
                builder.RegisterType<MsSrv.PointService>().As<MsSrv.IPointService>().InstancePerLifetimeScope();
                builder.RegisterType<MsSrv.ProtocolService>().As<MsSrv.IProtocolService>().InstancePerLifetimeScope();
            }

            if(dbManager.IsValid(EnmDatabaseType.History)) {
                var connectionString = dbManager.CurrentConnetions[EnmDatabaseType.History];

                //register repository
                builder.Register<HsData.IActAlmRepository>(c => new HsData.ActAlmRepository(connectionString)).InstancePerLifetimeScope();

                //register service
                builder.RegisterType<HsSrv.ActAlmService>().As<HsSrv.IActAlmService>().InstancePerLifetimeScope();
            }

            if(dbManager.IsValid(EnmDatabaseType.Resource)) {
                var connectionString = dbManager.CurrentConnetions[EnmDatabaseType.Resource];

                //register repository
                builder.Register<RsData.IEmployeeRepository>(c => new RsData.EmployeeRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<RsData.IDepartmentRepository>(c => new RsData.DepartmentRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<RsData.IAreaRepository>(c => new RsData.AreaRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<RsData.IStationRepository>(c => new RsData.StationRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<RsData.IRoomRepository>(c => new RsData.RoomRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<RsData.IDeviceRepository>(c => new RsData.DeviceRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<RsData.IBrandRepository>(c => new RsData.BrandRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<RsData.IDeviceStatusRepository>(c => new RsData.DeviceStatusRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<RsData.IDeviceTypeRepository>(c => new RsData.DeviceTypeRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<RsData.IDutyRepository>(c => new RsData.DutyRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<RsData.IEnumMethodsRepository>(c => new RsData.EnumMethodsRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<RsData.ILogicTypeRepository>(c => new RsData.LogicTypeRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<RsData.IProductorRepository>(c => new RsData.ProductorRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<RsData.IRoomTypeRepository>(c => new RsData.RoomTypeRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<RsData.IStationTypeRepository>(c => new RsData.StationTypeRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<RsData.ISubCompanyRepository>(c => new RsData.SubCompanyRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<RsData.ISubDeviceTypeRepository>(c => new RsData.SubDeviceTypeRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<RsData.ISupplierRepository>(c => new RsData.SupplierRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<RsData.IUnitRepository>(c => new RsData.UnitRepository(connectionString)).InstancePerLifetimeScope();
                                
                //register service
                builder.RegisterType<RsSrv.EmployeeService>().As<RsSrv.IEmployeeService>().InstancePerLifetimeScope();
                builder.RegisterType<RsSrv.DepartmentService>().As<RsSrv.IDepartmentService>().InstancePerLifetimeScope();
                builder.RegisterType<RsSrv.AreaService>().As<RsSrv.IAreaService>().InstancePerLifetimeScope();
                builder.RegisterType<RsSrv.StationService>().As<RsSrv.IStationService>().InstancePerLifetimeScope();
                builder.RegisterType<RsSrv.RoomService>().As<RsSrv.IRoomService>().InstancePerLifetimeScope();
                builder.RegisterType<RsSrv.DeviceService>().As<RsSrv.IDeviceService>().InstancePerLifetimeScope();
                builder.RegisterType<RsSrv.BrandService>().As<RsSrv.IBrandService>().InstancePerLifetimeScope();
                builder.RegisterType<RsSrv.DeviceStatusService>().As<RsSrv.IDeviceStatusService>().InstancePerLifetimeScope();
                builder.RegisterType<RsSrv.DeviceTypeService>().As<RsSrv.IDeviceTypeService>().InstancePerLifetimeScope();
                builder.RegisterType<RsSrv.DutyService>().As<RsSrv.IDutyService>().InstancePerLifetimeScope();
                builder.RegisterType<RsSrv.EnumMethodsService>().As<RsSrv.IEnumMethodsService>().InstancePerLifetimeScope();
                builder.RegisterType<RsSrv.LogicTypeService>().As<RsSrv.ILogicTypeService>().InstancePerLifetimeScope();
                builder.RegisterType<RsSrv.ProductorService>().As<RsSrv.IProductorService>().InstancePerLifetimeScope();
                builder.RegisterType<RsSrv.RoomTypeService>().As<RsSrv.IRoomTypeService>().InstancePerLifetimeScope();
                builder.RegisterType<RsSrv.StationTypeService>().As<RsSrv.IStationTypeService>().InstancePerLifetimeScope();
                builder.RegisterType<RsSrv.SubCompanyService>().As<RsSrv.ISubCompanyService>().InstancePerLifetimeScope();
                builder.RegisterType<RsSrv.SubDeviceTypeService>().As<RsSrv.ISubDeviceTypeService>().InstancePerLifetimeScope();
                builder.RegisterType<RsSrv.SupplierService>().As<RsSrv.ISupplierService>().InstancePerLifetimeScope();
                builder.RegisterType<RsSrv.UnitService>().As<RsSrv.IUnitService>().InstancePerLifetimeScope();
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