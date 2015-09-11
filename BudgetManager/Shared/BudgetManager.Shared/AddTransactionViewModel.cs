namespace BudgetManager.Shared
{
    using System;
    using System.Globalization;
    using BudgetManager.Models;
    using BudgetManager.DAL;
    using System.Windows.Input;
    using System.Collections.ObjectModel;
    using BudgetManager.Core;
    using System.Linq;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Views;
    using System.Threading.Tasks;
    using Windows.UI.Popups;

    public class AddTransactionViewModel : ViewModelBase
    {

        #region Fields

        private ObservableCollection<User> users;
        private ObservableCollection<Day> days;
        private ObservableCollection<Month> monthes;
        private ObservableCollection<Year> years;
        private ObservableCollection<Category> categories;
        private Category selectedCategory;
        private Category selectedMoneyAccount;
        private string newName;
        private Day newDay;
        private Month newMonth;
        private Year newYear;
        private User newUser;
        private string newCount;
        private long categoryId;
        private bool enable;
        private string rest;
        private string curMonth;
        private string typeOfTransaction;
        private bool visibilityOfIncomeFields;
        private bool visibilityOfExpenseFields;
        private bool visibilityOfSendingFields;
        private bool enableIncome;
        private bool enableExpense;
        private bool enableSending;
        private string title;

        #endregion

        #region Constructor

        public AddTransactionViewModel(INavigationService navigatedService)
        {
            this.navigation = navigatedService;
            this.monthes = new ObservableCollection<Month>();
            this.years = new ObservableCollection<Year>();
            this.curMonth = cultureInfo.DateTimeFormat.GetMonthName(System.DateTime.Now.Month);
            GetDatesColletions();
            SelectionChangedYear = new RelayCommand(ChangeDaysInMonthExecute);
            SelectionRest = new RelayCommand(SelectRestExecute);
            TextChange = new RelayCommand(TextChangeExecute);
            IncomeClick = new RelayCommand(ChooseIncome);
            ExpenseClick = new RelayCommand(ChooseExpense);
            SendingClick = new RelayCommand(ChooseSending);
            AddTransactionClick = new RelayCommand(AddExecute);
            Back = new RelayCommand(() => navigation.GoBack());
        }

        #endregion

        #region Basic Properties

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

        public ObservableCollection<Day> Days
        {
            get { return this.days; }
            set
            {
                if (value != this.days)
                {
                    this.days = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Month> Monthes
        {
            get { return this.monthes; }
            set
            {
                if (value != this.monthes)
                {
                    this.monthes = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Year> Years
        {
            get { return this.years; }
            set
            {
                if (value != this.years)
                {
                    this.years = value;
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

        public Category SelectedMoneyAccount
        {
            get { return this.selectedMoneyAccount; }
            set
            {
                if (value != this.selectedMoneyAccount)
                {
                    this.selectedMoneyAccount = value;
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

        public Day NewDay
        {
            get { return this.newDay; }
            set
            {
                if (value != this.newDay)
                {
                    this.newDay = value; 
                    OnPropertyChanged();
                }
            }
        }

        public Month NewMonth
        {
            get { return this.newMonth; }
            set
            {
                if (value != this.newMonth)
                {
                    this.newMonth = value; 
                    OnPropertyChanged();
                }
            }
        }

        public Year NewYear
        {
            get { return this.newYear; }
            set
            {
                if (value != this.newYear)
                {
                    this.newYear = value;
                    OnPropertyChanged();
                }
            }
        }

        public User NewUser
        {
            get { return this.newUser; }
            set
            {
                if (value != this.newUser)
                {
                    this.newUser = value; 
                    OnPropertyChanged();
                }
            }
        }

        public string NewCount
        {
            get { return this.newCount; }
            set
            {
                if (value != this.newCount)
                {
                    this.newCount = value;
                    OnPropertyChanged();
                }
            }
        }

        public long CategoryId
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

        public string Rest
        {
            get { return this.rest; }
            set
            {
                if (value != this.rest)
                {
                    this.rest = value; 
                    OnPropertyChanged();
                }
            }
        }

        public string TypeOfTransaction
        {
            get { return this.typeOfTransaction; }
            set
            {
                if (value != this.typeOfTransaction)
                {
                    this.typeOfTransaction = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool VisibilityOfIncomeFields
        {
            get { return this.visibilityOfIncomeFields; }
            set
            {
                if (value != this.visibilityOfIncomeFields)
                {
                    this.visibilityOfIncomeFields = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool VisibilityOfExpenseFields
        {
            get { return this.visibilityOfExpenseFields; }
            set
            {
                if (value != this.visibilityOfExpenseFields)
                {
                    this.visibilityOfExpenseFields = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool VisibilityOfSendingFields
        {
            get { return this.visibilityOfSendingFields; }
            set
            {
                if (value != this.visibilityOfSendingFields)
                {
                    this.visibilityOfSendingFields = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool EnableIncome
        {
            get { return this.enableIncome; }
            set
            {
                if (value != this.enableIncome)
                {
                    this.enableIncome = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool EnableExpense
        {
            get { return this.enableExpense; }
            set
            {
                if (value != this.enableExpense)
                {
                    this.enableExpense = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool EnableSending
        {
            get { return this.enableSending; }
            set
            {
                if (value != this.enableSending)
                {
                    this.enableSending = value;
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
        public RelayCommand SelectionChangedYear { get; set; }
        public RelayCommand SelectionRest { get; set; }
        public RelayCommand TextChange { get; set; }
        public RelayCommand IncomeClick { get; set; }
        public RelayCommand ExpenseClick { get; set; }
        public RelayCommand SendingClick { get; set; }
        public RelayCommand AddTransactionClick { get; set; }

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

        private void AddTransaction(string transType)
        {

            if (!string.IsNullOrWhiteSpace(NewName)
                && SelectedCategory != null
                && SelectedMoneyAccount != null
                && !string.IsNullOrWhiteSpace(NewCount)
                && Convert.ToInt32(NewCount) != 0
                && NewUser != null
                && NewDay != null
                && NewYear != null
                && NewMonth != null)
            {
                Transaction trans = new Transaction
                {
                    Name = newName,
                    Count = Convert.ToInt32(NewCount),
                    Userid = NewUser.Id,
                    Type = transType,
                    Day = NewDay.Text,
                    Month = DateTime.ParseExact(NewMonth.Text, "MMMM", cultureInfo).Month,
                    Year = NewYear.Text,
                    Categoryid = SelectedCategory.Id,
                    Accounttype = SelectedMoneyAccount.Id
                };
                budgetManagerRepository.InsertTransaction(trans);

                if (SelectedCategory.Type == "Subcategory")
                {
                    foreach (var category in Categories)
                    {
                        if (category.Id == SelectedCategory.AccountId)
                        {
                            category.Remaining += Convert.ToInt32(NewCount);
                            budgetManagerRepository.UpdateCategory(category);
                        }
                    }
                }

                OperationWithTransaction op;

                foreach (var category in Categories)
                {


                    if (category.Id == SelectedCategory.Id)
                    {

                        op = new OperationWithTransaction(Sum);

                        if (transType == "Expense")
                        {
                            category.Remaining = op(category.Remaining, Convert.ToInt32(NewCount));
                        }
                        else
                        {
                            category.Budgeted = op(category.Budgeted, Convert.ToInt32(NewCount));
                        }

                        budgetManagerRepository.UpdateCategory(category);

                    }
                }

                foreach (var accountCategory in Categories)
                {
                    if (accountCategory.Id == SelectedMoneyAccount.Id)
                    {
                        if (transType == "Income")
                        {
                            op = new OperationWithTransaction(Sum);
                        }
                        else
                        {
                            op = new OperationWithTransaction(Sub);
                        }

                        accountCategory.Budgeted = op(accountCategory.Budgeted, Convert.ToInt32(NewCount));
                        budgetManagerRepository.UpdateCategory(accountCategory);

                    }
                }

                navigation.GoBack();

            }
            else 
            {
                ShowWarningMessage();
            }

        }

        private void GetDatesColletions()
        {

            int monthcount = 12;

            for (int i = 1; i <= monthcount; i++)
            {

                Month m = new Month() { Text = cultureInfo.DateTimeFormat.MonthNames[i - 1], Dayscount = Days, MonthNumber = i };

                monthes.Add(m);
                if (i <= 5)
                {
                    Year y = new Year() { Text = currentTime.Year - 1 + i };
                    years.Add(y);
                }

            }

            foreach (var month in Monthes)
            {
                days = new ObservableCollection<Day>();
                long daysinmonth = System.DateTime.DaysInMonth(currentTime.Year, DateTime.ParseExact(month.Text, "MMMM", cultureInfo).Month);

                for (int j = 1; j <= daysinmonth; j++)
                {
                    Day d = new Day() { Text = j };
                    days.Add(d);
                }
                month.Dayscount = days;


            }


        }

        private void ChangeDaysInMonthExecute()
        {
            foreach (var month in Monthes)
            {

                if (month.MonthNumber == 2)
                {
                    days = new ObservableCollection<Day>();
                    long daysinmonth = System.DateTime.DaysInMonth((int)NewYear.Text, DateTime.ParseExact(month.Text, "MMMM", cultureInfo).Month);
                    for (int j = 1; j <= daysinmonth; j++)
                    {
                        Day d = new Day() { Text = j };
                        days.Add(d);
                    }
                    month.Dayscount = days;
                }

            }
        }

        private void SelectRestExecute()
        {
            if (SelectedMoneyAccount != null)
            {
                Rest = SelectedMoneyAccount.Budgeted.ToString();
            }
            else
            {
                Rest = string.Empty;
            }
        }

        private void TextChangeExecute() 
        {
            if (!string.IsNullOrWhiteSpace(NewCount) && SelectedMoneyAccount!=null)
            {
                Rest = (SelectedMoneyAccount.Budgeted - Convert.ToInt32(NewCount)).ToString();
            }
        }

        private void ChooseIncome()
        {
            TypeOfTransaction = "Income";
            VisibilityOfSendingFields = false;
            VisibilityOfExpenseFields = false;
            VisibilityOfIncomeFields = true;
            EnableExpense = true;
            EnableIncome = false;
            EnableSending = true;
        }

        private void ChooseExpense()
        {
            TypeOfTransaction = "Expense";
            VisibilityOfSendingFields = false;
            VisibilityOfExpenseFields = true;
            VisibilityOfIncomeFields = false;
            EnableExpense = false;
            EnableIncome = true;
            EnableSending = true;
        }

        private void ChooseSending()
        {
            TypeOfTransaction = "Sending";
            VisibilityOfSendingFields = true;
            VisibilityOfExpenseFields = false;
            VisibilityOfIncomeFields = false;
            EnableExpense = true;
            EnableIncome = true;
            EnableSending = false;
        }

        private void AddExecute()
        {
            AddTransaction(TypeOfTransaction);
        }

        private async Task ShowWarningMessage()
        {

            var dialog = new MessageDialog(LocalizedStrings.GetString(LocalizedStringEnum.WarningAddMessage),
                LocalizedStrings.GetString(LocalizedStringEnum.WarningAddTitle)
            );

            dialog.Commands.Add(new UICommand(LocalizedStrings.GetString(LocalizedStringEnum.Continue)));
            IUICommand iuiCommand = await dialog.ShowAsync();

        }

        public override void NavigateTo(object parameter)
        {
            this.Title = LocalizedStrings.GetString(LocalizedStringEnum.addTransactionTitle);
            this.users = budgetManagerRepository.GetUsers();
            this.categories = budgetManagerRepository.GetCategories();

            Category data = parameter as Category;
            if (data != null)
            {
                this.categoryId = data.Id;
            }

            this.NewName = string.Empty;
            this.NewCount = string.Empty;
            this.Rest = string.Empty;
            this.SelectedMoneyAccount = new Category();
            this.SelectedCategory = new Category();
            this.NewMonth = Monthes.FirstOrDefault(chosenMonth => chosenMonth.Text == cultureInfo.DateTimeFormat.GetMonthName(System.DateTime.Now.Month));
            this.NewYear = Years.FirstOrDefault(chosenYear => chosenYear.Text == currentTime.Year);
            this.NewDay = NewMonth.Dayscount.FirstOrDefault(chosenDay => chosenDay.Text == currentTime.Day);

            if (CategoryId != 0)
            {
                Category MainCategory = budgetManagerRepository.GetCategory(CategoryId);
                this.NewUser = Users.FirstOrDefault(a => a.Id == MainCategory.Userid);
                this.NewUser.Categoriesexpense = budgetManagerRepository.GetCategoryByType(NewUser.Id, "Subcategory",
                    DateTime.ParseExact(curMonth, "MMMM", cultureInfo).Month, currentTime.Year);
                this.NewUser.Categoriesexpense = new ObservableCollection<Category>(NewUser.Categoriesexpense.Where(a => a.AccountId == CategoryId));
                this.NewUser.Categoriesexpense.Insert(0, MainCategory);
                this.NewUser.Categoriesaccount = budgetManagerRepository.GetCategoryByType(NewUser.Id, "Account", DateTime.ParseExact(curMonth, "MMMM", cultureInfo).Month,
                    currentTime.Year);
                this.SelectedCategory = NewUser.Categoriesexpense.FirstOrDefault(x => x.Id == CategoryId);
                this.EnableExpense = false;
                this.EnableIncome = false;
                this.EnableSending = false;
            }
            else
            {
                NewUser = Users.FirstOrDefault(chosenUser => chosenUser.IsSelected == "Main");
                foreach (var user in Users)
                {
                    user.Categoriesaccount = budgetManagerRepository.GetCategoryByType(user.Id, "Account", DateTime.ParseExact(curMonth, "MMMM", cultureInfo).Month,
                        currentTime.Year);
                    user.Categoriesexpense = budgetManagerRepository.GetCategoryByType(user.Id, "Expense", DateTime.ParseExact(curMonth, "MMMM", cultureInfo).Month,
                        currentTime.Year);
                    user.Categoriesincome = budgetManagerRepository.GetCategoryByType(user.Id, "Income", DateTime.ParseExact(curMonth, "MMMM", cultureInfo).Month,
                        currentTime.Year);
                    this.EnableExpense = false;
                    this.EnableIncome = true;
                    this.EnableSending = true;
                }
            }
            ChooseExpense();


        }

        #endregion


    }
}
