
namespace BudgetManager.Models
{
    public class Color :BaseModel
    {
       
        #region Fields

        private string image;
        private string text;

        #endregion

        #region Properties

        public string Image
        {
            get { return this.image; }
            set
            {
                if (this.image != value)
                {
                    this.image = value;
                    OnPropertyChanged();
                }
            }
        }

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
