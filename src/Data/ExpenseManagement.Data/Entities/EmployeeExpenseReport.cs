namespace ExpenseManagement.Data.Entities;
public class EmployeeExpenseReport
{
    public int AppUserId { get; set; }
    public string? EmployeeName { get; set; }
    public decimal DailyExpenseAmount { get; set; }
    public decimal WeeklyExpenseAmount { get; set; }
    public decimal MonthlyExpenseAmount { get; set; }
}