using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using BudgetManager.Shared;
using Windows.UI.Xaml;


namespace BudgetManager.WindowsBudget.Behaviors
{

    public class NumericTextBox : Behavior<TextBox>
    {

        protected override void OnAttached()
        {
            this.AssociatedObject.TextChanged += OnTextBoxNumericTextChanged;
        }

        private void OnTextBoxNumericTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.MaxLength = 5;
            Int32 selectionStart = textBox.SelectionStart;
            Int32 selectionLength = textBox.SelectionLength;
            String newText = String.Empty;
            int count = 0;
            foreach (Char c in textBox.Text.ToCharArray())
            {
                if (Char.IsDigit(c) || Char.IsControl(c) || (c == '.' && count == 0))
                {
                    newText += c;
                    if (c == '.')
                        count += 1;
                }
            }
            textBox.Text = newText;
            textBox.SelectionStart = selectionStart <= textBox.Text.Length ? selectionStart : textBox.Text.Length;
        }

        protected override void OnDetached()
        {
            this.AssociatedObject.TextChanged -= OnTextBoxNumericTextChanged;
        }

    }
}
