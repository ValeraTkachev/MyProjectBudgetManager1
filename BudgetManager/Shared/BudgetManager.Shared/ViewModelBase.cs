namespace BudgetManager.Shared
{
    using System.ComponentModel;
    using BudgetManager.DAL;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using BudgetManager.Models;
    using GalaSoft.MvvmLight.Views;
    using BudgetManager.Core;

    public abstract class ViewModelBase : BaseModel,INavigable
    {
        public INavigationService navigation;
        public BudgetManagerRepository budgetManagerRepository = new BudgetManagerRepository("BudgetManagerDB.db");
        public FillingCollections fillingIconCollections = new FillingCollections();
        public DateTime currentTime = DateTime.Now;
        public CultureInfo cultureInfo = new CultureInfo(LocalizedStrings.GetString(LocalizedStringEnum.CultureInfo));
        public abstract void NavigateTo(object parameter);      
    }
}
