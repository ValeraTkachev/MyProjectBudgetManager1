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
    using System.Collections.Generic;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Views;


    public class SelectedCategoryViewModel : ViewModelBase
    {

        #region Fields

        private ObservableCollection<Category> Subcategories;
        private bool unchosen;
        private DateTime thisDate;
        private DateTime secondDate;
        private Category selectedCategory;
        private long categoryId;
        private string categoryName;
        private long selectedStartDay;
        private string selectedMonth;
        private long selectedYear;
        private long selectedEndDay;
        private long selectedEndYear;
        private string selectedEndMonth;
        private string chosenDateType;
        private User selectedUser;
        private ObservableCollection<User> users;
        private bool secondDateVisibility;
        private string title;
        private User allUsers;

        #endregion

        #region Constructor

        public SelectedCategoryViewModel(INavigationService navigatedService)
        {
            this.navigation = navigatedService;
            YearCommand = new RelayCommand(GetYearType);
            DayCommand = new RelayCommand(GetDayType);
            MonthCommand = new RelayCommand(GetMonthType);
            PeriodCommand = new RelayCommand(GetPeriodType);
            WeekCommand = new RelayCommand(GetWeekType);
            DeleteCommand = new RelayCommand(DeleteSubcategoryMethod);
            DateChangeCommand = new RelayCommand(ChangeDate);
            SecondDateChangeCommand = new RelayCommand(ChangeSecondDate);
            SelectionUserChange = new RelayCommand(ChangeUser);
            SelectItem = new RelayCommand(SelectRightItem);
            EditSubcategoryCommand = new RelayCommand(GoToEditSubcategoryPage);
            AddSubcategoryCommand = new RelayCommand(GoToAddSubcategoryPage);
            Back = new RelayCommand(BackExecute);
        }

        #endregion

        #region Basic properties

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
                if (value != this.selectedEndDay)
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

        public string CategoryName
        {
            get { return this.categoryName; }
            set
            {
                if (value != this.categoryName)
                {
                    this.categoryName = value;
                    OnPropertyChanged();
                }
            }
        }

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

        #region Commands

        public RelayCommand DayCommand { get; set; }
        public RelayCommand WeekCommand { get; set; }
        public RelayCommand MonthCommand { get; set; }
        public RelayCommand YearCommand { get; set; }
        public RelayCommand PeriodCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand DateChangeCommand { get; set; }
        public RelayCommand SecondDateChangeCommand { get; set; }
        public RelayCommand SelectionUserChange { get; set; }
        public RelayCommand SelectItem { get; set; }
        public RelayCommand EditSubcategoryCommand { get; set; }
        public RelayCommand AddSubcategoryCommand { get; set; }
        public RelayCommand Back { get; set; }

        #endregion

        #region methods

        public void InfoByYear()
        {
            InputtingData();
            SelectedUser.Subcategories = new ObservableCollection<Category>(Subcategories.Where(x => x.Year == SelectedYear));
            CountBudget();
        }

        public void InfoByDay()
        {
            InputtingData();
            SelectedUser.Subcategories = new ObservableCollection<Category>(Subcategories.Where(x => x.Year == SelectedYear
                && x.Day == SelectedStartDay && x.Month == DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month));
            CountBudget();
        }

        public void InfoByMonth()
        {
            InputtingData();
            SelectedUser.Subcategories = new ObservableCollection<Category>(Subcategories.Where(x => x.Year == SelectedYear
               && x.Month == DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month));
            CountBudget();
        }

        public void InfoByWeek()
        {
            InputtingData();
            int firstmonthnumber = DateTime.ParseExact(SelectedMonth, "MMMM", cultureInfo).Month;
            int daysinmonth = System.DateTime.DaysInMonth((int)SelectedYear, firstmonthnumber);
            long tempday = SelectedStartDay;
            int tempmonthnumber = firstmonthnumber;
            long tempyear = SelectedYear;
            SelectedUser.Subcategories.Clear();
            foreach (var searchinuser in Users)
            {
                if (searchinuser.Id == SelectedUser.Id)
                {

                    foreach (var searchingsubcategory in Subcategories)
                    {
                        for (int i = 0; i <= 7; i++)
                        {
                            if (tempday < daysinmonth)
                            {
                                if (searchingsubcategory.Year == tempyear && searchingsubcategory.Day == tempday
                                    && searchingsubcategory.Month == tempmonthnumber)
                                {
                                    searchinuser.Subcategories.Add(searchingsubcategory);
                                }
                                tempday += 1;
                            }
                            else
                            {
                                if (searchingsubcategory.Year == tempyear && searchingsubcategory.Day == tempday
                                    && searchingsubcategory.Month == tempmonthnumber)
                                {
                                    searchinuser.Subcategories.Add(searchingsubcategory);
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
                    }

                }

                tempday = SelectedStartDay;
            }
            CountBudget();

        }

        public void InfoByPeriod()
        {
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
            SelectedUser.Subcategories.Clear();
            foreach (var searchinuser in Users)
            {
                if (searchinuser.Id == SelectedUser.Id)
                {
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
                       for (int i = 1; i <= daysinmonth; i++)
                        {
                            foreach (var searchingsubcategory in Subcategories)
                            {
                                if (searchingsubcategory.Year == tempyear && searchingsubcategory.Day == i &&
                                    searchingsubcategory.Month == tempmonthnumber)
                                {
                                    searchinuser.Subcategories.Add(searchingsubcategory);
                                }
                            }
                        }
                        if (tempmonthnumber != secondmonthnumber || tempyear != SelectedEndYear)
                        {
                            if (tempmonthnumber == 12)
                            {
                                tempday = 1;
                                tempmonthnumber = 1;
                                tempyear++;
                            }
                            else
                            {
                                tempmonthnumber++;
                                tempday = 1;
                            }
                        }
                    }

                    foreach (var category in Subcategories)
                    {
                        if (category.Day == SelectedEndDay && category.Month == DateTime.ParseExact(SelectedEndMonth, "MMMM", cultureInfo).Month
                            && category.Year == SelectedEndYear)
                        {
                            searchinuser.Subcategories.Add(category);
                        }

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

        public void InputtingData()
        {
            Subcategories = new ObservableCollection<Category>();
            List<Category> tempCategories = budgetManagerRepository.GetCategories().ToList();

            if (SelectedUser.Id != 0)
            {
                tempCategories = tempCategories.Where(x => x.Userid == SelectedUser.Id).ToList();
                Category chosenCategory = budgetManagerRepository.GetCategory(Categoryid);
                List<Category> sameCategory = tempCategories.Where(x => x.Name == chosenCategory.Name).ToList();

                List<Category> tempSubcategories = budgetManagerRepository.GetCategoryByType(SelectedUser.Id, "Subcategory").ToList();
                foreach (var searchingCategory in sameCategory)
                {
                    foreach (var searchingSubcategory in tempSubcategories)
                    {
                        if (searchingCategory.Id == searchingSubcategory.AccountId)
                        {
                            Subcategories.Add(searchingSubcategory);
                        }
                    }
                }
            }

            else
            {
                Category chosenCategory = budgetManagerRepository.GetCategory(Categoryid);
                List<Category> sameCategory = tempCategories.Where(x => x.Name == chosenCategory.Name).ToList();

                List<Category> tempSubcategories = budgetManagerRepository.GetCategories().ToList();
                tempSubcategories = tempSubcategories.Where(x=>x.Type=="Subcategory").ToList();

                foreach (var searchingCategory in sameCategory)
                {
                    foreach (var searchingSubcategory in tempSubcategories)
                    {
                        if (searchingCategory.Id == searchingSubcategory.AccountId)
                        {
                            Subcategories.Add(searchingSubcategory);
                        }
                    }
                }
            }

            foreach (var searchingcategory in Subcategories)
            {
                searchingcategory.Percent = 1 - (searchingcategory.Remaining * 100 / searchingcategory.Budgeted) * 0.01;

                searchingcategory.Rest = searchingcategory.Budgeted - searchingcategory.Remaining;
                if (searchingcategory.Budgeted > searchingcategory.Remaining)
                {
                    searchingcategory.StrokeColor = "White";
                }
                else
                {
                    searchingcategory.StrokeColor = "Red";

                }
            }

        }

        public void DeleteSubcategoryMethod()
        {
            List<Category> Categories = budgetManagerRepository.GetCategories().ToList();
            List<Transaction> Transactions = budgetManagerRepository.GetTransactions().ToList();
            List<Transaction> transactionsForDelete = Transactions.Where(transaction => transaction.Categoryid == SelectedCategory.Id).ToList();
            Category categoryAccount;
            foreach (var transaction in transactionsForDelete)
            {
                categoryAccount = Categories.FirstOrDefault(category => category.Id == transaction.Accounttype);
                categoryAccount.Budgeted += transaction.Count;
                budgetManagerRepository.UpdateCategory(categoryAccount);
            }
            Category MainCategory = Categories.FirstOrDefault(a => a.Id == SelectedCategory.AccountId);
            MainCategory.Remaining -= SelectedCategory.Remaining;
            budgetManagerRepository.UpdateCategory(MainCategory);
            budgetManagerRepository.DeleteTransactionByCategory(SelectedCategory.Id, SelectedCategory.Id);
            budgetManagerRepository.DeleteCategory(SelectedCategory.Id);
            Category categoryForRemove;
            categoryForRemove = Subcategories.FirstOrDefault(category => category.Id == SelectedCategory.Id);
            SelectedUser.Subcategories.Remove(categoryForRemove);
        }

        private void BackExecute()
        {
            navigation.NavigateTo("AllSubcategories");
        }

        private void GetDayType()
        {
            SecondDateVisibility = false;
            ChosenDateType = "Day";
            InfoByDay();
        }

        private void GetMonthType()
        {
            SecondDateVisibility = false;
            ChosenDateType = "Month";
            InfoByMonth();
        }

        private void GetYearType()
        {
            SecondDateVisibility = false;
            ChosenDateType = "Year";
            InfoByYear();
        }

        private void GetWeekType()
        {
            SecondDateVisibility = false;
            ChosenDateType = "Week";
            InfoByWeek();
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

        private void SelectRightItem()
        {
            if (SelectedCategory != null)
            {
                Unchosen = true;
            }
            else
            {
                Unchosen = false;
            }
        }

        private void GoToEditSubcategoryPage()
        {
            var data = SelectedCategory;
            navigation.NavigateTo("AddSubcategory", data);
        }

        private void GoToAddSubcategoryPage()
        {
            Category NewCategory = budgetManagerRepository.GetCategory(Categoryid);
            var data = NewCategory;
            navigation.NavigateTo("AddSubcategory", data);
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

        private void CountBudget()
        {
            List<Category> ForGrouping = SelectedUser.Subcategories.GroupBy(a => a.Name).Select(category => category.First()).ToList();
            foreach (var sum in ForGrouping)
            {
                sum.Budgeted = SelectedUser.Subcategories.Where(category => category.Name == sum.Name).Sum(a => a.Budgeted);
                sum.Remaining = SelectedUser.Subcategories.Where(category => category.Name == sum.Name).Sum(a => a.Remaining);
            }
            SelectedUser.Subcategories = new ObservableCollection<Category>(ForGrouping);

        }

        public override void NavigateTo(object parameter)
        {
           
            this.unchosen = false;
            this.secondDate = this.thisDate = currentTime;
            this.selectedEndMonth = this.selectedMonth = cultureInfo.DateTimeFormat.GetMonthName(ThisDate.Month);
            this.selectedEndYear = this.selectedYear = ThisDate.Year;
            this.selectedEndDay = this.selectedStartDay = ThisDate.Day;
            string currentMonth = cultureInfo.DateTimeFormat.GetMonthName(System.DateTime.Now.Month);

            Category data = parameter as Category;
            if (data != null)
            {
                this.Categoryid = data.Id;
            }
            Category searchingcategory = budgetManagerRepository.GetCategory(categoryId);
            this.users = budgetManagerRepository.GetUsers();
            allUsers = new User()
            {
                Id=0,
                Name = LocalizedStrings.GetString(LocalizedStringEnum.AllUsers),
                Icon = "/Assets/icon.png"
            };

            users.Insert(0, allUsers);
            this.SelectedUser = Users.FirstOrDefault(chosenUser => chosenUser.Id == searchingcategory.Userid);
            this.Subcategories = SelectedUser.Subcategories = budgetManagerRepository.GetCategoryByType(SelectedUser.Id, "Subcategory", System.DateTime.Now.Month,
                currentTime.Year);
            Subcategories = SelectedUser.Subcategories = new ObservableCollection<Category>(Subcategories.Where(a => a.AccountId == Categoryid));
            this.CategoryName = " '' " + searchingcategory.Name + " '' ";
            foreach (var user in Users)
            {
                user.Subcategories = new ObservableCollection<Category>(Subcategories.Where(a => a.AccountId == Categoryid));
            }
            foreach (var category in Subcategories)
            {
                category.Percent = 1 - (category.Remaining * 100 / category.Budgeted) * 0.01;

                category.Rest = category.Budgeted - category.Remaining;
                if (category.Budgeted >= category.Remaining)
                {
                    category.StrokeColor = "White";
                    if (category.Budgeted == category.Remaining)
                    {
                        category.TextColor = "Gray";
                    }
                    else
                    {
                        category.TextColor = "Green";
                    }

                }
                else
                {
                    category.TextColor = category.StrokeColor = "Red";

                }
            }

            this.ChosenDateType = "Month";
            this.SecondDateVisibility = false;
            this.Title = LocalizedStrings.GetString(LocalizedStringEnum.ExpensesTitleForCategory) + "''" + searchingcategory.Name + "''";
        }


        #endregion
    }
}
