CREATE VIEW EmployeeExpenseReport AS
SELECT
    e.AppUserId,
    SUM(CASE WHEN DATEPART(DAY, ExpensedDate) = DAY(GETDATE()) THEN Amount ELSE 0 END) AS DailyExpense,
    SUM(CASE WHEN DATEPART(WEEK, ExpensedDate) = DATEPART(WEEK, GETDATE()) THEN Amount ELSE 0 END) AS WeeklyExpense,
    SUM(CASE WHEN DATEPART(MONTH, ExpensedDate) = MONTH(GETDATE()) THEN Amount ELSE 0 END) AS MonthlyExpense
FROM
    Expenses e
GROUP BY
    e.AppUserId;