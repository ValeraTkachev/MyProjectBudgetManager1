namespace BudgetManager.Shared
{

    using System;
    using BudgetManager.Models;
    using BudgetManager.DAL;
    using System.Windows.Input;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using BudgetManager.Core;
    using System.Linq;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Views;
    using System.Threading.Tasks;
    using Windows.UI.Popups;

    public class EditTransactionViewModel : ViewModelBase
    {

        #region Fields
        private ObservableCollection<User> users;
        private ObservableCollection<Day> days;
        private ObservableCollection<Month> monthes;
        private ObservableCollection<Year> years;
        private ObservableCollection<Category> categories;
        private ObservableCollection<Transaction> transactions;
        private ObservableCollection<Category> categoriesSender;
        private ObservableCollection<Category> categoriesReciever;
        private Category tempSenderCategory;
        private Category tempRecieverCategory;
        private Transaction selectedtransaction;
        private User newUser;
        private Category categorySender;
        private Category categoryReciever;
        private long transactionId;
        private long oldCount;
        private Day newDay;
        private Month newMonth;
        private Year newYear;
        private string newCount;
        private string title;

        #endregion

        #region Constructor

        public EditTransactionViewModel(INavigationService navigatedService)
        {
            this.navigation = navigatedService;
            users = budgetManagerRepository.GetUsers();
            NewUser = Users.FirstOrDefault(chosenUser => chosenUser.IsSelected == "Main");
            transactions = budgetManagerRepository.GetTransactions();
            categories = budgetManagerRepository.GetCategories();
            monthes = new ObservableCollection<Month>();
            years = new ObservableCollection<Year>();

            GetDatesColletions();
            UpdateCommand = new RelayCommand(UpdateExecute);
            SelectionUserChange = new RelayCommand(ChangeUser);
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

        public ObservableCollection<Category> CategoriesSender
        {
            get { return this.categoriesSender; }
            set
            {
                if (value != this.categoriesSender)
                {
                    this.categoriesSender = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Category> CategoriesReciever
        {
            get { return this.categoriesReciever; }
            set
            {
                if (value != this.categoriesSender)
                {
                    this.categoriesReciever = value;
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

        public long TransactionId
        {
            get { return this.transactionId; }
            set
            {
                if (value != this.transactionId)
                {
                    this.transactionId = value;
                    OnPropertyChanged();
                }
            }
        }

        public Category CategoryReciever
        {
            get { return this.categoryReciever; }
            set
            {
                if (value != this.categoryReciever)
                {
                    this.categoryReciever = value;
                    OnPropertyChanged();
                }
            }
        }

        public Category CategorySender
        {
            get { return this.categorySender; }
            set
            {
                if (value != this.categorySender)
                {
                    this.categorySender = value;
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

        public Transaction SelectedTransaction
        {
            get { return this.selectedtransaction; }
            set
            {
                if (value != this.selectedtransaction)
                {
                    this.selectedtransaction = value;
                    OnPropertyChanged();
                }
            }
        }

        public Category TempRecieverCategory
        {
            get { return this.tempRecieverCategory; }
            set
            {
                if (value != this.tempRecieverCategory)
                {
                    this.tempRecieverCategory = value;
                    OnPropertyChanged();
                }
            }
        }

        public Category TempSenderCategory
        {
            get { return this.tempSenderCategory; }
            set
            {
                if (value != this.tempSenderCategory)
                {
                    this.tempSenderCategory = value;
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
        public RelayCommand UpdateCommand { get; set; }
        public RelayCommand SelectionUserChange { get; set; }

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



        private void UpdateExecute()
        {

            foreach (var searchingtransaction in Transactions)
            {
                if (searchingtransaction.Id == TransactionId)
                {
                    if (!string.IsNullOrWhiteSpace(SelectedTransaction.Name)
                        && CategorySender != null
                        && CategoryReciever != null
                        && !string.IsNullOrWhiteSpace(NewCount)
                        && Convert.ToInt32(NewCount) != 0
                        && NewUser != null
                        && NewDay != null
                        && NewYear != null
                        && NewMonth != null)
                    {
                        Category OldAccount = budgetManagerRepository.GetCategory(searchingtransaction.Accounttype);
                        Category OldCategory = budgetManagerRepository.GetCategory(searchingtransaction.Categoryid);


                        searchingtransaction.Day = NewDay.Text;
                        searchingtransaction.Month = DateTime.ParseExact(NewMonth.Text, "MMMM", cultureInfo).Month;
                        searchingtransaction.Year = NewYear.Text;
                        searchingtransaction.Accounttype = CategorySender.Id;
                        searchingtransaction.Categoryid = CategoryReciever.Id;
                        searchingtransaction.Userid = NewUser.Id;
                        searchingtransaction.Count = Convert.ToInt32(NewCount);
                        searchingtransaction.Name = SelectedTransaction.Name;
                        budgetManagerRepository.UpdateTransaction(searchingtransaction);


                        foreach (var searchingcategory in Categories)
                        {
                            OperationWithTransaction op;

                            if (searchingcategory.Id == searchingtransaction.Accounttype && searchingtransaction.Accounttype == OldAccount.Id
                                || searchingcategory.Id == searchingtransaction.Categoryid && searchingtransaction.Categoryid == OldCategory.Id)
                            {
                                if (searchingcategory.Type == "Expense" || searchingcategory.Type == "Account" || searchingcategory.Type == "Subcategory")
                                {
                                    if (searchingcategory.Id == searchingtransaction.Accounttype)
                                    {
                                        op = new OperationWithTransaction(Sub);
                                    }
                                    else
                                    {
                                        op = new OperationWithTransaction(Sum);
                                    }


                                    if (searchingcategory.Type == "Account")
                                    {
                                        searchingcategory.Budgeted = op(searchingcategory.Budgeted, searchingtransaction.Count - OldCount);

                                    }
                                    else
                                    {

                                        if (searchingcategory.Type == "Subcategory")
                                        {
                                            foreach (var category in Categories)
                                            {
                                                if (category.Id == CategoryReciever.AccountId)
                                                {
                                                    category.Remaining += (searchingtransaction.Count - OldCount);
                                                    budgetManagerRepository.UpdateCategory(category);
                                                }
                                            }
                                        }
                                        searchingcategory.Remaining = op(searchingcategory.Remaining, searchingtransaction.Count - OldCount);
                                    }

                                }
                                else
                                {
                                    op = new OperationWithTransaction(Sum);
                                    searchingcategory.Budgeted = op(searchingcategory.Budgeted, searchingtransaction.Count - OldCount);

                                }
                                budgetManagerRepository.UpdateCategory(searchingcategory);

                            }

                            else
                            {

                                if (searchingcategory.Id == searchingtransaction.Accounttype || searchingcategory.Id == OldAccount.Id)
                                {
                                    OperationWithTransaction operation;


                                    if (searchingcategory.Id == searchingtransaction.Accounttype)
                                    {
                                        if (searchingtransaction.Type == "Income")
                                        {
                                            operation = new OperationWithTransaction(Sum);

                                        }
                                        else
                                        {
                                            operation = new OperationWithTransaction(Sub);

                                        }
                                        searchingcategory.Budgeted = operation(searchingcategory.Budgeted, searchingtransaction.Count);
                                    }
                                    else if (searchingcategory.Id == OldAccount.Id)
                                    {
                                        if (searchingtransaction.Type == "Income")
                                        {
                                            operation = new OperationWithTransaction(Sub);

                                        }
                                        else
                                        {
                                            operation = new OperationWithTransaction(Sum);

                                        }

                                        searchingcategory.Budgeted = operation(searchingcategory.Budgeted, OldCount);

                                    }





                                    budgetManagerRepository.UpdateCategory(searchingcategory);

                                }

                                if (searchingcategory.Id == searchingtransaction.Categoryid || searchingcategory.Id == OldCategory.Id)
                                {

                                    if (searchingcategory.Type == "Expense" || searchingcategory.Type == "Account" || searchingcategory.Type == "Subcategory")
                                    {
                                        if (searchingcategory.Id == searchingtransaction.Accounttype)
                                        {
                                            op = new OperationWithTransaction(Sub);
                                        }
                                        else
                                        {
                                            if (searchingcategory.Id == OldCategory.Id)
                                            {
                                                op = new OperationWithTransaction(Sub);
                                            }
                                            else
                                            {
                                                op = new OperationWithTransaction(Sum);
                                            }
                                        }


                                        if (searchingcategory.Type == "Account")
                                        {
                                            searchingcategory.Budgeted = op(searchingcategory.Budgeted, searchingtransaction.Count - OldCount);


                                        }
                                        else
                                        {

                                            if (searchingcategory.Type == "Subcategory")
                                            {
                                                foreach (var category in Categories)
                                                {
                                                    if (category.Id == CategoryReciever.AccountId)
                                                    {
                                                        category.Remaining += (searchingtransaction.Count - OldCount);
                                                        budgetManagerRepository.UpdateCategory(category);
                                                    }
                                                }
                                            }
                                            if (searchingcategory.Id == OldCategory.Id)
                                            {
                                                searchingcategory.Remaining = op(searchingcategory.Remaining, OldCount);
                                            }
                                            else
                                            {
                                                searchingcategory.Remaining = op(searchingcategory.Remaining, searchingtransaction.Count);
                                            }
                                        }

                                    }
                                    else
                                    {
                                        if (searchingcategory.Id == OldCategory.Id)
                                        {
                                            op = new OperationWithTransaction(Sub);
                                            searchingcategory.Budgeted = op(searchingcategory.Budgeted, OldCount);
                                        }
                                        else
                                        {
                                            op = new OperationWithTransaction(Sum);
                                            searchingcategory.Budgeted = op(searchingcategory.Budgeted, searchingtransaction.Count);
                                        }

                                    }
                                    budgetManagerRepository.UpdateCategory(searchingcategory);
                                }

                            }

                        }
                        navigation.GoBack();
                    }
                    else 
                    {
                        ShowWarningMessage();
                    }
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

        private void ChangeUser()
        {
            CategoriesSender = new ObservableCollection<Category>(Categories.Where(chosenCategory =>
                chosenCategory.Type == TempSenderCategory.Type && chosenCategory.Userid == NewUser.Id));
            CategoriesReciever = new ObservableCollection<Category>(Categories.Where(chosenCategory =>
                chosenCategory.Type == TempRecieverCategory.Type && chosenCategory.Userid == NewUser.Id));
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
            this.Title = LocalizedStrings.GetString(LocalizedStringEnum.editTransactionTitle);
            Transaction data = parameter as Transaction;
            if (data != null)
            {
                this.TransactionId = data.Id;
                this.SelectedTransaction = budgetManagerRepository.GetTransaction(TransactionId);
            }

            this.NewCount = SelectedTransaction.Count.ToString();
            this.tempSenderCategory = budgetManagerRepository.GetCategory(selectedtransaction.Accounttype);

            this.CategoriesSender = new ObservableCollection<Category>(Categories.Where(chosenCategory => chosenCategory.Type == tempSenderCategory.Type
                && chosenCategory.Userid == NewUser.Id && chosenCategory.Month == tempSenderCategory.Month && chosenCategory.Year == tempSenderCategory.Year));
            this.CategorySender = CategoriesSender.FirstOrDefault(chosenCategory => chosenCategory.Id == SelectedTransaction.Accounttype);

            this.tempRecieverCategory = budgetManagerRepository.GetCategory(selectedtransaction.Categoryid);

            if (TempRecieverCategory.Type == "Subcategory")
            {
                CategoriesReciever = new ObservableCollection<Category>(Categories.Where(chosenCategory => chosenCategory.Type == tempRecieverCategory.Type
                    && chosenCategory.Userid == NewUser.Id && chosenCategory.AccountId == tempRecieverCategory.AccountId && chosenCategory.Month == tempRecieverCategory.Month
                    && chosenCategory.Year == tempRecieverCategory.Year));
                Category MainCategory = budgetManagerRepository.GetCategory(tempRecieverCategory.AccountId);
                CategoriesReciever.Insert(0, MainCategory);

            }
            else
            {
                CategoriesReciever = new ObservableCollection<Category>(Categories.Where(chosenCategory => chosenCategory.Type == tempRecieverCategory.Type
                    && chosenCategory.Userid == NewUser.Id && chosenCategory.Month == tempRecieverCategory.Month
                    && chosenCategory.Year == tempRecieverCategory.Year));

            }

            this.CategoryReciever = CategoriesReciever.FirstOrDefault(chosenCategory => chosenCategory.Id == SelectedTransaction.Categoryid);
            this.NewMonth = Monthes.FirstOrDefault(chosenMonth => chosenMonth.Text == cultureInfo.DateTimeFormat.GetMonthName((int)SelectedTransaction.Month));
            this.NewYear = Years.FirstOrDefault(chosenYear => chosenYear.Text == SelectedTransaction.Year);
            this.NewDay = NewMonth.Dayscount.FirstOrDefault(chosenDay => chosenDay.Text == SelectedTransaction.Day);
            this.oldCount = SelectedTransaction.Count;
        }


        #endregion
    }
}
