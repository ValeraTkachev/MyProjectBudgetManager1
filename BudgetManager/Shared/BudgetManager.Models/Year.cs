namespace BudgetManager.Models
{
    public class Year : BaseModel
    {
        #region Fields

        private long text;

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

        #endregion
    }
}
