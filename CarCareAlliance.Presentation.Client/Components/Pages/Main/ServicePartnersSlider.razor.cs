using MudBlazor;

namespace CarCareAlliance.Presentation.Client.Components.Pages.Main
{
	public partial class ServicePartnersSlider
	{

		private MudCarousel<string> carousel;
		private IList<string> source = new List<string>() { "Item 1", "Item 2", "Item 3", "Item 4", "Item 5", "Item 6", "Item 7", "Item 8", "Item 9" };
		private int selectedIndex = 2;
    }
}
