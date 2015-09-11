using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using BudgetManager.Shared;
using Windows.UI.Xaml;

namespace BudgetManager.WindowsBudget.Behaviors
{
    public class GridViewHeight:Behavior<GridView>
    {
        protected override void OnAttached()
        {
            this.AssociatedObject.Loaded += OnGridViewLoadedHeight;
        }

        private void OnGridViewLoadedHeight(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            double windowHeight = Window.Current.Bounds.Height;
            this.AssociatedObject.Height = windowHeight / 1.65;
        }

        protected override void OnDetached()
        {
            this.AssociatedObject.Loaded -= OnGridViewLoadedHeight;
        }
    }
}
