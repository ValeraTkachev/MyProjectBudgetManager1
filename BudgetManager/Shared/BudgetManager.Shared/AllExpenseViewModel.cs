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
    using System.Collections.Generic;
    using GalaSoft.MvvmLight.Views;
    using GalaSoft.MvvmLight.Command;

    public class AllExpenseViewModel : ViewModelBase
    {

        #region fields

        private List<Category> categoriesexpense;
        private List<Category> categoriesincome;
        private List<Category> categoriesaccount;
        private ObservableCollection<User> users;
        private ObservableCollection<Category> categories;
        private Category selectedCategory;
        private DateTime thisDate;
        private DateTime secondDate;
        private User selectedUser;
        private long selectedStartDay;
        private string type;
        private string selectedMonth;
        private long selectedYear;
        private long selectedEndDay;
        private long selectedEndYear;
        private string selectedEndMonth;
        public double forPercent;
        private bool unchosenIncome;
        private bool unchosenExpense;
        private bool unchosen;
        private bool unchosenAccount;
        private bool incomeVisibility;
        private bool expenseVisibility;
        private bool accountVisibility;
        private bool secondDateVisibility;
        private string chosenDateType;
        private string title;
        private User allUsers;

        #endregion

        #region Constructor

        public AllExpenseViewModel(INavigationService navigatedService)
        {
            this.navigation = navigatedService;
            YearCommand = new RelayCommand(GetYearType);
            DayCommand = new RelayCommand(GetDayType);
            MonthCommand = new RelayCommand(GetMonthType);
            PeriodCommand = new RelayCommand(GetPeriodType);
            WeekCommand = new RelayCommand(GetWeekType);
            DeleteCommand = new RelayCommand(DeleteCategory);
            DateChangeCommand = new RelayCommand(ChangeDate);
            SecondDateChangeCommand = new RelayCommand(ChangeSecondDate);
            SelectionUserChange = new RelayCommand(ChangeUser);
            SelectItem = new RelayCommand(SelectCategory);
            EditIncomeCommand = new RelayCommand(OpenEditIncomeWindow);
            AddIncomeCommand = new RelayCommand(OpenAddIncomeWindow);
            EditExpenseCommand = new RelayCommand(OpenEditExpenseWindow);
            EditMoneyAccountCommand = new RelayCommand(OpenEditMoneyAccountWindow);
            AddMoneyAccountCommand = new RelayCommand(OpenAddMoneyAccountWindow);
            ShowLinearExpense = new RelayCommand(OpenLinearExpenseWindow);
            Back = new RelayCommand(BackExecute);
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

        public List<Category> CategoriesExpense
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

        public List<Category> CategoriesIncome
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

        public List<Category> CategoriesAccount
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

        public string Type
        {
            get { return this.type; }
            set
            {
                if (value != this.type)
                {
                    this.type = value;
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

        public double ForPercent
        {
            get { return this.forPercent; }
            set
            {
                this.forPercent = value;
                OnPropertyChanged();
            }
        }

        public bool UnchosenIncome
        {
            get { return this.unchosenIncome; }
            set
            {
                if (value != this.unchosenIncome)
                {
                    this.unchosenIncome = value;
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

        public bool UnchosenExpense
        {
            get { return this.unchosenExpense; }
            set
            {
                if (value != this.unchosenExpense)
                {
                    this.unchosenExpense = value;
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

        public bool UnchosenAccount
        {
            get { return this.unchosenAccount; }
            set
            {
                if (value != this.unchosenAccount)
                {
                    this.unchosenAccount = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IncomeVisibility
        {
            get { return this.incomeVisibility; }
            set
            {
                if (value != this.incomeVisibility)
                {
                    this.incomeVisibility = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool ExpenseVisibility
        {
            get { return this.expenseVisibility; }
            set
            {
                if (value != this.expenseVisibility)
                {
                    this.expenseVisibility = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool AccountVisibility
        {
            get { return this.accountVisibility; }
            set
            {
                if (value != this.accountVisibility)
                {
                    this.accountVisibility = value;
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

        #region commands

        public RelayCommand DayCommand { get; set; }
        public RelayCommand WeekCommand { get; set; }
        public RelayCommand MonthCommand { get; set; }
        public RelayCommand YearCommand { get; set; }
        public RelayCommand PeriodCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand Back { get; set; }
        public RelayCommand DateChangeCommand { get; set; }
        public RelayCommand SecondDateChangeCommand { get; set; }
        public RelayCommand SelectionUserChange { get; set; }
        public RelayCommand SelectItem { get; set; }
        public RelayCommand EditIncomeCommand { get; set; }
        public RelayCommand AddIncomeCommand { get; set; }
        public RelayCommand EditExpenseCommand { get; set; }
        public RelayCommand EditMoneyAccountCommand { get; set; }
        public RelayCommand AddMoneyAccountCommand { get; set; }
        public RelayCommand ShowLinearExpense { get; set; }

        #endregion

        #region methods

        private void InfoByYear()
        {
            InputtingData();

            SelectedUser.Categoriesexpense = new ObservableCollection<Category>(CategoriesExpense.Where(a => a.Year == SelectedYear));
            SelectedUser.Categoriesincome = new ObservableCollection<Category>(CategoriesIncome.Where(a => a.Year == SelectedYear));
            SelectedUser.Categoriesaccount = new ObservableCollection<Category>(CategoriesAccount.Where(a => a.Year == SelectedYear));
            CountBudget();
        }

        private void InfoByDay()
        {
            InputtingData();

            SelectedUser.Categoriesexpense = new ObservableCollection<Category>(CategoriesExpense.Where(a => a.Month == DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month && a.Year == SelectedYear
                && a.Day == SelectedStartDay));
            SelectedUser.Categoriesincome = new ObservableCollection<Category>(CategoriesIncome.Where(a => a.Month == DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month && a.Year == SelectedYear
                && a.Day == SelectedStartDay));
            SelectedUser.Categoriesaccount = new ObservableCollection<Category>(CategoriesAccount.Where(a => a.Month == DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month && a.Year == SelectedYear
                && a.Day == SelectedStartDay));
            CountBudget();
        }

        private void InfoByMonth()
        {
            InputtingData();

            SelectedUser.Categoriesexpense = new ObservableCollection<Category>(CategoriesExpense.Where(a => a.Month == DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month && a.Year == SelectedYear));
            SelectedUser.Categoriesincome = new ObservableCollection<Category>(CategoriesIncome.Where(a => a.Month == DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month && a.Year == SelectedYear));
            SelectedUser.Categoriesaccount = new ObservableCollection<Category>(CategoriesAccount.Where(a => a.Month == DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month && a.Year == SelectedYear));
            CountBudget();
        }

        private void InfoByWeek()
        {
            SelectedUser.Categoriesaccount.Clear();
            SelectedUser.Categoriesexpense.Clear();
            SelectedUser.Categoriesincome.Clear();

            int firstmonthnumber = DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month;
            int daysinmonth = System.DateTime.DaysInMonth((int)SelectedYear, firstmonthnumber);
            long tempday = SelectedStartDay;
            int tempmonthnumber = firstmonthnumber;
            long tempyear = SelectedYear;

            ObservableCollection<Category> Allcategories = budgetManagerRepository.GetCategoriesByUserId(SelectedUser.Id);

            foreach (var searchingcategory in Allcategories)
            {
                if (searchingcategory.Type == "Expense")
                {
                    searchingcategory.Percent = 1 - (searchingcategory.Remaining * 100 / searchingcategory.Budgeted) * 0.01;
                }
                    searchingcategory.Rest = searchingcategory.Budgeted - searchingcategory.Remaining;
                    if (searchingcategory.Budgeted >= searchingcategory.Remaining)
                    {
                        searchingcategory.StrokeColor = "White";
                        if (searchingcategory.Budgeted == searchingcategory.Remaining)
                        {
                            searchingcategory.TextColor = "Gray";
                        }
                        else
                        {
                            searchingcategory.TextColor = "Green";
                        }
                    }
                    else
                    {
                        searchingcategory.TextColor = searchingcategory.StrokeColor = "Red";

                    }
                
            }

            for (int i = 0; i <= 7; i++)
            {
                if (tempday <= daysinmonth)
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

            CountBudget();

        }

        private void InfoByPeriod()
        {
            SelectedUser.Categoriesaccount.Clear();
            SelectedUser.Categoriesexpense.Clear();
            SelectedUser.Categoriesincome.Clear();
            ObservableCollection<Category> Allcategories = budgetManagerRepository.GetCategoriesByUserId(SelectedUser.Id);

            foreach (var searchingcategory in Allcategories)
            {
                if (searchingcategory.Type == "Expense")
                {
                    searchingcategory.Percent = 1 - (searchingcategory.Remaining * 100 / searchingcategory.Budgeted) * 0.01;

                    searchingcategory.Rest = searchingcategory.Budgeted - searchingcategory.Remaining;
                    if (searchingcategory.Budgeted >= searchingcategory.Remaining)
                    {
                        searchingcategory.StrokeColor = "White";
                        if (searchingcategory.Budgeted == searchingcategory.Remaining)
                        {
                            searchingcategory.TextColor = "Gray";
                        }
                        else
                        {
                            searchingcategory.TextColor = "Green";
                        }
                    }
                    else
                    {
                        searchingcategory.TextColor = searchingcategory.StrokeColor = "Red";

                    }
                }
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

            foreach (var category in Allcategories)
            {
                if (category.Day == SelectedEndDay && category.Month == DateTime.ParseExact(SelectedEndMonth, "MMMM", cultureInfo).Month && category.Year == SelectedEndYear)
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

            CountBudget();
            SelectedStartDay = ThisDate.Day;
            SelectedMonth = cultureInfo.DateTimeFormat.GetMonthName(ThisDate.Month);
            SelectedYear = ThisDate.Year;

            SelectedEndDay = SecondDate.Day;
            SelectedEndMonth = cultureInfo.DateTimeFormat.GetMonthName(SecondDate.Month);
            SelectedEndYear = SecondDate.Year;
        }

        private void BackExecute()
        {
            navigation.NavigateTo("MainPage");
        }

        private void InputtingData()
        {
            Categories = budgetManagerRepository.GetCategories();
            if (SelectedUser.Id != 0)
            {
                CategoriesExpense = Categories.Where(s => s.Type == "Expense" && s.Userid == SelectedUser.Id).ToList();
                CategoriesIncome = Categories.Where(s => s.Type == "Income" && s.Userid == SelectedUser.Id).ToList();
                CategoriesAccount = Categories.Where(s => s.Type == "Account" && s.Userid == SelectedUser.Id).ToList();
            }
            else
            {
                CategoriesExpense = Categories.Where(s => s.Type == "Expense").ToList();
                CategoriesIncome = Categories.Where(s => s.Type == "Income").ToList();
                CategoriesAccount = Categories.Where(s => s.Type == "Account").ToList();
            }
            Painting();
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

        private void DeleteCategory()
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
                        categoryAccount = allCategories.FirstOrDefault(category => category.Id == item.Accounttype);
                        categoryAccount.Budgeted += subcategory.Remaining;
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
                }

                categoryForRemove = allCategories.FirstOrDefault(category => category.Id == SelectedCategory.Id);
                SelectedUser.Categoriesexpense.Remove(SelectedCategory);
            }

            else if (SelectedCategory.Type == "Account")
            {
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

                SelectedUser.Categoriesaccount.Remove(SelectedCategory);
            }

            SelectedUser.Expense = SelectedUser.Categoriesexpense.Sum(x => x.Remaining);
            SelectedUser.Income = SelectedUser.Categoriesincome.Sum(x => x.Budgeted);
            SelectedCategory = new Category();
        }

        private void Painting()
        {
            foreach (var searchingcategory in CategoriesExpense)
            {
                searchingcategory.Percent = 1 - (searchingcategory.Remaining * 100 / searchingcategory.Budgeted) * 0.01;

                searchingcategory.Rest = searchingcategory.Budgeted - searchingcategory.Remaining;

                if (searchingcategory.Rest < 0)
                {
                    searchingcategory.Rest = 0;
                }

                if (searchingcategory.Budgeted >= searchingcategory.Remaining)
                {
                    searchingcategory.StrokeColor = "White";
                    if (searchingcategory.Budgeted == searchingcategory.Remaining)
                    {
                        searchingcategory.TextColor = "Gray";
                    }
                    else
                    {
                        searchingcategory.TextColor = "Green";
                    }
                }
                else
                {
                    searchingcategory.TextColor = searchingcategory.StrokeColor = "Red";

                }
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

        private void ChangeUser()
        {
            ChooseRightPeriodByUserSwitch();
        }

        private void SelectCategory()
        {
            if (SelectedCategory != null)
            {
                switch (SelectedCategory.Type)
                {
                    case "Income":
                        UnchosenAccount = false;
                        UnchosenExpense = false;
                        UnchosenIncome = true;
                        break;
                    case "Expense":
                        UnchosenAccount = false;
                        UnchosenExpense = true;
                        UnchosenIncome = false; ;
                        break;
                    case "Account":
                        UnchosenAccount = true;
                        UnchosenExpense = false;
                        UnchosenIncome = false;
                        break;
                }
                Unchosen = true;
            }
            else
            {
                UnchosenAccount = false;
                UnchosenExpense = false;
                UnchosenIncome = false;
                Unchosen = false;
            }
        }

        private void OpenEditIncomeWindow()
        {
            var data = SelectedCategory;
            navigation.NavigateTo("AddIncome", data);
        }

        private void OpenAddIncomeWindow()
        {
            Category NewCategory = new Category();
            var data = NewCategory;
            navigation.NavigateTo("AddIncome", data);
        }

        private void OpenEditExpenseWindow()
        {
            var data = SelectedCategory;
            navigation.NavigateTo("EditExpense", data);
        }

        private void OpenEditMoneyAccountWindow()
        {
            var data = SelectedCategory;
            navigation.NavigateTo("AddMoneyAccount", data);
        }

        private void OpenAddMoneyAccountWindow()
        {
            Category NewCategory = new Category();
            var data = NewCategory;
            navigation.NavigateTo("AddMoneyAccount", data);
        }

        private void OpenLinearExpenseWindow()
        {
            navigation.NavigateTo("LinearExpense");
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
            this.UnchosenAccount = false;
            this.UnchosenExpense = false;
            this.UnchosenIncome = false;
            this.Unchosen = false;

            this.secondDate = this.thisDate = currentTime;
            this.SecondDateVisibility = false;
            this.categories = budgetManagerRepository.GetCategories();
            this.users = budgetManagerRepository.GetUsers();
            this.SelectedUser = Users.FirstOrDefault(chosenUser => chosenUser.IsSelected == "Main");
            string currentMonth = cultureInfo.DateTimeFormat.GetMonthName(System.DateTime.Now.Month);
            allUsers = new User()
            {
                Name = LocalizedStrings.GetString(LocalizedStringEnum.AllUsers),
                Icon = "/Assets/icon.png",    
                Id=0
            };

            users.Insert(0, allUsers);

            InputtingData();
            if (SelectedUser != null)
            {
                this.SelectedUser.Categoriesexpense = new ObservableCollection<Category>(CategoriesExpense.Where(chosenCategory =>
                    chosenCategory.Month == System.DateTime.Now.Month && chosenCategory.Year == currentTime.Year));
                this.SelectedUser.Categoriesincome = new ObservableCollection<Category>(CategoriesIncome.Where(chosenCategory =>
                    chosenCategory.Month == System.DateTime.Now.Month && chosenCategory.Year == currentTime.Year));
                this.SelectedUser.Categoriesaccount = new ObservableCollection<Category>(CategoriesAccount.Where(chosenCategory =>
                    chosenCategory.Month == System.DateTime.Now.Month && chosenCategory.Year == currentTime.Year));
                CountBudget();
            }
         

            this.selectedEndMonth = this.selectedMonth = cultureInfo.DateTimeFormat.GetMonthName(ThisDate.Month);
            this.selectedEndYear = this.selectedYear = ThisDate.Year;
            this.selectedEndDay = this.selectedStartDay = ThisDate.Day;
            string data = parameter as string;
            if (data != null)
            {
                this.Type = data;
            }

            if (Type == "Income")
            {
                this.Title = LocalizedStrings.GetString(LocalizedStringEnum.IncomeTitle);
                IncomeVisibility = true;
                ExpenseVisibility = false;
                AccountVisibility = false;
            }
            else if (Type == "Expense")
            {
                this.Title = LocalizedStrings.GetString(LocalizedStringEnum.ExpensesTitle);
                IncomeVisibility = false;
                ExpenseVisibility = true;
                AccountVisibility = false;
            }
            else
            {
                this.Title = LocalizedStrings.GetString(LocalizedStringEnum.MoneyAccountsTitle);
                IncomeVisibility = false;
                ExpenseVisibility = false;
                AccountVisibility = true;
            }
            ChosenDateType = "Month";
        }

        #endregion

    }
}
