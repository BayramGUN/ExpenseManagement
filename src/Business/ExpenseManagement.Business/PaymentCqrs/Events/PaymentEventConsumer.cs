using System.Diagnostics;
using System.Text;
using ExpenseManagement.Base.Constants.ContentTypes;
using ExpenseManagement.Base.Constants.Http;
using ExpenseManagement.Base.Constants.Messages;
using ExpenseManagement.Data.Entities;
using ExpenseManagement.Data.Repositories.Interfaces.Payments;
using MassTransit;
using Newtonsoft.Json;

namespace ExpenseManagement.Business.PaymentCqrs.Events;

/// <summary>
/// Consumer for handling PaymentEvent messages. Processes payment events by making an HTTP request to the Payment API,
/// logs results, and persists payment details to the local database.
/// </summary>
public sealed class PaymentEventConsumer : IConsumer<PaymentEvent>
{
    private readonly IEfPaymentRepository efPaymentRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="PaymentEventConsumer"/> class.
    /// </summary>
    /// <param name="efPaymentRepository">The repository for interacting with payment entities.</param>
    /// <param name="logger">The logger for logging events and errors.</param>
    public PaymentEventConsumer(
        IEfPaymentRepository efPaymentRepository)
    {
        this.efPaymentRepository = efPaymentRepository;
    }

    /// <summary>
    /// Consumes PaymentEvent messages, processes payment events, and persists payment details.
    /// </summary>
    /// <param name="context">The MassTransit consume context for handling the payment event.</param>
    public async Task Consume(ConsumeContext<PaymentEvent> context)
    {
        using(var client = new HttpClient())
        {
            // Prepare payment details for the HTTP request.            
            var endpoint = new Uri(EndPointUrls.PaymentApiEndpoint);

            var paymentObject = new
            {
                FromAccountNumber = context.Message.FromAccountNumber,
                ToAccountNumber = context.Message.ToAccountNumber,
                Description = context.Message.Description,
                Amount = context.Message.Amount
            };

            var paymentJson = JsonConvert.SerializeObject(paymentObject);
            var payload = new StringContent(
                content: paymentJson, 
                encoding: Encoding.UTF8, 
                mediaType: ResponseContentType.ApplicationJsonType);

            // Send HTTP request to the Payment API.
            var result = client.PostAsync(endpoint, payload).Result.Content
                               .ReadAsStringAsync().Result;

            // Handle the API response.
            if (!result.Contains("\"success\":true"))
            {
                throw new Exception(ExceptionMessages.PaymentCouldNotBeAble);
            }

            // Create a Payment entity and persist it to the local database.
            Payment payment = new()
            {
                Amount = context.Message.Amount,
                PaymentDate = DateTime.Now,
                InsertDate = DateTime.Now,
                PaymentMethod = "InBank",
                InsertUserId = context.Message.ApproverId,
                ExpenseId = context.Message.ExpenseId
            };
            await efPaymentRepository.CreatePaymentAsync(payment, context.CancellationToken);

        }   
    }
}