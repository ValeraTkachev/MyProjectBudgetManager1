namespace BudgetManager.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BudgetManager.Models;
    using BudgetManager.DAL;
    using System.Windows.Input;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using BudgetManager.Core;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Views;
    using Windows.UI.Xaml;

    public class AlltransactionsViewModel : ViewModelBase
    {

        #region fields

        private ObservableCollection<object> selectedTransactions;
        private ObservableCollection<Transaction> transactions;
        private ObservableCollection<Transaction> inputTransactions;
        private ObservableCollection<Transaction> temptransactions;
        private ObservableCollection<Category> tempcategories;
        private ObservableCollection<User> users;
        private ObservableCollection<Category> categories;
        private bool enable;
        private Transaction selectedTransaction;
        private DateTime thisDate;
        private DateTime secondDate;
        private long selectedStartDay;
        private string selectedMonth;
        private long selectedYear;
        private long selectedEndDay;
        private long selectedEndYear;
        private string selectedEndMonth;
        private List<TransactionGroups> items;
        private User selectedUser;
        private User allUsers;
        private Category selectedCategory;
        private Category allcategories;
        private long totalincome;
        private long totalexpense;
        private bool unchosenappbar;
        private bool chosenappbar;
        private bool unchosengridview;
        private bool chosengridview;
        private bool secondDateVisibility;
        private string chosenDateType;
        private string previousPage;
        private double height;
        private double heightForList;
        private double width;
        private string title;

        #endregion

        #region Constructor

        public AlltransactionsViewModel(INavigationService navigatedService)
        {
            this.navigation = navigatedService;
            YearCommand = new RelayCommand(GetYearType);
            DayCommand = new RelayCommand(GetDayType);
            MonthCommand = new RelayCommand(GetMonthType);
            PeriodCommand = new RelayCommand(GetPeriodType);
            WeekCommand = new RelayCommand(GetWeekType);
            DeleteTransactionCommand = new RelayCommand(DeleteTransaction);
            SortingCommand = new RelayCommand(Sorting);
            GroupCommand = new RelayCommand(GroupMethod);
            SelectionChangedUser = new RelayCommand(ChangeUser);
            SelectionChangedCategory = new RelayCommand(ChangeCategory);
            SelectedItemTransaction = new RelayCommand(SelectTransaction);
            DateChangeCommand = new RelayCommand(ChangeDate);
            SecondDateChangeCommand = new RelayCommand(ChangeSecondDate);
            AddTransactionCommand = new RelayCommand(GoToAddTransactionPage);
            EditTransactionCommand = new RelayCommand(GoToEditTransactionPage);
            Back = new RelayCommand(BackExecute);

        }

        #endregion

        #region Properties

        public Category Allcategories
        {
            get { return this.allcategories; }
            set
            {
                if (value != this.allcategories)
                {
                    this.allcategories = value;
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

        public List<TransactionGroups> Items
        {
            get { return this.items; }
            set
            {
                if (value != this.items)
                {
                    this.items = value;
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
                    OnPropertyChanged();
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
                if (value != this.selectedMonth)
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

        public ObservableCollection<Transaction> TempTransactions
        {
            get { return this.temptransactions; }
            set
            {
                if (value != this.temptransactions)
                {
                    this.temptransactions = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Category> TempCategories
        {
            get { return this.tempcategories; }
            set
            {
                if (value != this.tempcategories)
                {
                    this.tempcategories = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Transaction> InputTransactions
        {
            get { return this.inputTransactions; }
            set
            {
                if (value != this.inputTransactions)
                {
                    this.inputTransactions = value;
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

        public bool Chosengridview
        {
            get { return this.chosengridview; }
            set
            {
                if (value != this.chosengridview)
                {
                    this.chosengridview = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool Chosenappbar
        {
            get { return chosenappbar; }
            set
            {
                if (value != this.chosenappbar)
                {
                    this.chosenappbar = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool Unchosengridview
        {
            get { return this.unchosengridview; }
            set
            {
                if (value != this.unchosengridview)
                {
                    this.unchosengridview = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool Unchosenappbar
        {
            get { return this.unchosenappbar; }
            set
            {
                if (value != this.unchosenappbar)
                {
                    this.unchosenappbar = value;
                    OnPropertyChanged();
                }
            }
        }

        public long TotalExpense
        {
            get { return this.totalexpense; }
            set
            {
                if (value != this.totalexpense)
                {
                    this.totalexpense = value;
                    OnPropertyChanged();
                }
            }
        }

        public long TotalIncome
        {
            get { return this.totalincome; }
            set
            {
                if (value != this.totalincome)
                {
                    this.totalincome = value;
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

        public string PreviousPage
        {
            get { return this.previousPage; }
            set
            {
                if (value != this.previousPage)
                {
                    this.previousPage = value;
                    OnPropertyChanged();
                }
            }
        }

        public double Height
        {
            get { return this.height; }
            set
            {
                if (value != this.height)
                {
                    this.height = value;
                    OnPropertyChanged();
                }
            }
        }

        public double HeightForList
        {
            get { return this.heightForList; }
            set
            {
                if (value != this.heightForList)
                {
                    this.heightForList = value;
                    OnPropertyChanged();
                }
            }
        }

        public double Width
        {
            get { return this.width; }
            set
            {
                if (value != this.width)
                {
                    this.width = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Title
        {
            get { return this.title; }
            set
            {
                if (value != this.title)
                {
                    this.title = value;
                    OnPropertyChanged();
                }
            }
        }


        #endregion

        #region Commands

        public RelayCommand Back { get; set; }
        public RelayCommand DayCommand { get; set; }
        public RelayCommand WeekCommand { get; set; }
        public RelayCommand MonthCommand { get; set; }
        public RelayCommand YearCommand { get; set; }
        public RelayCommand PeriodCommand { get; set; }
        public RelayCommand SelectionChangedUser { get; set; }
        public RelayCommand SelectionChangedCategory { get; set; }
        public RelayCommand SelectedItemTransaction { get; set; }
        public RelayCommand DateChangeCommand { get; set; }
        public RelayCommand SecondDateChangeCommand { get; set; }
        public RelayCommand DeleteTransactionCommand { get; set; }
        public RelayCommand SortingCommand { get; set; }
        public RelayCommand GroupCommand { get; set; }
        public RelayCommand AddTransactionCommand { get; set; }
        public RelayCommand EditTransactionCommand { get; set; }

        #endregion

        #region Methods

        delegate long OperationWithTransaction(long budgeted, long transactionCount);

        static long Sum(long x, long y)
        {
            return x + y;
        }

        static long Sub(long x, long y)
        {
            return x - y;
        }


        private void InfoByYear()
        {
            InputtingData();
            TotalExpense = 0;
            TotalIncome = 0;

            if (SelectedCategory.Id == 0)
            {
                Transactions = new ObservableCollection<Transaction>(InputTransactions.Where(a => a.Year == SelectedYear));
                TotalIncome = Transactions.Where(a => a.Type == "Income").Sum(x => x.Count);
                TotalExpense = Transactions.Where(a => a.Type == "Expense").Sum(x => x.Count);
            }
            else
            {

                Transactions = new ObservableCollection<Transaction>(InputTransactions.Where(a => a.Year == SelectedYear 
                                                                                                  && (a.Accounttype == SelectedCategory.Id || a.Categoryid == SelectedCategory.Id)));

                if (SelectedCategory.Type == "Expense")
                {
                    List<Category> Subcategories = TempCategories.Where(x => x.AccountId == SelectedCategory.Id).ToList();

                    foreach (var sub in Subcategories)
                    {
                        foreach (var transaction in TempTransactions)
                        {
                            if (transaction.Categoryid == sub.Id && transaction.Year == SelectedYear)
                            {
                                Transactions.Add(transaction);
                            }
                        }
                    }
                }

                TotalIncome = Transactions.Where(a => a.Type == "Income").Sum(x => x.Count);
                TotalExpense = Transactions.Where(a => a.Type == "Expense").Sum(x => x.Count);
            }

        }

        private void InfoByDay()
        {
            TotalExpense = 0;
            TotalIncome = 0;
            InputtingData();

            if (SelectedCategory.Name == LocalizedStrings.GetString(LocalizedStringEnum.AllTransactions))
            {
                Transactions = new ObservableCollection<Transaction>(InputTransactions.Where(a => a.Year == SelectedYear
                   && a.Day == SelectedStartDay && a.Month == DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month));
                TotalIncome = Transactions.Where(a => a.Type == "Income").Sum(x => x.Count);
                TotalExpense = Transactions.Where(a => a.Type == "Expense").Sum(x => x.Count);
            }

            else
            {
                Transactions = new ObservableCollection<Transaction>(InputTransactions.Where(a => a.Year == SelectedYear
                    && a.Day == SelectedStartDay && a.Month == DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month && (a.Accounttype == SelectedCategory.Id || a.Categoryid == SelectedCategory.Id)));

                if (SelectedCategory.Type == "Expense")
                {
                    List<Category> Subcategories = TempCategories.Where(x => x.AccountId == SelectedCategory.Id).ToList();

                    foreach (var sub in Subcategories)
                    {
                        foreach (var transaction in TempTransactions)
                        {
                            if (transaction.Categoryid == sub.Id && transaction.Year == SelectedYear && transaction.Day == SelectedStartDay &&
                                transaction.Month == DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month)
                            {
                                Transactions.Add(transaction);
                            }
                        }
                    }
                }

                TotalIncome = Transactions.Where(a => a.Type == "Income").Sum(x => x.Count);
                TotalExpense = Transactions.Where(a => a.Type == "Expense").Sum(x => x.Count);
            }


        }

        private void InfoByMonth()
        {
            TotalExpense = 0;
            TotalIncome = 0;

            InputtingData();

            if (SelectedCategory.Name == LocalizedStrings.GetString(LocalizedStringEnum.AllTransactions))
            {
                Transactions = new ObservableCollection<Transaction>(InputTransactions.Where(a => a.Year == SelectedYear
                    && a.Month == DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month));

                TotalIncome = Transactions.Where(a => a.Type == "Income").Sum(x => x.Count);
                TotalExpense = Transactions.Where(a => a.Type == "Expense").Sum(x => x.Count);
            }

            else
            {
                Transactions = new ObservableCollection<Transaction>(InputTransactions.Where(a => a.Year == SelectedYear
                    && a.Month == DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month && (a.Accounttype == SelectedCategory.Id || a.Categoryid == SelectedCategory.Id)));

                if (SelectedCategory.Type == "Expense")
                {
                    List<Category> Subcategories = TempCategories.Where(x => x.AccountId == SelectedCategory.Id).ToList();

                    foreach (var sub in Subcategories)
                    {
                        foreach (var transaction in TempTransactions)
                        {
                            if (transaction.Categoryid == sub.Id && transaction.Year == SelectedYear &&
                                transaction.Month == DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month)
                            {
                                Transactions.Add(transaction);
                            }
                        }
                    }
                }
                TotalIncome = Transactions.Where(a => a.Type == "Income").Sum(x => x.Count);
                TotalExpense = Transactions.Where(a => a.Type == "Expense").Sum(x => x.Count);
            }

        }

        private void InfoByWeek()
        {
            TotalExpense = 0;
            TotalIncome = 0;
            InputtingData();

            int firstmonthnumber = DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month;
            int daysinmonth = System.DateTime.DaysInMonth((int)SelectedYear, firstmonthnumber);
            long tempday = SelectedStartDay;
            int tempmonthnumber = firstmonthnumber;
            long tempyear = SelectedYear;


            List<Category> Subcategories = TempCategories.Where(x => x.AccountId == SelectedCategory.Id).ToList();
            ObservableCollection<Transaction> ChosenTransaction = new ObservableCollection<Transaction>();

            foreach (var searchingtransaction in InputTransactions)
            {
                for (int i = 0; i <= 7; i++)
                {
                    if (tempday < daysinmonth)
                    {
                        if (SelectedCategory.Id == 0)
                        {
                            if (searchingtransaction.Year == tempyear && searchingtransaction.Day == tempday
                                && searchingtransaction.Month == tempmonthnumber)
                            {
                                ChosenTransaction.Add(searchingtransaction);
                            }
                        }
                        else
                        {
                            if (searchingtransaction.Year == tempyear && searchingtransaction.Day == tempday
                                && searchingtransaction.Month == tempmonthnumber)
                            {
                                if (searchingtransaction.Accounttype == SelectedCategory.Id || searchingtransaction.Categoryid == SelectedCategory.Id)
                                {
                                    ChosenTransaction.Add(searchingtransaction);
                                }
                                if (SelectedCategory.Type == "Expense")
                                {

                                    foreach (var sub in Subcategories)
                                    {
                                        if (sub.Id == searchingtransaction.Categoryid)
                                        {
                                            ChosenTransaction.Add(searchingtransaction);
                                        }
                                    }
                                }
                            }
                        }

                        tempday++;
                    }

                    else
                    {
                        if (searchingtransaction.Year == tempyear && searchingtransaction.Day + i == tempday &&
                            searchingtransaction.Month == tempmonthnumber)
                        {
                            if (SelectedCategory.Id == 0)
                            {
                                if (searchingtransaction.Year == tempyear && searchingtransaction.Day == tempday
                                    && searchingtransaction.Month == tempmonthnumber)
                                {
                                    ChosenTransaction.Add(searchingtransaction);

                                }
                            }
                            else
                            {
                                if (searchingtransaction.Year == tempyear && searchingtransaction.Day == tempday
                                && searchingtransaction.Month == tempmonthnumber)
                                {
                                    if (searchingtransaction.Accounttype == SelectedCategory.Id || searchingtransaction.Categoryid == SelectedCategory.Id)
                                    {
                                        ChosenTransaction.Add(searchingtransaction);
                                    }
                                    if (SelectedCategory.Type == "Expense")
                                    {

                                        foreach (var sub in Subcategories)
                                        {
                                            if (sub.Id == searchingtransaction.Categoryid)
                                            {
                                                ChosenTransaction.Add(searchingtransaction);
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        tempday = 1;

                        if (tempmonthnumber == 12)
                        {
                            tempyear++;
                            tempmonthnumber = 1;
                        }
                        else
                        {
                            tempmonthnumber++;
                        }

                    }
                }
                tempday = SelectedStartDay;
            }

            Transactions = ChosenTransaction;
            TotalIncome = Transactions.Where(a => a.Type == "Income").Sum(x => x.Count);
            TotalExpense = Transactions.Where(a => a.Type == "Expense").Sum(x => x.Count);
        }

        private void InfoByPeriod()
        {

            TotalExpense = 0;
            TotalIncome = 0;
            InputtingData();
            long temp = 0;
            int firstmonthnumber = DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month;
            int secondmonthnumber = DateTime.ParseExact(SelectedEndMonth, "MMMM", cultureInfo).Month;
            List<Category> Subcategories = TempCategories.Where(x => x.AccountId == SelectedCategory.Id).ToList();

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
            ObservableCollection<Transaction> ChosenTransaction = new ObservableCollection<Transaction>();
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

                        foreach (var searchingtransaction in InputTransactions)
                        {
                            if (SelectedCategory.Id == 0)
                            {
                                if (searchingtransaction.Year == tempyear && searchingtransaction.Day == i
                                    && searchingtransaction.Month == tempmonthnumber)
                                {
                                    ChosenTransaction.Add(searchingtransaction);
                                }
                            }
                            else
                            {
                                if (searchingtransaction.Year == tempyear && searchingtransaction.Day == i
                                       && searchingtransaction.Month == tempmonthnumber)
                                {
                                    if (searchingtransaction.Accounttype == SelectedCategory.Id || searchingtransaction.Categoryid == SelectedCategory.Id)
                                    {
                                        ChosenTransaction.Add(searchingtransaction);
                                    }

                                    if (SelectedCategory.Type == "Expense")
                                    {
                                        foreach (var sub in Subcategories)
                                        {
                                            if (sub.Id == searchingtransaction.Categoryid)
                                            {
                                                ChosenTransaction.Add(searchingtransaction);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 1; i <= daysinmonth; i++)
                    {
                        foreach (var searchingtransaction in InputTransactions)
                        {
                            if (SelectedCategory.Id == 0)
                            {
                                if (searchingtransaction.Year == tempyear && searchingtransaction.Day == i
                                    && searchingtransaction.Month == tempmonthnumber)
                                {
                                    ChosenTransaction.Add(searchingtransaction);
                                }
                            }
                            else
                            {
                                if (searchingtransaction.Year == tempyear && searchingtransaction.Day == i
                                       && searchingtransaction.Month == tempmonthnumber)
                                {
                                    if (searchingtransaction.Accounttype == SelectedCategory.Id || searchingtransaction.Categoryid == SelectedCategory.Id)
                                    {
                                        ChosenTransaction.Add(searchingtransaction);
                                    }
                                    if (SelectedCategory.Type == "Expense")
                                    {

                                        foreach (var sub in Subcategories)
                                        {
                                            if (sub.Id == searchingtransaction.Categoryid)
                                            {
                                                ChosenTransaction.Add(searchingtransaction);
                                            }
                                        }
                                    }
                                }
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
                if (transaction.Year == SelectedEndYear && transaction.Day == SelectedEndDay
                                           && transaction.Month == DateTime.ParseExact(SelectedEndMonth, "MMMM", cultureInfo).Month)
                {
                    ChosenTransaction.Add(transaction);
                }
            }

            Transactions = ChosenTransaction;
            TotalIncome = Transactions.Where(a => a.Type == "Income").Sum(x => x.Count);
            TotalExpense = Transactions.Where(a => a.Type == "Expense").Sum(x => x.Count);

            SelectedStartDay = ThisDate.Day;
            SelectedMonth = cultureInfo.DateTimeFormat.GetMonthName(ThisDate.Month);
            SelectedYear = ThisDate.Year;

            SelectedEndDay = SecondDate.Day;
            SelectedEndMonth = cultureInfo.DateTimeFormat.GetMonthName(SecondDate.Month);
            SelectedEndYear = SecondDate.Year;

        }

        private void InputtingData()
        {
            Unchosengridview = false;
            Chosengridview = true;
            TotalExpense = 0;
            TotalIncome = 0;

            if (SelectedUser.Id != 0)
            {
                InputTransactions = budgetManagerRepository.GetTransactionsByUserId(SelectedUser.Id);
            }
            else
            {
                InputTransactions = budgetManagerRepository.GetTransactions();
            }

            foreach (var searchingtransaction in InputTransactions)
            {
                searchingtransaction.MonthNumber = searchingtransaction.Month;
                Category category = budgetManagerRepository.GetCategory(searchingtransaction.Accounttype);
                searchingtransaction.AccountName = category.Name;
                Category categoryforicon = budgetManagerRepository.GetCategory(searchingtransaction.Categoryid);

                if (searchingtransaction.Type == "Sending")
                {
                    searchingtransaction.CountIncome = "$" + Convert.ToString(searchingtransaction.Count);
                    searchingtransaction.CountExpense = "$" + Convert.ToString(searchingtransaction.Count);
                    searchingtransaction.TextColor = "Gray";
                    searchingtransaction.IconName = "/Assets/trans.png";
                }
                else if (searchingtransaction.Type == "Income")
                {
                    searchingtransaction.TextColor = "Green";
                    searchingtransaction.IconName = categoryforicon.Icon;
                    searchingtransaction.CountIncome = "$" + Convert.ToString(searchingtransaction.Count);
                }
                else
                {
                    searchingtransaction.CountExpense = "$" + Convert.ToString(searchingtransaction.Count);
                    searchingtransaction.TextColor = "Red";
                    searchingtransaction.IconName = categoryforicon.Icon;
                }
            }

        }

        private void DeleteTransaction()
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
                    if (SelectedTransaction.Accounttype == category.Id || SelectedTransaction.Categoryid == category.Id)
                    {
                        OperationWithTransaction op;

                        if (SelectedTransaction.Type == "Income")
                        {
                            op = new OperationWithTransaction(Sub);
                            category.Budgeted = op(category.Budgeted, SelectedTransaction.Count);
                            this.TotalIncome -= SelectedTransaction.Count;
                        }

                        else if (SelectedTransaction.Type == "Expense")
                        {
                            if (SelectedTransaction.Categoryid == category.Id)
                            {
                                op = new OperationWithTransaction(Sub);
                                category.Remaining = op(category.Remaining, SelectedTransaction.Count);
                                
                                if (category.Type == "Subcategory")
                                {
                                    foreach (var item in allCategories)
                                    {
                                        if (item.Id == category.AccountId)
                                        {
                                            item.Remaining = op(item.Remaining, SelectedTransaction.Count);
                                            budgetManagerRepository.UpdateCategory(item);
                                        }
                                    }
                                }

                                this.TotalExpense -= SelectedTransaction.Count;
                            }

                            if (SelectedTransaction.Accounttype == category.Id)
                            {
                                op = new OperationWithTransaction(Sum);
                                category.Budgeted = op(category.Budgeted, SelectedTransaction.Count);
                            }
                        }
                        else
                        {
                            if (SelectedTransaction.Categoryid == category.Id)
                            {
                                op = new OperationWithTransaction(Sub);
                                category.Budgeted = op(category.Budgeted, SelectedTransaction.Count);
                            }
                            if (SelectedTransaction.Accounttype == category.Id)
                            {
                                op = new OperationWithTransaction(Sum);
                                category.Budgeted = op(category.Budgeted, SelectedTransaction.Count);
                            }
                        }

                        budgetManagerRepository.UpdateCategory(category);
                    }
                }

                budgetManagerRepository.DeleteTransaction(selectedtransaction.Id);
                Transactions.Remove(Transactions.FirstOrDefault(transacion => transacion.Id == selectedtransaction.Id));
                var transactionsByCategories = Transactions.GroupBy(x => x.Type).Select(x => new TransactionGroups { Title = x.Key, Items = x.ToList() });
                Items = transactionsByCategories.ToList();
            }
            Unchosenappbar = false;
        }

        private void Sorting()
        {
            Unchosengridview = false;
            Chosengridview = true;

            Transactions = new ObservableCollection<Transaction>(Transactions.OrderBy(transaction => transaction.Day));
            Transactions = new ObservableCollection<Transaction>(Transactions.OrderBy(transaction => transaction.MonthNumber));
            Transactions = new ObservableCollection<Transaction>(Transactions.OrderBy(transaction => transaction.Year));

        }

        private void GroupMethod()
        {
            Unchosengridview = true;
            Chosengridview = false;

            var transactionsByCategories = Transactions.GroupBy(x => x.Type).Select(x => new TransactionGroups { Title = x.Key, Items = x.ToList() });
            Items = transactionsByCategories.ToList();
        }

        private void InputCategories()
        {

            foreach (var searchingcategory in TempCategories)
            {
                if (searchingcategory.Name != LocalizedStrings.GetString(LocalizedStringEnum.AllTransactions) && searchingcategory.Id != SelectedCategory.Id)
                {
                    Categories.Remove(Categories.FirstOrDefault(category => category.Id == searchingcategory.Id));
                }
            }
        }

        private void PaintTransaction()
        {

            foreach (var searchingtransaction in Transactions)
            {
                searchingtransaction.MonthNumber = searchingtransaction.Month;
                Category category = budgetManagerRepository.GetCategory(searchingtransaction.Accounttype);
                searchingtransaction.AccountName = category.Name;
                Category categoryforicon = budgetManagerRepository.GetCategory(searchingtransaction.Categoryid);

                if (searchingtransaction.Type == "Sending")
                {
                    searchingtransaction.CountIncome = "$" + Convert.ToString(searchingtransaction.Count);
                    searchingtransaction.CountExpense = "$" + Convert.ToString(searchingtransaction.Count);
                    searchingtransaction.TextColor = "Gray";
                    searchingtransaction.IconName = "/Assets/trans.png";
                }
                else if (searchingtransaction.Type == "Income")
                {
                    searchingtransaction.TextColor = "Green";
                    searchingtransaction.IconName = categoryforicon.Icon;
                    searchingtransaction.CountIncome = "$" + Convert.ToString(searchingtransaction.Count);
                }
                else
                {
                    searchingtransaction.CountExpense = "$" + Convert.ToString(searchingtransaction.Count);
                    searchingtransaction.TextColor = "Red";
                    searchingtransaction.IconName = categoryforicon.Icon;
                }
            }
        }

        private void GetSubCategories()
        {

            List<Category> Subcategories = TempCategories.Where(x => x.AccountId == SelectedCategory.Id).ToList();
            foreach (var sub in Subcategories)
            {
                foreach (var transaction in TempTransactions)
                {
                    if (transaction.Categoryid == sub.Id)
                    {
                        Transactions.Add(transaction);
                    }
                }
            }
        }

        private void BackExecute()
        {
            switch (PreviousPage)
            { 
                case "Main":
                    navigation.NavigateTo("MainPage");
                    break;
                case "Subcategory":
                    navigation.NavigateTo("AllSubcategories");
                    break;
            }

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

        private void ChangeUser()
        {
            Unchosengridview = false;
            Chosengridview = true;

            if (SelectedCategory.Name != LocalizedStrings.GetString(LocalizedStringEnum.AllTransactions))
            {
                SelectedCategory = Allcategories;
            }

            TotalIncome = 0;
            TotalExpense = 0;

            foreach (var searchingcategory in TempCategories)
            {
                if (searchingcategory.Name != LocalizedStrings.GetString(LocalizedStringEnum.AllTransactions))
                {
                    Categories.Remove(Categories.FirstOrDefault(category => category.Id == searchingcategory.Id));
                }
            }

            Transactions = new ObservableCollection<Transaction>();
            foreach (var category in TempCategories)
            {
                if (category.Userid == SelectedUser.Id && category.Month == DateTime.ParseExact(SelectedEndMonth, "MMMM", cultureInfo).Month 
                    && category.Year == SelectedEndYear)
                {
                    if (category.Name != LocalizedStrings.GetString(LocalizedStringEnum.AllTransactions))
                    {

                        Categories.Add(category);

                    }
                }
            }

            ChooseRightPeriodByUserSwitch();
            PaintTransaction();
            InputTransactions = Transactions;
        }

        private void ChangeCategory()
        {
            TotalIncome = 0;
            TotalExpense = 0;
            ChooseRightPeriodByUserSwitch();
            InputTransactions = Transactions;
            PaintTransaction();
        }

        private void SelectTransaction()
        {
            Chosenappbar = false;
            Unchosenappbar = true;
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

        private void GoToAddTransactionPage()
        {
            Category NewCategory = new Category();
            var data = NewCategory;
            navigation.NavigateTo("AddTransaction", data);
        }

        private void GoToEditTransactionPage()
        {
            var data = SelectedTransaction;
            navigation.NavigateTo("EditTransaction", data);
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

        public override void NavigateTo(object parameter)
        {
            this.Title = LocalizedStrings.GetString(LocalizedStringEnum.transactionTitle);
            this.secondDate = thisDate = currentTime;
            this.selectedEndMonth = this.selectedMonth = cultureInfo.DateTimeFormat.GetMonthName(ThisDate.Month);
            this.selectedEndYear = this.selectedYear = ThisDate.Year;
            this.selectedEndDay = this.selectedStartDay = ThisDate.Day;
            this.unchosenappbar = false;
            this.chosenappbar = true;
            this.chosengridview = true;
            this.unchosengridview = false;
            allUsers = new User() { Name = LocalizedStrings.GetString(LocalizedStringEnum.AllUsers), Icon = "/Assets/icon.png" };
            allcategories = new Category() { Name = LocalizedStrings.GetString(LocalizedStringEnum.AllTransactions), Icon = "/Assets/trans.png" };
            this.selectedTransactions = new ObservableCollection<object>();
            this.users = budgetManagerRepository.GetUsers();
            this.temptransactions = budgetManagerRepository.GetTransactions();
            this.tempcategories = budgetManagerRepository.GetCategories();
            this.categories = new ObservableCollection<Category>();
            this.transactions = new ObservableCollection<Transaction>();
            users.Insert(0, allUsers);
            categories.Add(allcategories);
            this.Height = Window.Current.Bounds.Height - 400;
            this.Width = Window.Current.Bounds.Width - 40;
            this.HeightForList = Window.Current.Bounds.Height - 540;

            Category data = parameter as Category;

            if (data != null)
            {
                SelectedCategory = data;
            }
            else
            {
                SelectedCategory = Allcategories;

            }
            if (SelectedCategory.Id != 0)
            {
                this.PreviousPage = "Subcategory";
                SelectedUser = Users.FirstOrDefault(chosenUser => chosenUser.Id == SelectedCategory.Userid);

                foreach (var category in TempCategories)
                {
                    if (category.Userid == SelectedUser.Id && category.Month == System.DateTime.Now.Month &&
                        category.Year == currentTime.Year)
                    {
                        categories.Add(category);
                    }
                }               

                Transactions = new ObservableCollection<Transaction>(TempTransactions.Where(x => x.Userid == SelectedUser.Id &&
                (x.Categoryid == SelectedCategory.Id || x.Accounttype == SelectedCategory.Id) &&
                x.Month == System.DateTime.Now.Month &&
                x.Year == currentTime.Year));
                GetSubCategories();
                TotalExpense = Transactions.Where(a => a.Type == "Expense").Sum(x => x.Count);
                TotalIncome = Transactions.Where(a => a.Type == "Income").Sum(x => x.Count);

                Category checkForExist = Categories.FirstOrDefault(x => x.Id == SelectedCategory.Id);

                if (checkForExist == null)
                {
                    Categories.Insert(1, SelectedCategory);
                }

                SelectedCategory = Categories.FirstOrDefault(chosencategory => chosencategory.Id == SelectedCategory.Id);
            }

            else
            {
                this.PreviousPage = "Main";
                if (Users.Count > 1)
                {
                    SelectedUser = Users.FirstOrDefault(chosenUser => chosenUser.IsSelected == "Main");
                }
                else 
                {
                    SelectedUser = AllUsers;
                }
                foreach (var category in TempCategories)
                {
                    if (category.Userid == SelectedUser.Id && category.Month == System.DateTime.Now.Month
                        && category.Year == currentTime.Year)
                    {
                        categories.Add(category);
                    }
                }

                Transactions = new ObservableCollection<Transaction>(TempTransactions.Where(x => x.Month == System.DateTime.Now.Month &&
                    x.Year == currentTime.Year && x.Userid == SelectedUser.Id));
                TotalExpense = Transactions.Where(a => a.Type == "Expense").Sum(x => x.Count);
                TotalIncome = Transactions.Where(a => a.Type == "Income").Sum(x => x.Count);
            }

            PaintTransaction();
            inputTransactions = Transactions;
            this.ChosenDateType = "Month";
            this.SecondDateVisibility = false;
        }

        #endregion
    }
}
