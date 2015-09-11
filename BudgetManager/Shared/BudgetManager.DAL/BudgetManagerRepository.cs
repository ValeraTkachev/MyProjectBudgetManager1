namespace BudgetManager.DAL
{

    using SQLitePCL;
    using BudgetManager.Models;
    using System.Collections.ObjectModel;
    using System;


    public class BudgetManagerRepository
    {
        public SQLiteConnection con;

        public BudgetManagerRepository(string dbname)
        {
            this.con = new SQLiteConnection(dbname);
        }

        public void CreateTableCategory()
        {
            string sql = @"CREATE TABLE IF NOT EXISTS
                                Category(id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, 
                                        name VARCHAR(100),
                                        budgeted INTEGER,
                                        remaining INTEGER,
                                        icon VARCHAR(100),
                                        color VARCHAR(100),
                                        day  INTEGER,
                                        month INTEGER,
                                        year INTEGER,
                                        userid INTEGER,
                                        accountId INTEGER,
                                        type VARCHAR(100));";

            using (var statement = con.Prepare(sql))
            {
                statement.Step();
            }
        }

        public void InsertCategory(Category category)
        {
            using (var statement = con.Prepare("INSERT INTO Category(name, budgeted, remaining,icon,color,day,month,year,userid,accountId,type) VALUES (?,?,?,?,?,?,?,?,?,?,?)"))
            {
                statement.Bind(1, category.Name);
                statement.Bind(2, category.Budgeted);
                statement.Bind(3, category.Remaining);
                statement.Bind(4, category.Icon);
                statement.Bind(5, category.Color);
                statement.Bind(6, category.Day);
                statement.Bind(7, category.Month);
                statement.Bind(8, category.Year);
                statement.Bind(9, category.Userid);
                statement.Bind(10, category.AccountId);
                statement.Bind(11, category.Type);
                statement.Step();
            }
        }

        public ObservableCollection<Category> GetCategories()
        {
            ObservableCollection<Category> categories = new ObservableCollection<Category>();

            using (var statement = con.Prepare("SELECT id,name, budgeted, remaining,icon,color,day,month,year,userid,accountId,type FROM Category"))
            {
                while (statement.Step() == SQLiteResult.ROW)
                {
                    Category category = new Category();
                    category.Id = (long)statement[0];
                    category.Name = (string)statement[1];
                    category.Budgeted = (long)statement[2];
                    category.Remaining = (long)statement[3];
                    category.Icon = (string)statement[4];
                    category.Color = (string)statement[5];
                    category.Day = (long)statement[6];
                    category.Month = (long)statement[7];
                    category.Year = (long)statement[8];
                    category.Userid = (long)statement[9];
                    category.AccountId = (long)statement[10];
                    category.Type = (string)statement[11];
                    categories.Add(category);
                }
            }
            return categories;
        }

        public ObservableCollection<Category> GetCategoriesByUserId(long userid)
        {
            ObservableCollection<Category> categories = new ObservableCollection<Category>();

            using (var statement = con.Prepare("SELECT id,name, budgeted, remaining,icon,color,day,month,year,userid,accountId,type  FROM Category Where userid=?"))
            {
                statement.Bind(1, userid);

                while (statement.Step() == SQLiteResult.ROW)
                {
                    Category category = new Category();
                    category.Id = (long)statement[0];
                    category.Name = (string)statement[1];
                    category.Budgeted = (long)statement[2];
                    category.Remaining = (long)statement[3];
                    category.Icon = (string)statement[4];
                    category.Color = (string)statement[5];
                    category.Day = (long)statement[6];
                    category.Month = (long)statement[7];
                    category.Year = (long)statement[8];
                    category.Userid = (long)statement[9];
                    category.AccountId = (long)statement[10];
                    category.Type = (string)statement[11];
                    categories.Add(category);
                }
            }
            return categories;
        }

        public Category GetCategory(long id)
        {
            Category category = null;

            using (var statement = con.Prepare("SELECT id,name,budgeted, remaining,icon,color,day,month,year,userid,accountId,type FROM Category WHERE id=?"))
            {
                statement.Bind(1, id);
                if (statement.Step() == SQLiteResult.ROW)
                {
                    category = new Category();
                    category.Id = (long)statement[0];
                    category.Name = (string)statement[1];
                    category.Budgeted = (long)statement[2];
                    category.Remaining = (long)statement[3];
                    category.Icon = (string)statement[4];
                    category.Color = (string)statement[5];
                    category.Day = (long)statement[6];
                    category.Month = (long)statement[7];
                    category.Year = (long)statement[8];
                    category.Userid = (long)statement[9];
                    category.AccountId = (long)statement[10];
                    category.Type = (string)statement[11];
                }
            }

            return category;
        }

        public void UpdateCategory(Category category)
        {
            using (var statement = con.Prepare("UPDATE Category SET name=?, budgeted=?, remaining=?, icon=?, color=?,day=?,month=?,year=?,userid=?,accountId=?,type=? WHERE id=?"))
            {
                statement.Bind(1, category.Name);
                statement.Bind(2, category.Budgeted);
                statement.Bind(3, category.Remaining);
                statement.Bind(4, category.Icon);
                statement.Bind(5, category.Color);
                statement.Bind(6, category.Day);
                statement.Bind(7, category.Month);
                statement.Bind(8, category.Year);
                statement.Bind(9, category.Userid);
                statement.Bind(10, category.AccountId);
                statement.Bind(11, category.Type);
                statement.Bind(12, category.Id);
                statement.Step();
            }
        }

        public void DeleteCategory(long Id)
        {
            using (var statement = con.Prepare("DELETE FROM Category WHERE id=?"))
            {
                statement.Bind(1, Id);
                statement.Step();
            }
        }

        public void CreateTableUser()
        {
            string sql = @"CREATE TABLE IF NOT EXISTS 
                                User(id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, 
                                        name VARCHAR(100),
                                        color VARCHAR(100),
                                        icon VARCHAR(100),
                                        income INTEGER,
                                        isSelected VARCHAR(100));";

            using (var statement = con.Prepare(sql))
            {
                statement.Step();
            }
        }

        public void InsertUser(User user)
        {
            using (var statement = con.Prepare("INSERT INTO User(name, color, icon,income,isSelected) VALUES (?,?,?,?,?)"))
            {
                statement.Bind(1, user.Name);
                statement.Bind(2, user.Color);
                statement.Bind(3, user.Icon);
                statement.Bind(4, user.Income);
                statement.Bind(5, user.IsSelected);
                statement.Step();
            }
        }

        public ObservableCollection<User> GetUsers()
        {
            ObservableCollection<User> users = new ObservableCollection<User>();

            using (var statement = con.Prepare("SELECT id,name, color, icon,income,isSelected FROM User"))
            {
                while (statement.Step() == SQLiteResult.ROW)
                {
                    User user = new User();
                    user.Id = (long)statement[0];
                    user.Name = (string)statement[1];
                    user.Color = (string)statement[2];
                    user.Icon = (string)statement[3];
                    user.Income = (long)statement[4];
                    user.IsSelected = (string)statement[5];
                    users.Add(user);
                }
            }

            return users;
        }

        public User GetUser(long id)
        {
            User user = null;

            using (var statement = con.Prepare("SELECT id, name, color, icon,income,isSelected FROM User WHERE id=?"))
            {
                statement.Bind(1, id);

                if (statement.Step() == SQLiteResult.ROW)
                {
                    user = new User();
                    user.Id = (long)statement[0];
                    user.Name = (string)statement[1];
                    user.Color = (string)statement[2];
                    user.Icon = (string)statement[3];
                    user.Income = (long)statement[4];
                    user.IsSelected = (string)statement[5];
                }
            }

            return user;
        }

        public void UpdateUser(User user)
        {
            using (var statement = con.Prepare("UPDATE User SET name=?, color=?, icon=?,isSelected=? WHERE id=?"))
            {
                statement.Bind(1, user.Name);
                statement.Bind(2, user.Color);
                statement.Bind(3, user.Icon);
                statement.Bind(4, user.IsSelected);
                statement.Bind(5, user.Id);
                statement.Step();
            }
        }

        public void DeleteUser(long id)
        {
            using (var statement = con.Prepare("DELETE FROM User WHERE id=?"))
            {
                statement.Bind(1, id);
                statement.Step();
            }
        }

        public void CreateTableTransaction()
        {
            string sql = @"CREATE TABLE IF NOT EXISTS
                                Trans(id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, 
                                        name VARCHAR(100),
                                        count INTEGER,
                                        accounttype INTEGER,
                                        day  INTEGER,
                                        month INTEGER,
                                        year INTEGER,
                                        userid INTEGER,
                                        categoryid INTEGER,
                                        type VARCHAR(100));";

            using (var statement = con.Prepare(sql))
            {
                statement.Step();
            }
        }

        public void InsertTransaction(Transaction transaction)
        {
            using (var statement = con.Prepare("INSERT INTO Trans(name, count,accounttype,day,month,year,userid,type,categoryid) VALUES (?,?,?,?,?,?,?,?,?)"))
            {
                statement.Bind(1, transaction.Name);
                statement.Bind(2, transaction.Count);
                statement.Bind(3, transaction.Accounttype);
                statement.Bind(4, transaction.Day);
                statement.Bind(5, transaction.Month);
                statement.Bind(6, transaction.Year);
                statement.Bind(7, transaction.Userid);
                statement.Bind(8, transaction.Type);
                statement.Bind(9, transaction.Categoryid);
                statement.Step();
            }
        }

        public ObservableCollection<Transaction> GetTransactions()
        {
            ObservableCollection<Transaction> transactions = new ObservableCollection<Transaction>();

            using (var statement = con.Prepare("SELECT id,name, count,accounttype,day,month,year,userid,type,categoryid FROM Trans"))
            {
                while (statement.Step() == SQLiteResult.ROW)
                {
                    Transaction transaction = new Transaction();
                    transaction.Id = (long)statement[0];
                    transaction.Name = (string)statement[1];
                    transaction.Count = (long)statement[2];
                    transaction.Accounttype = (long)statement[3];
                    transaction.Day = (long)statement[4];
                    transaction.Month = (long)statement[5];
                    transaction.Year = (long)statement[6];
                    transaction.Userid = (long)statement[7];
                    transaction.Type = (string)statement[8];
                    transaction.Categoryid = (long)statement[9];
                    transactions.Add(transaction);
                }
            }
            return transactions;
        }

        public Transaction GetTransaction(long id)
        {
            Transaction transaction = null;

            using (var statement = con.Prepare("SELECT id,name, count,accounttype,day,month,year,userid,type,categoryid FROM Trans WHERE id=?"))
            {
                statement.Bind(1, id);
                while (statement.Step() == SQLiteResult.ROW)
                {
                    transaction = new Transaction();
                    transaction.Id = (long)statement[0];
                    transaction.Name = (string)statement[1];
                    transaction.Count = (long)statement[2];
                    transaction.Accounttype = (long)statement[3];
                    transaction.Day = (long)statement[4];
                    transaction.Month = (long)statement[5];
                    transaction.Year = (long)statement[6];
                    transaction.Userid = (long)statement[7];
                    transaction.Type = (string)statement[8];
                    transaction.Categoryid = (long)statement[9];

                }
            }
            return transaction;
        }

        public ObservableCollection<Transaction> GetTransactionsByUserId(long id)
        {
            ObservableCollection<Transaction> transactions = new ObservableCollection<Transaction>();

            using (var statement = con.Prepare("SELECT id,name, count,accounttype,day,month,year,userid,type,categoryid FROM Trans WHERE userid=?"))
            {
                statement.Bind(1, id);
                while (statement.Step() == SQLiteResult.ROW)
                {
                    Transaction transaction = new Transaction();
                    transaction.Id = (long)statement[0];
                    transaction.Name = (string)statement[1];
                    transaction.Count = (long)statement[2];
                    transaction.Accounttype = (long)statement[3];
                    transaction.Day = (long)statement[4];
                    transaction.Month = (long)statement[5];
                    transaction.Year = (long)statement[6];
                    transaction.Userid = (long)statement[7];
                    transaction.Type = (string)statement[8];
                    transaction.Categoryid = (long)statement[9];
                    transactions.Add(transaction);
                }
            }
            return transactions;
        }

        public ObservableCollection<Transaction> GetTransactionsByUserId(long id, long categoryid)
        {
            ObservableCollection<Transaction> transactions = new ObservableCollection<Transaction>();

            using (var statement = con.Prepare("SELECT id,name, count,accounttype,day,month,year,userid,type,categoryid FROM Trans WHERE userid=? AND categoryid=?"))
            {
                statement.Bind(1, id);
                statement.Bind(2, categoryid);
                while (statement.Step() == SQLiteResult.ROW)
                {
                    Transaction transaction = new Transaction();
                    transaction.Id = (long)statement[0];
                    transaction.Name = (string)statement[1];
                    transaction.Count = (long)statement[2];
                    transaction.Accounttype = (long)statement[3];
                    transaction.Day = (long)statement[4];
                    transaction.Month = (long)statement[5];
                    transaction.Year = (long)statement[6];
                    transaction.Userid = (long)statement[7];
                    transaction.Type = (string)statement[8];
                    transaction.Categoryid = (long)statement[9];
                    transactions.Add(transaction);
                }
            }

            return transactions;
        }

        public void UpdateTransaction(Transaction transaction)
        {
            using (var statement = con.Prepare("UPDATE Trans SET name=?, count=?, accounttype=?,day=?,month=?,year=?,userid=?,type=?,categoryid=? WHERE id=?"))
            {
                statement.Bind(1, transaction.Name);
                statement.Bind(2, transaction.Count);
                statement.Bind(3, transaction.Accounttype);
                statement.Bind(4, transaction.Day);
                statement.Bind(5, transaction.Month);
                statement.Bind(6, transaction.Year);
                statement.Bind(7, transaction.Userid);
                statement.Bind(8, transaction.Type);
                statement.Bind(9, transaction.Categoryid);
                statement.Bind(10, transaction.Id);
                statement.Step();
            }
        }

        public void DeleteTransaction(long id)
        {
            using (var statement = con.Prepare("DELETE FROM Trans WHERE id=?"))
            {
                statement.Bind(1, id);
                statement.Step();
            }
        }

        public void DeleteTransactionByCategory(long id, long accountId)
        {
            using (var statement = con.Prepare("DELETE FROM Trans WHERE categoryid=? OR accounttype=?"))
            {
                statement.Bind(1, id);
                statement.Bind(1, accountId);
                statement.Step();
            }
        }

        public ObservableCollection<Category> GetCategoryByType(long userid, string type)
        {
            ObservableCollection<Category> categories = new ObservableCollection<Category>();

            using (var statement = con.Prepare("SELECT id,name, budgeted, icon,color,remaining,day,month,year,userid,accountId,type FROM Category WHERE userid=? AND type=?"))
            {
                statement.Bind(1, userid);
                statement.Bind(2, type);
                while (statement.Step() == SQLiteResult.ROW)
                {
                    Category category = new Category();
                    category.Id = (long)statement[0];
                    category.Name = (string)statement[1];
                    category.Budgeted = (long)statement[2];
                    category.Icon = (string)statement[3];
                    category.Color = (string)statement[4];
                    category.Remaining = (long)statement[5];
                    category.Day = (long)statement[6];
                    category.Month = (long)statement[7];
                    category.Year = (long)statement[8];
                    category.Userid = (long)statement[9];
                    category.AccountId = (long)statement[10];
                    category.Type = (string)statement[11];
                    categories.Add(category);
                }
            }
            return categories;
        }

        public ObservableCollection<Category> GetCategoryByType(long userid, string type, long month, long year)
        {
            ObservableCollection<Category> categories = new ObservableCollection<Category>();

            using (var statement = con.Prepare("SELECT id,name, budgeted, icon,color,remaining,day,month,year,userid,accountId,type FROM Category WHERE userid=? AND type=? AND month=? AND year=?"))
            {
                statement.Bind(1, userid);
                statement.Bind(2, type);
                statement.Bind(3, month);
                statement.Bind(4, year);
                while (statement.Step() == SQLiteResult.ROW)
                {
                    Category category = new Category();
                    category.Id = (long)statement[0];
                    category.Name = (string)statement[1];
                    category.Budgeted = (long)statement[2];
                    category.Icon = (string)statement[3];
                    category.Color = (string)statement[4];
                    category.Remaining = (long)statement[5];
                    category.Day = (long)statement[6];
                    category.Month = (long)statement[7];
                    category.Year = (long)statement[8];
                    category.Userid = (long)statement[9];
                    category.AccountId = (long)statement[10];
                    category.Type = (string)statement[11];
                    categories.Add(category);
                }
            }
            return categories;
        }

        public void DeleteCategorybyUser(long userid)
        {
            using (var statement = con.Prepare("DELETE FROM Category WHERE userid=?"))
            {
                statement.Bind(1, userid);
                statement.Step();
            }
        }

        public void DeleteTransactionbyUser(long userid)
        {
            using (var statement = con.Prepare("DELETE FROM Trans WHERE userid=?"))
            {
                statement.Bind(1, userid);
                statement.Step();
            }
        }


    }
}
