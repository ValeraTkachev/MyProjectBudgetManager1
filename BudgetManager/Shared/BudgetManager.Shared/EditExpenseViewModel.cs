namespace BudgetManager.Shared
{
    using BudgetManager.Models;
    using BudgetManager.DAL;
    using System.Windows.Input;
    using System.Collections.ObjectModel;
    using BudgetManager.Core;
    using System.Collections.Generic;
    using System.Linq;
    using GalaSoft.MvvmLight.Views;
    using GalaSoft.MvvmLight.Command;
    using System;
    using System.Threading.Tasks;
    using Windows.UI.Popups;

    public class EditExpenseViewModel : ViewModelBase
    {

        #region Fields

        private ObservableCollection<User> users;
        private List<Color> colors;
        private List<Icon> icons;
        private ObservableCollection<Category> categories;
        private Category selectedCategory;
        private Category selectedMoneyAccount;
        private Icon newIcon;
        private Color newColor;
        private User newUser;
        private long categoryId;
        private string newBudgeted;
        private string title;

        #endregion

        #region Constructor

        public EditExpenseViewModel(INavigationService navigatedService)
        {
            this.navigation = navigatedService;
            UpdateCommand = new RelayCommand(UpdateExecute);
            Back = new RelayCommand(() => navigation.GoBack());
        }

        #endregion

        #region Basic Properties

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

        public Category SelectedMoneyAccount
        {
            get { return selectedMoneyAccount; }
            set
            {
                if (value != this.selectedMoneyAccount)
                {
                    this.selectedMoneyAccount = value;
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

        public List<Color> Colors
        {
            get 
            { 
                return this.colors; 
            }
            set
            {
                if (value != this.colors)
                {
                    this.colors = value;
                    OnPropertyChanged();
                }
            }
        }

        public List<Icon> Icons
        {
            get { return this.icons; }
            set
            {
                if (value != this.icons)
                {
                    this.icons = value;
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

        #endregion

        #region Methods

        private void UpdateExecute()
        {
            if (!string.IsNullOrWhiteSpace(SelectedCategory.Name)
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
                        category.Name = SelectedCategory.Name;
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
            this.Title = LocalizedStrings.GetString(LocalizedStringEnum.editexpenseTitle);
            this.users = budgetManagerRepository.GetUsers();
            this.categories = budgetManagerRepository.GetCategories();
            this.colors = fillingIconCollections.Colors;
            this.icons = fillingIconCollections.ExpenseIcons;
            
            Category data = parameter as Category;

            if (data != null)
            {
                this.CategoryId = data.Id;
                this.SelectedCategory = budgetManagerRepository.GetCategory(CategoryId);
                this.NewColor = Colors.FirstOrDefault(chosenColor => chosenColor.Text == data.Color);
                this.NewIcon = Icons.FirstOrDefault(chosenIcon => chosenIcon.Text == data.Icon);
                this.NewUser = Users.FirstOrDefault(chosenUser => chosenUser.Id == data.Userid);
            }

            this.newBudgeted = SelectedCategory.Budgeted.ToString();
        }

        #endregion

    }
}
