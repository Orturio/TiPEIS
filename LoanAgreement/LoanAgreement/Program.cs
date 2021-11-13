using MaterialAccountingDatabase;
using System;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;
using MaterialAccountingBusinessLogic;
using MaterialAccountingBusinessLogic.BusinessLogic;
using MaterialAccountingBusinessLogic.Interfaces;
using MaterialAccountingBusinessLogic.ViewModels;
using MaterialAccountingDatabase.Implements;

namespace LoanAgreement
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormMain>());
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<IChartOfAccountsStorage, ChartOfAccountStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ChartOfAccountsLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMaterialStorage, MaterialStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ChartOfAccountsLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IProviderStorage, ProviderStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ChartOfAccountsLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISubdivisionStorage, SubdivisionStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ChartOfAccountsLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IResponsePersonStorage, ResponsePersonStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ChartOfAccountsLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IOperationStorage, OperationStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<OperationLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IPostingJournalStorage, PostingJournalStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<PostingJournalLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ITablePartStorage, TablePartStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<TablePartLogic>(new HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}
