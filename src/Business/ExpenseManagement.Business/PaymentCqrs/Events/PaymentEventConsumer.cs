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
public sealed class PaymentEventConsumer : IConsumer<PaymentEvent>
{
    private readonly IEfPaymentRepository efPaymentRepository;

    public PaymentEventConsumer(IEfPaymentRepository efPaymentRepository)
    {
        this.efPaymentRepository = efPaymentRepository;
    }

    public async Task Consume(ConsumeContext<PaymentEvent> context)
    {
        using(var client = new HttpClient())
        {
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

            var result = client.PostAsync(endpoint, payload).Result.Content.ReadAsStringAsync().Result;
            if (!result.Contains("\"success\":true"))
                throw new Exception(ExceptionMessages.PaymentCouldNotBeAble);
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

            await Task.CompletedTask;
        }   
    }
}