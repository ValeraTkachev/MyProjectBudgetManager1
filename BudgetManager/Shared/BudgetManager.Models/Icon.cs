
namespace BudgetManager.Models
{
    public class Icon : BaseModel
    {
        
        #region Fields

        private string text;

        #endregion

        #region

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

        #endregion
    }
}
