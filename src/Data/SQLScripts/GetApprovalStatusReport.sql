CREATE PROCEDURE GetApprovalStatusReport
AS
BEGIN
    SELECT
        SUM(CASE WHEN Status = 0 THEN 1 ELSE 0 END) AS PendingExpenseCount,
        SUM(CASE WHEN Status = 1 THEN 1 ELSE 0 END) AS ApprovedExpenseCount,
        SUM(CASE WHEN Status = 2 THEN 1 ELSE 0 END) AS RejectedExpenseCount
    FROM
        Expenses;
END
