namespace BudgetManager.Models
{
    using System.Collections.ObjectModel;

    public class User : BaseModel
    {
        #region Fields

        private long id;
        private string name;
        private string color;
        private string icon;
        private long income;
        private long expense;
        private long rest;
        private double restPercent;
        private double expensePercent;
        private bool visibility;
        private string isSelected;
        private ObservableCollection<Category> categoriesaccount;
        private ObservableCollection<Category> categoriesincome;
        private ObservableCollection<Category> categoriesexpense;
        private ObservableCollection<Transaction> transactions;
        private ObservableCollection<Category> categories;
        private ObservableCollection<Category> subcategories;

        #endregion

        #region Properties

        public long Id
        {
            get { return this.id; }
            set
            {
                if (this.id != value)
                {
                    this.id = value;
                    OnPropertyChanged();
                }
            }
        }


        public string Name
        {
            get { return this.name; }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Color
        {
            get { return this.color; }
            set
            {
                if (this.color != value)
                {
                    this.color = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Icon
        {
            get { return this.icon; }
            set
            {
                if (this.icon != value)
                {
                    this.icon = value;
                    OnPropertyChanged();
                }
            }
        }

        public long Income
        {
            get { return this.income; }
            set
            {
                if (this.income != value)
                {
                    this.income = value;
                    OnPropertyChanged();
                }
            }
        }

        public string IsSelected
        {
            get { return this.isSelected; }
            set
            {
                if (this.isSelected != value)
                {
                    this.isSelected = value;
                    OnPropertyChanged();
                }
            }
        }


        public double RestPercent
        {
            get { return this.restPercent; }
            set
            {
                if (this.restPercent != value)
                {
                    this.restPercent = value;
                    OnPropertyChanged();
                }
            }
        }

        public double ExpensePercent
        {
            get { return this.expensePercent; }
            set
            {
                if (this.expensePercent != value)
                {
                    this.expensePercent = value;
                    OnPropertyChanged();
                }
            }
        }

        public long Expense
        {
            get { return this.expense; }
            set
            {
                if (this.expense != value)
                {
                    this.expense = value;
                    OnPropertyChanged();
                }
            }
        }

        public long Rest
        {
            get { return this.rest; }
            set
            {
                if (this.rest != value)
                {
                    this.rest = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool Visibility
        {
            get { return this.visibility; }
            set
            {
                if (this.visibility != value)
                {
                    this.visibility = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Category> Categoriesaccount
        {
            get { return this.categoriesaccount; }
            set
            {
                if (this.categoriesaccount != value)
                {
                    this.categoriesaccount = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Category> Categoriesincome
        {
            get { return this.categoriesincome; }
            set
            {
                if (this.categoriesincome != value)
                {
                    this.categoriesincome = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Category> Categoriesexpense
        {
            get { return this.categoriesexpense; }
            set
            {
                if (this.categoriesexpense != value)
                {
                    this.categoriesexpense = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Category> Categories
        {
            get { return this.categories; }
            set
            {
                if (this.categories != value)
                {
                    this.categories = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Transaction> Transactions
        {
            get { return this.transactions; }
            set
            {
                if (this.transactions != value)
                {
                    this.transactions = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Category> Subcategories
        {
            get { return this.subcategories; }
            set
            {
                if (this.subcategories != value)
                {
                    this.subcategories = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion
    }
}
