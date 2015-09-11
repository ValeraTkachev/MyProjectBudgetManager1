namespace BudgetManager.Shared
{
    using System;
    using System.Globalization;
    using System.Linq;
    using BudgetManager.Models;
    using System.Windows.Input;
    using System.Collections.ObjectModel;
    using System.Collections.Generic;
    using GalaSoft.MvvmLight.Views;
    using GalaSoft.MvvmLight.Command;
    using BudgetManager.Core;
    using System.Threading.Tasks;
    using Windows.UI.Popups;

    public class MainPageViewModel : ViewModelBase
    {

        #region fields

        private ObservableCollection<User> users;
        private ObservableCollection<Category> categoriesaccount;
        private ObservableCollection<Category> categoriesincome;
        private ObservableCollection<Category> categoriesexpense;
        private ObservableCollection<Transaction> transactions;
        private ObservableCollection<object> selectedTransactions;
        private ObservableCollection<Category> categories;
        private ObservableCollection<Category> statisticCategories;
        private DateTime thisDate;
        private DateTime secondDate;
        private bool unchosen;
        private bool unchosenincome;
        private bool unchosenaccount;
        private bool unchosentrans;
        private bool chosen;
        private bool deletevisibility;
        private bool secondDateVisibility;
        private bool enable;
        private long percent;
        private long selectedStartDay;
        private string selectedMonth;
        private long selectedYear;
        private long selectedEndDay;
        private long selectedEndYear;
        private string selectedEndMonth;
        private User allUsers;
        private User selectedUser;
        private Category selectedCategory;
        private Transaction selectedTransaction;
        private string chosenDateType;
        private bool allBudgetVisibility;
        private Category tempCategory;
        private List<Transaction> tempTransactions;

        #endregion

        #region Constructor

        public MainPageViewModel(INavigationService navigation)
        {
            this.navigation = navigation;
            DeleteCommand = new RelayCommand(DeleteCategory);
            YearCommand = new RelayCommand(GetYearType);
            DayCommand = new RelayCommand(GetDayType);
            MonthCommand = new RelayCommand(GetMonthType);
            PeriodCommand = new RelayCommand(GetPeriodType);
            WeekCommand = new RelayCommand(GetWeekType);
            DeleteTransactionCommand = new RelayCommand(DeleteTransaction);
            IncomeCommand = new RelayCommand(ShowAllIncome);
            EditIncomeCommand = new RelayCommand(EditIncome);
            AddIncomeCommand = new RelayCommand(AddIncome);
            ExpenseCommand = new RelayCommand(ShowAllExpense);
            EditExpenseCommand = new RelayCommand(EditExpense);
            AccountCommand = new RelayCommand(ShowAllMoneyAccount);
            EditMoneyAccountCommand = new RelayCommand(EditMoneyAccount);
            AddMoneyAccountCommand = new RelayCommand(AddMoneyAccount);
            AddTransactionCommand = new RelayCommand(AddTransaction);
            EditTransactionCommand = new RelayCommand(EditTransaction);
            AllTransactionCommand = new RelayCommand(ShowAllTransactions);
            SelectedItemExpense = new RelayCommand(SelectExpense);
            SelectedItemIncome = new RelayCommand(SelectIncome);
            SelectedItemAccount = new RelayCommand(SelectAccount);
            SelectedItemTransaction = new RelayCommand(SelectTransaction);
            SelectionChangedUser = new RelayCommand(ChangeUser);
            DateChangeCommand = new RelayCommand(ChangeDate);
            SecondDateChangeCommand = new RelayCommand(ChangeSecondDate);
            AllSubcategoriesCommand = new RelayCommand(ShowAllSubcategories);
            GotFocusOnTransaction = new RelayCommand(GotTransactionFocusExecute);
            GotFocusOnAccount = new RelayCommand(GotAccountFocusExecute);
            GotFocusOnIncome = new RelayCommand(GotIncomeFocusExecute);
            GotFocusOnExpense = new RelayCommand(GotExpenseFocusExecute);
        }

        #endregion

        #region Basic Properties

        public User AllUsers
        {
            get { return this.allUsers; }
            set
            {
                if (value != this.allUsers)
                {
                    this.allUsers = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SelectedEndMonth
        {
            get { return this.selectedEndMonth; }
            set
            {
                if (value != this.selectedEndMonth)
                {
                    this.selectedEndMonth = value;
                    OnPropertyChanged();
                }
            }
        }

        public long SelectedEndYear
        {
            get { return this.selectedEndYear; }
            set
            {
                if (value != this.selectedEndYear)
                {
                    this.selectedEndYear = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public long SelectedEndDay
        {
            get { return this.selectedEndDay; }
            set
            {
                if (value != this.selectedEndDay)
                {
                    this.selectedEndDay = value;
                    OnPropertyChanged();
                }
            }
        }

        public long SelectedYear
        {
            get { return this.selectedYear; }
            set
            {
                if (value != this.selectedYear)
                {
                    this.selectedYear = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SelectedMonth
        {
            get { return this.selectedMonth; }
            set
            {
                if (value != selectedMonth)
                {
                    this.selectedMonth = value;
                    OnPropertyChanged();
                }
            }
        }

        public long SelectedStartDay
        {
            get { return this.selectedStartDay; }
            set
            {
                if (value != this.selectedStartDay)
                {
                    this.selectedStartDay = value;
                    OnPropertyChanged();
                }
            }
        }

        public long Percent
        {
            get { return this.percent; }
            set
            {
                if (value != this.percent)
                {
                    this.percent = value;
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

        public bool Deletevisibility
        {
            get { return this.deletevisibility; }
            set
            {
                if (value != this.deletevisibility)
                {
                    this.deletevisibility = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool SecondDateVisibility
        {
            get { return this.secondDateVisibility; }
            set
            {
                if (value != this.secondDateVisibility)
                {
                    this.secondDateVisibility = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool Chosen
        {
            get { return this.chosen; }
            set
            {
                if (value != this.chosen)
                {
                    this.chosen = value;
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

        public bool Unchosenaccount
        {
            get { return this.unchosenaccount; }
            set
            {
                if (value != this.unchosenaccount)
                {
                    this.unchosenaccount = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool Unchosenincome
        {
            get { return this.unchosenincome; }
            set
            {
                if (value != this.unchosenincome)
                {
                    this.unchosenincome = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool Unchosen
        {
            get { return this.unchosen; }
            set
            {
                if (value != this.unchosen)
                {
                    this.unchosen = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime ThisDate
        {
            get { return this.thisDate; }
            set
            {
                if (value != this.thisDate)
                {
                    this.thisDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime SecondDate
        {
            get { return this.secondDate; }
            set
            {
                if (value != this.secondDate)
                {
                    this.secondDate = value;
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

        public ObservableCollection<Category> StatisticCategories
        {
            get { return this.statisticCategories; }
            set
            {
                if (value != this.statisticCategories)
                {
                    this.statisticCategories = value;
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

        public Category SelectedCategory
        {
            get { return selectedCategory; }
            set
            {
                if (value != this.selectedCategory)
                {
                    this.selectedCategory = value;
                    OnPropertyChanged();
                }
            }
        }


        public Transaction SelectedTransaction
        {
            get { return this.selectedTransaction; }
            set
            {
                if (value != selectedTransaction)
                {
                    this.selectedTransaction = value;
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

        public ObservableCollection<Category> CategoriesAccount
        {
            get { return this.categoriesaccount; }
            set
            {
                if (value != this.categoriesaccount)
                {
                    this.categoriesaccount = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Category> CategoriesIncome
        {
            get { return this.categoriesincome; }
            set
            {
                if (value != this.categoriesincome)
                {
                    this.categoriesincome = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Category> CategoriesExpense
        {
            get { return this.categoriesexpense; }
            set
            {
                if (value != this.categoriesexpense)
                {
                    this.categoriesexpense = value;
                    OnPropertyChanged();
                }
            }
        }

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


        public string ChosenDateType
        {
            get { return this.chosenDateType; }
            set
            {
                if (value != this.chosenDateType)
                {
                    this.chosenDateType = value;
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

        #region commands

        public RelayCommand IncomeCommand { get; set; }
        public RelayCommand EditIncomeCommand { get; set; }
        public RelayCommand AddIncomeCommand { get; set; }
        public RelayCommand ExpenseCommand { get; set; }
        public RelayCommand EditExpenseCommand { get; set; }
        public RelayCommand AccountCommand { get; set; }
        public RelayCommand EditMoneyAccountCommand { get; set; }
        public RelayCommand AddMoneyAccountCommand { get; set; }
        public RelayCommand AddTransactionCommand { get; set; }
        public RelayCommand EditTransactionCommand { get; set; }
        public RelayCommand AllTransactionCommand { get; set; }
        public RelayCommand SelectedItemExpense { get; set; }
        public RelayCommand SelectedItemIncome { get; set; }
        public RelayCommand SelectedItemAccount { get; set; }
        public RelayCommand SelectedItemTransaction { get; set; }
        public RelayCommand SelectionChangedUser { get; set; }
        public RelayCommand DateChangeCommand { get; set; }
        public RelayCommand SecondDateChangeCommand { get; set; }
        public RelayCommand AllSubcategoriesCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand DeleteTransactionCommand { get; set; }
        public RelayCommand DayCommand { get; set; }
        public RelayCommand WeekCommand { get; set; }
        public RelayCommand MonthCommand { get; set; }
        public RelayCommand YearCommand { get; set; }
        public RelayCommand PeriodCommand { get; set; }
        public RelayCommand GotFocusOnTransaction { get; set; }
        public RelayCommand GotFocusOnAccount { get; set; }
        public RelayCommand GotFocusOnIncome { get; set; }
        public RelayCommand GotFocusOnExpense { get; set; }
        #endregion

        #region methods

        delegate long OperationWithTransaction(long budgeted, long transactionCount);

        static long Sum(long x, long y)
        {
            return x + y;
        }

        static long Sub(long x, long y)
        {
            return x - y;
        }

        private void DeleteCategory()
        {
            if (SelectedCategory != null)
            {
                Category categoryAccount;
                Category categoryForRemove;
                List<Transaction> allTransactions = budgetManagerRepository.GetTransactions().ToList();
                List<Category> allCategories = budgetManagerRepository.GetCategories().ToList();
                List<Transaction> transactionsForDelete = allTransactions.Where(transaction => transaction.Categoryid == SelectedCategory.Id
                    || transaction.Accounttype == SelectedCategory.Id).ToList();
                budgetManagerRepository.DeleteCategory(SelectedCategory.Id);
                budgetManagerRepository.DeleteTransactionByCategory(SelectedCategory.Id, SelectedCategory.Id);

                if (SelectedCategory.Type == "Income")
                {
                    categoryAccount = allCategories.FirstOrDefault(category => category.Id == SelectedCategory.AccountId);
                    categoryAccount.Budgeted -= SelectedCategory.Budgeted;
                    budgetManagerRepository.UpdateCategory(categoryAccount);
                    SelectedUser.Categoriesincome.Remove(SelectedCategory);
                }

                else if (SelectedCategory.Type == "Expense")
                {
                    ObservableCollection<Category> SubcategoriesForRemove = new ObservableCollection<Category>(allCategories.Where(a => a.AccountId == SelectedCategory.Id));
                    foreach (var subcategory in SubcategoriesForRemove)
                    {

                        List<Transaction> tempTransactions = allTransactions.Where(x => x.Categoryid == subcategory.Id || x.Accounttype == subcategory.Id).ToList();
                        foreach (var item in tempTransactions)
                        {
                            Transaction transactionForDelete = SelectedUser.Transactions.FirstOrDefault(x => x.Id == item.Id);
                            categoryAccount = allCategories.FirstOrDefault(category => category.Id == item.Accounttype);
                            categoryAccount.Budgeted += subcategory.Remaining;
                            SelectedUser.Transactions.Remove(transactionForDelete);
                            budgetManagerRepository.UpdateCategory(categoryAccount);
                        }

                        budgetManagerRepository.DeleteTransactionByCategory(subcategory.Id, subcategory.Id);
                        budgetManagerRepository.DeleteCategory(subcategory.Id);

                    }

                    foreach (var transaction in transactionsForDelete)
                    {
                        categoryAccount = allCategories.FirstOrDefault(category => category.Id == transaction.Accounttype);
                        categoryAccount.Budgeted += transaction.Count;
                        budgetManagerRepository.UpdateCategory(categoryAccount);
                        SelectedUser.Transactions.Remove(transaction);
                    }

                    categoryForRemove = allCategories.FirstOrDefault(category => category.Id == SelectedCategory.Id);
                    SelectedUser.Categoriesexpense.Remove(SelectedCategory);
                }

                else if (SelectedCategory.Type == "Account")
                {
                    List<Category> incomeCategories = budgetManagerRepository.GetCategories().ToList();
                    incomeCategories = incomeCategories.Where(category => category.AccountId == SelectedCategory.Id).ToList();
                    foreach (var transaction in transactionsForDelete)
                    {
                        if (transaction.Accounttype == SelectedCategory.Id)
                        {
                            Category temp = allCategories.FirstOrDefault(category => category.Id == transaction.Categoryid);
                            temp.Budgeted -= transaction.Count;
                            budgetManagerRepository.UpdateCategory(temp);

                        }
                        if (transaction.Categoryid == SelectedCategory.Id)
                        {
                            Category temp = allCategories.FirstOrDefault(category => category.Id == transaction.Accounttype);
                            temp.Budgeted += transaction.Count;
                            budgetManagerRepository.UpdateCategory(temp);
                        }

                    }

                    foreach (var searchingCategory in incomeCategories)
                    {
                        SelectedUser.Categoriesincome.Remove(SelectedUser.Categoriesincome.First(x => x.Id == searchingCategory.Id));
                        budgetManagerRepository.DeleteCategory(searchingCategory.Id);

                    }

                    SelectedUser.Categoriesaccount.Remove(SelectedCategory);
                }
                foreach (var item in transactionsForDelete)
                {
                    Transaction transactionForDelete = SelectedUser.Transactions.FirstOrDefault(x => x.Id == item.Id);
                    SelectedUser.Transactions.Remove(transactionForDelete);
                }
                List<Category> tempAccounts = SelectedUser.Categoriesaccount.ToList();
                List<Category> tempAllAccounts = budgetManagerRepository.GetCategories().ToList();
                tempAllAccounts = tempAllAccounts.Where(x => x.Userid == SelectedUser.Id && x.Type == "Account").ToList();
                SelectedUser.Categoriesaccount = new ObservableCollection<Category>();
                foreach (var chosencategory in tempAccounts)
                {
                    foreach (var category in tempAllAccounts)
                    {
                        if (category.Id == chosencategory.Id)
                        {
                            SelectedUser.Categoriesaccount.Add(category);
                        }
                    }
                }
                GradientMehod();
                SelectedCategory = new Category();
                Chosen = true;
                Unchosen = false;
                Unchosentrans = false;
                Unchosenincome = false;
                Unchosenaccount = false;
            }
        }

        private void DeleteTransaction()
        {
            if (SelectedTransactions.Count != 0)
            {
                List<Category> allCategories = budgetManagerRepository.GetCategories().ToList();
                List<Transaction> tempSelectedTransactions = new List<Transaction>();

                foreach (var searchingtransaction in SelectedTransactions)
                {
                    Transaction chosenTransaction = (Transaction)searchingtransaction;
                    tempSelectedTransactions.Add(chosenTransaction);

                }

                foreach (var selectedtransaction in tempSelectedTransactions)
                {
                    foreach (var category in allCategories)
                    {
                        if (category.Id == selectedtransaction.Accounttype || category.Id == selectedtransaction.Categoryid)
                        {
                            OperationWithTransaction op;

                            if (selectedtransaction.Type == "Income")
                            {
                                op = new OperationWithTransaction(Sub);
                                category.Budgeted = op(category.Budgeted, selectedtransaction.Count);
                            }

                            else if (selectedtransaction.Type == "Expense")
                            {
                                if (selectedtransaction.Categoryid == category.Id)
                                {
                                    op = new OperationWithTransaction(Sub);
                                    category.Remaining = op(category.Remaining, selectedtransaction.Count);
                                    if (category.Type == "Subcategory")
                                    {
                                        foreach (var item in allCategories)
                                        {
                                            if (item.Id == category.AccountId)
                                            {
                                                item.Remaining = op(item.Remaining, selectedtransaction.Count);
                                                budgetManagerRepository.UpdateCategory(item);
                                            }
                                        }
                                    }
                                }
                                if (selectedtransaction.Accounttype == category.Id)
                                {
                                    op = new OperationWithTransaction(Sum);
                                    category.Budgeted = op(category.Budgeted, selectedtransaction.Count);
                                }
                            }
                            else
                            {
                                if (selectedtransaction.Categoryid == category.Id)
                                {
                                    op = new OperationWithTransaction(Sub);
                                    category.Budgeted = op(category.Budgeted, selectedtransaction.Count);
                                }
                                if (selectedtransaction.Accounttype == category.Id)
                                {
                                    op = new OperationWithTransaction(Sum);
                                    category.Budgeted = op(category.Budgeted, selectedtransaction.Count);
                                }
                            }
                            budgetManagerRepository.UpdateCategory(category);
                        }
                    }
                    SelectedUser.Transactions.Remove(SelectedUser.Transactions.FirstOrDefault(transaction => transaction.Id == selectedtransaction.Id));
                    budgetManagerRepository.DeleteTransaction(selectedtransaction.Id);
                }
                ChooseRightPeriodByUserSwitch();
                GradientMehod();
                SelectedTransaction = new Transaction();
                Painting();
                Chosen = true;
                Unchosen = false;
                Unchosentrans = false;
                Unchosenincome = false;
                Unchosenaccount = false;
                deletevisibility = false;
            }
        }

        private void InfoByYear()
        {
            AllBudgetVisibility = false;
            InputtingData();
            SelectedUser.Categoriesaccount = new ObservableCollection<Category>(CategoriesAccount.Where(a => a.Year == SelectedYear));
            SelectedUser.Categoriesexpense = new ObservableCollection<Category>(CategoriesExpense.Where(a => a.Year == SelectedYear));
            SelectedUser.Categoriesincome = new ObservableCollection<Category>(CategoriesIncome.Where(a => a.Year == SelectedYear));
            SelectedUser.Transactions = new ObservableCollection<Transaction>(Transactions.Where(a => a.Year == SelectedYear));
            CountBudget();
            SelectedUser.Expense = SelectedUser.Transactions.Where(a => a.Type == "Expense").Sum(x => x.Count);
            SelectedUser.Income = SelectedUser.Categoriesincome.Sum(x => x.Budgeted);
            List<Transaction> transactionsIncome = SelectedUser.Transactions.Where(x => x.Type == "Income").ToList();
            foreach (var category in SelectedUser.Categoriesincome)
            {
                foreach (var transaction in SelectedUser.Transactions)
                {
                    if (transaction.Categoryid == category.Id)
                    {
                        transactionsIncome.Remove(transaction);
                    }
                }
            }

            foreach (var item in transactionsIncome)
            {
                SelectedUser.Income += item.Count;
            }

            StatisticMethod();
            GradientMehod();
            Painting();
            if (SelectedUser.Income == 0 && SelectedUser.Expense == 0)
            {
                AllBudgetVisibility = true;
            }
        }

        private void InfoByDay()
        {
            AllBudgetVisibility = false;
            InputtingData();
            SelectedUser.Categoriesaccount = new ObservableCollection<Category>(CategoriesAccount.Where(a => a.Year == SelectedYear && a.Day == SelectedStartDay
                && a.Month == DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month));
            SelectedUser.Categoriesexpense = new ObservableCollection<Category>(CategoriesExpense.Where(a => a.Year == SelectedYear && a.Day == SelectedStartDay
                && a.Month == DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month));
            SelectedUser.Categoriesincome = new ObservableCollection<Category>(CategoriesIncome.Where(a => a.Year == SelectedYear && a.Day == SelectedStartDay
                && a.Month == DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month));
            SelectedUser.Transactions = new ObservableCollection<Transaction>(Transactions.Where(a => a.Year == SelectedYear && a.Day == SelectedStartDay
                && a.Month == DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month));
            CountBudget();
            SelectedUser.Expense = SelectedUser.Transactions.Where(a => a.Type == "Expense").Sum(x => x.Count);
            SelectedUser.Income = SelectedUser.Categoriesincome.Sum(x => x.Budgeted);
            List<Transaction> transactionsIncome = SelectedUser.Transactions.Where(x => x.Type == "Income").ToList();
            foreach (var category in SelectedUser.Categoriesincome)
            {
                foreach (var transaction in SelectedUser.Transactions)
                {
                    if (transaction.Categoryid == category.Id)
                    {
                        transactionsIncome.Remove(transaction);
                    }
                }
            }

            foreach (var item in transactionsIncome)
            {
                SelectedUser.Income += item.Count;
            }

            StatisticMethod();
            GradientMehod();
            Painting();
            if (SelectedUser.Income == 0 && SelectedUser.Expense == 0)
            {
                AllBudgetVisibility = true;
            }
        }

        private void InfoByMonth()
        {
            AllBudgetVisibility = false;
            InputtingData();
            SelectedUser.Categoriesaccount = new ObservableCollection<Category>(CategoriesAccount.Where(a => a.Year == SelectedYear
                && a.Month == DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month));
            SelectedUser.Categoriesexpense = new ObservableCollection<Category>(CategoriesExpense.Where(a => a.Year == SelectedYear
                && a.Month == DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month));
            SelectedUser.Categoriesincome = new ObservableCollection<Category>(CategoriesIncome.Where(a => a.Year == SelectedYear
                && a.Month == DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month));
            SelectedUser.Transactions = new ObservableCollection<Transaction>(Transactions.Where(a => a.Year == SelectedYear
                && a.Month == DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month));
            CountBudget();
            SelectedUser.Expense = SelectedUser.Transactions.Where(a => a.Type == "Expense").Sum(x => x.Count);
            SelectedUser.Income = SelectedUser.Categoriesincome.Sum(x => x.Budgeted);
            List<Transaction> transactionsIncome = SelectedUser.Transactions.Where(x => x.Type == "Income").ToList();
            foreach (var category in SelectedUser.Categoriesincome)
            {
                foreach (var transaction in SelectedUser.Transactions)
                {
                    if (transaction.Categoryid == category.Id)
                    {
                        transactionsIncome.Remove(transaction);
                    }
                }
            }

            foreach (var item in transactionsIncome)
            {
                SelectedUser.Income += item.Count;
            }

            StatisticMethod();
            GradientMehod();
            Painting();
            if (SelectedUser.Income == 0 && SelectedUser.Expense == 0)
            {
                AllBudgetVisibility = true;
            }
        }

        private void InfoByWeek()
        {
            AllBudgetVisibility = false;
            int firstmonthnumber = DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month;
            int daysinmonth = System.DateTime.DaysInMonth((int)SelectedYear, firstmonthnumber);
            long tempday = SelectedStartDay;
            int tempmonthnumber = firstmonthnumber;
            long tempyear = SelectedYear;
            SelectedUser.Categoriesaccount.Clear();
            SelectedUser.Categoriesexpense.Clear();
            SelectedUser.Categoriesincome.Clear();
            SelectedUser.Transactions.Clear();
            ObservableCollection<Category> Allcategories = new ObservableCollection<Category>();
            if (SelectedUser != AllUsers)
            {
                Allcategories = budgetManagerRepository.GetCategoriesByUserId(SelectedUser.Id);
                Transactions = budgetManagerRepository.GetTransactionsByUserId(SelectedUser.Id);
            }
            else
            {
                Allcategories = budgetManagerRepository.GetCategories();
                Transactions = budgetManagerRepository.GetTransactions();
            }

            for (int i = 0; i <= 7; i++)
            {
                if (tempday < daysinmonth)
                {
                    foreach (var category in Allcategories)
                    {
                        if (category.Year == tempyear && category.Day == tempday && category.Month == tempmonthnumber)
                        {
                            if (category.Type == "Income")
                            {
                                SelectedUser.Categoriesincome.Add(category);
                            }

                            else if (category.Type == "Expense")
                            {
                                SelectedUser.Categoriesexpense.Add(category);
                            }

                            else if (category.Type == "Account")
                            {

                                SelectedUser.Categoriesaccount.Add(category);

                            }

                        }

                    }

                    foreach (var searchingtransaction in Transactions)
                    {
                        if (searchingtransaction.Year == tempyear && searchingtransaction.Day == tempday &&
                            searchingtransaction.Month == tempmonthnumber)
                        {

                            SelectedUser.Transactions.Add(searchingtransaction);
                        }
                    }
                    tempday++;
                }

                else
                {
                    foreach (var category in Allcategories)
                    {
                        if (category.Year == tempyear && category.Day == tempday && category.Month == tempmonthnumber)
                        {
                            if (category.Type == "Income")
                            {
                                SelectedUser.Categoriesincome.Add(category);
                            }
                            else if (category.Type == "Expense")
                            {
                                SelectedUser.Categoriesexpense.Add(category);
                            }
                            else if (category.Type == "Account")
                            {
                                SelectedUser.Categoriesaccount.Add(category);
                            }
                        }
                    }

                    foreach (var searchingtransaction in Transactions)
                    {
                        if (searchingtransaction.Year == tempyear && searchingtransaction.Day == tempday && searchingtransaction.Month == tempmonthnumber)
                        {
                            SelectedUser.Transactions.Add(searchingtransaction);
                        }
                    }

                    tempday = 1;
                    if (tempmonthnumber == 12)
                    {
                        tempmonthnumber = 1;
                        tempyear++;
                    }
                    else
                    {
                        tempmonthnumber++;
                    }
                }
            }

            SelectedUser.Expense = SelectedUser.Transactions.Where(a => a.Type == "Expense").Sum(x => x.Count);
            SelectedUser.Income = SelectedUser.Categoriesincome.Sum(x => x.Budgeted);
            List<Transaction> transactionsIncome = SelectedUser.Transactions.Where(x => x.Type == "Income").ToList();
            foreach (var category in SelectedUser.Categoriesincome)
            {
                foreach (var transaction in SelectedUser.Transactions)
                {
                    if (transaction.Categoryid == category.Id)
                    {
                        transactionsIncome.Remove(transaction);
                    }
                }
            }

            foreach (var item in transactionsIncome)
            {
                SelectedUser.Income += item.Count;
            }

            StatisticMethod();
            GradientMehod();
            Painting();
            CountBudget();
            if (SelectedUser.Income == 0 && SelectedUser.Expense == 0)
            {
                AllBudgetVisibility = true;
            }
        }

        private void InfoByPeriod()
        {
            AllBudgetVisibility = false;
            SelectedUser.Categoriesaccount.Clear();
            SelectedUser.Categoriesexpense.Clear();
            SelectedUser.Categoriesincome.Clear();
            SelectedUser.Transactions.Clear();
            ObservableCollection<Category> Allcategories = new ObservableCollection<Category>();
            if (SelectedUser != AllUsers)
            {
                Allcategories = budgetManagerRepository.GetCategoriesByUserId(SelectedUser.Id);
                Transactions = budgetManagerRepository.GetTransactionsByUserId(SelectedUser.Id);
            }
            else
            {
                Allcategories = budgetManagerRepository.GetCategories();
                Transactions = budgetManagerRepository.GetTransactions();
            }

            int firstmonthnumber = DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month;
            int secondmonthnumber = DateTime.ParseExact(SelectedEndMonth, "MMMM", cultureInfo).Month;
            long temp = 0;

            if (SelectedEndYear < SelectedYear)
            {
                temp = SelectedYear;
                SelectedYear = SelectedEndYear;
                SelectedEndYear = temp;
                temp = firstmonthnumber;
                firstmonthnumber = secondmonthnumber;
                secondmonthnumber = Convert.ToInt32(temp);
                temp = SelectedStartDay;
                SelectedStartDay = SelectedEndDay;
                SelectedEndDay = temp;

            }
            else if (firstmonthnumber > secondmonthnumber && SelectedEndYear == SelectedYear)
            {

                temp = firstmonthnumber;
                firstmonthnumber = secondmonthnumber;
                secondmonthnumber = Convert.ToInt32(temp);
                temp = SelectedStartDay;
                SelectedStartDay = SelectedEndDay;
                SelectedEndDay = temp;

            }
            else if (SelectedStartDay > SelectedEndDay && firstmonthnumber == secondmonthnumber && SelectedEndYear == SelectedYear)
            {
                temp = SelectedStartDay;
                SelectedStartDay = SelectedEndDay;
                SelectedEndDay = temp;

            }

            long daysinmonth = 0;
            long tempday = SelectedStartDay;
            int tempmonthnumber = firstmonthnumber;
            long tempyear = SelectedYear;

            while (tempmonthnumber != secondmonthnumber || tempyear != SelectedEndYear || tempday != SelectedEndDay)
            {
                if (tempmonthnumber != secondmonthnumber || tempyear != SelectedEndYear)
                {
                    daysinmonth = System.DateTime.DaysInMonth((int)tempyear, tempmonthnumber);
                }
                else
                {

                    daysinmonth = SelectedEndDay;
                    tempday = SelectedEndDay;
                }

                if (tempmonthnumber == firstmonthnumber)
                {
                    for (int i = Convert.ToInt32(SelectedStartDay); i <= daysinmonth; i++)
                    {
                        foreach (var category in Allcategories)
                        {
                            if (category.Year == tempyear && category.Day == i && category.Month == tempmonthnumber)
                            {
                                if (category.Type == "Income")
                                {
                                    SelectedUser.Categoriesincome.Add(category);
                                }

                                else if (category.Type == "Expense")
                                {
                                    SelectedUser.Categoriesexpense.Add(category);
                                }

                                else if (category.Type == "Account")
                                {

                                    SelectedUser.Categoriesaccount.Add(category);

                                }

                            }

                        }

                        foreach (var searchingtransaction in Transactions)
                        {
                            if (searchingtransaction.Year == tempyear && searchingtransaction.Day == i &&
                                searchingtransaction.Month == tempmonthnumber)
                            {
                                SelectedUser.Transactions.Add(searchingtransaction);
                            }

                        }

                    }
                }
                else
                {
                    for (int i = 1; i <= daysinmonth; i++)
                    {

                        foreach (var category in Allcategories)
                        {
                            if (category.Year == tempyear && category.Day == i && category.Month == tempmonthnumber)
                            {
                                if (category.Type == "Income")
                                {
                                    SelectedUser.Categoriesincome.Add(category);
                                }

                                else if (category.Type == "Expense")
                                {
                                    SelectedUser.Categoriesexpense.Add(category);
                                }

                                else if (category.Type == "Account")
                                {

                                    SelectedUser.Categoriesaccount.Add(category);

                                }

                            }

                        }

                        foreach (var searchingtransaction in Transactions)
                        {
                            if (searchingtransaction.Year == tempyear && searchingtransaction.Day == i
                                && searchingtransaction.Month == tempmonthnumber)
                            {
                                SelectedUser.Transactions.Add(searchingtransaction);
                            }
                        }
                    }
                }
                if (tempmonthnumber != secondmonthnumber || tempyear != SelectedEndYear)
                {
                    if (tempmonthnumber == 12)
                    {
                        tempday = 1;
                        tempmonthnumber = 1;
                        if (tempyear != SelectedEndYear)
                        {
                            tempyear++;
                        }
                    }
                    else
                    {
                        tempmonthnumber++;
                        tempday = 1;
                    }
                }
            }

            foreach (var transaction in Transactions)
            {
                if (transaction.Day == SelectedEndDay && transaction.Month == DateTime.ParseExact(SelectedEndMonth, "MMMM", cultureInfo).Month
                    && transaction.Year == SelectedEndYear)
                {
                    SelectedUser.Transactions.Add(transaction);
                }

            }

            foreach (var category in Allcategories)
            {
                if (category.Day == SelectedEndDay && category.Month == DateTime.ParseExact(SelectedEndMonth, "MMMM", cultureInfo).Month
                    && category.Year == SelectedEndYear)
                {
                    if (category.Type == "Expense")
                    {
                        SelectedUser.Categoriesexpense.Add(category);
                    }
                    if (category.Type == "Income")
                    {
                        SelectedUser.Categoriesincome.Add(category);
                    }
                    if (category.Type == "Account")
                    {
                        SelectedUser.Categoriesaccount.Add(category);
                    }
                }

            }

            SelectedUser.Expense = SelectedUser.Transactions.Where(a => a.Type == "Expense").Sum(x => x.Count);
            SelectedUser.Income = SelectedUser.Categoriesincome.Sum(x => x.Budgeted);
            List<Transaction> transactionsIncome = SelectedUser.Transactions.Where(x => x.Type == "Income").ToList();
            foreach (var category in SelectedUser.Categoriesincome)
            {
                foreach (var transaction in SelectedUser.Transactions)
                {
                    if (transaction.Categoryid == category.Id)
                    {
                        transactionsIncome.Remove(transaction);
                    }
                }
            }

            foreach (var item in transactionsIncome)
            {
                SelectedUser.Income += item.Count;
            }

            StatisticMethod();
            GradientMehod();
            Painting();
            CountBudget();
            SelectedStartDay = ThisDate.Day;
            SelectedMonth = cultureInfo.DateTimeFormat.GetMonthName(ThisDate.Month);
            SelectedYear = ThisDate.Year;
            SelectedEndDay = SecondDate.Day;
            SelectedEndMonth = cultureInfo.DateTimeFormat.GetMonthName(SecondDate.Month);
            SelectedEndYear = SecondDate.Year;
            if (SelectedUser.Income == 0 && SelectedUser.Expense == 0)
            {
                AllBudgetVisibility = true;
            }
        }

        private void InputtingData()
        {

            Categories = budgetManagerRepository.GetCategories();
            if (SelectedUser != AllUsers)
            {
                CategoriesExpense = new ObservableCollection<Category>(Categories.Where(s => s.Type == "Expense" && s.Userid == SelectedUser.Id));
                CategoriesIncome = new ObservableCollection<Category>(Categories.Where(s => s.Type == "Income" && s.Userid == SelectedUser.Id));
                CategoriesAccount = new ObservableCollection<Category>(Categories.Where(s => s.Type == "Account" && s.Userid == SelectedUser.Id));
                Transactions = budgetManagerRepository.GetTransactionsByUserId(SelectedUser.Id);
            }
            else
            {
                CategoriesExpense = new ObservableCollection<Category>(Categories.Where(s => s.Type == "Expense"));
                CategoriesIncome = new ObservableCollection<Category>(Categories.Where(s => s.Type == "Income"));
                CategoriesAccount = new ObservableCollection<Category>(Categories.Where(s => s.Type == "Account"));
                Transactions = budgetManagerRepository.GetTransactions();

            }



        }

        private void StatisticMethod()
        {
            Category Income = new Category();
            Category Expense = new Category();
            List<Category> tempcat = new List<Category>();
            foreach (var category in StatisticCategories)
            {
                if (category.Name == "Расходы")
                {
                    Expense = new Category() { Name = category.Name, StatisticType = SelectedUser.Expense };
                    tempcat.Add(Expense);
                }
                else
                {
                    Income = new Category() { Name = category.Name, StatisticType = SelectedUser.Income };
                    tempcat.Add(Income);
                }
            }

            StatisticCategories.Clear();
            StatisticCategories = new ObservableCollection<Category>(tempcat);
            if (SelectedUser.Income == 0)
            {
                Percent = 1;
            }
            else
            {
                Percent = 0;
            }
        }

        private void GradientMehod()
        {

            if (SelectedUser.Income != 0)
            {
                SelectedUser.RestPercent = (SelectedUser.Expense * 100 / SelectedUser.Income) * 0.01;
                SelectedUser.ExpensePercent = 1 - SelectedUser.RestPercent;
                SelectedUser.Rest = SelectedUser.Income - SelectedUser.Expense;
                Percent = 1;
            }

            else if (SelectedUser.Expense != 0)
            {
                SelectedUser.ExpensePercent = 0;
                SelectedUser.RestPercent = 1;
                SelectedUser.Rest = SelectedUser.Income - SelectedUser.Expense;
                Percent = 0;

            }
            else
            {

                SelectedUser.ExpensePercent = 1;
                SelectedUser.RestPercent = 1;
                SelectedUser.Rest = SelectedUser.Income - SelectedUser.Expense;
                Percent = 0;
            }

            if (SelectedUser.Rest < 0)
            {
                SelectedUser.Rest = 0;
            }
        }

        private void Painting()
        {

            foreach (var searchingcategory in SelectedUser.Categoriesexpense)
            {
                double tempForCategory = (searchingcategory.Remaining * 100 / searchingcategory.Budgeted) * 0.01;
                searchingcategory.Percent = 1 - tempForCategory;
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

        private void CountBudget()
        {

            List<Category> ForGrouping = SelectedUser.Categoriesexpense.GroupBy(a => a.Name).Select(category => category.First()).ToList();
            foreach (var sum in ForGrouping)
            {
                sum.Budgeted = SelectedUser.Categoriesexpense.Where(category => category.Name == sum.Name).Sum(a => a.Budgeted);
                sum.Remaining = SelectedUser.Categoriesexpense.Where(category => category.Name == sum.Name).Sum(a => a.Remaining);
            }
            SelectedUser.Categoriesexpense = new ObservableCollection<Category>(ForGrouping);


            ForGrouping = SelectedUser.Categoriesincome.GroupBy(a => a.Name).Select(category => category.First()).ToList();
            foreach (var sum in ForGrouping)
            {
                sum.Budgeted = SelectedUser.Categoriesincome.Where(category => category.Name == sum.Name).Sum(a => a.Budgeted);
            }
            SelectedUser.Categoriesincome = new ObservableCollection<Category>(ForGrouping);

            ForGrouping = SelectedUser.Categoriesaccount.GroupBy(a => a.Name).Select(category => category.First()).ToList();
            foreach (var sum in ForGrouping)
            {
                sum.Budgeted = SelectedUser.Categoriesaccount.Where(category => category.Name == sum.Name).Sum(a => a.Budgeted);
            }
            SelectedUser.Categoriesaccount = new ObservableCollection<Category>(ForGrouping);


        }

        private void GetDayType()
        {
            SecondDateVisibility = false;
            ChosenDateType = "Day";
            if (SelectedUser != null)
            {
                InfoByDay();
            }
        }

        private void GetMonthType()
        {
            SecondDateVisibility = false;
            ChosenDateType = "Month";
            if (SelectedUser != null)
            {
                InfoByMonth();
            }
        }

        private void GetYearType()
        {
            SecondDateVisibility = false;
            ChosenDateType = "Year";
            if (SelectedUser != null)
            {
                InfoByYear();
            }
        }

        private void GetWeekType()
        {
            SecondDateVisibility = false;
            ChosenDateType = "Week";
            if (SelectedUser != null)
            {
                InfoByWeek();
            }
        }

        private void GetPeriodType()
        {
            SecondDateVisibility = true;
            ChosenDateType = "Period";
        }

        private void ShowAllIncome()
        {
            var data = "Income";
            navigation.NavigateTo("AllExpense", data);
        }

        private void EditIncome()
        {
            var data = SelectedCategory;
            navigation.NavigateTo("AddIncome", data);
        }

        private void AddIncome()
        {
            Category NewCategory = new Category();
            var data = NewCategory;
            navigation.NavigateTo("AddIncome", data);
        }

        private void ShowAllExpense()
        {
            var data = "Expense";
            navigation.NavigateTo("AllExpense", data);
        }

        private void EditExpense()
        {
            var data = SelectedCategory;
            navigation.NavigateTo("EditExpense", data);
        }

        private void ShowAllMoneyAccount()
        {
            var data = "Account";
            navigation.NavigateTo("AllExpense", data);
        }

        private void EditMoneyAccount()
        {
            var data = SelectedCategory;
            navigation.NavigateTo("AddMoneyAccount", data);
        }

        private void AddMoneyAccount()
        {
            Category NewCategory = new Category();
            var data = NewCategory;
            navigation.NavigateTo("AddMoneyAccount", data);
        }

        private void AddTransaction()
        {
            Category NewCategory = new Category();
            var data = NewCategory;
            navigation.NavigateTo("AddTransaction", data);
        }

        private void EditTransaction()
        {
            var data = SelectedTransaction;
            navigation.NavigateTo("EditTransaction", data);
        }

        private void ShowAllTransactions()
        {
            Transaction trans = new Transaction();
            var data = trans;
            navigation.NavigateTo("AllTransactions", data);
        }

        private void SelectExpense()
        {
            if (SelectedCategory != null)
            {
                Chosen = false;
                Unchosen = true;
                Unchosentrans = false;
                Unchosenincome = false;
                Unchosenaccount = false;
                Deletevisibility = true;
                tempCategory = SelectedCategory;
                SelectedCategory = null;
                SelectedCategory = tempCategory;
                tempTransactions = SelectedUser.Transactions.ToList();
                SelectedUser.Transactions = new ObservableCollection<Transaction>(tempTransactions);
            }
            else
            {
                Chosen = true;
                Unchosen = false;
                Unchosentrans = false;
                Unchosenincome = false;
                Unchosenaccount = false;
                Deletevisibility = false;
            }
        }

        private void SelectIncome()
        {
            if (SelectedCategory != null)
            {
                Chosen = false;
                Unchosen = false;
                Unchosentrans = false;
                Unchosenincome = true;
                Unchosenaccount = false;
                Deletevisibility = true;
                tempCategory = SelectedCategory;
                SelectedCategory = null;
                SelectedCategory = tempCategory;
                tempTransactions = SelectedUser.Transactions.ToList();
                SelectedUser.Transactions = new ObservableCollection<Transaction>(tempTransactions);
            }
            else
            {
                Chosen = true;
                Unchosen = false;
                Unchosentrans = false;
                Unchosenincome = false;
                Unchosenaccount = false;
                Deletevisibility = false;
            }
        }

        private void SelectAccount()
        {

            if (SelectedCategory != null)
            {
                Chosen = false;
                Unchosen = false;
                Unchosentrans = false;
                Unchosenincome = false;
                Unchosenaccount = true;
                Deletevisibility = true;
                tempCategory = SelectedCategory;
                SelectedCategory = null;
                SelectedCategory = tempCategory;
                tempTransactions = SelectedUser.Transactions.ToList();
                SelectedUser.Transactions = new ObservableCollection<Transaction>(tempTransactions);
            }
            else
            {
                Chosen = true;
                Unchosen = false;
                Unchosentrans = false;
                Unchosenincome = false;
                Unchosenaccount = false;
                Deletevisibility = false;
            }
        }

        private void SelectTransaction()
        {
            if (SelectedTransactions.Count != 0)
            {
                Chosen = false;
                Unchosen = false;
                Unchosentrans = true;
                Unchosenincome = false;
                Unchosenaccount = false;
                Deletevisibility = false;
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
                Chosen = true;
                Unchosen = false;
                Unchosentrans = false;
                Unchosenincome = false;
                Unchosenaccount = false;
                Deletevisibility = false;
            }
        }

        private void ChangeUser()
        {
            ChooseRightPeriodByUserSwitch();
            StatisticMethod();

            if (SelectedUser != AllUsers)
            {
                foreach (var user in Users)
                {
                    if (user.Id == SelectedUser.Id)
                    {
                        user.IsSelected = "Main";

                    }
                    else
                    {
                        user.IsSelected = "Secondary";
                    }
                    budgetManagerRepository.UpdateUser(user);
                }
            }
            Painting();
            GradientMehod();
            CountBudget();
        }

        private void ChangeDate()
        {
            SelectedStartDay = ThisDate.Day;
            SelectedMonth = cultureInfo.DateTimeFormat.GetMonthName(ThisDate.Month);
            SelectedYear = ThisDate.Year;
            ChooseRightPeriodByUserSwitch();

        }

        private void ChangeSecondDate()
        {
            SelectedEndDay = SecondDate.Day;
            SelectedEndMonth = cultureInfo.DateTimeFormat.GetMonthName(SecondDate.Month);
            SelectedEndYear = SecondDate.Year;
            InfoByPeriod();
        }

        private void ShowAllSubcategories()
        {
            if (SelectedCategory != null && SelectedCategory.Type == "Expense")
            {
                var data = SelectedCategory;
                navigation.NavigateTo("AllSubcategories", data);
            }
            else
            {
                ShowWarningMessage();
            }

        }

        private void ChooseRightPeriodByUserSwitch()
        {
            if (SelectedUser != null)
            {
                switch (ChosenDateType)
                {
                    case "Day":
                        InfoByDay();
                        break;
                    case "Month":
                        InfoByMonth();
                        break;
                    case "Year":
                        InfoByYear();
                        break;
                    case "Week":
                        InfoByWeek();
                        break;
                    case "Period":
                        InfoByPeriod();
                        break;
                }
            }
        }

        private void GotTransactionFocusExecute()
        {
            SelectedCategory = null;
            Chosen = false;
            Unchosen = false;
            Unchosentrans = true;
            Unchosenincome = false;
            Unchosenaccount = false;
            Deletevisibility = false;
        }

        private void GotAccountFocusExecute()
        {
            if (SelectedCategory != null)
            {
                Chosen = false;
                Unchosen = false;
                Unchosentrans = false;
                Unchosenincome = false;
                Unchosenaccount = true;
                Deletevisibility = true;
            }
        }

        private void GotIncomeFocusExecute()
        {
                Chosen = false;
                Unchosen = false;
                Unchosentrans = false;
                Unchosenincome = true;
                Unchosenaccount = false;
                Deletevisibility = true;
            
        }

        private void GotExpenseFocusExecute()
        {
            if (SelectedCategory.Id!= 0)
            {
                Chosen = false;
                Unchosen = true;
                Unchosentrans = false;
                Unchosenincome = false;
                Unchosenaccount = false;
                Deletevisibility = true;
            }
            
        }

        private async Task ShowWarningMessage()
        {
            var dialog = new MessageDialog(LocalizedStrings.GetString(LocalizedStringEnum.WarningGoToAllSubcategories),
                LocalizedStrings.GetString(LocalizedStringEnum.WarningAddTitle)
            );

            dialog.Commands.Add(new UICommand(LocalizedStrings.GetString(LocalizedStringEnum.Continue)));
            IUICommand iuiCommand = await dialog.ShowAsync();


        }

        public override void NavigateTo(object parameter)
        {
            this.secondDate = this.thisDate = currentTime;
            this.transactions = new ObservableCollection<Transaction>();
            this.selectedTransactions = new ObservableCollection<object>();
            this.users = budgetManagerRepository.GetUsers();
            this.categories = budgetManagerRepository.GetCategories();
            this.allBudgetVisibility = false;
            this.unchosen = false;
            this.chosen = true;
            this.unchosentrans = false;
            this.unchosenaccount = false;
            this.unchosenincome = false;
            this.deletevisibility = false;
            this.secondDateVisibility = false;
            this.enable = true;
            string currentMonth = cultureInfo.DateTimeFormat.GetMonthName(System.DateTime.Now.Month);
            string previousMonth = cultureInfo.DateTimeFormat.GetMonthName(System.DateTime.Now.Month - 1);

            foreach (var searchinuser in Users)
            {

                List<Category> accountsFromPreviousMonth = budgetManagerRepository.GetCategoryByType(searchinuser.Id, "Account",
                    DateTime.ParseExact(previousMonth, "MMMM", cultureInfo).Month, currentTime.Year).ToList();
                foreach (var category in accountsFromPreviousMonth)
                {
                    if (ThisDate.Day == 1 && accountsFromPreviousMonth.Count != 0)
                    {
                        category.Day = ThisDate.Day;
                        category.Month = ThisDate.Month;
                        category.Year = ThisDate.Year;
                        budgetManagerRepository.UpdateCategory(category);
                    }
                }

                searchinuser.Categoriesaccount = budgetManagerRepository.GetCategoryByType(searchinuser.Id, "Account",
                    DateTime.ParseExact(currentMonth, "MMMM", cultureInfo).Month, currentTime.Year);
                foreach (var account in searchinuser.Categoriesaccount)
                {
                    if (account.Budgeted < 0)
                    {
                        account.Budgeted = 0;
                        budgetManagerRepository.UpdateCategory(account);
                    }
                }
                searchinuser.Categoriesincome = budgetManagerRepository.GetCategoryByType(searchinuser.Id, "Income",
                    DateTime.ParseExact(currentMonth, "MMMM", cultureInfo).Month, currentTime.Year);
                searchinuser.Categoriesexpense = budgetManagerRepository.GetCategoryByType(searchinuser.Id, "Expense",
                    DateTime.ParseExact(currentMonth, "MMMM", cultureInfo).Month, currentTime.Year);
                searchinuser.Visibility = true;
                searchinuser.Transactions = budgetManagerRepository.GetTransactionsByUserId(searchinuser.Id);
                searchinuser.Transactions = new ObservableCollection<Transaction>(searchinuser.Transactions.Where(a => a.Month ==
                    DateTime.ParseExact(currentMonth, "MMMM", cultureInfo).Month && a.Year == currentTime.Year));
                searchinuser.Expense = searchinuser.Transactions.Where(a => a.Type == "Expense").Sum(x => x.Count);
                searchinuser.Income = searchinuser.Categoriesincome.Sum(x => x.Budgeted);
                List<Transaction> transactionsIncome = searchinuser.Transactions.Where(x => x.Type == "Income").ToList();

                foreach (var category in searchinuser.Categoriesincome)
                {
                    foreach (var transaction in searchinuser.Transactions)
                    {
                        if (transaction.Categoryid == category.Id)
                        {
                            transactionsIncome.Remove(transaction);
                        }
                    }
                }

                foreach (var item in transactionsIncome)
                {
                    searchinuser.Income += item.Count;
                }
            }

            allUsers = new User()
            {
                Name = LocalizedStrings.GetString(LocalizedStringEnum.AllUsers),
                Icon = "/Assets/icon.png",
                Income = Users.Sum(a => a.Income),
                Expense = Users.Sum(a => a.Expense),
                Transactions = new ObservableCollection<Transaction>(budgetManagerRepository.GetTransactions().Where(a =>
                    a.Month == DateTime.ParseExact(currentMonth, "MMMM", cultureInfo).Month && a.Year == currentTime.Year)),
                Categoriesaccount = new ObservableCollection<Category>(budgetManagerRepository.GetCategories().Where(a => a.Type == "Account" &&
                    a.Month == DateTime.ParseExact(currentMonth, "MMMM", cultureInfo).Month && a.Year == currentTime.Year)),
                Categoriesincome = new ObservableCollection<Category>(budgetManagerRepository.GetCategories().Where(a => a.Type == "Income"
                    && a.Month == DateTime.ParseExact(currentMonth, "MMMM", cultureInfo).Month && a.Year == currentTime.Year)),
                Categoriesexpense = new ObservableCollection<Category>(budgetManagerRepository.GetCategories().Where(a => a.Type == "Expense"
                    && a.Month == DateTime.ParseExact(currentMonth, "MMMM", cultureInfo).Month && a.Year == currentTime.Year)),
                Visibility = true
            };
            users.Insert(0, allUsers);

            if (users.Count == 1)
            {
                selectedUser = allUsers;
                selectedUser.ExpensePercent = 1;
                selectedUser.RestPercent = 1;
                selectedUser.Visibility = false;
            }
            else
            {

                selectedUser = Users.FirstOrDefault(chosenUser => chosenUser.IsSelected == "Main");

            }

            GradientMehod();
            Painting();
            CountBudget();
            this.selectedEndMonth = this.selectedMonth = cultureInfo.DateTimeFormat.GetMonthName(ThisDate.Month);
            this.selectedEndYear = this.selectedYear = ThisDate.Year;
            this.selectedEndDay = this.selectedStartDay = ThisDate.Day;
            this.SelectedCategory = new Category();
            this.statisticCategories = new ObservableCollection<Category>();
            Category Expense = new Category() { Name = "Расходы", StatisticType = SelectedUser.Expense };
            Category Income = new Category() { Name = "Доходы", StatisticType = SelectedUser.Income };
            statisticCategories.Add(Expense);
            statisticCategories.Add(Income);
            this.ChosenDateType = "Month";
            this.SecondDateVisibility = false;
            if (SelectedUser.Income == 0 && SelectedUser.Expense == 0)
            {
                this.AllBudgetVisibility = true;
            }


        }

        #endregion

    }
}
