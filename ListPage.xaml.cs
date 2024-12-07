using RusDavidSorinLab7.Models;
namespace RusDavidSorinLab7;

public partial class ListPage : ContentPage
{
	public ListPage()
	{
		InitializeComponent();
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();

		var shopl = (ShopList)BindingContext;

		ListView.ItemsSource = await App.Database.GetListProductsAsync(shopl.ID);
	}
	async void OnChooseButtonClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new ProductPage((ShopList)this.BindingContext)
		{
			BindingContext = new Product()
		});
	}
	async void OnSaveButtonClicked(object sender, EventArgs e)
	{
		var slist = (ShopList)BindingContext;
		slist.Date = DateTime.UtcNow;
		await App.Database.SaveShopListAsync(slist);
		await Navigation.PopAsync();
	}

	async void OnDeleteButtonClicked(object sender, EventArgs e)
	{
		var slist = (ShopList)BindingContext;
		await App.Database.DeleteShopListAsync(slist);
		await Navigation.PopAsync();
	}

    async void OnDeleteItemButtonClicked(object sender, EventArgs e)
    {
        var product = ListView.SelectedItem as Product;
        var shopl = (ShopList)BindingContext;
		if (product != null)
		{
			await App.Database.DeleteListProductAsync(product, shopl);
			ListView.ItemsSource = await App.Database.GetListProductsAsync(shopl.ID);
		}
    }
}