using System;
using Prism.Events;

namespace InsuranceV2.Common.MVVM
{
    public class EventBus : IEventBus
    {
        private readonly IEventAggregator _eventAggregator;

        public EventBus(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void Publish<TEvent>(TEvent payload)
        {
            _eventAggregator.GetEvent<PubSubEvent<TEvent>>().Publish(payload);
        }

        public SubscriptionToken Subscribe<TEvent>(Action<TEvent> subscription,
            bool keepSubscriberReferenceAlive = false)
        {
            return _eventAggregator.GetEvent<PubSubEvent<TEvent>>()
                .Subscribe(subscription, keepSubscriberReferenceAlive);
        }

        public void Unsubscribe<TEvent>(Action<TEvent> subscription)
        {
            _eventAggregator.GetEvent<PubSubEvent<TEvent>>().Unsubscribe(subscription);
        }

        public SubscriptionToken Subscribe<TEvent>(Action<TEvent> subscription, ThreadOption threadOption,
            bool keepSubscriberReferenceAlive = false, Predicate<TEvent> filter = null)
        {
            return _eventAggregator.GetEvent<PubSubEvent<TEvent>>()
                .Subscribe(subscription, threadOption, keepSubscriberReferenceAlive, filter);
        }

        public void Unsubscribe<TEvent>(SubscriptionToken token)
        {
            _eventAggregator.GetEvent<PubSubEvent<TEvent>>().Unsubscribe(token);
        }
    }
}