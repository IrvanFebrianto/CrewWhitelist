using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enums
{
    public enum FinanceAccountCategory
    {
        [Description("Income")]
        INCOME,
        [Description("Expense")]
        EXPENSE,
        [Description("Interest & Taxes")]
        INTEREST
    };

    public enum FinanceAccountSubcategory
    {
        [Description("Depreciation & Amortization")]
        DEPRECIATION
    }

    public enum Day
    {
        SUNDAY = 0,
        MONDAY = 1,
        TUESDAY = 2,
        WEDNESDAY = 3,
        THURSDAY = 4,
        FRIDAY = 5,
        SATURDAY = 6,
    }
}
