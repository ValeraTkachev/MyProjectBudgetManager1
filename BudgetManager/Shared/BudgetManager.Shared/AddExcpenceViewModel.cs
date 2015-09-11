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
    using GalaSoft.MvvmLight.Views;
    using GalaSoft.MvvmLight.Command;
    using Windows.UI.Popups;
    using System.Threading.Tasks;


    public class AddExcpenceViewModel : ViewModelBase
    {
        #region Fields

        private ObservableCollection<User> users;
        private Category selectedMoneyAccount;
        private Icon newIcon;
        private Color newColor;
        private string newName;
        private int newBudgeted;
        private User newUser;
        private long categoryId;
        private string title;

        #endregion

        #region Constructor

        public AddExcpenceViewModel(INavigationService navigatedService)
        {
            this.navigation = navigatedService;

            AddCommand = new RelayCommand(AddCommandExecute);
            Back = new RelayCommand(() => navigation.GoBack());
        }

        #endregion

        #region Basic Properties

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

        public int NewBudgeted
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

        #endregion

        #region Methods

        private void AddCommandExecute()
        {
            if (!string.IsNullOrWhiteSpace(NewName)
                && NewColor != null
                && NewIcon != null
                && Convert.ToString(NewBudgeted) != null
                && NewBudgeted != 0)
            {
                Category category = new Category
                {
                    Name = newName,
                    Color = NewColor.Text,
                    Icon = NewIcon.Text,
                    Budgeted = newBudgeted,
                    Userid = NewUser.Id,
                    Type = "Expense",
                    Remaining = 0,
                    Day = currentTime.Day,
                    Month = System.DateTime.Now.Month,
                    Year = currentTime.Year
                };

                budgetManagerRepository.InsertCategory(category);
                navigation.GoBack();
            }
            else 
            {
                ShowWarningMessage();
            }

        }

        public override void NavigateTo(object parameter)
        {
            this.Title = LocalizedStrings.GetString(LocalizedStringEnum.expenseTitle);
            InputInfo();
            this.users = budgetManagerRepository.GetUsers();
            this.NewUser = Users.FirstOrDefault(chosenUser => chosenUser.IsSelected == "Main");

        }

        private void InputInfo()
        {
            this.Colors = fillingIconCollections.Colors;
            this.Icons = fillingIconCollections.ExpenseIcons;
        }

        private async Task ShowWarningMessage()
        {

            var dialog = new MessageDialog(LocalizedStrings.GetString(LocalizedStringEnum.WarningAddMessage),
                LocalizedStrings.GetString(LocalizedStringEnum.WarningAddTitle)
            );

            dialog.Commands.Add(new UICommand(LocalizedStrings.GetString(LocalizedStringEnum.Continue)));
            IUICommand iuiCommand = await dialog.ShowAsync();

        }

        #endregion

    }
}
