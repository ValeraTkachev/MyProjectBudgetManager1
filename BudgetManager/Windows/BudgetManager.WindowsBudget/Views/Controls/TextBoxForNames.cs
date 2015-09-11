namespace BudgetManager.WindowsBudget.Views.Controls
{

    using System.Collections.ObjectModel;
    using System.Linq;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    public class TextBoxForNames : TextBox
    {
        public TextBoxForNames()
        {
            this.MaxLength = 10;
        }
    }

}
