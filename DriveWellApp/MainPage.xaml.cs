using DriveWellApp.BusinessLogic;

namespace DriveWellApp;

public partial class MainPage : ContentPage
{
    List<Car> _cars = new List<Car>();
    private CarInventory carInventory;
    public MainPage()
    {
        InitializeComponent();
        
        carInventory = new CarInventory();
        foreach (var car in carInventory.Cars)
        {
            _cars.Add(car);
        }
        AvailableCarsListView.ItemsSource = _cars;
        PopulatePicker();
    }

    private void PopulatePicker()
    {
        
        CarTypePicker.ItemsSource = Enum.GetValues<CarType>(); // populating picker with cartype
        List<int> years = new List<int>();
        for (int i = 2013; i <= DateTime.Now.Year; i++)
        {
            years.Add(i);
        }
        ModelYearPicker.ItemsSource = years;
    }

    private void AddCarOnClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(VinEntry.Text) || 
            string.IsNullOrWhiteSpace(CarMakeEntry.Text) || 
            string.IsNullOrWhiteSpace(CarPriceEntry.Text) || 
            CarTypePicker.SelectedItem == null || 
            ModelYearPicker.SelectedItem == null)
        {
            DisplayAlert("Error", "Please fill in all fields correctly.", "OK");
            return;
        }
        
        try
        {
            string vin = VinEntry.Text.Trim();
            if (_cars.Any(car => car.Vin == vin))
            {
                DisplayAlert("Error", "A car with this VIN already exists.", "OK");
                return;
            }
            string make = CarMakeEntry.Text.Trim();
            CarType carType = (CarType)CarTypePicker.SelectedItem;
            float price = float.Parse(CarPriceEntry.Text);
            int modelYear = Convert.ToInt32(ModelYearPicker.SelectedItem);
            Car newCar = new Car(vin, make, carType, price, modelYear);
            _cars.Add(newCar);
            AvailableCarsListView.ItemsSource = null;
            AvailableCarsListView.ItemsSource = new List<Car>(_cars);
            DisplayAlert("Success", "Car added successfully.", "OK");
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", $"Invalid input: {ex.Message}", "OK");
        }
    }

    private void ClearOnClicked(object sender, EventArgs e)
    {
        VinEntry.Text = string.Empty;
        CarMakeEntry.Text = string.Empty;
        CarPriceEntry.Text = string.Empty;
        CarTypePicker.SelectedItem = null;
        ModelYearPicker.SelectedItem = null;
        KilometersEntry.Text = string.Empty;
        IsUsed.IsChecked = false;
    }

    private void CarTypeIcon(CarType type)
    {
        string path = type switch
        {
            CarType.Sedan => "sedan.png",
            CarType.Hatchback => "hatchback.png",
            CarType.Sportcar => "sportcar.png",
            CarType.Suv => "suv.png",
        };
        CarTypeImage.Source = path;
    }

    private void CarTypePickerIndex(object sender, EventArgs e)
    {
        if (CarTypePicker.SelectedItem is CarType selectedCarType)
        {
            CarTypeIcon(selectedCarType);
        }
    }
    private void OnCarSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is Car selectedCar)
        {
            VinEntry.Text = selectedCar.Vin;
            CarMakeEntry.Text = selectedCar.Make;
            CarPriceEntry.Text = selectedCar.Price.ToString("F2");
            CarTypePicker.SelectedItem = selectedCar.Type;
            ModelYearPicker.SelectedItem = selectedCar.ModelYear;

            CarTypeIcon(selectedCar.Type);
        }
    }

    private void UpdateOnClicked(object sender, EventArgs e)
    {
        if (AvailableCarsListView.SelectedItem is Car selectedCar)
        {
            string vin = selectedCar.Vin;
            string make = CarMakeEntry.Text;
            CarType carType = (CarType)CarTypePicker.SelectedItem;
            float price = float.Parse(CarPriceEntry.Text);
            int modelYear = (int)ModelYearPicker.SelectedItem;
            
            carInventory.UpdateCar(vin, make, carType, price, modelYear);
            AvailableCarsListView.ItemsSource = null;
            AvailableCarsListView.ItemsSource = _cars;
            DisplayAlert("Success", "Car details updated.", "OK");
        }
        else
        {

            DisplayAlert("Error", "Please select a car to update.", "OK");
        }
    }
}
