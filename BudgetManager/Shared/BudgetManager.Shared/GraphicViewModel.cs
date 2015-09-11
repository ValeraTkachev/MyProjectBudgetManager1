namespace BudgetManager.Shared
{

    using System;
    using System.Globalization;
    using System.Linq;
    using BudgetManager.Models;
    using BudgetManager.DAL;
    using System.Windows.Input;
    using System.Collections.ObjectModel;
    using BudgetManager.Core;
    using System.Collections.Generic;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Views;
    using Windows.UI.Xaml;

    public class GraphicViewModel : ViewModelBase
    {

        #region fields

        private ObservableCollection<Category> tempcategories;
        private ObservableCollection<User> users;
        private ObservableCollection<Category> categories;
        private ObservableCollection<Category> recievedCategories;
        private ObservableCollection<Transaction> transactions;
        private ObservableCollection<Day> planned;
        private ObservableCollection<Day> income;
        private ObservableCollection<Day> expense;
        private bool isDayExist = false;
        private DateTime thisDate;
        private DateTime secondDate;
        private long userId;
        private User selectedUser;
        private Category selectedCategory;
        private Category allcategories;
        private long daysInMonth;
        private long monthnumber;
        private long budgetIncome;
        private long budgetExpense;
        private long budgetPlanned;
        private long selectedStartDay;
        private string selectedMonth;
        private long selectedYear;
        private long selectedEndDay;
        private long selectedEndYear;
        private string selectedEndMonth;
        private string chosenDateType;
        private bool secondDateVisibility;
        private double height;
        private double width;
        private string title;
        private User allUsers;

        #endregion

        #region Constructor

        public GraphicViewModel(INavigationService navigatedService)
        {
            this.navigation = navigatedService;
            this.budgetExpense = 0;
            this.budgetIncome = 0;
            this.budgetPlanned = 0;
            this.categories = new ObservableCollection<Category>();
            allcategories = new Category()
            {
                Name = LocalizedStrings.GetString(LocalizedStringEnum.AllCategories),
                Icon = "/Assets/trans.png",
                Month = System.DateTime.Now.Month,
                Year = currentTime.Year
            };
            this.income = new ObservableCollection<Day>();
            this.expense = new ObservableCollection<Day>();
            this.planned = new ObservableCollection<Day>();
            YearCommand = new RelayCommand(GetYearType);
            DayCommand = new RelayCommand(GetDayType);
            MonthCommand = new RelayCommand(GetMonthType);
            PeriodCommand = new RelayCommand(GetPeriodType);
            WeekCommand = new RelayCommand(GetWeekType);
            SelectionChangedUser = new RelayCommand(ChangeUser);
            SelectionChangedCategory = new RelayCommand(ChangeCategory);
            DateChangeCommand = new RelayCommand(ChangeDate);
            SecondDateChangeCommand = new RelayCommand(ChangeSecondDate);
            Back = new RelayCommand(BackExecute);

        }

        #endregion

        #region Properties

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

        public long BudgetPlanned
        {
            get { return this.budgetPlanned; }
            set
            {
                if (value != this.budgetPlanned)
                {
                    this.budgetPlanned = value;
                    OnPropertyChanged();
                }
            }
        }

        public long BudgetExpense
        {
            get { return this.budgetExpense; }
            set
            {
                if (value != this.budgetExpense)
                {
                    this.budgetExpense = value;
                    OnPropertyChanged();
                }
            }
        }

        public long BudgetIncome
        {
            get { return this.budgetIncome; }
            set
            {
                if (value != this.budgetIncome)
                {
                    this.budgetIncome = value;
                    OnPropertyChanged();
                }
            }
        }

        public long Monthnumber
        {
            get { return this.monthnumber; }
            set
            {
                if (value != this.monthnumber)
                {
                    this.monthnumber = value;
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



        public long DaysInMonth
        {
            get { return this.daysInMonth; }
            set
            {
                if (value != this.daysInMonth)
                {
                    this.daysInMonth = value;
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

        public long UserId
        {
            get { return this.userId; }
            set
            {
                if (value != this.userId)
                {
                    this.userId = value;
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


        public ObservableCollection<Day> Income
        {
            get { return this.income; }
            set
            {
                if (value != this.income)
                {
                    this.income = value;
                    OnPropertyChanged();
                }
            }

        }

        public ObservableCollection<Day> Expense
        {
            get { return this.expense; }
            set
            {
                if (value != this.expense)
                {

                    this.expense = value;
                    OnPropertyChanged();
                }
            }

        }

        public ObservableCollection<Day> Planned
        {
            get { return this.planned; }
            set
            {
                if (value != this.planned)
                {
                    this.planned = value;
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

        public ObservableCollection<Category> RecievedCategories
        {
            get { return this.recievedCategories; }
            set
            {
                if (value != this.recievedCategories)
                {
                    this.recievedCategories = value;
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
        public RelayCommand DateChangeCommand { get; set; }
        public RelayCommand SecondDateChangeCommand { get; set; }
        public RelayCommand GetSizes { get; set; }

        #endregion

        #region methods

        private void GettingCategoriesMonth()
        {
            Monthnumber = DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month;
            DaysInMonth = System.DateTime.DaysInMonth((int)SelectedYear, (int)Monthnumber);

            foreach (var searchinuser in Users)
            {

                if (searchinuser.Id == SelectedUser.Id)
                {
                    Planned = new ObservableCollection<Day>();
                    Income = new ObservableCollection<Day>();
                    Expense = new ObservableCollection<Day>();

                    Day startDayForGraphic = new Day
                    {
                        Text = 0,
                        FullDate = "",
                        Income = BudgetIncome,
                        Expense = BudgetExpense,
                        Planned = BudgetPlanned
                    };

                    Planned.Add(startDayForGraphic);
                    Income.Add(startDayForGraphic);
                    Expense.Add(startDayForGraphic);

                    for (int i = 1; i <= DaysInMonth; i++)
                    {
                        foreach (var transaction in searchinuser.Transactions)
                        {

                            if (transaction.Month == DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month
                                && transaction.Year == SelectedYear && transaction.Day == i)
                            {

                                if (transaction.Type == "Expense")
                                {
                                    BudgetExpense += transaction.Count;

                                }
                                else if (transaction.Type == "Sending")
                                {
                                    if (SelectedCategory.Id == transaction.Accounttype)
                                    {
                                        BudgetExpense += transaction.Count;
                                    }
                                    else if (SelectedCategory.Id == transaction.Categoryid)
                                    {
                                        BudgetIncome += transaction.Count;
                                    }


                                }
                            }
                        }

                        foreach (var category in searchinuser.Categories)
                        {

                            if (category.Month == DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month 
                                && category.Year == SelectedYear && category.Day == i && category.Type == "Income")
                            {

                                BudgetIncome += category.Budgeted;
                            }
                        }

                        if (i == DaysInMonth)
                        {
                            BudgetPlanned = searchinuser.Categories.Where(a => a.Type == "Expense").Sum(x => x.Budgeted);
                        }

                        if (Monthnumber < 10)
                        {
                            Day dayForAdd = new Day { Text = i, FullDate = i + ".0" + Monthnumber, Income = BudgetIncome, Expense = BudgetExpense, Planned = BudgetPlanned };


                            if (i == 1 || i == DaysInMonth)
                            {
                                Planned.Add(dayForAdd);
                            }

                            Income.Add(dayForAdd);
                            Expense.Add(dayForAdd);


                        }
                        else
                        {
                            Day dayForAdd = new Day { Text = i, FullDate = i + "." + Monthnumber, Income = BudgetIncome, Expense = BudgetExpense, Planned = BudgetPlanned };


                            if (i == 1 || i == DaysInMonth)
                            {

                                Planned.Add(dayForAdd);
                            }

                            Income.Add(dayForAdd);
                            Expense.Add(dayForAdd);
                        }



                    }
                }
            }

            BudgetIncome = 0;
            BudgetExpense = 0;
            BudgetPlanned = 0;
        }

        private void GettingCategoriesDay()
        {
            foreach (var searchinuser in Users)
            {


                if (searchinuser.Id == SelectedUser.Id)
                {

                    Monthnumber = DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month;
                    Planned = new ObservableCollection<Day>();
                    Income = new ObservableCollection<Day>();
                    Expense = new ObservableCollection<Day>();
                    Day startDayForGraphic = new Day
                    {
                        Text = 0,
                        FullDate = "",
                        Income = BudgetIncome,
                        Expense = BudgetExpense,
                        Planned = BudgetPlanned
                    };

                    Planned.Add(startDayForGraphic);
                    Income.Add(startDayForGraphic);
                    Expense.Add(startDayForGraphic);

                    for (int i = 1; i <= SelectedStartDay; i++)
                    {
                        foreach (var transaction in searchinuser.Transactions)
                        {

                            if (transaction.Month == DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month
                                && transaction.Year == SelectedYear && transaction.Day == i)
                            {

                                if (transaction.Type == "Expense")
                                {
                                    BudgetExpense += transaction.Count;

                                }
                                else if (transaction.Type == "Sending")
                                {
                                    if (SelectedCategory.Id == transaction.Accounttype)
                                    {
                                        BudgetExpense += transaction.Count;
                                    }
                                    else if (SelectedCategory.Id == transaction.Categoryid)
                                    {
                                        BudgetIncome += transaction.Count;
                                    }


                                }
                            }
                        }

                        foreach (var category in searchinuser.Categories)
                        {

                            if (category.Month == DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month 
                                && category.Year == SelectedYear && category.Day == i && category.Type == "Income")
                            {

                                BudgetIncome += category.Budgeted;
                            }
                        }

                        if (i == SelectedStartDay)
                        {
                            BudgetPlanned = searchinuser.Categories.Where(a => a.Type == "Expense").Sum(x => x.Budgeted);
                        }

                        if (Monthnumber < 10)
                        {
                            Day dayForAdd = new Day
                            {
                                Text = i,
                                FullDate = i + ".0" + DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month,
                                Income = BudgetIncome,
                                Expense = BudgetExpense,
                                Planned = BudgetPlanned
                            };

                            if (i == 1 || i == SelectedStartDay)
                            {

                                Planned.Add(dayForAdd);

                            }

                            Income.Add(dayForAdd);
                            Expense.Add(dayForAdd);

                        }
                        else
                        {
                            Day dayForAdd = new Day { Text = i, FullDate = i + "." + DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month,
                                Income = BudgetIncome, Expense = BudgetExpense, Planned = BudgetPlanned };

                            if (i == 1 || i == SelectedStartDay)
                            {

                                Planned.Add(dayForAdd);

                            }

                            Income.Add(dayForAdd);
                            Expense.Add(dayForAdd);


                        }



                    }
                }
            }

            BudgetIncome = 0;
            BudgetExpense = 0;
            BudgetPlanned = 0;
        }

        private void GettingCategoriesWeek()
        {
            foreach (var searchinuser in Users)
            {
                Monthnumber = DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month;
                long tempday = SelectedStartDay;
                long tempmonthnumber = Monthnumber;
                long tempyear = SelectedYear;

                if (searchinuser.Id == SelectedUser.Id)
                {

                    Planned = new ObservableCollection<Day>();
                    Income = new ObservableCollection<Day>();
                    Expense = new ObservableCollection<Day>();
                    Day startDayForGraphic = new Day
                    {
                        Text = 0,
                        FullDate = "",
                        Income = BudgetIncome,
                        Expense = BudgetExpense,
                        Planned = BudgetPlanned
                    };

                    Planned.Add(startDayForGraphic);
                    Income.Add(startDayForGraphic);
                    Expense.Add(startDayForGraphic);

                    for (int i = 0; i <= 7; i++)
                    {
                        int daysinmonth = System.DateTime.DaysInMonth((int)tempyear, (int)tempmonthnumber);

                        if (tempday <= daysinmonth)
                        {
                            foreach (var transaction in searchinuser.Transactions)
                            {

                                if (transaction.Month == DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month
                                    && transaction.Year == SelectedYear && transaction.Day == tempday)
                                {

                                    if (transaction.Type == "Expense")
                                    {
                                        BudgetExpense += transaction.Count;

                                    }
                                    else if (transaction.Type == "Sending")
                                    {
                                        if (SelectedCategory.Id == transaction.Accounttype)
                                        {
                                            BudgetExpense += transaction.Count;
                                        }
                                        else if (SelectedCategory.Id == transaction.Categoryid)
                                        {
                                            BudgetIncome += transaction.Count;
                                        }
                                    }
                                }
                            }

                            foreach (var category in searchinuser.Categories)
                            {

                                if (category.Month == DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month 
                                    && category.Year == SelectedYear && category.Day == tempday && category.Type == "Income")
                                {
                                    BudgetIncome += category.Budgeted;
                                }
                            }

                            if (i == 7)
                            {
                                BudgetPlanned = searchinuser.Categories.Where(a => a.Type == "Expense").Sum(x => x.Budgeted);
                            }

                            if (Monthnumber < 10)
                            {
                                Day dayForAdd = new Day
                                {
                                    Text = tempday,
                                    FullDate = tempday + ".0" + tempmonthnumber,
                                    Income = BudgetIncome,
                                    Expense = BudgetExpense,
                                    Planned = BudgetPlanned
                                };

                                if (tempday == SelectedStartDay || i == 7)
                                {

                                    Planned.Add(dayForAdd);
                                }
                                Income.Add(dayForAdd);
                                Expense.Add(dayForAdd);

                            }
                            else
                            {
                                Day dayForAdd = new Day
                                {
                                    Text = tempday,
                                    FullDate = i + "." + tempmonthnumber,
                                    Income = BudgetIncome,
                                    Expense = BudgetExpense,
                                    Planned = BudgetPlanned
                                };

                                if (tempday == SelectedStartDay || i == 7)
                                {

                                    Planned.Add(dayForAdd);
                                }

                                Income.Add(dayForAdd);
                                Expense.Add(dayForAdd);

                            }
                            tempday++;
                        }
                        else
                        {
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

                            foreach (var transaction in searchinuser.Transactions)
                            {

                                if (transaction.Month == DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month
                                    && transaction.Year == SelectedYear && transaction.Day == tempday)
                                {

                                    if (transaction.Type == "Expense")
                                    {
                                        BudgetExpense += transaction.Count;

                                    }
                                    else if (transaction.Type == "Sending")
                                    {
                                        if (SelectedCategory.Id == transaction.Accounttype)
                                        {
                                            BudgetExpense += transaction.Count;
                                        }
                                        else if (SelectedCategory.Id == transaction.Categoryid)
                                        {
                                            BudgetIncome += transaction.Count;
                                        }


                                    }
                                }
                            }

                            foreach (var category in searchinuser.Categories)
                            {

                                if (category.Month == DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month
                                    && category.Year == SelectedYear && category.Day == tempday && category.Type == "Income")
                                {

                                    BudgetIncome += category.Budgeted;
                                }
                            }

                            if (i == 7)
                            {
                                BudgetPlanned = searchinuser.Categories.Where(a => a.Type == "Expense").Sum(x => x.Budgeted);
                            }

                            if (Monthnumber < 10)
                            {
                                Day dayForAdd = new Day
                                {
                                    Text = tempday,
                                    FullDate = i + ".0" + tempmonthnumber,
                                    Income = BudgetIncome,
                                    Expense = BudgetExpense,
                                    Planned = BudgetPlanned
                                };

                                if (tempday == SelectedStartDay || i == 7)
                                {

                                    Planned.Add(dayForAdd);
                                }
                                Income.Add(dayForAdd);
                                Expense.Add(dayForAdd);

                            }
                            else
                            {
                                Day dayForAdd = new Day
                                {
                                    Text = tempday,
                                    FullDate = i + "." + tempmonthnumber,
                                    Income = BudgetIncome,
                                    Expense = BudgetExpense,
                                    Planned = BudgetPlanned
                                };

                                if (tempday == SelectedStartDay || i == 7)
                                {

                                    Planned.Add(dayForAdd);
                                }

                                Income.Add(dayForAdd);
                                Expense.Add(dayForAdd);

                            }


                        }


                    }
                }
            }

            BudgetIncome = 0;
            BudgetExpense = 0;
            BudgetPlanned = 0;
        }

        private void GettingCategoriesYear()
        {

            foreach (var searchinuser in Users)
            {


                if (searchinuser.Id == SelectedUser.Id)
                {

                    Monthnumber = DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month;

                    Planned = new ObservableCollection<Day>();
                    Income = new ObservableCollection<Day>();
                    Expense = new ObservableCollection<Day>();

                    Day startDayForGraphic = new Day
                    {
                        Text = 0,
                        FullDate = "",
                        Income = BudgetIncome,
                        Expense = BudgetExpense,
                        Planned = BudgetPlanned
                    };

                    Planned.Add(startDayForGraphic);
                    Income.Add(startDayForGraphic);
                    Expense.Add(startDayForGraphic);

                    int monthCount = 12;

                    for (int j = 1; j <= monthCount; j++)
                    {

                        foreach (var category in searchinuser.Categories)
                        {
                            foreach (var transaction in searchinuser.Transactions)
                            {

                                if (transaction.Month == j  && transaction.Year == SelectedYear)
                                {
                                    if (transaction.Type == "Expense")
                                    {
                                        BudgetExpense += transaction.Count;

                                    }
                                    else if (transaction.Type == "Sending")
                                    {
                                        if (category.Id == transaction.Accounttype)
                                        {
                                            BudgetExpense += transaction.Count;
                                        }
                                        else if (category.Id == transaction.Categoryid)
                                        {
                                            BudgetIncome += transaction.Count;
                                        }
                                    }


                                }
                            }

                            if (category.Month == j  && category.Year == SelectedYear && category.Type == "Income")
                            {

                                BudgetIncome += category.Budgeted;
                            }
                        }


                        if (j == monthCount)
                        {
                            BudgetPlanned = searchinuser.Categories.Where(a => a.Type == "Expense").Sum(x => x.Budgeted);
                        }



                        Day dayForAdd = new Day { FullDate = cultureInfo.DateTimeFormat.MonthNames[j - 1], Income = BudgetIncome, Expense = BudgetExpense,
                            Planned = BudgetPlanned };

                        if (j == 1 || j == monthCount)
                        {

                            Planned.Add(dayForAdd);
                        }


                        Income.Add(dayForAdd);
                        Expense.Add(dayForAdd);

                    }
                }
            }

            BudgetIncome = 0;
            BudgetExpense = 0;
            BudgetPlanned = 0;
        }

        private void GettingCategories()
        {


            foreach (var searchinuser in Users)
            {
                long tempday = SelectedStartDay;
                long tempyear = SelectedYear;
                long daysinmonth = 0;
                long parameter = 1;
                int firstmonthnumber = DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month;
                int tempmonthnumber = firstmonthnumber;
                int secondmonthnumber = DateTime.ParseExact(SelectedEndMonth, "MMMM", cultureInfo).Month;
                double difference = (SecondDate - ThisDate).TotalDays;
                int count = 0;
                if (difference > DaysInMonth)
                {
                    parameter *= 10;
                }

                if (searchinuser.Id == SelectedUser.Id)
                {
                    Planned = new ObservableCollection<Day>();
                    Income = new ObservableCollection<Day>();
                    Expense = new ObservableCollection<Day>();
                    Day startDayForGraphic = new Day
                    {
                        Text = 0,
                        FullDate = "",
                        Income = BudgetIncome,
                        Expense = BudgetExpense,
                        Planned = BudgetPlanned
                    };

                    Planned.Add(startDayForGraphic);
                    Income.Add(startDayForGraphic);
                    Expense.Add(startDayForGraphic);

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
                            for (int i = (int)SelectedStartDay; i <= daysinmonth; i++)
                            {
                                isDayExist = false;
                                count++;
                                foreach (var transaction in searchinuser.Transactions)
                                {
                                    if (transaction.Month == tempmonthnumber  && transaction.Year == tempyear && transaction.Day == i)
                                    {

                                        if (transaction.Type == "Expense")
                                        {
                                            BudgetExpense += transaction.Count;

                                        }
                                        else if (transaction.Type == "Sending")
                                        {
                                            if (SelectedCategory.Id == transaction.Accounttype)
                                            {
                                                BudgetExpense += transaction.Count;
                                            }
                                            else if (SelectedCategory.Id == transaction.Categoryid)
                                            {
                                                BudgetIncome += transaction.Count;
                                            }

                                        }
                                        isDayExist = true;
                                    }
                                }

                                foreach (var category in searchinuser.Categories)
                                {

                                    if (category.Month == tempmonthnumber  && category.Year == tempyear && category.Day == i
                                        && category.Type == "Income")
                                    {

                                        BudgetIncome += category.Budgeted;
                                        isDayExist = true;
                                    }
                                }

                                if (i == SelectedEndDay)
                                {
                                    BudgetPlanned = searchinuser.Categories.Where(a => a.Type == "Expense").Sum(x => x.Budgeted);
                                }

                                if (tempmonthnumber < 10)
                                {
                                    Day dayForAdd = new Day
                                    {
                                        Text = i,
                                        FullDate = i + ".0" + tempmonthnumber,
                                        Income = BudgetIncome,
                                        Expense = BudgetExpense,
                                        Planned = BudgetPlanned
                                    };

                                    if (i == SelectedStartDay && firstmonthnumber == tempmonthnumber && tempyear == SelectedYear
                                        || i == SelectedEndDay && secondmonthnumber == tempmonthnumber && tempyear == SelectedEndYear)
                                    {
                                        if (i == SelectedStartDay && firstmonthnumber == tempmonthnumber && tempyear == SelectedYear)
                                        {
                                            dayForAdd.Planned = 0;
                                        }
                                        Planned.Add(dayForAdd);
                                        Income.Add(dayForAdd);
                                        Expense.Add(dayForAdd);
                                    }
                                    if (isDayExist || count % parameter == 0)
                                    {
                                        Income.Add(dayForAdd);
                                        Expense.Add(dayForAdd);
                                    }
                                }
                                else
                                {
                                    Day dayForAdd = new Day
                                    {
                                        Text = i,
                                        FullDate = i + "." + tempmonthnumber,
                                        Income = BudgetIncome,
                                        Expense = BudgetExpense,
                                        Planned = BudgetPlanned
                                    };

                                    if (i == SelectedStartDay && firstmonthnumber == tempmonthnumber && tempyear == SelectedYear
                                        || i == SelectedEndDay && secondmonthnumber == tempmonthnumber && tempyear == SelectedEndYear)
                                    {
                                        if (i == SelectedStartDay && firstmonthnumber == tempmonthnumber && tempyear == SelectedYear)
                                        {
                                            dayForAdd.Planned = 0;
                                        }
                                        Planned.Add(dayForAdd);
                                        Income.Add(dayForAdd);
                                        Expense.Add(dayForAdd);
                                    }


                                    if (isDayExist || count % parameter == 0)
                                    {
                                        Income.Add(dayForAdd);
                                        Expense.Add(dayForAdd);
                                    }
                                }
                            }
                        }
                        else
                        {
                            for (int i = 1; i <= daysinmonth; i++)
                            {
                                isDayExist = false;
                                count++;

                                foreach (var transaction in searchinuser.Transactions)
                                {

                                    if (transaction.Month == tempmonthnumber && transaction.Year == tempyear && transaction.Day == i)
                                    {

                                        if (transaction.Type == "Expense")
                                        {
                                            BudgetExpense += transaction.Count;

                                        }
                                        else if (transaction.Type == "Sending")
                                        {
                                            if (SelectedCategory.Id == transaction.Accounttype)
                                            {
                                                BudgetExpense += transaction.Count;
                                            }
                                            else if (SelectedCategory.Id == transaction.Categoryid)
                                            {
                                                BudgetIncome += transaction.Count;
                                            }
                                        }
                                        isDayExist = true;
                                    }
                                }

                                foreach (var category in searchinuser.Categories)
                                {

                                    if (category.Month == tempmonthnumber && category.Year == tempyear && category.Day == i
                                  && category.Type == "Income")
                                    {

                                        BudgetIncome += category.Budgeted;
                                        isDayExist = true;
                                    }
                                }

                                if (i == 7)
                                {
                                    BudgetPlanned = searchinuser.Categories.Where(a => a.Type == "Expense").Sum(x => x.Budgeted);
                                }

                                if (tempmonthnumber < 10)
                                {
                                    Day dayForAdd = new Day
                                    {
                                        Text = i,
                                        FullDate = i + ".0" + tempmonthnumber,
                                        Income = BudgetIncome,
                                        Expense = BudgetExpense,
                                        Planned = BudgetPlanned
                                    };

                                    if (i == SelectedStartDay && firstmonthnumber == tempmonthnumber && tempyear == SelectedYear
                                        || i == SelectedEndDay && secondmonthnumber == tempmonthnumber && tempyear == SelectedEndYear)
                                    {
                                        Planned.Add(dayForAdd);
                                        Income.Add(dayForAdd);
                                        Expense.Add(dayForAdd);
                                    }


                                    if (isDayExist || count % parameter == 0)
                                    {
                                        Income.Add(dayForAdd);
                                        Expense.Add(dayForAdd);
                                    }

                                }
                                else
                                {
                                    Day dayForAdd = new Day
                                    {
                                        Text = i,
                                        FullDate = i + "." + tempmonthnumber,
                                        Income = BudgetIncome,
                                        Expense = BudgetExpense,
                                        Planned = BudgetPlanned
                                    };

                                    if (i == SelectedStartDay && firstmonthnumber == tempmonthnumber && tempyear == SelectedYear
                                        || i == SelectedEndDay && secondmonthnumber == tempmonthnumber && tempyear == SelectedEndYear)
                                    {

                                        Planned.Add(dayForAdd);
                                        Income.Add(dayForAdd);
                                        Expense.Add(dayForAdd);
                                    }


                                    if (isDayExist || count % parameter == 0)
                                    {
                                        Income.Add(dayForAdd);
                                        Expense.Add(dayForAdd);
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
                }

            }


            BudgetIncome = 0;
            BudgetExpense = 0;
            BudgetPlanned = 0;
        }

        private void InfoByYear()
        {
            Planned = new ObservableCollection<Day>();
            Income = new ObservableCollection<Day>();
            Expense = new ObservableCollection<Day>();
            InputtingData();

            if (SelectedCategory.Name == LocalizedStrings.GetString(LocalizedStringEnum.AllCategories))
            {
                SelectedUser.Categories = new ObservableCollection<Category>(RecievedCategories.Where(a => a.Year == SelectedYear));
                SelectedUser.Transactions = new ObservableCollection<Transaction>(Transactions.Where(a => a.Year == SelectedYear));

            }
            else
            {
                List<Category> Subcategories = new List<Category>();
                List<Category> Tempsubcategories = new List<Category>();
                SelectedUser.Categories = new ObservableCollection<Category>(RecievedCategories.Where(a => a.Year == SelectedYear && a.Name == SelectedCategory.Name));

                foreach (var category in SelectedUser.Categories)
                {
                    foreach (var transaction in Transactions)
                    {
                        if (transaction.Categoryid == category.Id || transaction.Accounttype == category.Id)
                        {
                            SelectedUser.Transactions.Add(transaction);
                        }
                    }

                    if (SelectedCategory.Type == "Expense")
                    {
                        Tempsubcategories = RecievedCategories.Where(a => a.AccountId == SelectedCategory.Id && a.Year == SelectedYear).ToList();
                        foreach (var subcategory in Tempsubcategories)
                        {
                            Subcategories.Add(subcategory);
                        }
                    }
                }


                if (SelectedCategory.Type == "Expense")
                {

                    foreach (var subcategory in Subcategories)
                    {
                        foreach (var transaction in Transactions)
                        {
                            if (transaction.Accounttype == subcategory.Id || transaction.Categoryid == subcategory.Id)
                            {
                                SelectedUser.Transactions.Add(transaction);
                            }
                        }
                    }
                }

            }

            GettingCategoriesYear();



        }

        private void InfoByDay()
        {
            Planned = new ObservableCollection<Day>();
            Income = new ObservableCollection<Day>();
            Expense = new ObservableCollection<Day>();

            InputtingData();

            if (SelectedCategory.Name == LocalizedStrings.GetString(LocalizedStringEnum.AllCategories))
            {
                SelectedUser.Categories = new ObservableCollection<Category>(RecievedCategories.Where(a => a.Year == SelectedYear 
                    && a.Month == DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month && a.Day <= SelectedStartDay));
                SelectedUser.Transactions = new ObservableCollection<Transaction>(Transactions.Where(a => a.Year == SelectedYear
                    && a.Month == DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month && a.Day <= SelectedStartDay));

            }
            else
            {
                SelectedUser.Categories = new ObservableCollection<Category>(RecievedCategories.Where(a => a.Year == SelectedYear
                    && a.Month == DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month && a.Id == SelectedCategory.Id && a.Day <= SelectedStartDay));
                SelectedUser.Transactions = new ObservableCollection<Transaction>(Transactions.Where(a => a.Year == SelectedYear
                    && a.Month == DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month && (a.Categoryid == SelectedCategory.Id || a.Accounttype == SelectedCategory.Id && a.Day <= SelectedStartDay)));

                List<Category> Subcategories = new List<Category>();
                if (SelectedCategory.Type == "Expense")
                {
                    Subcategories = RecievedCategories.Where(a => a.AccountId == SelectedCategory.Id && a.Year == SelectedYear
                        && a.Month == DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month && a.Day <= SelectedStartDay).ToList();

                    foreach (var subcategory in Subcategories)
                    {
                        foreach (var transaction in Transactions)
                        {
                            if (transaction.Accounttype == subcategory.Id || transaction.Categoryid == subcategory.Id)
                            {
                                SelectedUser.Transactions.Add(transaction);
                            }
                        }
                    }
                }

            }

            GettingCategoriesDay();

        }

        private void InfoByMonth()
        {
            Planned = new ObservableCollection<Day>();
            Income = new ObservableCollection<Day>();
            Expense = new ObservableCollection<Day>();


            InputtingData();
            if (SelectedCategory.Id == 0)
            {

                SelectedUser.Categories = new ObservableCollection<Category>(RecievedCategories.Where(a => a.Year == SelectedYear 
                    && a.Month == DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month));
                SelectedUser.Transactions = new ObservableCollection<Transaction>(Transactions.Where(a => a.Year == SelectedYear 
                    && a.Month == DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month));
            }

            else
            {

                SelectedUser.Categories = new ObservableCollection<Category>(RecievedCategories.Where(a => a.Year == SelectedYear
                    && a.Month == DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month && a.Id == SelectedCategory.Id));
                SelectedUser.Transactions = new ObservableCollection<Transaction>(Transactions.Where(a => a.Year == SelectedYear 
                    && a.Month == DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month && (a.Categoryid == SelectedCategory.Id || a.Accounttype == SelectedCategory.Id)));

                List<Category> Subcategories = new List<Category>();
                if (SelectedCategory.Type == "Expense")
                {
                    Subcategories = RecievedCategories.Where(a => a.AccountId == SelectedCategory.Id && a.Year == SelectedYear
                        && a.Month == DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month).ToList();

                    foreach (var subcategory in Subcategories)
                    {
                        foreach (var transaction in Transactions)
                        {
                            if (transaction.Accounttype == subcategory.Id || transaction.Categoryid == subcategory.Id)
                            {
                                SelectedUser.Transactions.Add(transaction);
                            }
                        }
                    }
                }


            }

            GettingCategoriesMonth();

        }

        private void InfoByWeek()
        {

            SelectedUser.Transactions.Clear();
            if (SelectedCategory == Allcategories)
            {
                SelectedUser.Categories = new ObservableCollection<Category>(SelectedUser.Categories.Where(a => a.Id == SelectedCategory.Id));
            }
            else
            {
                SelectedUser.Categories.Clear();
            }

            List<Category> subcategories = new List<Category>();
            List<Transaction> searchingTransactions = new List<Transaction>();

            Planned = new ObservableCollection<Day>();
            Income = new ObservableCollection<Day>();
            Expense = new ObservableCollection<Day>();

            SelectedStartDay = SelectedEndDay;
            SelectedMonth = SelectedEndMonth;
            SelectedYear = SelectedEndYear;
            InputtingData();

            int firstmonthnumber = DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month;
            int daysinmonth = System.DateTime.DaysInMonth((int)SelectedYear, firstmonthnumber);
            long tempday = SelectedStartDay;
            int tempmonthnumber = firstmonthnumber;
            long tempyear = SelectedYear;


            for (int i = 0; i <= 7; i++)
            {
                if (tempday < daysinmonth)
                {

                    foreach (var category in RecievedCategories)
                    {
                        if (category.Year == tempyear && category.Day == tempday && category.Month == tempmonthnumber)
                        {

                            if (SelectedCategory.Name == LocalizedStrings.GetString(LocalizedStringEnum.AllCategories))
                            {
                                SelectedUser.Categories.Add(category);

                            }
                            else
                            {

                                if (category.Name == SelectedCategory.Name)
                                {
                                    SelectedUser.Categories.Add(category);
                                }
                            }

                        }

                    }

                    foreach (var transaction in Transactions)
                    {
                        if (transaction.Year == tempyear && transaction.Day == tempday && transaction.Month == tempmonthnumber)
                        {
                            searchingTransactions.Add(transaction);
                        }
                    }
                    tempday++;
                }
                else
                {

                    foreach (var category in RecievedCategories)
                    {
                        if (category.Year == tempyear && category.Day == tempday && category.Month == tempmonthnumber)
                        {
                            if (SelectedCategory.Name == LocalizedStrings.GetString(LocalizedStringEnum.AllCategories))
                            {
                                SelectedUser.Categories.Add(category);

                            }


                            else
                            {
                                if (category.Name == SelectedCategory.Name)
                                {
                                    SelectedUser.Categories.Add(category);
                                }
                            }
                        }

                    }
                    foreach (var transaction in Transactions)
                    {
                        if (transaction.Year == tempyear && transaction.Day == tempday && transaction.Month == tempmonthnumber)
                        {
                            searchingTransactions.Add(transaction);
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
            List<Category> Tempsubcategories = new List<Category>();
            foreach (var category in SelectedUser.Categories)
            {
                if (SelectedCategory == Allcategories)
                {
                    foreach (var transaction in searchingTransactions)
                    {
                        if (transaction.Accounttype == category.Id || transaction.Categoryid == category.Id)
                        {
                            SelectedUser.Transactions.Add(transaction);
                        }
                    }
                }
                else
                {

                    if (category.Type == "Expense")
                    {
                        if (SelectedCategory.Type == "Expense")
                        {
                            Tempsubcategories = RecievedCategories.Where(a => a.AccountId == SelectedCategory.Id && a.Year == SelectedYear).ToList();
                            foreach (var subcategory in Tempsubcategories)
                            {
                                subcategories.Add(subcategory);
                            }
                        }

                        foreach (var subcategory in subcategories)
                        {
                            foreach (var transaction in searchingTransactions)
                            {
                                if (transaction.Accounttype == subcategory.Id || transaction.Categoryid == subcategory.Id)
                                {
                                    SelectedUser.Transactions.Add(transaction);
                                }
                            }
                        }
                    }


                    foreach (var transaction in searchingTransactions)
                    {
                        if (transaction.Accounttype == category.Id || transaction.Categoryid == category.Id)
                        {
                            SelectedUser.Transactions.Add(transaction);
                        }
                    }


                }
            }
            SelectedUser.Transactions = new ObservableCollection<Transaction>(SelectedUser.Transactions.GroupBy(a => a.Id).Select(category => category.First()));
            GettingCategoriesWeek();

        }

        private void InfoByPeriod()
        {

            SelectedUser.Transactions.Clear();
            if (SelectedCategory == Allcategories)
            {
                SelectedUser.Categories = new ObservableCollection<Category>(SelectedUser.Categories.Where(a => a.Id == SelectedCategory.Id));
            }
            else
            {
                SelectedUser.Categories.Clear();
            }

            List<Category> subcategories = new List<Category>();
            List<Transaction> searchingTransactions = new List<Transaction>();
            Planned = new ObservableCollection<Day>();
            Income = new ObservableCollection<Day>();
            Expense = new ObservableCollection<Day>();
            RecievedCategories = budgetManagerRepository.GetCategoriesByUserId(SelectedUser.Id);
            Transactions = budgetManagerRepository.GetTransactionsByUserId(SelectedUser.Id);

            int firstmonthnumber = DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month;
            int secondmonthnumber = DateTime.ParseExact(SelectedEndMonth, "MMMM", cultureInfo).Month;
            long temp = 0;
            string tempMonthName;
            if (SelectedEndYear < SelectedYear)
            {
                temp = SelectedYear;
                SelectedYear = SelectedEndYear;
                SelectedEndYear = temp;
                temp = firstmonthnumber;
                firstmonthnumber = secondmonthnumber;
                secondmonthnumber = Convert.ToInt32(temp);
                tempMonthName = SelectedMonth;
                SelectedMonth = SelectedEndMonth;
                SelectedEndMonth = SelectedMonth;
                temp = SelectedStartDay;
                SelectedStartDay = SelectedEndDay;
                SelectedEndDay = temp;

            }
            else if (firstmonthnumber > secondmonthnumber && SelectedEndYear == SelectedYear)
            {

                temp = firstmonthnumber;
                firstmonthnumber = secondmonthnumber;
                secondmonthnumber = Convert.ToInt32(temp);
                tempMonthName = SelectedMonth;
                SelectedMonth = SelectedEndMonth;
                SelectedEndMonth = tempMonthName;
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


            if (ThisDate == SecondDate)
            {
                InfoByDay();
            }

            else
            {

                foreach (var searchinuser in Users)
                {
                    if (searchinuser.Id == SelectedUser.Id)
                    {
                        searchinuser.Expense = 0;
                        searchinuser.Income = 0;
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
                                    foreach (var category in RecievedCategories)
                                    {
                                        if (category.Year == tempyear && category.Day == i && category.Month == tempmonthnumber)
                                        {
                                            if (SelectedCategory.Name == LocalizedStrings.GetString(LocalizedStringEnum.AllCategories))
                                            {
                                                SelectedUser.Categories.Add(category);

                                            }
                                            else
                                            {

                                                if (category.Name == SelectedCategory.Name)
                                                {
                                                    SelectedUser.Categories.Add(category);
                                                }
                                            }

                                        }

                                    }
                                    foreach (var transaction in Transactions)
                                    {
                                        if (transaction.Year == tempyear && transaction.Day == i &&
                                            transaction.Month == tempmonthnumber)
                                        {
                                            searchingTransactions.Add(transaction);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                for (int i = 1; i <= daysinmonth; i++)
                                {
                                    foreach (var category in RecievedCategories)
                                    {
                                        if (category.Year == tempyear && category.Day == i &&
                                            category.Month == tempmonthnumber)
                                        {
                                            if (category.Year == tempyear && category.Day == i && 
                                                category.Month == tempmonthnumber)
                                            {
                                                if (SelectedCategory.Name == LocalizedStrings.GetString(LocalizedStringEnum.AllCategories))
                                                {
                                                    SelectedUser.Categories.Add(category);

                                                }
                                                else
                                                {

                                                    if (category.Name == SelectedCategory.Name)
                                                    {
                                                        SelectedUser.Categories.Add(category);
                                                    }
                                                }

                                            }
                                        }

                                    }

                                    foreach (var transaction in Transactions)
                                    {
                                        if (transaction.Year == tempyear && transaction.Day == i && transaction.Month == tempmonthnumber)
                                        {
                                            searchingTransactions.Add(transaction);
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

                        List<Category> Tempsubcategories = new List<Category>();
                        foreach (var category in SelectedUser.Categories)
                        {
                            if (SelectedCategory == Allcategories)
                            {
                                foreach (var transaction in searchingTransactions)
                                {
                                    if (transaction.Accounttype == category.Id || transaction.Categoryid == category.Id)
                                    {
                                        SelectedUser.Transactions.Add(transaction);
                                    }
                                }
                            }
                            else
                            {

                                if (category.Type == "Expense")
                                {
                                    if (SelectedCategory.Type == "Expense")
                                    {
                                        Tempsubcategories = RecievedCategories.Where(a => a.AccountId == SelectedCategory.Id && a.Year == SelectedYear).ToList();
                                        foreach (var subcategory in Tempsubcategories)
                                        {
                                            subcategories.Add(subcategory);
                                        }
                                    }

                                    foreach (var subcategory in subcategories)
                                    {
                                        foreach (var transaction in searchingTransactions)
                                        {
                                            if (transaction.Accounttype == subcategory.Id || transaction.Categoryid == subcategory.Id)
                                            {
                                                SelectedUser.Transactions.Add(transaction);
                                            }
                                        }
                                    }
                                }


                                foreach (var transaction in searchingTransactions)
                                {
                                    if (transaction.Accounttype == category.Id || transaction.Categoryid == category.Id)
                                    {
                                        SelectedUser.Transactions.Add(transaction);
                                    }
                                }


                            }
                        }
                        searchinuser.Transactions = new ObservableCollection<Transaction>(SelectedUser.Transactions.GroupBy(a => a.Id).Select(category => category.First()));
                        GettingCategories();

                    }
                }
            }
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
            if (SelectedUser.Id != 0)
            {
                RecievedCategories = budgetManagerRepository.GetCategoriesByUserId(SelectedUser.Id);
                Transactions = budgetManagerRepository.GetTransactionsByUserId(SelectedUser.Id);
            }
            else
            {
                RecievedCategories = budgetManagerRepository.GetCategories();
                Transactions = budgetManagerRepository.GetTransactions();
            }
        }

        private void ClearCategories()
        {

            foreach (var searchingcategory in TempCategories)
            {
                if (searchingcategory.Name != LocalizedStrings.GetString(LocalizedStringEnum.AllCategories) && searchingcategory.Id != SelectedCategory.Id)
                {
                    Categories.Remove(Categories.FirstOrDefault(category => category.Id == searchingcategory.Id));
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

        private void ChangeUser()
        {
            if (SelectedCategory.Id != Allcategories.Id)
            {
                SelectedCategory = Allcategories;
            }

            ClearCategories();

            if (SelectedUser.Id != 0)
            {
                foreach (var category in TempCategories)
                {
                    if (category.Userid == SelectedUser.Id)
                    {
                        if (category.Name != LocalizedStrings.GetString(LocalizedStringEnum.AllCategories))
                        {
                            if (category.Month == DateTime.ParseExact(SelectedEndMonth, "MMMM", cultureInfo).Month && category.Year == SelectedEndYear &&
                                (category.Type != "Subcategory"))
                            {
                                Categories.Add(category);
                            }
                        }

                    }
                }
            }
            else 
            {
                foreach (var category in TempCategories)
                {
                
                        if (category.Name != LocalizedStrings.GetString(LocalizedStringEnum.AllCategories))
                        {
                            if (category.Month == DateTime.ParseExact(SelectedEndMonth, "MMMM", cultureInfo).Month && category.Year == SelectedEndYear &&
                                (category.Type != "Subcategory"))
                            {
                                Categories.Add(category);
                            }
                        }

                    
                }
            }
            ChooseRightPeriodByUserSwitch();
        }

        private void ChangeCategory()
        {
            InfoByMonth();
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

        public override void NavigateTo(object parameter)
        {
            this.Title = LocalizedStrings.GetString(LocalizedStringEnum.statiscticTitle);
            this.users = budgetManagerRepository.GetUsers();
            this.secondDate = this.thisDate = currentTime;
            this.selectedEndMonth = this.selectedMonth = cultureInfo.DateTimeFormat.GetMonthName(ThisDate.Month);
            this.selectedEndYear = this.selectedYear = ThisDate.Year;
            this.selectedEndDay = this.selectedStartDay = ThisDate.Day;
            this.monthnumber = DateTime.ParseExact(cultureInfo.DateTimeFormat.GetMonthName(System.DateTime.Now.Month), "MMMM", cultureInfo).Month;
            this.daysInMonth = System.DateTime.DaysInMonth((int)SelectedYear, (int)monthnumber);
            this.Height = Window.Current.Bounds.Height / 1.5;
            this.Width = Window.Current.Bounds.Width - 100;
            string currentMonth = cultureInfo.DateTimeFormat.GetMonthName(System.DateTime.Now.Month);
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
                    && a.Month == DateTime.ParseExact(currentMonth, "MMMM", cultureInfo).Month && a.Year == currentTime.Year))
        
            };
            users.Insert(0, allUsers);

            if (Users.Count > 1)
            {
                this.recievedCategories = this.tempcategories = budgetManagerRepository.GetCategories();
                SelectedUser = Users.FirstOrDefault(chosenUser => chosenUser.IsSelected == "Main");
                this.transactions = budgetManagerRepository.GetTransactionsByUserId(SelectedUser.Id);
                this.SelectedUser.Categories = new ObservableCollection<Category>(TempCategories.Where(a => a.Userid == SelectedUser.Id
                    && a.Month == System.DateTime.Now.Month && a.Year == currentTime.Year));

                List<Category> allCategories = SelectedUser.Categories.ToList();
                this.SelectedUser.Transactions = new ObservableCollection<Transaction>(Transactions.Where(a => a.Month == System.DateTime.Now.Month
                    && a.Year == currentTime.Year));
                allCategories.Insert(0, Allcategories);

                foreach (var category in allCategories)
                {
                    if (Categories.Count == 0)
                    {
                        Categories.Add(category);
                    }
                    else
                    {
                        Category existCategory = Categories.FirstOrDefault(x => x.Id == category.Id);

                        if (existCategory == null)
                        {
                            Categories.Add(category);
                        }
                    }
                }
                SelectedCategory = Allcategories;
                GettingCategoriesMonth();
            }
            else
            {
                SelectedUser = AllUsers;
                this.recievedCategories = this.tempcategories = budgetManagerRepository.GetCategories();
                this.transactions = budgetManagerRepository.GetTransactions();
                this.SelectedUser.Categories = new ObservableCollection<Category>(TempCategories.Where(a => a.Month == System.DateTime.Now.Month && a.Year == currentTime.Year));

                List<Category> allCategories = SelectedUser.Categories.ToList();
                this.SelectedUser.Transactions = new ObservableCollection<Transaction>(Transactions.Where(a => a.Month == System.DateTime.Now.Month
                    && a.Year == currentTime.Year));
                allCategories.Insert(0, Allcategories);

                foreach (var category in allCategories)
                {
                    if (Categories.Count == 0)
                    {
                        Categories.Add(category);
                    }
                    else
                    {
                        Category existCategory = Categories.FirstOrDefault(x => x.Id == category.Id);

                        if (existCategory == null)
                        {
                            Categories.Add(category);
                        }
                    }
                }
                SelectedCategory = Allcategories;
                GettingCategoriesMonth();

            }
            this.ChosenDateType = "Month";
            this.SecondDateVisibility = false;
        }

        #endregion

    }
}


