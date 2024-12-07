using RusDavidSorinLab7.Models;

namespace RusDavidSorinLab7;

public partial class ProductPage : ContentPage
{
	ShopList sl;
	public ProductPage(ShopList slist)
	{
		InitializeComponent();
		sl = slist;
	}

	async void OnAddButtonClicked(object sender, EventArgs e)
	{
		Product p;
		if (ListView.SelectedItem != null)
		{
			p = ListView.SelectedItem as Product;
			var lp = new ListProduct()
			{
				ShopListID = sl.ID,
				ProductID = p.ID
			};
			await App.Database.SaveListProductAsync(lp);
			p.ListProducts = new List<ListProduct> { lp };

			await Navigation.PopAsync();
		}
	}
	async void OnSaveButtonClicked(object sender, EventArgs e)
	{
		var product = (Product)BindingContext;
		await App.Database.SaveProductAsync(product);
		ListView.ItemsSource = await App.Database.GetProductsAsync();
	}

	async void OnDeleteButtonClicked(object sender, EventArgs e)
	{
		var product= ListView.SelectedItem as Product;
		await App.Database.DeleteProductAsync(product);
		ListView.ItemsSource = await App.Database.GetProductsAsync();
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();

		ListView.ItemsSource = await App.Database.GetProductsAsync();
	}
}