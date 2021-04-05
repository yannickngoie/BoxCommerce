using System;

namespace EventBus.Messages
{
    public class IntegratonBasedEvent
    {
        public IntegratonBasedEvent()
        {
            CorrelationId = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        public IntegratonBasedEvent(Guid id, DateTime createDate)
        {
            CorrelationId = id;
            CreationDate = createDate;
        }

        /* Use for correlaton*/
        public Guid  CorrelationId { get; private set; }

        public DateTime CreationDate { get; private set; }

    }
}
