using System;
using Prism.Events;

namespace InsuranceV2.Common.MVVM
{
    public interface IEventBus
    {
        void Publish<TEvent>(TEvent payload);

        SubscriptionToken Subscribe<TEvent>(Action<TEvent> subscription,
            bool keepSubscriberReferenceAlive = false);

        void Unsubscribe<TEvent>(Action<TEvent> subscription);
    }
}