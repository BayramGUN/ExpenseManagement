using System.Diagnostics;
using System.Text;
using ExpenseManagement.Base.Constants.ContentTypes;
using MassTransit;
using Newtonsoft.Json;

namespace ExpenseManagement.Business.PaymentCqrs.Events;
public class PaymentEventConsumer : IConsumer<PaymentEvent>
{
    public async Task Consume(ConsumeContext<PaymentEvent> context)
    {
        using(var client = new HttpClient())
        {
            var endpoint = new Uri("https://localhost:7296/api/Transfers/Transfer");
        
            var paymentJson = JsonConvert.SerializeObject(context.Message);
            var payload = new StringContent(
                content: paymentJson, 
                encoding: Encoding.UTF8, 
                mediaType: ResponseContentType.ApplicationJsonType);

            var result = client.PostAsync(endpoint, payload).Result.Content.ReadAsStringAsync().Result;
            Debug.WriteLine(result);
            //if(result.Contains())
            await Task.CompletedTask;
        }   
    }
}