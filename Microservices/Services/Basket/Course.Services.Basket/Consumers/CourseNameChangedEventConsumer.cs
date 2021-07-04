using Course.Services.Basket.Services.Abstract;
using Course.Services.Basket.Services.Concrede;
using Course.Shared.MassTransitMessages;
using Course.Shared.Services.Abstract;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Course.Services.Basket.Consumers
{
    public class CourseNameChangedEventConsumer : IConsumer<CourseNameChangedEvent>
    {
        public Task Consume(ConsumeContext<CourseNameChangedEvent> context)
        {
            throw new NotImplementedException();
        }
    }
}
