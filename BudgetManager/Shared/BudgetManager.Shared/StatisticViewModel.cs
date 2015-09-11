namespace BudgetManager.Shared
{

    using System;
    using System.Linq;
    using BudgetManager.Models;
    using BudgetManager.DAL;
    using System.Windows.Input;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using BudgetManager.Core;
    using System.Collections.Generic;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Views;


    public class StatisticViewModel : ViewModelBase
    {

        #region fields

        private ObservableCollection<User> users;
        private List<Category> categoriesincome;
        private List<Category> categoriesexpense;
        private ObservableCollection<Category> categories;
        private List<Category> categoriesIncomeRepeat;
        private List<Category> categoriesExpenseRepeat;
        private List<Transaction> transactions;
        private string output;
        private DateTime thisDate;
        private long selectedStartDay;
        private string selectedMonth;
        private long selectedYear;
        private long selectedEndDay;
        private long selectedEndYear;
        private string selectedEndMonth;
        private bool visibilitycolumn;
        private bool visibilitydohnut;
        private bool enablecolumn;
        private bool enabledohnut;
        private long userIncome;
        private long userExpense;
        private User selectedUser;
        private DateTime secondDate;
        private bool secondDateVisibility;
        private string chosenDateType;
        private bool allBudgetVisibility;
        private bool incomesVisibility;
        private bool expensesVisibility;
        private string title;
        private User allUsers;

        #endregion

        #region Constructor

        public StatisticViewModel(INavigationService navigatedService)
        {
            this.navigation = navigatedService;

            SelectColumnChartCommand = new RelayCommand(SelectColumnChart);
            SelectedDohnutChartCommand = new RelayCommand(SelectedDohnutChart);
            YearCommand = new RelayCommand(GetYearType);
            DayCommand = new RelayCommand(GetDayType);
            MonthCommand = new RelayCommand(GetMonthType);
            PeriodCommand = new RelayCommand(GetPeriodType);
            WeekCommand = new RelayCommand(GetWeekType);
            SelectionChangedUser = new RelayCommand(ChangeUser);
            DateChangeCommand = new RelayCommand(ChangeDate);
            SecondDateChangeCommand = new RelayCommand(ChangeSecondDate);
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

        public long UserExpense
        {
            get { return this.userExpense; }
            set
            {
                if (value != this.userExpense)
                {
                    this.userExpense = value; 
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

        public long UserIncome
        {
            get { return this.userIncome; }
            set
            {
                if (value != this.userIncome)
                {
                    this.userIncome = value; 
                    OnPropertyChanged();
                }
            }
        }

        public bool Enabledohnut
        {
            get { return this.enabledohnut; }
            set
            {
                if (value != this.enabledohnut)
                {
                    this.enabledohnut = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool Enablecolumn
        {
            get { return this.enablecolumn; }
            set
            {
                if (value != this.enablecolumn)
                {
                    this.enablecolumn = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool Visibilitydohnut
        {
            get { return this.visibilitydohnut; }
            set
            {
                if (value != this.visibilitydohnut)
                {
                    this.visibilitydohnut = value; 
                    OnPropertyChanged();
                }
            }
        }

        public bool Visibilitycolumn
        {
            get { return this.visibilitycolumn; }
            set
            {
                if (value != this.visibilitycolumn)
                {
                    this.visibilitycolumn = value;
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


        public string Output
        {
            get { return this.output; }
            set
            {
                if (value != this.output)
                {
                    this.output = value; 
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

        public List<Category> CategoriesIncomeRepeat
        {
            get { return this.categoriesIncomeRepeat; }
            set
            {
                if (value != this.categoriesIncomeRepeat)
                {
                    this.categoriesIncomeRepeat = value; 
                    OnPropertyChanged();
                }
            }
        }

        public List<Category> CategoriesExpenseRepeat
        {
            get { return this.categoriesExpenseRepeat; }
            set
            {
                if (value != this.categoriesExpenseRepeat)
                {
                    this.categoriesExpenseRepeat = value;
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

        public List<Transaction> Transactions
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

        public bool IncomesVisibility
        {
            get { return this.incomesVisibility; }
            set
            {
                if (value != this.incomesVisibility)
                {
                    this.incomesVisibility = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool ExpensesVisibility
        {
            get { return this.expensesVisibility; }
            set
            {
                if (value != this.expensesVisibility)
                {
                    this.expensesVisibility = value;
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
        public RelayCommand SelectionChangedUser { get; set; }
        public RelayCommand DateChangeCommand { get; set; }
        public RelayCommand SecondDateChangeCommand { get; set; }
        public RelayCommand SelectColumnChartCommand { get; set; }
        public RelayCommand SelectedDohnutChartCommand { get; set; }
        public RelayCommand DayCommand { get; set; }
        public RelayCommand WeekCommand { get; set; }
        public RelayCommand MonthCommand { get; set; }
        public RelayCommand YearCommand { get; set; }
        public RelayCommand PeriodCommand { get; set; }

        #endregion

        #region Methods

        private void SelectColumnChart()
        {

            Visibilitycolumn = true;
            Visibilitydohnut = false;
            Enablecolumn = false;
            Enabledohnut = true;

        }

        private void SelectedDohnutChart()
        {

            Visibilitycolumn = false;
            Visibilitydohnut = true;
            Enablecolumn = true;
            Enabledohnut = false;

        }

        private void InfoByYear()
        {
            AllBudgetVisibility = false;
            IncomesVisibility = false;
            ExpensesVisibility = false;
            InputtingData();
            SelectedUser.Expense = 0;
            SelectedUser.Income = 0;
            List<Category> ChosenCategoryexpense = new List<Category>();
            List<Category> ChosenCategoryincome = new List<Category>(CategoriesIncome.Where(chosencategory =>
              chosencategory.Year == SelectedYear));

            List<Transaction> ChosenTransactions = new List<Transaction>(Transactions.Where(chosenTransaction => chosenTransaction.Year == SelectedYear
                && (chosenTransaction.Type == "Expense" ||chosenTransaction.Type=="Income")));
            foreach (var item in ChosenTransactions)
            {
                Category categoryByTransaction = budgetManagerRepository.GetCategory(item.Categoryid);
                categoryByTransaction.StatisticType = item.Count;

                if (item.Type == "Expense")
                {
                    ChosenCategoryexpense.Add(categoryByTransaction);
                }
                else 
                {
                    ChosenCategoryincome.Add(categoryByTransaction);
                }
            }

            CountBudget(ChosenCategoryexpense, ChosenCategoryincome);
            SelectedUser.Expense = SelectedUser.Categoriesexpense.Sum(x => x.StatisticType);
            SelectedUser.Income = SelectedUser.Categoriesincome.Sum(x => x.StatisticType);
            StatisticMethod();
            UserExpense = SelectedUser.Expense;
            UserIncome = SelectedUser.Income;
            Output = Convert.ToString(SelectedYear);
            ChangeVisibilityOfCharts();

        }

        private void InfoByDay()
        {
            AllBudgetVisibility = false;
            IncomesVisibility = false;
            ExpensesVisibility = false;
            InputtingData();
            SelectedUser.Expense = 0;
            SelectedUser.Income = 0;
            List<Category> ChosenCategoryexpense = new List<Category>();
            List<Category> ChosenCategoryincome = new List<Category>(CategoriesIncome.Where(chosencategory => chosencategory.Year == SelectedYear
             && chosencategory.Day == SelectedStartDay && chosencategory.Month == DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month));

            List<Transaction> ChosenTransactions = new List<Transaction>(Transactions.Where(chosenTransaction => chosenTransaction.Year == SelectedYear
                && (chosenTransaction.Type == "Expense"||chosenTransaction.Type=="Income")
                && chosenTransaction.Day == SelectedStartDay && chosenTransaction.Month == DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month));

            foreach (var item in ChosenTransactions)
            {
                Category categoryByTransaction = budgetManagerRepository.GetCategory(item.Categoryid);
                categoryByTransaction.StatisticType = item.Count;

                if (item.Type == "Expense")
                {
                    ChosenCategoryexpense.Add(categoryByTransaction);
                }
                else
                {
                    ChosenCategoryincome.Add(categoryByTransaction);
                }

            }

            CountBudget(ChosenCategoryexpense, ChosenCategoryincome);
            SelectedUser.Expense = SelectedUser.Categoriesexpense.Sum(x => x.StatisticType);
            SelectedUser.Income = SelectedUser.Categoriesincome.Sum(x => x.StatisticType);
            StatisticMethod();
            UserExpense = SelectedUser.Expense;
            UserIncome = SelectedUser.Income;
            Output = Convert.ToString(SelectedStartDay) + "." + DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month + "." + Convert.ToString(SelectedYear);
            ChangeVisibilityOfCharts();

        }

        private void InfoByMonth()
        {
            AllBudgetVisibility = false;
            IncomesVisibility = false;
            ExpensesVisibility = false;
            InputtingData();
            SelectedUser.Expense = 0;
            SelectedUser.Income = 0;

            List<Category> ChosenCategoryexpense = new List<Category>();
            List<Category> ChosenCategoryincome = new List<Category>(CategoriesIncome.Where(chosencategory =>
               chosencategory.Year == SelectedYear && chosencategory.Month == DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month));
            List<Transaction> ChosenTransactions = new List<Transaction>(Transactions.Where(chosenTransaction => chosenTransaction.Year == SelectedYear
                && chosenTransaction.Month == DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month && (chosenTransaction.Type == "Expense" 
                || chosenTransaction.Type == "Income")));
            foreach (var item in ChosenTransactions)
            {
                Category categoryByTransaction = budgetManagerRepository.GetCategory(item.Categoryid);
                categoryByTransaction.StatisticType = item.Count;

                if (item.Type == "Expense")
                {
                    ChosenCategoryexpense.Add(categoryByTransaction);
                }
                else
                {
                    ChosenCategoryincome.Add(categoryByTransaction);
                }

            }
            CountBudget(ChosenCategoryexpense, ChosenCategoryincome);
            SelectedUser.Expense = SelectedUser.Categoriesexpense.Sum(x => x.StatisticType);
            SelectedUser.Income = SelectedUser.Categoriesincome.Sum(x => x.StatisticType);
            StatisticMethod();
            UserExpense = SelectedUser.Expense;
            UserIncome = SelectedUser.Income;
            Output = SelectedMonth;
            ChangeVisibilityOfCharts();
        }

        private void InfoByWeek()
        {
            AllBudgetVisibility = false;
            IncomesVisibility = false;
            ExpensesVisibility = false;
            InputtingData();

            int firstmonthnumber = DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month;
            int daysinmonth = System.DateTime.DaysInMonth((int)SelectedYear, firstmonthnumber);
            long tempday = SelectedStartDay;
            int tempmonthnumber = firstmonthnumber;
            long tempyear = SelectedYear;
            ObservableCollection<Category> allcategories = budgetManagerRepository.GetCategories();
            allcategories = new ObservableCollection<Category>(allcategories.Where(category => category.Userid == SelectedUser.Id));
            foreach (var item in allcategories)
            {
                item.StatisticType = item.Budgeted;
            }
            ObservableCollection<Transaction> alltransactions = budgetManagerRepository.GetTransactions();
            alltransactions = new ObservableCollection<Transaction>(alltransactions.Where(item => item.Userid == SelectedUser.Id));

            foreach (var searchinuser in Users)
            {
                if (searchinuser.Id == SelectedUser.Id)
                {
                    searchinuser.Expense = 0;
                    searchinuser.Income = 0;
                    List<Category> ChosenCategoryexpense = new List<Category>();
                    List<Category> ChosenCategoryincome = new List<Category>();
                    List<Transaction> ChosenTransactions = new List<Transaction>();


                    for (int i = 0; i < 7; i++)
                    {
                        if (tempday < daysinmonth)
                        {
                            foreach (var category in allcategories)
                            {
                                if (category.Year == tempyear && category.Day == tempday && category.Month == tempmonthnumber)
                                {
                                    if (category.Type == "Income")
                                    {
                                        ChosenCategoryincome.Add(category);

                                    }

                                }
                            }

                            foreach (var transaction in alltransactions)
                            {
                                if (transaction.Year == tempyear && transaction.Day == tempday &&
                                    transaction.Month == tempmonthnumber)
                                {
                                    ChosenTransactions.Add(transaction);
                                }
                            }


                            tempday += 1;
                        }
                        else
                        {
                            foreach (var category in allcategories)
                            {
                                if (category.Year == tempyear && category.Day == tempday && category.Month == tempmonthnumber)
                                {
                                    if (category.Type == "Income")
                                    {
                                        ChosenCategoryincome.Add(category);

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

                    Output = SelectedStartDay + "." + DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month + "." + SelectedYear
                            + "-" + "\n" + tempday + "." + tempmonthnumber + "." + tempyear;
                    tempday = SelectedStartDay;
                    tempmonthnumber = firstmonthnumber;

                    foreach (var item in ChosenTransactions)
                    {
                        Category categoryByTransaction = budgetManagerRepository.GetCategory(item.Categoryid);
                        categoryByTransaction.StatisticType = item.Count;

                        if (item.Type == "Expense")
                        {
                            ChosenCategoryexpense.Add(categoryByTransaction);
                        }
                        else
                        {
                            ChosenCategoryincome.Add(categoryByTransaction);
                        }

                    }

                    CountBudget(ChosenCategoryexpense, ChosenCategoryincome);
                    SelectedUser.Expense = SelectedUser.Categoriesexpense.Sum(x => x.StatisticType);
                    SelectedUser.Income = SelectedUser.Categoriesincome.Sum(x => x.StatisticType);
                    StatisticMethod();
                    UserExpense = searchinuser.Expense;
                    UserIncome = searchinuser.Income;
                    ChangeVisibilityOfCharts();

                }

            }


        }

        private void InfoByPeriod()
        {
            AllBudgetVisibility = false;
            IncomesVisibility = false;
            ExpensesVisibility = false;
            InputtingData();

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
            ObservableCollection<Transaction> alltransactions = budgetManagerRepository.GetTransactions();
            alltransactions = new ObservableCollection<Transaction>(alltransactions.Where(item => item.Userid == SelectedUser.Id
                && (item.Type == "Expense" || item.Type == "Income")));

            foreach (var searchinuser in Users)
            {
                if (searchinuser.Id == SelectedUser.Id)
                {
                    searchinuser.Expense = 0;
                    searchinuser.Income = 0;
                    List<Category> ChosenCategoryexpense = new List<Category>();
                    List<Category> ChosenCategoryincome = new List<Category>();
                    List<Transaction> ChosenTransactions = new List<Transaction>();

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
                                foreach (var transaction in alltransactions)
                                {
                                    if (transaction.Year == tempyear && transaction.Day == i && transaction.Month == tempmonthnumber)
                                    {
                                        ChosenTransactions.Add(transaction);

                                    }
                                }

                                foreach (var category in CategoriesIncome)
                                {
                                    if (category.Year == tempyear && category.Day == i && category.Month == tempmonthnumber)
                                    {
                                        ChosenCategoryincome.Add(category);

                                    }
                                }

                            }
                        }
                        else
                        {
                            for (int i = 1; i <= daysinmonth; i++)
                            {
                                foreach (var transaction in alltransactions)
                                {
                                    if (transaction.Year == tempyear && transaction.Day == i && transaction.Month == tempmonthnumber)
                                    {
                                        ChosenTransactions.Add(transaction);

                                    }
                                }

                                foreach (var category in CategoriesIncome)
                                {
                                    if (category.Year == tempyear && category.Day == i && category.Month == tempmonthnumber)
                                    {
                                        ChosenCategoryincome.Add(category);

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

                    foreach (var transaction in alltransactions)
                    {
                        if (transaction.Day == SelectedEndDay && transaction.Month == DateTime.ParseExact(SelectedEndMonth, "MMMM", cultureInfo).Month 
                            && transaction.Year == SelectedEndYear)
                        {
                            ChosenTransactions.Add(transaction);
                        }
                    
                    }

                    foreach (var category in CategoriesIncome)
                    {
                        if (category.Day == SelectedEndDay && category.Month == DateTime.ParseExact(SelectedEndMonth, "MMMM", cultureInfo).Month
                            && category.Year == SelectedEndYear)
                        {
                            ChosenCategoryincome.Add(category);
                        }

                    }

                    foreach (var item in ChosenTransactions)
                    {
                        Category categoryByTransaction = budgetManagerRepository.GetCategory(item.Categoryid);
                        categoryByTransaction.StatisticType = item.Count;

                        if (item.Type == "Expense")
                        {
                            ChosenCategoryexpense.Add(categoryByTransaction);
                        }
                        else
                        {
                            ChosenCategoryincome.Add(categoryByTransaction);
                        }

                    }

                    CountBudget(ChosenCategoryexpense, ChosenCategoryincome);
                    SelectedUser.Expense = SelectedUser.Categoriesexpense.Sum(x => x.StatisticType);
                    SelectedUser.Income = SelectedUser.Categoriesincome.Sum(x => x.StatisticType);
                    StatisticMethod();
                    UserExpense = searchinuser.Expense;
                    UserIncome = searchinuser.Income;
                    ChangeVisibilityOfCharts();
                }

            }

            SelectedStartDay = ThisDate.Day;
            SelectedMonth = cultureInfo.DateTimeFormat.GetMonthName(ThisDate.Month);
            SelectedYear = ThisDate.Year;
            SelectedEndDay = SecondDate.Day;
            SelectedEndMonth = cultureInfo.DateTimeFormat.GetMonthName(SecondDate.Month);
            SelectedEndYear = SecondDate.Year;
            Output = SelectedStartDay + "." + DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month + "." + SelectedYear
                + "-" + "\n" + SelectedEndDay + "." + DateTime.ParseExact(SelectedEndMonth, "MMMM", cultureInfo).Month + "." + SelectedEndYear;
        }

        private void InputtingData()
        {
            if (SelectedUser.Id != 0)
            {
                CategoriesIncome = budgetManagerRepository.GetCategoryByType(SelectedUser.Id, "Income").ToList();
                foreach (var item in CategoriesIncome)
                {
                    item.StatisticType = item.Budgeted;
                }
                CategoriesExpense = budgetManagerRepository.GetCategoryByType(SelectedUser.Id, "Expense").ToList();
                Transactions = budgetManagerRepository.GetTransactions().ToList();
                Transactions = Transactions.Where(x => x.Userid == SelectedUser.Id).ToList();
            }
            else
            {
                CategoriesIncome = budgetManagerRepository.GetCategories().ToList();
                CategoriesIncome = CategoriesIncome.Where(x => x.Type == "Income").ToList();
                foreach (var item in CategoriesIncome)
                {
                    item.StatisticType = item.Budgeted;
                }
                CategoriesExpense = budgetManagerRepository.GetCategories().ToList();
                CategoriesExpense = CategoriesExpense.Where(x => x.Type == "Expense").ToList();
                Transactions = budgetManagerRepository.GetTransactions().ToList();
            }
        }

        private void BackExecute()
        {
            navigation.NavigateTo("MainPage");
        }

        private void CountBudget(List<Category> ChosenCategoryexpense, List<Category> ChosenCategoryincome)
        {

            List<Category> ForGrouping = ChosenCategoryexpense.GroupBy(a => a.Name).Select(category => category.First()).ToList();
            foreach (var sum in ForGrouping)
            {
                sum.StatisticType = ChosenCategoryexpense.Where(category => category.Name == sum.Name).Sum(a => a.StatisticType);
            }
            SelectedUser.Categoriesexpense = new ObservableCollection<Category>(ForGrouping);


            ForGrouping = ChosenCategoryincome.GroupBy(a => a.Name).Select(category => category.First()).ToList();
            foreach (var sum in ForGrouping)
            {
                sum.Budgeted = ChosenCategoryincome.Where(category => category.Name == sum.Name).Sum(a => a.Budgeted);
            }
            SelectedUser.Categoriesincome = new ObservableCollection<Category>(ForGrouping);

        }

        private void StatisticMethod()
        {
            Category Income = new Category();
            Category Expense = new Category();
            List<Category> tempcat = new List<Category>();

            foreach (var category in Categories)
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

            Categories.Clear();
            Categories = new ObservableCollection<Category>(tempcat);

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
           StatisticMethod();
           InputtingData();
           ChooseRightPeriodByUserSwitch();
           UserExpense = SelectedUser.Expense;
           UserIncome = SelectedUser.Income;
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

        private void ChangeVisibilityOfCharts()
        {

            if (SelectedUser.Income == 0 || SelectedUser.Expense == 0)
            {
                if (SelectedUser.Income == 0 && SelectedUser.Expense == 0)
                {
                    AllBudgetVisibility = true;
                    IncomesVisibility = true;
                    ExpensesVisibility = true;
                }

                else if (SelectedUser.Income == 0)
                {
                    AllBudgetVisibility = false;
                    IncomesVisibility = true;
                    ExpensesVisibility = false;
                }

                else if (SelectedUser.Expense == 0)
                {
                    AllBudgetVisibility = false;
                    IncomesVisibility = false;
                    ExpensesVisibility = true;
                }

            }   
    
        }

        public override void NavigateTo(object parameter)
        {
            this.Title = LocalizedStrings.GetString(LocalizedStringEnum.statiscticTitle);
            this.allBudgetVisibility = false;
            this.incomesVisibility = false;
            this.expensesVisibility = false;
            this.visibilitycolumn = false;
            this.visibilitydohnut = true;
            this.enablecolumn = true;
            this.enabledohnut = false;
            this.secondDate = this.thisDate = currentTime;
            this.users = budgetManagerRepository.GetUsers();
            this.SelectedUser = Users.FirstOrDefault(chosenUser => chosenUser.IsSelected == "Main");
            this.transactions = budgetManagerRepository.GetTransactions().ToList();
            string currentMonth = cultureInfo.DateTimeFormat.GetMonthName(System.DateTime.Now.Month);

            foreach (var searchinuser in Users)
            {
                searchinuser.Categoriesincome = budgetManagerRepository.GetCategoryByType(searchinuser.Id, "Income",
                    System.DateTime.Now.Month, currentTime.Year);
                searchinuser.Categoriesexpense = budgetManagerRepository.GetCategoryByType(searchinuser.Id, "Expense",
                   System.DateTime.Now.Month, currentTime.Year);
                categoriesIncomeRepeat = searchinuser.Categoriesincome.GroupBy(x => x.Name).Select(a => a.First()).ToList();
                foreach (var sum in CategoriesIncomeRepeat)
                {
                    sum.Budgeted = searchinuser.Categoriesincome.Where(category => category.Name == sum.Name).Sum(a => 
                        a.Budgeted);
                }

                categoriesExpenseRepeat = searchinuser.Categoriesexpense.GroupBy(x => x.Name).Select(a => a.First()).ToList();
                foreach (var sum in CategoriesExpenseRepeat)
                {
                    sum.Budgeted = searchinuser.Categoriesexpense.Where(category => category.Name == sum.Name).Sum(a => a.Budgeted);
                    sum.Remaining = searchinuser.Categoriesexpense.Where(category => category.Name == sum.Name).Sum(a => a.Remaining);
                }

                searchinuser.Categoriesincome = new ObservableCollection<Category>(CategoriesIncomeRepeat);
                searchinuser.Categoriesexpense = new ObservableCollection<Category>(CategoriesExpenseRepeat);
                searchinuser.Transactions = budgetManagerRepository.GetTransactionsByUserId(searchinuser.Id);
                searchinuser.Transactions = new ObservableCollection<Transaction>(searchinuser.Transactions.Where(a =>
                    a.Month == System.DateTime.Now.Month && a.Year == currentTime.Year));

                searchinuser.Expense = searchinuser.Transactions.Where(a => a.Type == "Expense").Sum(x => x.Count);
                searchinuser.Income = searchinuser.Categoriesincome.Sum(x => x.Budgeted);

            }

            allUsers = new User()
            {
                Id=0,
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
                    && a.Month == DateTime.ParseExact(currentMonth, "MMMM", cultureInfo).Month && a.Year == currentTime.Year))
            };
            users.Insert(0, allUsers);

            this.output = this.selectedEndMonth = this.selectedMonth = cultureInfo.DateTimeFormat.GetMonthName(ThisDate.Month);
            this.selectedEndYear = this.selectedYear = ThisDate.Year;
            this.selectedEndDay = this.selectedStartDay = ThisDate.Day;
            this.ChosenDateType = "Month";
            this.SecondDateVisibility = false;

            if (SelectedUser != null)
            {
                this.userIncome = SelectedUser.Income;
                this.userExpense = SelectedUser.Expense;
                this.categories = new ObservableCollection<Category>();
                Category Expense = new Category() { Name = "Расходы", Budgeted = SelectedUser.Expense, StatisticType = SelectedUser.Expense };
                Category Income = new Category() { Name = "Доходы", Budgeted = SelectedUser.Income, StatisticType = SelectedUser.Income };
                categories.Add(Expense);
                categories.Add(Income);
                InfoByMonth();
                ChangeVisibilityOfCharts();
            }
            else {
                AllBudgetVisibility = true;
                IncomesVisibility = true;
                ExpensesVisibility = true;
            }
        }

        #endregion
    }
}
