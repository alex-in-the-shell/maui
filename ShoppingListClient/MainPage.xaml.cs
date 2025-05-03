using ShoppingListClient.Models;
using ShoppingListClient.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ShoppingListClient;

public partial class MainPage : ContentPage
{
    private readonly ShoppingListService _shoppingListService;
    private ObservableCollection<ShoppingItem> _shoppingItems;

    public ObservableCollection<ShoppingItem> ShoppingItems
    {
        get => _shoppingItems;
        set
        {
            _shoppingItems = value;
            OnPropertyChanged(nameof(ShoppingItems));
        }
    }

    public MainPage()
    {
        InitializeComponent();
        _shoppingListService = new ShoppingListService(new HttpClient());
        ShoppingItems = new ObservableCollection<ShoppingItem>();
        BindingContext = this;
        LoadItemsAsync();
    }

    private async void LoadItemsAsync()
    {
        try
        {
            var items = await _shoppingListService.GetShoppingItemsAsync();
            ShoppingItems.Clear();
            foreach (var item in items)
            {
                ShoppingItems.Add(item);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Failed to load items: " + ex.Message, "OK");
        }
    }

    private async void OnAddItemClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(NewItemName.Text))
        {
            await DisplayAlert("Error", "Please enter item name", "OK");
            return;
        }

        var newItem = new ShoppingItem
        {
            Name = NewItemName.Text,
            Description = NewItemDescription.Text,
            IsCompleted = false,
            CreatedAt = DateTime.Now
        };

        try
        {
            var createdItem = await _shoppingListService.CreateShoppingItemAsync(newItem);
            ShoppingItems.Add(createdItem);
            NewItemName.Text = string.Empty;
            NewItemDescription.Text = string.Empty;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Failed to add item: " + ex.Message, "OK");
        }
    }

    private async void OnItemToggled(object sender, ToggledEventArgs e)
    {
        var switchControl = sender as Switch;
        var item = (ShoppingItem)switchControl.BindingContext;

        try
        {
            item.IsCompleted = e.Value;
            if (e.Value)
            {
                item.CompletedAt = DateTime.Now;
            }
            else
            {
                item.CompletedAt = null;
            }
            await _shoppingListService.UpdateShoppingItemAsync(item);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Failed to update item: " + ex.Message, "OK");
        }
    }
}

