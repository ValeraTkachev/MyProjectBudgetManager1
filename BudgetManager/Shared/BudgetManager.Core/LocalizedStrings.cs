using System;
using Windows.ApplicationModel.Resources;

namespace BudgetManager.Core
{
    public enum LocalizedStringEnum
    {
        AllUsers,
        AllCategories,
        AllTransactions,
        WarningMessageSubcategory,
        TitleMessageSubcategory,
        WarningMessageTransaction,
        CultureInfo,
        Continue,
        Cancel,
        WarningAddTitle,
        WarningAddMessage,
        WarningGoToAllSubcategories,
        accountTitle,
        expenseTitle,
        addIncomeTitle,
        addmoneyAccountTextBlock,
        addTransactionTitle,
        transactionTitle,
        editexpenseTitle,
        editTransactionTitle,
        statiscticTitle,
        moneytransactionTitle,
        ExpensesTitleForCategory,
        ExpensesTitle,
        IncomeTitle,
        MoneyAccountsTitle
    }

    public static class LocalizedStrings
    {
        private static ResourceLoader resourceLoader;

        static LocalizedStrings()
        {
            resourceLoader = new ResourceLoader();
        }

        public static string GetString(LocalizedStringEnum resource)
        {
            return resourceLoader.GetString(resource.ToString());
        }
    }
}
