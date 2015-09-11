namespace BudgetManager.WindowsBudget.Views.Controls
{

    using System.Collections.ObjectModel;
    using System.Linq;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    public class ExtendedTextBoxForNames : TextBox
    {

        public ExtendedTextBoxForNames()
        {
            this.MaxLength = 25;
        }

    }
}
