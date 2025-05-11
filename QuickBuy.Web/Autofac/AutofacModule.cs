using Autofac;
using QuickBuy.UoW;
using QuickBuy.UoW.Base;
using QuickBuy.Web.ApplicationService.Base;
using QuickBuy.Web.ApplicationService;
using QuickBuy.WEB.ApplicationServices;
using QuickBuy.WEB.ApplicationServices.Base;
using System.Reflection;

namespace QuickBuy.WEB.Autofac
{
    public class AutofacModule : global::Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserApplicationService>().As<IUserApplicationService>();
            builder.RegisterType<ManageUsersUoW>().As<IManageUsersUoW>();
            builder.RegisterType<TransactionApplicationService>().As<ITransactionApplicationService>();
            builder.RegisterType<ManageTransactionsUoW>().As<IManageTransactionsUoW>();
            builder.RegisterType<PickupDetailsApplicationService>().As<IPickupDetailsApplicationService>();
            builder.RegisterType<ManagePickupDetailsUoW>().As<IManagePickupDetailsUoW>();
            builder.RegisterType<LoginApplicationService>().As<ILoginApplicationService>();
            builder.RegisterType<MessageApplicationService>().As<IMessageApplicationService>();
            builder.RegisterType<ManageMessagesUoW>().As<IManageMessagesUoW>();
            builder.RegisterType<ItemApplicationService>().As<IItemApplicationService>();
            builder.RegisterType<ManageItemsUoW>().As<IManageItemsUoW>();
            builder.RegisterType<ChatApplicationService>().As<IChatApplicationService>();
            builder.RegisterType<ManageChatsUoW>().As<IManageChatsUoW>();
            builder.RegisterType<SystemApplicationService>().As<ISystemApplicationService>();
        }
    }
}
