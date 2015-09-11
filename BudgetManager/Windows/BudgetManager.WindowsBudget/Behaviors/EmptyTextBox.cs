using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using BudgetManager.Shared;
using Windows.UI.Xaml;


namespace BudgetManager.WindowsBudget.Behaviors
{
    public class EmptyTextBox : Behavior<TextBox>
    {

        protected override void OnAttached()
        {
            this.AssociatedObject.Loaded += OnTextBoxEmptyLoad;
        }


        private void OnTextBoxEmptyLoad(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.AssociatedObject.Text = string.Empty;

        }

        protected override void OnDetached()
        {
            this.AssociatedObject.Loaded -= OnTextBoxEmptyLoad;
        }

    }
}
