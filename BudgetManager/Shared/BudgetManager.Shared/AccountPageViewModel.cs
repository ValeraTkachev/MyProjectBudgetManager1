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
    using System.Threading.Tasks;
    using Windows.UI.Popups;

    public class AccountPageViewModel : ViewModelBase
    {

        #region Basic Fields

        private List<Color> colors;
        private List<Icon> icons;
        private ObservableCollection<User> users;

        #endregion

        #region Fields for Updating

        private User selectedUser;
        private string updateName;

        #endregion

        #region Fields for Adding

        private bool isPopupAddOpen;
        private bool isPopupUpdateOpen;
        private bool unchosen;
        private bool chosen;
        private Color selectedColor;
        private Icon selectedIcon;
        private Color selectedUpdateColor;
        private Icon selectedUpdateIcon;
        private string newName;
        private string title;

        #endregion

        #region Constructor

        public AccountPageViewModel(INavigationService navigatedService)
        {
            this.navigation = navigatedService;
            AddCommand = new RelayCommand(AddExecute);
            EditClickCommand = new RelayCommand(EditClickExecute);
            DeleteClickCommand = new RelayCommand(DeleteClickExecute);
            UpdateCommand = new RelayCommand(UpdateExecute);
            PopupAddWindowOpenCommand = new RelayCommand(OpenPopupAddWindowExecute);
            SelectedUserFocus = new RelayCommand(SelectedUserFocusExecute);
            ClosePopup = new RelayCommand(ClosePopupExecute);
            Back = new RelayCommand(() => navigation.GoBack());
        }

        #endregion

        #region Basic Properties


        public List<Color> Colors
        {
            get { return this.colors; }
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

        #endregion

        #region Properties For Updating

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


        public string UpdateName
        {
            get { return this.updateName; }
            set
            {
                if (value != this.updateName)
                {
                    this.updateName = value; 
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region Properties For Adding

        public bool IsPopupAddOpen
        {
            get { return this.isPopupAddOpen; }
            set
            {
                if (value != this.isPopupAddOpen)
                {
                    this.isPopupAddOpen = value;
                    OnPropertyChanged();
                }
            }

        }

        public bool IsPopupUpdateOpen
        {
            get 
            { 
                return this.isPopupUpdateOpen; 
            }
            set
            {
                if (value != this.isPopupUpdateOpen)
                {
                    this.isPopupUpdateOpen = value; 
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

        public bool Chosen
        {
            get { return this.chosen; }
            set
            {
                if (value != this.chosen)
                {
                    this.chosen = value;
                    OnPropertyChanged();
                }
            }
        }

        public Color SelectedColor
        {
            get { return this.selectedColor; }
            set
            {
                if (value != this.selectedColor)
                {
                    this.selectedColor = value;
                    OnPropertyChanged();
                }
            }
        }

        public Icon SelectedIcon
        {
            get { return this.selectedIcon; }
            set
            {
                if (value != this.selectedIcon)
                {
                    this.selectedIcon = value;
                    OnPropertyChanged();
                }
            }
        }

        public Color SelectedUpdateColor
        {
            get { return this.selectedUpdateColor; }
            set
            {
                if (value != this.selectedUpdateColor)
                {
                    this.selectedUpdateColor = value;
                    OnPropertyChanged();
                }
            }
        }

        public Icon SelectedUpdateIcon
        {
            get { return this.selectedUpdateIcon; }
            set
            {
                if (value != this.selectedUpdateIcon)
                {
                    this.selectedUpdateIcon = value;
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
        public RelayCommand UpdateCommand { get; set; }
        public RelayCommand EditClickCommand { get; set; }
        public RelayCommand DeleteClickCommand { get; set; }
        public RelayCommand PopupAddWindowOpenCommand { get; set; }
        public RelayCommand SelectedUserFocus { get; set; }
        public RelayCommand ClosePopup { get; set; }

        #endregion

        #region Methods

        private void AddExecute()
        {
            if (!string.IsNullOrWhiteSpace(NewName) 
                && SelectedColor != null 
                && SelectedIcon != null)
            {
                var user = new User { Name = newName, Color = SelectedColor.Text, Icon = SelectedIcon.Text, IsSelected = "Main" };
                budgetManagerRepository.InsertUser(user);
                Users = budgetManagerRepository.GetUsers();
            }
            else
            {
                ShowWarningMessage();
            }

            IsPopupAddOpen = !IsPopupAddOpen;
        }

        private void EditClickExecute()
        {
            this.SelectedUpdateIcon = Icons.FirstOrDefault(chosenIcon => chosenIcon.Text == SelectedUser.Icon);
            SelectedUpdateColor = Colors.FirstOrDefault(chosenColor => chosenColor.Text == SelectedUser.Color);
            UpdateName = SelectedUser.Name;
            IsPopupUpdateOpen = !IsPopupUpdateOpen;
        }

        private void DeleteClickExecute()
        {
            if (SelectedUser.Id != 0)
            {
                budgetManagerRepository.DeleteCategorybyUser(SelectedUser.Id);
                budgetManagerRepository.DeleteTransactionbyUser(SelectedUser.Id);
                budgetManagerRepository.DeleteUser(SelectedUser.Id);
                Users = budgetManagerRepository.GetUsers();

            }

            SelectedUser = new User();
        }

        private void UpdateExecute()
        {
            if (!string.IsNullOrWhiteSpace(UpdateName))
            {
                User user = new User { Name = UpdateName, Color = SelectedUpdateColor.Text, Icon = SelectedUpdateIcon.Text, Id = SelectedUser.Id };
                budgetManagerRepository.UpdateUser(user);
                Users = budgetManagerRepository.GetUsers();
            }
            else
            {
                ShowWarningMessage();
            }

            IsPopupUpdateOpen = !IsPopupUpdateOpen;
        }

        private void OpenPopupAddWindowExecute()
        {
            IsPopupAddOpen = !IsPopupAddOpen;
            this.NewName = string.Empty;
        }

        private void ClosePopupExecute()
        {
            IsPopupAddOpen = false;
            this.NewName = string.Empty;
        }

        private void SelectedUserFocusExecute()
        {
            if (SelectedUser != null)
            {
                Chosen = false;
                Unchosen = true;
            }
            else {
                Chosen = true;
                Unchosen = false;
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
            this.Title = LocalizedStrings.GetString(LocalizedStringEnum.accountTitle);
            IsPopupAddOpen = false;
            IsPopupUpdateOpen = false;
            this.users = budgetManagerRepository.GetUsers();
            this.chosen = true;
            this.unchosen = false;
            this.colors = fillingIconCollections.Colors;
            this.icons = fillingIconCollections.UserIcons;

        }

        #endregion
    }
}
