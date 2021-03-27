using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus.Messages
{
    class IntegratonBasedEvent
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

        public Guid Id { get; private set; }

        public DateTime CreationDate { get; private set; }

    }
}
