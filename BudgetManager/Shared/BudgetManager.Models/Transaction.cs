
namespace BudgetManager.Models
{
    public class Transaction :BaseModel
    {
        #region Fields

        private long id;
        private string name;
        private long count;
        private long accounttype;
        private long day;
        private long month;
        private long year;
        private long userid;
        private string type;
        private long categoryid;
        private string accountName;
        private string textColor;
        private string iconName;
        private string countExpense;
        private string countIncome;
        private long monthNumber;

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

        public long Count
        {
            get { return this.count; }
            set
            {
                if (this.count != value)
                {
                    this.count = value;
                    OnPropertyChanged();
                }
            }
        }

        public long Accounttype
        {
            get { return this.accounttype; }
            set
            {
                if (this.accounttype != value)
                {
                    this.accounttype = value;
                    OnPropertyChanged();
                }
            }
        }
      
        public long Day
        {
            get { return this.day; }
            set
            {
                if (this.day != value)
                {
                    this.day = value;
                    OnPropertyChanged();
                }
            }
        }

        public long Month
        {
            get { return this.month; }
            set
            {
                if (this.month != value)
                {
                    this.month = value;
                    OnPropertyChanged();
                }
            }
        }

        public long Year
        {
            get { return this.year; }
            set
            {
                if (this.year != value)
                {
                    this.year = value;
                    OnPropertyChanged();
                }
            }
        }

        public long Userid
        {
            get { return this.userid; }
            set
            {
                if (this.userid != value)
                {
                    this.userid = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Type
        {
            get { return this.type; }
            set
            {
                if (this.type != value)
                {
                    this.type = value;
                    OnPropertyChanged();
                }
            }
        }

        public long Categoryid
        {
            get { return this.categoryid; }
            set
            {
                if (this.categoryid != value)
                {
                    this.categoryid = value;
                    OnPropertyChanged();
                }
            }
        }

        public string AccountName
        {
            get { return this.accountName; }
            set
            {
                if (this.accountName != value)
                {
                    this.accountName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string TextColor
        {
            get { return this.textColor; }
            set
            {
                if (this.textColor != value)
                {
                    this.textColor = value;
                    OnPropertyChanged();
                }
            }
        }

        public string IconName
        {
            get { return this.iconName; }
            set
            {
                if (this.iconName != value)
                {
                    this.iconName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string CountExpense
        {
            get { return this.countExpense; }
            set
            {
                if (this.countExpense != value)
                {
                    this.countExpense = value;
                    OnPropertyChanged();
                }
            }
        }

        public string CountIncome
        {
            get { return this.countIncome; }
            set
            {
                if (this.countIncome != value)
                {
                    this.countIncome = value;
                    OnPropertyChanged();
                }
            }
        }

        public long MonthNumber
        {
            get { return this.monthNumber; }
            set
            {
                if (this.monthNumber != value)
                {
                    this.monthNumber = value;
                    OnPropertyChanged();
                }
            }
        }
        
        #endregion
    }
}
