using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace BudgetManager.Models
{
    public class FillingCollections:BaseModel
    {
        #region Basic Fields

        private List<Color> colors;
        private List<Icon> userIcons;
        private List<Icon> expenseIcons;
        private List<Icon> incomeIcons;
        private List<Icon> subcategoryIcons;
        private List<Icon> accountIcons;

        #endregion

        public  FillingCollections()
        {
            //TODO:fix Black Color
            colors = new List<Color>() {
            new Color() { Image = "/Assets/Colors/red.jpg", Text = "Red" },
            new Color() { Image = "/Assets/Colors/AquaBlue.jpg", Text = "Aqua" },
            new Color() { Image = "/Assets/Colors/purple.jpg", Text = "Purple" },
            new Color() { Image = "/Assets/Colors/green.jpg", Text = "Green" },
            new Color() { Image = "/Assets/Colors/black.jpg", Text = "Black" },
            new Color() { Image = "/Assets/Colors/violet.jpg", Text = "Violet" },
            new Color() { Image = "/Assets/Colors/brown.jpg", Text = "Brown" },
            new Color() { Image = "/Assets/Colors/orange.jpg", Text = "Orange" },
            new Color() { Image = "/Assets/Colors/blue.jpg", Text = "Blue" },
            new Color() { Image = "/Assets/Colors/gray.jpg", Text = "Gray" }

        };

            userIcons = new List<Icon>(){
            new Icon() {  Text = "/Assets/IconUser/icon.png" },
            new Icon() {  Text = "/Assets/IconUser/icon1.png" },
            new Icon() {  Text = "/Assets/IconUser/female.png" },
            new Icon() {  Text = "/Assets/IconUser/female1.png" },
            new Icon() {  Text = "/Assets/IconUser/female3.png" },
            new Icon() {  Text = "/Assets/IconUser/male2.png" },
             new Icon() {  Text = "/Assets/IconUser/male3.png" }
        };

            expenseIcons = new List<Icon>(){
            new Icon() { Text = "/Assets/IconExpense/clothes.png" },
            new Icon() { Text = "/Assets/IconExpense/house.png" },
            new Icon() { Text = "/Assets/IconExpense/meal.png" },
            new Icon() {  Text = "/Assets/IconExpense/phone.png" },  
            new Icon() {  Text = "/Assets/IconExpense/plane.png" },
            new Icon() {  Text = "/Assets/IconExpense/cart.png" },
            new Icon() {  Text = "/Assets/IconExpense/devices.png" },
            new Icon() {  Text = "/Assets/IconExpense/equipment.png" },
            new Icon() {  Text = "/Assets/IconExpense/internet.png" },
            new Icon() {  Text = "/Assets/IconExpense/school.png" },
            new Icon() {  Text = "/Assets/IconExpense/ticket.png" },
            new Icon() {  Text = "/Assets/IconExpense/gym.png" },
            new Icon() {  Text = "/Assets/IconExpense/car.png" },
            new Icon() {  Text = "/Assets/IconExpense/bicycle.png" },
            new Icon() {  Text = "/Assets/IconExpense/medicine.png" },
            new Icon() {  Text = "/Assets/IconExpense/barber.png" },
        };

            incomeIcons = new List<Icon>(){
            new Icon() {  Text = "/Assets/IconIncome/iconshop.png" },
            new Icon() {  Text = "/Assets/IconIncome/iconcoin.png" },
            new Icon() { Text = "/Assets/IconIncome/iconmoney.png" },
            new Icon() { Text = "/Assets/IconIncome/family.png" },
            new Icon() { Text = "/Assets/IconIncome/Sale.png" },
            new Icon() { Text = "/Assets/IconIncome/present.png" },
            new Icon() { Text = "/Assets/IconIncome/book.png" },
            new Icon() { Text = "/Assets/IconIncome/borrow.png" },
            new Icon() { Text = "/Assets/IconIncome/bank.png" },
             new Icon() { Text = "/Assets/IconIncome/mall.png" }
        };

            accountIcons = new List<Icon>(){
            new Icon() { Text = "/Assets/IconAccount/creditcard.png"},
            new Icon() {  Text = "/Assets/IconAccount/wallet.png" },
            new Icon() { Text = "/Assets/IconAccount/card.png"},
            new Icon() {  Text = "/Assets/IconAccount/card1.png" },
            new Icon() { Text = "/Assets/IconAccount/credit.png"},
            new Icon() {  Text = "/Assets/IconAccount/wallet1.png" },
            new Icon() { Text = "/Assets/IconAccount/mastercard.png"},
            new Icon() {  Text = "/Assets/IconAccount/piggy.png" },
            new Icon() { Text = "/Assets/IconAccount/wallet2.png"},
            new Icon() {  Text = "/Assets/IconAccount/wallet3.png" },
            new Icon() {  Text = "/Assets/IconAccount/bed.png" }
        };

            subcategoryIcons = new List<Icon>(){
             new Icon() { Text = "/Assets/IconExpense/dress.png" },
            new Icon() { Text = "/Assets/IconExpense/sale.png" },
            new Icon() { Text = "/Assets/IconExpense/meal.png" },
            new Icon() {  Text = "/Assets/IconExpense/house.png" },  
            new Icon() {  Text = "/Assets/IconExpense/ticket.png" },
            new Icon() {  Text = "/Assets/IconExpense/cart.png" },
            new Icon() {  Text = "/Assets/IconExpense/tv.png" },
            new Icon() {  Text = "/Assets/IconExpense/water.png" },
            new Icon() {  Text = "/Assets/IconExpense/internet.png" },
            new Icon() {  Text = "/Assets/IconExpense/jeans.png" },
            new Icon() {  Text = "/Assets/IconExpense/water.png" },
            new Icon() {  Text = "/Assets/IconExpense/product.png" },
            new Icon() {  Text = "/Assets/IconExpense/light.png" },
            new Icon() {  Text = "/Assets/IconExpense/gas.png" },
            new Icon() {  Text = "/Assets/IconExpense/cake.png" },
            new Icon() {  Text = "/Assets/IconExpense/travel.png" },
        };

        }

        #region Properties

        public List<Color> Colors
        {
            get { return this.colors; }
            set
            {
                if (value != this.colors)
                {
                    this.colors = value;
                    OnPropertyChanged();
                }
            }
        }

        public List<Icon> UserIcons
        {
            get { return this.userIcons; }
            set
            {
                if (value != this.userIcons)
                {
                    this.userIcons = value;
                    OnPropertyChanged();
                }
            }
        }

        public List<Icon> ExpenseIcons
        {
            get { return this.expenseIcons; }
            set
            {
                if (value != this.expenseIcons)
                {
                    this.expenseIcons = value;
                    OnPropertyChanged();
                }
            }
        }

        public List<Icon> IncomeIcons
        {
            get { return this.incomeIcons; }
            set
            {
                if (value != this.incomeIcons)
                {
                    this.incomeIcons = value;
                    OnPropertyChanged();
                }
            }
        }

        public List<Icon> SubcategoryIcons
        {
            get { return this.subcategoryIcons; }
            set
            {
                if (value != this.subcategoryIcons)
                {
                    this.subcategoryIcons = value;
                    OnPropertyChanged();
                }
            }
        }

        public List<Icon> AccountIcons
        {
            get { return this.accountIcons; }
            set
            {
                if (value != this.accountIcons)
                {
                    this.accountIcons = value;
                    OnPropertyChanged();
                }
            }
        }


        #endregion

    }
}
