namespace BudgetManager.Shared
{
    using System;
    using System.Linq;
    using System.Globalization;
    using BudgetManager.Models;
    using BudgetManager.DAL;
    using System.Windows.Input;
    using System.Collections.ObjectModel;
    using BudgetManager.Core;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Views;


    public class MoneyTransactionModelView : ViewModelBase
    {

        #region Fields for Adding

        private ObservableCollection<User> users;
        private ObservableCollection<User> allExceptSelectedUser;
        private Category senderAccounts;
        private Category recieverAccounts;
        private ObservableCollection<Category> categories;
        private ObservableCollection<Day> days;
        private ObservableCollection<Month> monthes;
        private ObservableCollection<Year> years;
        private User userSender;
        private User userReciever;
        private Day newDay;
        private Month newMonth;
        private Year newYear;
        private string newCount;
        private string rest;
        private string title;

        #endregion

        #region Constructor

        public MoneyTransactionModelView(INavigationService navigatedService)
        {
            this.navigation = navigatedService;
            AddTransactionCommand = new RelayCommand(AddTransactionExecute);
            SelectionChangedYear = new RelayCommand(ChangeYear);
            SelectionRest = new RelayCommand(SelectRest);
            TextChange = new RelayCommand(ChangeText);
            Back = new RelayCommand(() => navigation.GoBack());
        }

        #endregion

        #region Basic Properties

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

        public User UserReciever
        {
            get { return this.userReciever; }
            set
            {
                if (value != this.userReciever)
                {
                    this.userReciever = value; 
                    OnPropertyChanged();
                }
            }
        }

        public User UserSender
        {
            get { return this.userSender; }
            set
            {
                if (value != this.userSender)
                {
                    this.userSender = value;
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

        public ObservableCollection<User> AllExceptSelectedUser
        {
            get { return this.allExceptSelectedUser; }
            set
            {
                if (value != this.allExceptSelectedUser)
                {
                    this.allExceptSelectedUser = value;
                    OnPropertyChanged();
                }
            }
        }

        public Category SenderAccounts
        {
            get { return this.senderAccounts; }
            set
            {
                if (value != this.senderAccounts)
                {
                    this.senderAccounts = value;
                    OnPropertyChanged();
                }
            }
        }

        public Category RecieverAccounts
        {
            get { return this.recieverAccounts; }
            set
            {
                if (value != this.recieverAccounts)
                {
                    this.recieverAccounts = value;
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

        public RelayCommand AddTransactionCommand { get; set; }
        public RelayCommand Back { get; set; }
        public RelayCommand SelectionChangedYear { get; set; }
        public RelayCommand SelectionRest { get; set; }
        public RelayCommand TextChange { get; set; }


        #endregion

        #region Methods

        private void AddTransactionExecute()
        {

            if (NewMonth != null
                && UserSender != null 
                && NewDay != null 
                && NewYear != null 
                && UserReciever != null 
                && SenderAccounts != null
                && RecieverAccounts != null
                && !string.IsNullOrWhiteSpace(NewCount) 
                && Convert.ToInt32(NewCount) != 0)
            {
                Transaction transsender = new Transaction
                {
                    Name = "перевод средств" + "\n" + UserReciever.Name,
                    Count = Convert.ToInt32(NewCount),
                    Userid = UserSender.Id,
                    Type = "Expense",
                    Day = NewDay.Text,
                    Month = DateTime.ParseExact(NewMonth.Text, "MMMM", cultureInfo).Month,
                    Year = NewYear.Text,
                    Categoryid = SenderAccounts.Id,
                    Accounttype = SenderAccounts.Id
                };
                budgetManagerRepository.InsertTransaction(transsender);

                Transaction transreciever = new Transaction
                {
                    Name = "перевод средств" + "\n" + UserSender.Name,
                    Count = Convert.ToInt32(NewCount),
                    Userid = UserReciever.Id,
                    Type = "Income",
                    Day = NewDay.Text,
                    Month = DateTime.ParseExact(NewMonth.Text, "MMMM", cultureInfo).Month,
                    Year = NewYear.Text,
                    Categoryid = RecieverAccounts.Id,
                    Accounttype = RecieverAccounts.Id
                };
                budgetManagerRepository.InsertTransaction(transreciever);

                foreach (var category in Categories)
                {
                    if (category.Id == SenderAccounts.Id)
                    {
                        category.Budgeted -= Convert.ToInt32(NewCount);
                        budgetManagerRepository.UpdateCategory(category);
                    }
                    else if (category.Id == RecieverAccounts.Id)
                    {
                        category.Budgeted += Convert.ToInt32(NewCount);
                        budgetManagerRepository.UpdateCategory(category);

                    }
                }

                navigation.GoBack();
            }
        }

        private void GetDatesCollections()
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

        private void ChangeYear()
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

        private void SelectRest()
        {
            if (SenderAccounts != null)
            {
                Rest = SenderAccounts.Budgeted.ToString();
            }
            else
            {
                Rest = string.Empty;
            }

        }

        private void ChangeText()
        {
            if (!string.IsNullOrWhiteSpace(NewCount))
            {
                Rest = (SenderAccounts.Budgeted - Convert.ToInt32(NewCount)).ToString();
            }
        }

        public override void NavigateTo(object parameter)
        {
            this.Title = LocalizedStrings.GetString(LocalizedStringEnum.moneytransactionTitle);
            this.categories = budgetManagerRepository.GetCategories();
            this.users = budgetManagerRepository.GetUsers();
            this.days = new ObservableCollection<Day>();
            this.monthes = new ObservableCollection<Month>();
            this.years = new ObservableCollection<Year>();

            GetDatesCollections();

            this.NewMonth = Monthes.FirstOrDefault(chosenMonth => chosenMonth.Text == cultureInfo.DateTimeFormat.GetMonthName(System.DateTime.Now.Month));
            this.NewYear = Years.FirstOrDefault(chosenYear => chosenYear.Text == currentTime.Year);
            this.NewDay = NewMonth.Dayscount.FirstOrDefault(chosenDay => chosenDay.Text == currentTime.Day);

            foreach (var user in Users)
            {
                user.Categoriesaccount = budgetManagerRepository.GetCategoryByType(user.Id, "Account");
            }

            this.UserSender = new User();
            this.UserReciever = new User();
            this.SenderAccounts = new Category();
            this.RecieverAccounts = new Category();
            this.Rest = string.Empty;
            this.NewCount = string.Empty;
        }

        #endregion

    }
}