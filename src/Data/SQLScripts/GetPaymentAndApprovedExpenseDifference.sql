CREATE PROCEDURE GetPaymentAndApprovedExpenseDifference
AS
BEGIN
SELECT Difference
FROM (
	SELECT 
		SUM(Payments.Amount)/COUNT(Payments.Id) AS PaymentAmount,
		COALESCE(SUM(CASE WHEN Expenses.Status = 1 THEN Expenses.Amount END), 0) AS ApprovedExpensesAmount,
		SUM(Payments.Amount)/COUNT(Payments.Id) - COALESCE(SUM(CASE WHEN Expenses.Status = 1 THEN Expenses.Amount END), 0) AS Difference
	FROM Payments
	LEFT JOIN Expenses ON Expenses.Status = 1) 
AS SubqueryAlias;
END