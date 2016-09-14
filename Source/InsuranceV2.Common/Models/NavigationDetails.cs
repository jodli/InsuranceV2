namespace InsuranceV2.Common.Models
{
    public class NavigationDetails
    {
        public string CurrentRegion { get; set; }
        public bool CanGoBackward { get; set; }
        public bool CanGoForward { get; set; }

        public NavigationDetails(string currentRegion, bool canGoBackward, bool canGoForward)
        {
            CurrentRegion = currentRegion;
            CanGoBackward = canGoBackward;
            CanGoForward = canGoForward;
        }
    }
}
