
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

    public class AddSubcategoryViewModel : ViewModelBase
    {

        public AddSubcategoryViewModel(INavigationService navigatedService)
        {
            this.navigation = navigatedService;
            AddSubcategoryCommand = new RelayCommand(AddSubcategoryExecute);
            Back = new RelayCommand(() => navigation.GoBack());
        }

        #region Fields

        private long categoryId;
        private User selectedUser;
        private Icon newIcon;
        private Color newColor;
        private string newName;
        private string newBudgeted;
        private ObservableCollection<User> users;
        private string title;

        #endregion

        #region Basic Properties

        public Category FoundedCategory { get; set; }

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


        public List<Color> Colors { get; set; }

        public List<Icon> Icons { get; set; }

        #endregion

        #region Commands

        public RelayCommand AddSubcategoryCommand { get; set; }
        public RelayCommand Back { get; set; }

        #endregion

        #region Methods

        private void AddSubcategoryExecute()
        {
            if (FoundedCategory.Type != "Subcategory")
            {
                if (!string.IsNullOrWhiteSpace(NewName)
                    && NewColor != null && NewIcon != null 
                    && !string.IsNullOrWhiteSpace(NewBudgeted)
                    && Convert.ToInt32(NewBudgeted) != 0 
                    && SelectedUser != null && Categoryid != 0)
                {
                    Category subcategory = new Category
                    {
                        Name = newName,
                        Color = NewColor.Text,
                        Icon = NewIcon.Text,
                        Budgeted = Convert.ToInt32(NewBudgeted),
                        Userid = SelectedUser.Id,
                        Type = "Subcategory",
                        Remaining = 0,
                        Day = currentTime.Day,
                        Month = System.DateTime.Now.Month,
                        Year = currentTime.Year,
                        AccountId = FoundedCategory.Id
                    };
                    budgetManagerRepository.InsertCategory(subcategory);
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
                    && NewColor != null && NewIcon != null
                    && !string.IsNullOrWhiteSpace(NewBudgeted)
                    && Convert.ToInt32(NewBudgeted) != 0
                    && SelectedUser != null
                    && Categoryid != 0)
                {
                    FoundedCategory.Userid = SelectedUser.Id;
                    FoundedCategory.Color = NewColor.Text;
                    FoundedCategory.Icon = NewIcon.Text;
                    FoundedCategory.Budgeted = Convert.ToInt32(NewBudgeted);
                    FoundedCategory.Name = NewName;
                    budgetManagerRepository.UpdateCategory(FoundedCategory);
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
            this.Title = LocalizedStrings.GetString(LocalizedStringEnum.expenseTitle);
            this.Colors = fillingIconCollections.Colors;
            this.Icons = fillingIconCollections.SubcategoryIcons;
            this.users = budgetManagerRepository.GetUsers();
            Category data = parameter as Category;
            if (data != null)
            {
                this.categoryId = data.Id;
            }

            this.FoundedCategory = budgetManagerRepository.GetCategory(Categoryid);

            if (FoundedCategory.Type == "Subcategory" && categoryId != 0)
            {
                this.NewBudgeted = FoundedCategory.Budgeted.ToString();
                this.NewName = FoundedCategory.Name;
                this.NewColor = Colors.FirstOrDefault(chosenColor => chosenColor.Text == FoundedCategory.Color);
                this.NewIcon = Icons.FirstOrDefault(chosenIcon => chosenIcon.Text == FoundedCategory.Icon);
                this.SelectedUser = Users.FirstOrDefault(a => a.Id == FoundedCategory.Userid);
            }
            else
            {

                this.NewBudgeted = string.Empty;
                this.NewName = string.Empty;
                this.NewColor = Colors.FirstOrDefault(color => color.Text == "");
                this.NewIcon = Icons.FirstOrDefault(icon => icon.Text == "");
                this.SelectedUser = Users.FirstOrDefault(a => a.Id == FoundedCategory.Userid);
            }

            List<User> tempUsers = Users.ToList();
            foreach (var user in tempUsers)
            {
                if (user != SelectedUser)
                {
                    Users.Remove(user);
                }
            }

        }

        #endregion

    }
}
