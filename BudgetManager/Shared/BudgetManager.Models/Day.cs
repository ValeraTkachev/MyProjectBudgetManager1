
using System;
namespace BudgetManager.Models
{
    public class Day : BaseModel
    {
       
        #region Fields

        private long text;
        private string fullDate;
        private long income;
        private long expense;
        private long planned;
     

        #endregion

        #region

        public long Text
        {
            get { return this.text; }
            set
            {
                if (this.text != value)
                {
                    this.text = value;
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

        public long Planned
        {
            get { return this.planned; }
            set
            {
                if (this.planned != value)
                {
                    this.planned = value;
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

        public string FullDate
        {
            get { return this.fullDate; }
            set
            {
                if (this.fullDate != value)
                {
                    this.fullDate = value;
                    OnPropertyChanged();
                }
            }
        }

      

     



        #endregion
    }
}
