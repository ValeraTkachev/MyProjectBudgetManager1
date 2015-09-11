namespace BudgetManager.Models
{
    using System.Collections.ObjectModel;


    public class Month : BaseModel
    {

        #region Fields

        private string text;
        private ObservableCollection<Day> daysCount;
        private long monthNumber;

        #endregion

        #region Properties

        public string Text
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

        public ObservableCollection<Day> Dayscount
        {
            get { return this.daysCount; }
            set
            {
                if (this.daysCount != value)
                {
                    this.daysCount = value;
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
