using ExpenseManagement.Api.Caching;
using ExpenseManagement.Base.Constants.Caching;
using ExpenseManagement.Base.Constants.DataBase.Dapper.ExpenseQueries;
using ExpenseManagement.Base.Response;
using ExpenseManagement.Base.Routes;
using ExpenseManagement.Data.Entities;
using ExpenseManagement.Data.Repositories.Implementations.Expenses;
using ExpenseManagement.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.StackExchangeRedis;

namespace ExpenseManagement.Api.Controllers;

[Route(ControllerRoute.BaseRoute)]
[ApiController]
public class ReportController : ControllerBase
{  
    private readonly IDapperExpenseReportRepository expenseReportRepository;
    private readonly ICacheService cacheService;

    public ReportController(
        IDapperExpenseReportRepository expenseReportRepository, 
        ICacheService cacheService)
    {
        this.expenseReportRepository = expenseReportRepository;
        this.cacheService = cacheService;
    }

    [HttpGet(EndpointRoute.GetByAppUserId)]
    public async Task<ApiResponse<IEnumerable<EmployeeExpenseReport>>> GetEmployeeExpenseReportAsync([FromRoute]int appUserId)
    {
        var cacheData = await cacheService.GetData<IEnumerable<EmployeeExpenseReport>>(RedisKeys.EmployeeExpenseReportKey);
        if(cacheData is not null && cacheData.Count() > 0)
            return new ApiResponse<IEnumerable<EmployeeExpenseReport>>(cacheData);

        cacheData = expenseReportRepository.GetEmployeeExpenseReport(appUserId); 
        var expiryTime = DateTimeOffset.Now.AddDays(1);
        await cacheService.SetData<IEnumerable<EmployeeExpenseReport>>(
            key: RedisKeys.EmployeeExpenseReportKey, 
            value: cacheData, 
            expirationTime: expiryTime);

        return new ApiResponse<IEnumerable<EmployeeExpenseReport>>(cacheData);
    }
    [HttpGet(EndpointRoute.ApprovalStatusReport)]
    public async Task<ApiResponse<IEnumerable<ApprovalStatusReport>>> GetApprovalStatusReport()
    {
        var cacheData = await cacheService.GetData<IEnumerable<ApprovalStatusReport>>(RedisKeys.ApprovalStatusReportKey);
        if(cacheData is not null && cacheData.Count() > 0)
            return new ApiResponse<IEnumerable<ApprovalStatusReport>>(cacheData);

        cacheData = expenseReportRepository.GetApprovalStatusReports(); 
        var expiryTime = DateTimeOffset.Now.AddDays(1);
        await cacheService.SetData<IEnumerable<ApprovalStatusReport>>(
            key: RedisKeys.ApprovalStatusReportKey, 
            value: cacheData, 
            expirationTime: expiryTime);
            
        return new ApiResponse<IEnumerable<ApprovalStatusReport>>(cacheData);
    }

    [HttpGet(EndpointRoute.PaymentDifferenceReport)]
    public async Task<ApiResponse<IEnumerable<PaymentAndApprovedExpenseDifference>>> GetPaymentAndApprovedExpenseDifference()
    {
        var cacheData = await cacheService.GetData<IEnumerable<PaymentAndApprovedExpenseDifference>>(
                        RedisKeys.PaymentAndApprovedExpenseDifferenceKey);
                        
        if(cacheData is not null && cacheData.Count() > 0)
            return new ApiResponse<IEnumerable<PaymentAndApprovedExpenseDifference>>(cacheData);

        cacheData = expenseReportRepository.GetPaymentAndApprovedExpenseDifference(); 
        var expiryTime = DateTimeOffset.Now.AddDays(1);
        await cacheService.SetData<IEnumerable<PaymentAndApprovedExpenseDifference>>(
            key: RedisKeys.PaymentAndApprovedExpenseDifferenceKey, 
            value: cacheData, 
            expirationTime: expiryTime);
            
        return new ApiResponse<IEnumerable<PaymentAndApprovedExpenseDifference>>(cacheData);
    }
}