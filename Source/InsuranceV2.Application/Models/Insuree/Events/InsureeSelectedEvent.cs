using Prism.Common;
using Prism.Events;

namespace InsuranceV2.Application.Models.Insuree.Events
{
    public class InsureeSelectedEvent : PubSubEvent<ObservableObject<DetailInsuree>>
    {
    }
}
