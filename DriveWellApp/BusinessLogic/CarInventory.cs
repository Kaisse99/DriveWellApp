namespace DriveWellApp.BusinessLogic;

public class CarInventory
{
    private List<Car> _cars = new List<Car>();
    //Gets the list of all cars in the inventory
    public CarInventory()
    { //initializes a new instance of the carInventory class with three hard coded cars.
        _cars.Add(new Car("12345678901234567", "BMW", CarType.Sedan, 19999, 2014));
        _cars.Add(new Car("123456789B1234567", "Ford", CarType.Suv, 30000, 2021));
        _cars.Add(new Car("123456789A1234567", "Nissan", CarType.Hatchback, 35000, 2022));
    }

    public Car? GetByVin(string vin) //method to search and return a car based on VIN
    {
        foreach (var car in _cars)
        {
            if (car.Vin == vin)
            {
                return car;
            }
        }
        return null;
    }
    // Adding a new car to inventory
    public void AddCar(Car car)
    {
        if (GetByVin(car.Vin) == null)
        {
            _cars.Add(car);
        }
    }
    //Updating a car
    public void UpdateCar(string vin, string make, CarType carType, float price, int modelYear)
    {
        var car = GetByVin(vin);
        if (car != null)
        {
            car.Make = make;
            car.Type = carType;
            car.Price = price;
            car.ModelYear = modelYear;
        }
    }
    // copy of _cars as a readonly to prevent any changes 
    public IReadOnlyList<Car> Cars => _cars.AsReadOnly();

}