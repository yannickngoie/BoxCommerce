using System;

namespace EventBus.Messages
{
    public class IntegratonBasedEvent
    {
        public IntegratonBasedEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        public IntegratonBasedEvent(Guid id, DateTime createDate)
        {
            Id = id;
            CreationDate = createDate;
        }

        /* Use for correlaton*/
        public Guid Id { get; private set; }

        public DateTime CreationDate { get; private set; }

    }
}
