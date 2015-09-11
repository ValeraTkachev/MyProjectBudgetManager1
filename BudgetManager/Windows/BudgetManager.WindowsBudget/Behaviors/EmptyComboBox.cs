using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using BudgetManager.Shared;
using Windows.UI.Xaml;


namespace BudgetManager.WindowsBudget.Behaviors
{
    public class EmptyComboBox : Behavior<ComboBox>
    {
        protected override void OnAttached()
        {
            this.AssociatedObject.Loaded += OnEmptyComboBoxLoad;
        }

        private void OnEmptyComboBoxLoad(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.AssociatedObject.SelectedItem = null;

        }

        protected override void OnDetached()
        {
            this.AssociatedObject.Loaded -= OnEmptyComboBoxLoad;
        }

    }
}
