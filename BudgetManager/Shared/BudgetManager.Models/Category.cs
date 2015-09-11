
namespace BudgetManager.Models
{
    public class Category : BaseModel
    {  
        #region Fields

        private long id;
        private string name;
        private long budgeted;
        private long remaining;
        private string icon;
        private string color;
        private long day;
        private long month;
        private long year;
        private long userid;
        private long accountId;
        private string type;
        private double percent;
        private double restPercent;
        private long statisticType;
        private long rest;
        private string textColor;
        private string strokeColor;
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

        public long Budgeted
        {
            get { return this.budgeted; }
            set
            {
                if (this.budgeted != value)
                {
                    this.budgeted = value;
                    OnPropertyChanged();
                }
            }
        }

        public long Remaining
        {
            get { return this.remaining; }
            set
            {
                if (this.remaining != value)
                {
                    this.remaining = value;
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

        public long AccountId
        {
            get { return this.accountId; }
            set
            {
                if (this.accountId != value)
                {
                    this.accountId = value;
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


        public double Percent
        {
            get { return this.percent; }
            set
            {
                if (this.percent != value)
                {
                    this.percent = value;
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

        public string StrokeColor
        {
            get { return this.strokeColor; }
            set
            {
                if (this.strokeColor != value)
                {
                    this.strokeColor = value;
                    OnPropertyChanged();
                }
            }
        }

        public long StatisticType
        {
            get { return this.statisticType; }
            set
            {
                if (this.statisticType != value)
                {
                    this.statisticType = value;
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
