using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using BudgetManager.Shared;
using Windows.UI.Xaml;

namespace BudgetManager.WindowsBudget.Behaviors
{
    public class GridViewHeightAndWidth : Behavior<GridView>
    {

        protected override void OnAttached()
        {
            this.AssociatedObject.Loaded += OnGridViewLoadedHeight;
        }

        private void OnGridViewLoadedHeight(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            double windowHeight = Window.Current.Bounds.Height;
            double windowWidth = Window.Current.Bounds.Width;
            this.AssociatedObject.Height = windowHeight / 1.5;
            this.AssociatedObject.Width = windowWidth-120;
        }

        protected override void OnDetached()
        {
            this.AssociatedObject.Loaded -= OnGridViewLoadedHeight;
        }

    }
}
