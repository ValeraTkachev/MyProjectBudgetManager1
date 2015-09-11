namespace BudgetManager.Shared
{
    using System;
    using System.Threading.Tasks;
    using BudgetManager.Models;
    using BudgetManager.DAL;
    using System.Windows.Input;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Linq;
    using System.Collections.Generic;
    using BudgetManager.Core;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Views;
    using Windows.UI.Popups;

    public class AllSubcategoriesViewModel : ViewModelBase
    {

        #region Fields

        private bool enable;
        private long categoryId;
        private bool unchosensub;
        private bool unchosentrans;
        private bool visibilityChart;
        private bool visibilityStatistic;
        private bool visibilityString;
        private ObservableCollection<object> selectedTransactions;
        private Category selectedCategory;
        private Transaction selectedTransaction;
        private Icon newIcon;
        private Color newColor;
        private string newName;
        private int newBudgeted;
        private ObservableCollection<Transaction> transactions;
        private ObservableCollection<Category> subcategories;
        private User selectedUser;
        private ObservableCollection<User> users;
        private ObservableCollection<Category> categories;
        private bool allBudgetVisibility;

        #endregion

        #region Constructor

        public AllSubcategoriesViewModel(INavigationService navigatedService)
        {
            this.navigation = navigatedService;
            DeleteSubcategoryCommand = new RelayCommand(DeleteSubcategory);
            DeleteTransactionCommand = new RelayCommand(DeleteTransaction);
            AddSubcategoryCommand = new RelayCommand(GoToAddSubcategory);
            AddTransactionCommand = new RelayCommand(GoToAddTransaction);
            SelectedSubcategory = new RelayCommand(SelectSubcategory);
            SelectedItemTransaction = new RelayCommand(SelectTransaction);
            EditTransactionCommand = new RelayCommand(GoToEditTransactionPage);
            EditSubcategoryCommand = new RelayCommand(GoToEditSubcategoryPage);
            AllTransactionCommand = new RelayCommand(ShowAllTransactions);
            AllSubcategoriesCommand = new RelayCommand(ShowAllSubcategories);
            GotFocusSubcategoryCommand = new RelayCommand(GotFocusSubcategoryExecute);
            GotFocusTransactionCommand = new RelayCommand(GotFocusTransactionExecute);
            Back = new RelayCommand(() => navigation.NavigateTo("MainPage"));

        }

        #endregion

        #region Basic Properties

        public long Categoryid
        {
            get { return this.categoryId; }
            set
            {
                if (value != this.categoryId)
                {
                    this.categoryId = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool Enable
        {
            get { return this.enable; }
            set
            {
                if (value != this.enable)
                {
                    this.enable = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool VisibilityString
        {
            get { return this.visibilityString; }
            set
            {
                if (value != this.visibilityString)
                {
                    this.visibilityString = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool VisibilityStatistic
        {
            get { return this.visibilityStatistic; }
            set
            {
                if (value != this.visibilityStatistic)
                {
                    this.visibilityStatistic = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool VisibilityChart
        {
            get { return this.visibilityChart; }
            set
            {
                if (value != this.visibilityChart)
                {
                    this.visibilityChart = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool Unchosentrans
        {
            get { return this.unchosentrans; }
            set
            {
                if (value != this.unchosentrans)
                {
                    this.unchosentrans = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool Unchosensub
        {
            get { return this.unchosensub; }
            set
            {
                if (value != this.unchosensub)
                {
                    this.unchosensub = value;
                    OnPropertyChanged();
                }
            }
        }

        public int NewBudgeted
        {
            get { return this.newBudgeted; }
            set
            {
                if (value != this.newBudgeted)
                {
                    this.newBudgeted = value;
                    OnPropertyChanged();
                }
            }
        }

        public string NewName
        {
            get { return this.newName; }
            set
            {
                if (value != this.newName)
                {
                    this.newName = value;
                    OnPropertyChanged();
                }
            }
        }

        public Color NewColor
        {
            get { return this.newColor; }
            set
            {
                if (value != this.newColor)
                {
                    this.newColor = value;
                    OnPropertyChanged();
                }
            }
        }

        public Icon NewIcon
        {
            get { return this.newIcon; }
            set
            {
                if (value != this.newIcon)
                {
                    this.newIcon = value;
                    OnPropertyChanged();
                }
            }
        }

        public Transaction SelectedTransaction
        {
            get { return this.selectedTransaction; }
            set
            {
                if (value != this.selectedTransaction)
                {
                    this.selectedTransaction = value;
                    OnPropertyChanged();
                }
            }
        }

        public Category SelectedCategory
        {
            get { return this.selectedCategory; }
            set
            {
                if (value != this.selectedCategory)
                {
                    this.selectedCategory = value;
                    OnPropertyChanged();
                }
            }
        }

        public Category FoundedCategory { get; set; }

        public ObservableCollection<Transaction> Transactions
        {
            get { return this.transactions; }
            set
            {
                if (value != this.transactions)
                {
                    this.transactions = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Category> Subcategories
        {
            get { return this.subcategories; }
            set
            {
                if (value != this.subcategories)
                {
                    this.subcategories = value;
                    OnPropertyChanged();
                }
            }
        }

        public User SelectedUser
        {
            get { return this.selectedUser; }
            set
            {
                if (value != this.selectedUser)
                {
                    this.selectedUser = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<User> Users
        {
            get { return this.users; }
            set
            {
                if (value != this.users)
                {
                    this.users = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Category> Categories
        {
            get { return this.categories; }
            set
            {
                if (value != this.categories)
                {
                    this.categories = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Color> Colors { get; set; }

        public ObservableCollection<Icon> Icons { get; set; }

        public ObservableCollection<object> SelectedTransactions
        {
            get { return this.selectedTransactions; }
            set
            {
                if (value != this.selectedTransactions)
                {
                    this.selectedTransactions = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool AllBudgetVisibility
        {
            get { return this.allBudgetVisibility; }
            set
            {
                if (value != this.allBudgetVisibility)
                {
                    this.allBudgetVisibility = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region Commands

        public RelayCommand Back { get; set; }
        public RelayCommand DeleteSubcategoryCommand { get; set; }
        public RelayCommand DeleteTransactionCommand { get; set; }
        public RelayCommand GoToAddTransactionPageCommand { get; set; }
        public RelayCommand GoToAddSubcategoryPageCommand { get; set; }
        public RelayCommand AddSubcategoryCommand { get; set; }
        public RelayCommand AddTransactionCommand { get; set; }
        public RelayCommand SelectedSubcategory { get; set; }
        public RelayCommand SelectedItemTransaction { get; set; }
        public RelayCommand EditTransactionCommand { get; set; }
        public RelayCommand EditSubcategoryCommand { get; set; }
        public RelayCommand AllTransactionCommand { get; set; }
        public RelayCommand AllSubcategoriesCommand { get; set; }
        public RelayCommand GotFocusSubcategoryCommand { get; set; }
        public RelayCommand GotFocusTransactionCommand { get; set; }

        #endregion

        #region Methods

        private void Painting()
        {
            foreach (var searchingcategory in Subcategories)
            {
                searchingcategory.Percent = 1 - (searchingcategory.Remaining * 100 / searchingcategory.Budgeted) * 0.01;

                searchingcategory.Rest = searchingcategory.Budgeted - searchingcategory.Remaining;
                if (searchingcategory.Budgeted >= searchingcategory.Remaining)
                {
                    searchingcategory.StrokeColor = "White";
                }
                else
                {
                    searchingcategory.StrokeColor = "Red";

                }
            }

            foreach (var searchingtransaction in SelectedUser.Transactions)
            {
                searchingtransaction.MonthNumber = searchingtransaction.Month;
                Category category = budgetManagerRepository.GetCategory(searchingtransaction.Accounttype);
                searchingtransaction.AccountName = category.Name;
                Category categoryforicon = budgetManagerRepository.GetCategory(searchingtransaction.Categoryid);

                if (searchingtransaction.Type == "Sending")
                {
                    searchingtransaction.TextColor = "Gray";
                    searchingtransaction.IconName = "/Assets/trans.png";
                }
                else if (searchingtransaction.Type == "Income")
                {
                    searchingtransaction.TextColor = "Green";
                    searchingtransaction.IconName = categoryforicon.Icon;

                }
                else
                {
                    searchingtransaction.TextColor = "Red";
                    searchingtransaction.IconName = categoryforicon.Icon;
                }
            }
        }

        private void DeleteTransaction()
        {
            List<Transaction> tempSelectedTransactions = new List<Transaction>();

            foreach (var searchingtransaction in SelectedTransactions)
            {
                Transaction chosenTransaction = (Transaction)searchingtransaction;
                tempSelectedTransactions.Add(chosenTransaction);

            }

            Category Subcategory = Categories.FirstOrDefault(a => a.Id == SelectedTransaction.Categoryid);

            foreach (var selectedtransaction in tempSelectedTransactions)
            {

                foreach (var category in Categories)
                {
                    if (Subcategory.Type == "Subcategory")
                    {
                        if (category.Id == Subcategory.AccountId)
                        {
                            category.Remaining -= selectedtransaction.Count;
                            budgetManagerRepository.UpdateCategory(category);
                            this.FoundedCategory.Remaining = category.Remaining;
                            this.FoundedCategory.RestPercent = (FoundedCategory.Remaining * 100 / FoundedCategory.Budgeted) * 0.01;
                            this.FoundedCategory.Percent = 1 - FoundedCategory.RestPercent;
                            this.FoundedCategory.Rest = FoundedCategory.Budgeted - FoundedCategory.Remaining;

                        }
                    }

                    if (category.Id == Subcategory.Id)
                    {
                        category.Remaining -= selectedtransaction.Count;
                        budgetManagerRepository.UpdateCategory(category);
                        if (category.Type != "Subcategory")
                        {
                            this.FoundedCategory.Remaining = category.Remaining;
                            this.FoundedCategory.RestPercent = (FoundedCategory.Remaining * 100 / FoundedCategory.Budgeted) * 0.01;
                            this.FoundedCategory.Percent = 1 - FoundedCategory.RestPercent;
                            this.FoundedCategory.Rest = FoundedCategory.Budgeted - FoundedCategory.Remaining;
                        }
                    }

                    if (selectedtransaction.Accounttype == category.Id)
                    {
                        category.Budgeted += selectedtransaction.Count;
                        budgetManagerRepository.UpdateCategory(category);
                    }


                }

                budgetManagerRepository.DeleteTransaction(selectedtransaction.Id);
                SelectedUser.Transactions.Remove(SelectedUser.Transactions.FirstOrDefault(transaction => transaction.Id == selectedtransaction.Id));
            }

            List<Category> tempsubcategories = budgetManagerRepository.GetCategoryByType(SelectedUser.Id, "Subcategory").ToList();
            Subcategories = new ObservableCollection<Category>(tempsubcategories.Where(x => x.AccountId == FoundedCategory.Id));
            Painting();
            Unchosentrans = false;
        }

        private void DeleteSubcategory()
        {
            List<Transaction> AllTransactions = budgetManagerRepository.GetTransactions().ToList();
            List<Transaction> transactionsForDelete = AllTransactions.Where(transaction => transaction.Categoryid == SelectedCategory.Id).ToList();
            Category categoryAccount;

            foreach (var transaction in transactionsForDelete)
            {

                categoryAccount = Categories.FirstOrDefault(category => category.Id == transaction.Accounttype);
                categoryAccount.Budgeted += transaction.Count;
                budgetManagerRepository.UpdateCategory(categoryAccount);
                SelectedUser.Transactions.Remove(transaction);

            }


            Category MainCategory = Categories.FirstOrDefault(a => a.Id == SelectedCategory.AccountId);
            MainCategory.Remaining -= SelectedCategory.Remaining;
            this.FoundedCategory.Remaining = MainCategory.Remaining;
            this.FoundedCategory.RestPercent = (FoundedCategory.Remaining * 100 / FoundedCategory.Budgeted) * 0.01;
            this.FoundedCategory.Percent = 1 - FoundedCategory.RestPercent;
            this.FoundedCategory.Rest = FoundedCategory.Budgeted - FoundedCategory.Remaining;
            budgetManagerRepository.UpdateCategory(MainCategory);


            long foundId = SelectedCategory.Id;
            budgetManagerRepository.DeleteTransactionByCategory(SelectedCategory.Id, SelectedCategory.Id);
            budgetManagerRepository.DeleteCategory(SelectedCategory.Id);


            Category categoryForRemove;
            categoryForRemove = Subcategories.FirstOrDefault(category => category.Id == SelectedCategory.Id);
            Subcategories.Remove(categoryForRemove);
            List<Transaction> oldTransactions = SelectedUser.Transactions.ToList();
            foreach (var transaction in oldTransactions)
            {
                if (transaction.Categoryid == foundId)
                {
                    SelectedUser.Transactions.Remove(transaction);
                }
            }
            Unchosensub = false;
        }

        private void GoToAddSubcategory()
        {
            GoToAddSubcategoryPage();
        }

        private void GoToAddTransaction()
        {
            GoToAddTransactionPage();
        }

        private async Task GoToAddSubcategoryPage()
        {
            bool result = false;

            if (FoundedCategory.Remaining >= FoundedCategory.Budgeted)
            {
                var dialog = new MessageDialog(LocalizedStrings.GetString(LocalizedStringEnum.WarningMessageSubcategory), 
                    LocalizedStrings.GetString(LocalizedStringEnum.TitleMessageSubcategory)
                );

                dialog.Commands.Add(new UICommand(LocalizedStrings.GetString(LocalizedStringEnum.Continue), new UICommandInvokedHandler((cmd) => result = true)));
                dialog.Commands.Add(new UICommand(LocalizedStrings.GetString(LocalizedStringEnum.Cancel)));
                IUICommand iuiCommand = await dialog.ShowAsync();

                if (result == true)
                {
                    Category NewCategory = new Category();
                    var data = FoundedCategory;
                    navigation.NavigateTo("AddSubcategory", data);
                }
            }
            else
            {
                Category NewCategory = new Category();
                var data = FoundedCategory;
                navigation.NavigateTo("AddSubcategory", data);
            }
        }

        private async Task GoToAddTransactionPage()
        {
            bool result = false;

            if (FoundedCategory.Remaining >= FoundedCategory.Budgeted)
            {
                var dialog = new MessageDialog(LocalizedStrings.GetString(LocalizedStringEnum.WarningMessageTransaction),
                    LocalizedStrings.GetString(LocalizedStringEnum.TitleMessageSubcategory)
                );
                dialog.Commands.Add(new UICommand(LocalizedStrings.GetString(LocalizedStringEnum.Continue), new UICommandInvokedHandler((cmd) => result = true)));
                dialog.Commands.Add(new UICommand(LocalizedStrings.GetString(LocalizedStringEnum.Cancel)));
                IUICommand iuiCommand = await dialog.ShowAsync();
                if (result == true)
                {
                    var data = FoundedCategory;
                    navigation.NavigateTo("AddTransaction", data);

                }
            }
            else
            {
                var data = FoundedCategory;
                navigation.NavigateTo("AddTransaction", data);
            }
        }

        private void SelectSubcategory()
        {
            if (SelectedCategory != null)
            {
                List<Transaction> tempTransactions = SelectedUser.Transactions.ToList();
                SelectedUser.Transactions = new ObservableCollection<Transaction>(tempTransactions);
                Unchosentrans = false;
                Unchosensub = true;
            }
            else 
            {
                Unchosentrans = false;
                Unchosensub = false;
            }
        }

        private void GotFocusSubcategoryExecute()
        {
            Unchosentrans = false;
            Unchosensub = true;
        }

        private void GotFocusTransactionExecute()
        {
            SelectedCategory = null;
            Unchosentrans = true;
            Unchosensub = false;
        }

        private void SelectTransaction()
        {
            if (SelectedTransactions.Count != 0)
            {
                SelectedCategory = null;
                Unchosentrans = true;
                Unchosensub = false;
                if (SelectedTransactions.Count > 1)
                {
                    Enable = false;
                }
                else
                {
                    Enable = true;
                }
                foreach (var transaction in SelectedTransactions)
                {
                    SelectedTransaction = (Transaction)transaction;
                }
            }
            else 
            {
                Unchosentrans = false;
                Unchosensub = false;
            }
        }

        private void GoToEditTransactionPage()
        {
            var data = SelectedTransaction;
            navigation.NavigateTo("EditTransaction", data);
        }

        private void GoToEditSubcategoryPage()
        {
            var data = SelectedCategory;
            navigation.NavigateTo("AddSubcategory", data);
        }

        private void ShowAllTransactions()
        {
            var data = FoundedCategory;
            navigation.NavigateTo("AllTransactions", data);
        }

        private void ShowAllSubcategories()
        {
            var data = FoundedCategory;
            navigation.NavigateTo("SelectedCategory", data);
        }

        public override void NavigateTo(object parameter)
        {
            this.SelectedCategory = new Category();
            this.users = budgetManagerRepository.GetUsers();
            this.categories = budgetManagerRepository.GetCategories();
            this.unchosentrans = false;
            this.unchosensub = false;
            this.selectedTransactions = new ObservableCollection<object>();


            Category data = parameter as Category;
            if (data != null)
            {
                this.categoryId = data.Id;
                this.FoundedCategory = budgetManagerRepository.GetCategory(Categoryid);
                this.SelectedUser = Users.FirstOrDefault(a => a.Id == FoundedCategory.Userid);
                this.subcategories = budgetManagerRepository.GetCategoryByType(SelectedUser.Id, "Subcategory");
                Subcategories = new ObservableCollection<Category>(Subcategories.Where(a => a.AccountId == Categoryid));
                this.transactions = budgetManagerRepository.GetTransactionsByUserId(SelectedUser.Id);
                Transactions = new ObservableCollection<Transaction>(Transactions.Where(a => a.Month == FoundedCategory.Month && a.Year == FoundedCategory.Year));

                List<long> SubcategoriesId = new List<long>();
                foreach (var category in Categories)
                {
                    if (category.Id == FoundedCategory.Id || category.AccountId == FoundedCategory.Id)
                    {
                        SubcategoriesId.Add(category.Id);
                    }

                }

                this.SelectedUser.Transactions = new ObservableCollection<Transaction>();
                foreach (var id in SubcategoriesId)
                {
                    foreach (var transaction in Transactions)
                    {

                        if (id == transaction.Categoryid || id == transaction.Accounttype)
                        {
                            SelectedUser.Transactions.Add(transaction);
                        }
                    }

                }

                FoundedCategory.RestPercent = (FoundedCategory.Remaining * 100 / FoundedCategory.Budgeted) * 0.01;
                FoundedCategory.Percent = 1 - FoundedCategory.RestPercent;
                FoundedCategory.Rest = FoundedCategory.Budgeted - FoundedCategory.Remaining;
                if (FoundedCategory.Rest < 0)
                {
                    FoundedCategory.Rest = 0;
                }


                if (Subcategories.Count == 0 && SelectedUser.Transactions.Count == 0)
                {
                    this.VisibilityChart = false;
                    this.VisibilityStatistic = false;
                    this.VisibilityString = true;
                }
                if (Subcategories.Count != 0 || SelectedUser.Transactions.Count != 0)
                {
                    this.VisibilityChart = false;
                    this.VisibilityStatistic = true;
                    this.VisibilityString = false;
                }
                if (Subcategories.Count != 0 && SelectedUser.Transactions.Count != 0)
                {
                    this.VisibilityChart = true;
                    this.VisibilityStatistic = true;
                    this.VisibilityString = false;
                }
                Painting();
            }

            if (Subcategories.Sum(subcategory => subcategory.Remaining) == 0)
            {
                AllBudgetVisibility = true;
            }
            else
            {
                AllBudgetVisibility = false;
            }
        }



        #endregion


    }
}
