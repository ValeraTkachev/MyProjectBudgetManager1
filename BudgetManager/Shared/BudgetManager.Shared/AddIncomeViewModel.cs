namespace BudgetManager.Shared
{

    using System;
    using System.Globalization;
    using BudgetManager.Models;
    using BudgetManager.DAL;
    using System.Windows.Input;
    using System.Collections.ObjectModel;
    using BudgetManager.Core;
    using System.Collections.Generic;
    using System.Linq;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Views;
    using System.Threading.Tasks;
    using Windows.UI.Popups;


    public class AddIncomeViewModel : ViewModelBase
    {

        #region Fields

        private ObservableCollection<User> users;
        private ObservableCollection<Category> categories;
        private ObservableCollection<Day> days;
        private ObservableCollection<Month> monthes;
        private ObservableCollection<Year> years;
        private long categoryId;
        private long oldCount;
        private Icon newIcon;
        private Color newColor;
        private string newName;
        private Day newDay;
        private Month newMonth;
        private Year newYear;
        private User newUser;
        private Category selectedMoneyAccount;
        private Category oldAccount;
        private string newBudgeted;
        private string title;

        #endregion

        #region Constructor

        public AddIncomeViewModel(INavigationService navigatedService)
        {
            this.navigation = navigatedService;
            this.monthes = new ObservableCollection<Month>();
            this.years = new ObservableCollection<Year>();
            this.NewColor = null;
            GetDatesColletions();
            AddCommand = new RelayCommand(AddExecute);
            SelectionChangedYear = new RelayCommand(ChangeDaysInMonthExecute);
            Back = new RelayCommand(() => navigation.GoBack());
        }

        #endregion

        #region Basic Properties

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

        public List<Color> Colors { get; set; }

        public List<Icon> Icons { get; set; }

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

        public string NewBudgeted
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

        public Category OldAccount
        {
            get { return this.oldAccount; }
            set
            {
                if (value != this.oldAccount)
                {
                    this.oldAccount = value;
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

        public long OldCount
        {
            get { return this.oldCount; }
            set
            {
                if (value != this.oldCount)
                {
                    this.oldCount = value;
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
        public RelayCommand AddCommand { get; set; }
        public RelayCommand SelectionChangedYear { get; set; }

        #endregion

        #region Methods

        private void AddExecute()
        {
            if (CategoryId == 0)
            {

                if (!string.IsNullOrWhiteSpace(NewName)
                    && NewColor != null && NewIcon != null
                    && !string.IsNullOrWhiteSpace(NewBudgeted)
                    && Convert.ToInt32(NewBudgeted) != 0
                    && NewDay != null && NewYear != null
                    && NewMonth != null)
                {
                    Category category = new Category
                    {
                        Name = newName,
                        Color = NewColor.Text,
                        Icon = NewIcon.Text,
                        Budgeted = Convert.ToInt32(NewBudgeted),
                        Userid = NewUser.Id,
                        Type = "Income",
                        Day = NewDay.Text,
                        Month = DateTime.ParseExact(NewMonth.Text, "MMMM", cultureInfo).Month,
                        Year = NewYear.Text,
                        AccountId = SelectedMoneyAccount.Id
                    };
                    budgetManagerRepository.InsertCategory(category);

                    List<Category> Allcategories = budgetManagerRepository.GetCategories().ToList();

                    foreach (var searchingcategory in Allcategories)
                    {
                        if (searchingcategory.Id == SelectedMoneyAccount.Id)
                        {
                            searchingcategory.Budgeted += Convert.ToInt32(NewBudgeted);
                            budgetManagerRepository.UpdateCategory(searchingcategory);
                        }
                    }

                    navigation.GoBack();
                }
                else
                {
                    ShowWarningMessage();
                }
               
            }


            else
            {
                if (!string.IsNullOrWhiteSpace(NewName)
                    && NewColor != null
                    && NewIcon != null
                    && !string.IsNullOrWhiteSpace(NewBudgeted)
                    && Convert.ToInt32(NewBudgeted) != 0
                    && NewDay != null
                    && NewYear != null
                    && NewMonth != null)
                {
                    Category NewCategory = NewUser.Categoriesaccount.FirstOrDefault(a => a.Id == SelectedMoneyAccount.Id);

                    foreach (var category in Categories)
                    {
                        if (category.Id == CategoryId)
                        {
                            category.Budgeted = Convert.ToInt32(NewBudgeted);
                            category.Color = NewColor.Text;
                            category.Icon = NewIcon.Text;
                            category.Name = NewName;
                            category.Userid = NewUser.Id;
                            category.Year = NewYear.Text;
                            category.Month = DateTime.ParseExact(NewMonth.Text, "MMMM", cultureInfo).Month;
                            category.Day = NewDay.Text;
                            category.AccountId = SelectedMoneyAccount.Id;
                            budgetManagerRepository.UpdateCategory(category);
                        }
                    }

                    if (OldAccount.Id != SelectedMoneyAccount.Id)
                    {
                        foreach (var category in Categories)
                        {
                            if (category.Id == OldAccount.Id)
                            {
                                category.Budgeted -= OldCount;
                                budgetManagerRepository.UpdateCategory(category);
                            }
                        }

                        NewCategory.Budgeted += Convert.ToInt32(NewBudgeted);
                        budgetManagerRepository.UpdateCategory(NewCategory);

                    }

                    NewCategory.Budgeted += (Convert.ToInt32(NewBudgeted) - OldCount);
                    budgetManagerRepository.UpdateCategory(NewCategory);
                    navigation.GoBack();
                }
                else
                {
                    ShowWarningMessage();
                }
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
                this.days = new ObservableCollection<Day>();
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
            this.Title = LocalizedStrings.GetString(LocalizedStringEnum.addIncomeTitle);
            this.users = budgetManagerRepository.GetUsers();
            this.categories = budgetManagerRepository.GetCategories();
            this.Colors = fillingIconCollections.Colors;
            this.Icons = fillingIconCollections.IncomeIcons;

            Category data = parameter as Category;

            if (data != null)
            {
                this.CategoryId = data.Id;
            }

            Category SelectedCategory = budgetManagerRepository.GetCategory(CategoryId);

            if (CategoryId != 0 && SelectedCategory.Type == "Income")
            {
                this.NewBudgeted = SelectedCategory.Budgeted.ToString();
                this.NewName = SelectedCategory.Name;
                this.NewColor = Colors.FirstOrDefault(chosenColor => chosenColor.Text == SelectedCategory.Color);
                this.NewIcon = Icons.FirstOrDefault(chosenIcon => chosenIcon.Text == SelectedCategory.Icon);
                this.NewMonth = Monthes.FirstOrDefault(chosenMonth => chosenMonth.Text == cultureInfo.DateTimeFormat.GetMonthName((int)SelectedCategory.Month));
                this.NewYear = Years.FirstOrDefault(chosenYear => chosenYear.Text == SelectedCategory.Year);
                this.NewDay = NewMonth.Dayscount.FirstOrDefault(chosenDay => chosenDay.Text == SelectedCategory.Day);
                this.NewUser = Users.FirstOrDefault(chosenUser => chosenUser.Id == SelectedCategory.Userid);
                this.NewUser.Categoriesaccount = budgetManagerRepository.GetCategoryByType(NewUser.Id, "Account", System.DateTime.Now.Month, currentTime.Year);
                this.SelectedMoneyAccount = NewUser.Categoriesaccount.FirstOrDefault(a => a.Id == SelectedCategory.AccountId);
                this.oldCount = SelectedCategory.Budgeted;
                this.OldAccount = SelectedMoneyAccount;
            }
            else
            {
                this.NewBudgeted = string.Empty;
                this.NewName = string.Empty;
                this.NewColor = Colors.FirstOrDefault(color => color.Text == "");
                this.NewIcon = Icons.FirstOrDefault(icon => icon.Text == "");
                this.NewMonth = Monthes.FirstOrDefault(chosenMonth => chosenMonth.Text == cultureInfo.DateTimeFormat.GetMonthName(System.DateTime.Now.Month));
                this.NewYear = Years.FirstOrDefault(chosenYear => chosenYear.Text == currentTime.Year);
                this.NewDay = NewMonth.Dayscount.FirstOrDefault(chosenDay => chosenDay.Text == currentTime.Day);

                if (Users.Count != 0)
                {
                    this.NewUser = Users.FirstOrDefault(chosenUser => chosenUser.IsSelected == "Main");
                    this.NewUser.Categoriesaccount = budgetManagerRepository.GetCategoryByType(NewUser.Id, "Account",
                        System.DateTime.Now.Month, currentTime.Year);
                }

                this.oldCount = 0;

            }
        }

        #endregion

    }
}