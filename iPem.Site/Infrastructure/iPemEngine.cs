using Autofac;
using Autofac.Integration.Mvc;
using iPem.Core;
using iPem.Core.Caching;
using iPem.Core.Data;
using iPem.Core.Enum;
using iPem.Core.NPOI;
using iPem.Data.Installation;
using iPem.Data.Repository.Cs;
using iPem.Data.Repository.Rs;
using iPem.Data.Repository.Sc;
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
        private Dictionary<Guid, Store> _workStores;

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

            //register bi class
            builder.RegisterType<SCPackMgr>().As<IPackMgr>().InstancePerLifetimeScope();

            var dbManager = new SqlDbManager(new SqliteDataProvider());
            if(dbManager.IsValid(EnmDbType.Rs)) {
                var connectionString = dbManager.CurrentConnetions[EnmDbType.Rs];

                //register executor
                builder.Register<IRsExecutor>(c => new RsExecutor(connectionString, c.Resolve<IDbInstaller>())).InstancePerLifetimeScope();

                //register repository
                builder.Register<IA_AreaRepository>(c => new A_AreaRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IC_BrandRepository>(c => new C_BrandRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IC_DepartmentRepository>(c => new C_DepartmentRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IC_DeviceTypeRepository>(c => new C_DeviceTypeRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IC_DutyRepository>(c => new C_DutyRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IC_EnumMethodRepository>(c => new C_EnumMethodRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IC_GroupRepository>(c => new C_GroupRepository(connectionString)).InstancePerLifetimeScope();                
                builder.Register<IC_LogicTypeRepository>(c => new C_LogicTypeRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IC_ProductorRepository>(c => new C_ProductorRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IC_RoomTypeRepository>(c => new C_RoomTypeRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IC_SCVendorRepository>(c => new C_SCVendorRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IC_StationTypeRepository>(c => new C_StationTypeRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IC_SubCompanyRepository>(c => new C_SubCompanyRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IC_SupplierRepository>(c => new C_SupplierRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IC_UnitRepository>(c => new C_UnitRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<ID_DeviceRepository>(c => new D_DeviceRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<ID_FsuRepository>(c => new D_FsuRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<ID_RedefinePointRepository>(c => new D_RedefinePointRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IH_NoteRepository>(c => new H_NoteRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IP_PointRepository>(c => new P_PointRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IP_ProtocolRepository>(c => new P_ProtocolRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IR_DBScriptRepository>(c => new R_DBScriptRepository(connectionString)).InstancePerLifetimeScope();                
                builder.Register<IS_RoomRepository>(c => new S_RoomRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IS_StationRepository>(c => new S_StationRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IU_EmployeeRepository>(c => new U_EmployeeRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IV_CameraRepository>(c => new V_CameraRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IV_ChannelRepository>(c => new V_ChannelRepository(connectionString)).InstancePerLifetimeScope();

                //register service
                builder.RegisterType<AreaService>().As<IAreaService>().InstancePerLifetimeScope();
                builder.RegisterType<BrandService>().As<IBrandService>().InstancePerLifetimeScope();
                builder.RegisterType<CameraService>().As<ICameraService>().InstancePerLifetimeScope();
                builder.RegisterType<ChannelService>().As<IChannelService>().InstancePerLifetimeScope();
                builder.RegisterType<DepartmentService>().As<IDepartmentService>().InstancePerLifetimeScope();
                builder.RegisterType<DeviceService>().As<IDeviceService>().InstancePerLifetimeScope();
                builder.RegisterType<DeviceTypeService>().As<IDeviceTypeService>().InstancePerLifetimeScope();
                builder.RegisterType<DutyService>().As<IDutyService>().InstancePerLifetimeScope();
                builder.RegisterType<EmployeeService>().As<IEmployeeService>().InstancePerLifetimeScope();
                builder.RegisterType<EnumMethodService>().As<IEnumMethodService>().InstancePerLifetimeScope();
                builder.RegisterType<GroupService>().As<IGroupService>().InstancePerLifetimeScope();
                builder.RegisterType<FsuService>().As<IFsuService>().InstancePerLifetimeScope();
                builder.RegisterType<LogicTypeService>().As<ILogicTypeService>().InstancePerLifetimeScope();
                builder.RegisterType<PointService>().As<IPointService>().InstancePerLifetimeScope();
                builder.RegisterType<ProductorService>().As<IProductorService>().InstancePerLifetimeScope();
                builder.RegisterType<ProtocolService>().As<IProtocolService>().InstancePerLifetimeScope();
                builder.RegisterType<RDBScriptService>().As<IRDBScriptService>().InstancePerLifetimeScope();                
                builder.RegisterType<RedefinePointService>().As<IRedefinePointService>().InstancePerLifetimeScope();
                builder.RegisterType<NoteService>().As<INoteService>().InstancePerLifetimeScope();
                builder.RegisterType<RoomService>().As<IRoomService>().InstancePerLifetimeScope();
                builder.RegisterType<RoomTypeService>().As<IRoomTypeService>().InstancePerLifetimeScope();
                builder.RegisterType<SCVendorService>().As<ISCVendorService>().InstancePerLifetimeScope();
                builder.RegisterType<StationService>().As<IStationService>().InstancePerLifetimeScope();
                builder.RegisterType<StationTypeService>().As<IStationTypeService>().InstancePerLifetimeScope();
                builder.RegisterType<SubCompanyService>().As<ISubCompanyService>().InstancePerLifetimeScope();
                builder.RegisterType<SupplierService>().As<ISupplierService>().InstancePerLifetimeScope();
                builder.RegisterType<UnitService>().As<IUnitService>().InstancePerLifetimeScope();
            }

            if(dbManager.IsValid(EnmDbType.Cs)) {
                var connectionString = dbManager.CurrentConnetions[EnmDbType.Cs];

                //register executor
                builder.Register<ICsExecutor>(c => new CsExecutor(connectionString, c.Resolve<IDbInstaller>())).InstancePerLifetimeScope();

                //register repository
                builder.Register<IA_AAlarmRepository>(c => new A_AAlarmRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IA_HAlarmRepository>(c => new A_HAlarmRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IH_DBScriptRepository>(c => new H_DBScriptRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IH_FsuEventRepository>(c => new H_FsuEventRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IH_IAreaRepository>(c => new H_IAreaRepository(connectionString)).InstancePerLifetimeScope();                
                builder.Register<IH_IDeviceRepository>(c => new H_IDeviceRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IH_IStationRepository>(c => new H_IStationRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IV_AMeasureRepository>(c => new V_AMeasureRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IV_BatRepository>(c => new V_BatRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IV_BatTimeRepository>(c => new V_BatTimeRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IV_CutRepository>(c => new V_CutRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IV_ElecRepository>(c => new V_ElecRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IV_HMeasureRepository>(c => new V_HMeasureRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IV_LoadRepository>(c => new V_LoadRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IV_ParamDiffRepository>(c => new V_ParamDiffRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IV_StaticRepository>(c => new V_StaticRepository(connectionString)).InstancePerLifetimeScope();

                //register service
                builder.RegisterType<AAlarmService>().As<IAAlarmService>().InstancePerLifetimeScope();
                builder.RegisterType<AMeasureService>().As<IAMeasureService>().InstancePerLifetimeScope();
                builder.RegisterType<BatService>().As<IBatService>().InstancePerLifetimeScope();
                builder.RegisterType<BatTimeService>().As<IBatTimeService>().InstancePerLifetimeScope();
                builder.RegisterType<CutService>().As<ICutService>().InstancePerLifetimeScope();
                builder.RegisterType<ElecService>().As<IElecService>().InstancePerLifetimeScope();
                builder.RegisterType<FsuEventService>().As<IFsuEventService>().InstancePerLifetimeScope();
                builder.RegisterType<HAlarmService>().As<IHAlarmService>().InstancePerLifetimeScope();
                builder.RegisterType<HDBScriptService>().As<IHDBScriptService>().InstancePerLifetimeScope();
                builder.RegisterType<HIAreaService>().As<IHIAreaService>().InstancePerLifetimeScope();
                builder.RegisterType<HIDeviceService>().As<IHIDeviceService>().InstancePerLifetimeScope();
                builder.RegisterType<HIStationService>().As<IHIStationService>().InstancePerLifetimeScope();
                builder.RegisterType<HMeasureService>().As<IHMeasureService>().InstancePerLifetimeScope();
                builder.RegisterType<LoadRateService>().As<ILoadService>().InstancePerLifetimeScope();
                builder.RegisterType<ParamDiffService>().As<IParamDiffService>().InstancePerLifetimeScope();
                builder.RegisterType<StaticService>().As<IStaticService>().InstancePerLifetimeScope();
            }

            if(dbManager.IsValid(EnmDbType.Sc)) {
                var connectionString = dbManager.CurrentConnetions[EnmDbType.Sc];

                //register executor
                builder.Register<IScExecutor>(c => new ScExecutor(connectionString, c.Resolve<IDbInstaller>())).InstancePerLifetimeScope();

                //register repository
                builder.Register<IH_NoticeInUserRepository>(c => new H_NoticeInUserRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IH_NoticeRepository>(c => new H_NoticeRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IH_WebEventRepository>(c => new H_WebEventRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IM_DictionaryRepository>(c => new M_DictionaryRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IM_FormulaRepository>(c => new M_FormulaRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IM_NodesInReservationRepository>(c => new M_NodesInReservationRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IM_ProjectRepository>(c => new M_ProjectRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IM_ReservationRepository>(c => new M_ReservationRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IS_DBScriptRepository>(c => new S_DBScriptRepository(connectionString)).InstancePerLifetimeScope();                
                builder.Register<IU_EntitiesInRoleRepository>(c => new U_EntitiesInRoleRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IU_FollowPointRepository>(c => new U_FollowPointRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IU_MenuRepository>(c => new U_MenuRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IU_ProfileRepository>(c => new U_ProfileRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IU_RoleRepository>(c => new U_RoleRepository(connectionString)).InstancePerLifetimeScope();
                builder.Register<IU_UserRepository>(c => new UserRepository(connectionString)).InstancePerLifetimeScope();

                //register service
                builder.RegisterType<DictionaryService>().As<IDictionaryService>().InstancePerLifetimeScope();
                builder.RegisterType<EntitiesInRoleService>().As<IEntitiesInRoleService>().InstancePerLifetimeScope();
                builder.RegisterType<FollowPointService>().As<IFollowPointService>().InstancePerLifetimeScope();
                builder.RegisterType<FormulaService>().As<IFormulaService>().InstancePerLifetimeScope();
                builder.RegisterType<MenuService>().As<IMenuService>().InstancePerLifetimeScope();
                builder.RegisterType<NodeInReservationService>().As<INodeInReservationService>().InstancePerLifetimeScope();
                builder.RegisterType<NoticeInUserService>().As<INoticeInUserService>().InstancePerLifetimeScope();
                builder.RegisterType<NoticeService>().As<INoticeService>().InstancePerLifetimeScope();
                builder.RegisterType<ProfileService>().As<IProfileService>().InstancePerLifetimeScope();
                builder.RegisterType<ProjectService>().As<IProjectService>().InstancePerLifetimeScope();
                builder.RegisterType<ReservationService>().As<IReservationService>().InstancePerLifetimeScope();
                builder.RegisterType<RoleService>().As<IRoleService>().InstancePerLifetimeScope();
                builder.RegisterType<SDBScriptService>().As<ISDBScriptService>().InstancePerLifetimeScope();                
                builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
                builder.RegisterType<WebEventService>().As<IWebEventService>().InstancePerLifetimeScope();
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
        public Dictionary<Guid, Store> WorkStores {
            get { return _workStores; }
        }

        #endregion

    }
}