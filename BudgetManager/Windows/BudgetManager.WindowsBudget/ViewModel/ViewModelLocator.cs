using BudgetManager.Shared;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;

namespace BudgetManager.WindowsBudget.ViewModel
{

    public class ViewModelLocator
    {

        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            var navigationService = this.CreateNavigationService();
            SimpleIoc.Default.Register<INavigationService>(() => navigationService);
            SimpleIoc.Default.Register<IDialogService, DialogService>();
            SimpleIoc.Default.Register<MainPageViewModel>();
            SimpleIoc.Default.Register<AllExpenseViewModel>();
            SimpleIoc.Default.Register<EditExpenseViewModel>();
            SimpleIoc.Default.Register<AddIncomeViewModel>();
            SimpleIoc.Default.Register<AddMoneyAccountViewModel>();
            SimpleIoc.Default.Register<AccountPageViewModel>();
            SimpleIoc.Default.Register<AddExcpenceViewModel>();
            SimpleIoc.Default.Register<MoneyTransactionModelView>();
            SimpleIoc.Default.Register<AddSubcategoryViewModel>();
            SimpleIoc.Default.Register<StatisticViewModel>();
            SimpleIoc.Default.Register<GraphicViewModel>();
            SimpleIoc.Default.Register<AddTransactionViewModel>();
            SimpleIoc.Default.Register<EditTransactionViewModel>();
            SimpleIoc.Default.Register<AlltransactionsViewModel>();
            SimpleIoc.Default.Register<SelectedCategoryViewModel>();
            SimpleIoc.Default.Register<AllSubcategoriesViewModel>();
        }

        private INavigationService CreateNavigationService()
        {
            var navigationService = new NavigationService();

            navigationService.Configure("MainPage", typeof(BudgetManager.WindowsBudget.Views.MainPage.MainPage));
            navigationService.Configure("AllExpense", typeof(BudgetManager.WindowsBudget.Views.AllExpense.AllExpense));
            navigationService.Configure("LinearExpense", typeof(BudgetManager.WindowsBudget.Views.LinearExpence.LinearExpence));
            navigationService.Configure("EditExpense", typeof(BudgetManager.WindowsBudget.Views.EditExpense.EditExpense));
            navigationService.Configure("AddIncome", typeof(BudgetManager.WindowsBudget.Views.AddIncome.AddIncome));
            navigationService.Configure("AddMoneyAccount", typeof(BudgetManager.WindowsBudget.Views.AddMoneyAccount.AddMoneyAccount));
            navigationService.Configure("AccountPage", typeof(BudgetManager.WindowsBudget.Views.AccountPage.AccountPage));
            navigationService.Configure("AddExpense", typeof(BudgetManager.WindowsBudget.Views.AddExpence.AddExpence));
            navigationService.Configure("MoneyTransaction", typeof(BudgetManager.WindowsBudget.Views.MoneyTransaction.MoneyTransaction));
            navigationService.Configure("AddSubcategory", typeof(BudgetManager.WindowsBudget.Views.AddSubcategory.AddSubcategory));
            navigationService.Configure("Graphic", typeof(BudgetManager.WindowsBudget.Views.Statistic.Graphic));
            navigationService.Configure("Statistic", typeof(BudgetManager.WindowsBudget.Views.Statistic.Statistic));
            navigationService.Configure("AddTransaction", typeof(BudgetManager.WindowsBudget.Views.AddTransaction.AddTransactionExpense));
            navigationService.Configure("EditTransaction", typeof(BudgetManager.WindowsBudget.Views.EditTransaction.EditTransaction));
            navigationService.Configure("AllTransactions", typeof(BudgetManager.WindowsBudget.Views.Alltransactions.Alltransactions));
            navigationService.Configure("SelectedCategory", typeof(BudgetManager.WindowsBudget.Views.Selectedcategory.SelectedCategory));
            navigationService.Configure("AllSubcategories", typeof(BudgetManager.WindowsBudget.Views.AllSubcategories.AllSubcategories));

            return navigationService;
        }



        public AlltransactionsViewModel AlltransactionsViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AlltransactionsViewModel>();
            }
        }

        public AllExpenseViewModel AllExpenseViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AllExpenseViewModel>();
            }
        }

        public MainPageViewModel MainPageViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainPageViewModel>();
            }

        }

        public EditExpenseViewModel EditExpenseViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<EditExpenseViewModel>();
            }
        }

        public AddIncomeViewModel AddIncomeViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AddIncomeViewModel>();
            }
        }

        public AddMoneyAccountViewModel AddMoneyAccountViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AddMoneyAccountViewModel>();
            }
        }

        public AccountPageViewModel AccountPageViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AccountPageViewModel>();
            }
        }

        public AddExcpenceViewModel AddExcpenceViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AddExcpenceViewModel>();
            }
        }

        public MoneyTransactionModelView MoneyTransactionModelView
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MoneyTransactionModelView>();
            }
        }

        public AddSubcategoryViewModel AddSubcategoryViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AddSubcategoryViewModel>();
            }
        }

        public StatisticViewModel StatisticViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<StatisticViewModel>();
            }
        }

        public GraphicViewModel GraphicViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<GraphicViewModel>();
            }
        }

        public AddTransactionViewModel AddTransactionViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AddTransactionViewModel>();
            }
        }

        public EditTransactionViewModel EditTransactionViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<EditTransactionViewModel>();
            }
        }

        public SelectedCategoryViewModel SelectedCategoryViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SelectedCategoryViewModel>();
            }
        }

        public AllSubcategoriesViewModel AllSubcategoriesViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AllSubcategoriesViewModel>();
            }
        }

    }

}
