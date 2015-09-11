namespace BudgetManager.WindowsBudget.Views.Controls
{

    using System.Collections.ObjectModel;
    using System.Linq;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    public class MyListView : ListView
    {
        public ObservableCollection<object> BindableSelectedItems
        {
            get { return GetValue(BindableSelectedItemsProperty) as ObservableCollection<object>; }
            set { SetValue(BindableSelectedItemsProperty, value as ObservableCollection<object>); }
        }

        public static readonly DependencyProperty BindableSelectedItemsProperty =
            DependencyProperty.Register("BindableSelectedItems",
            typeof(ObservableCollection<object>), typeof(MyListView),
            new PropertyMetadata(null, (s, e) =>
            {
                (s as MyListView).SelectionChanged -= (s as MyListView).MyListView_SelectionChanged;
                (s as MyListView).SelectionChanged += (s as MyListView).MyListView_SelectionChanged;
            }));

        void MyListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BindableSelectedItems == null)
                return;
            foreach (var item in BindableSelectedItems.Where(x => !this.SelectedItems.Contains(x)).ToArray())
                BindableSelectedItems.Remove(item);
            foreach (var item in this.SelectedItems.Where(x => !BindableSelectedItems.Contains(x)))
                BindableSelectedItems.Add(item);
        }

    }
}
