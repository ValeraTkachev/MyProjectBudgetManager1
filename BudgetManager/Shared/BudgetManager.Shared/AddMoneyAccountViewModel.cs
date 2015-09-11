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
    using GalaSoft.MvvmLight.Views;
    using GalaSoft.MvvmLight.Command;
    using System.Threading.Tasks;
    using Windows.UI.Popups;

    public class AddMoneyAccountViewModel : ViewModelBase
    {

        #region Fields

        private ObservableCollection<User> users;
        private ObservableCollection<Category> categories;
        private long categoryId;
        private Icon newIcon;
        private Color newColor;
        private string newName;
        private string newBudgeted;
        private User newUser;
        private string title;
     
        #endregion

        #region Constructor

        public AddMoneyAccountViewModel(INavigationService navigatedService)
        {
            this.navigation = navigatedService;
            AddCommand = new RelayCommand(AddExecute);
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

        private void AddExecute()
        {
            if (CategoryId == 0)
            {
                if (!string.IsNullOrWhiteSpace(NewName)
                    && NewColor != null && NewIcon != null
                    && !string.IsNullOrWhiteSpace(NewBudgeted.ToString())
                    && Convert.ToInt32(NewBudgeted) != 0
                    && NewUser != null)
                {
                    Category category = new Category
                    {
                        Name = newName,
                        Color = NewColor.Text,
                        Icon = NewIcon.Text,
                        Budgeted = Convert.ToInt32(NewBudgeted),
                        Userid = NewUser.Id,
                        Type = "Account",
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
            else
            {

                if (!string.IsNullOrWhiteSpace(NewName) 
                    && !string.IsNullOrWhiteSpace(NewBudgeted) 
                    && Convert.ToInt32(NewBudgeted) != 0)
                {
                    foreach (var category in Categories)
                    {
                        if (category.Id == CategoryId)
                        {
                            category.Budgeted = Convert.ToInt32(NewBudgeted);
                            category.Color = NewColor.Text;
                            category.Icon = NewIcon.Text;
                            category.Name = NewName;
                            category.Userid = NewUser.Id;
                            budgetManagerRepository.UpdateCategory(category);
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
            this.Title = LocalizedStrings.GetString(LocalizedStringEnum.addmoneyAccountTextBlock);
            this.users = budgetManagerRepository.GetUsers();
            this.categories = budgetManagerRepository.GetCategories();
            this.Colors = fillingIconCollections.Colors;
            this.Icons = fillingIconCollections.AccountIcons;

            Category data = parameter as Category;

            if (data != null)
            {
                this.CategoryId = data.Id;
            }

            Category SelectedCategory = budgetManagerRepository.GetCategory(CategoryId);

            if (CategoryId != 0 && SelectedCategory.Type == "Account")
            {

                this.NewBudgeted = SelectedCategory.Budgeted.ToString();
                this.NewName = SelectedCategory.Name;
                this.NewColor = Colors.FirstOrDefault(chosenColor => chosenColor.Text == SelectedCategory.Color);
                this.NewIcon = Icons.FirstOrDefault(chosenIcon => chosenIcon.Text == SelectedCategory.Icon);
                this.NewUser = Users.FirstOrDefault(chosenUser => chosenUser.Id == SelectedCategory.Userid);

            }
            else
            {

                this.NewBudgeted = string.Empty;
                this.NewName = string.Empty;
                this.NewColor = Colors.FirstOrDefault(color => color.Text == "");
                this.NewIcon = Icons.FirstOrDefault(icon => icon.Text == "");
                this.NewUser = Users.FirstOrDefault(chosenUser => chosenUser.IsSelected == "Main");
            }
        }
        #endregion
    }
}
