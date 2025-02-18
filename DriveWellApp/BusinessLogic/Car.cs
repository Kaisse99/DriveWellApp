namespace DriveWellApp.BusinessLogic;

public class Car
{
    private string _vin;
    // sets or gets VIN 
    public string Vin
    {
        get => _vin;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new Exception("Vin cannot be empty");
            if (value.Length != 17) // must be of 17 characters
                throw new Exception("Vin must be 17 characters long");
            _vin = value;
        }
    }
    private string _make;
    public string Make
    {
        get => _make;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new Exception("Make cannot be empty");
            _make = value;
        }
    }
    private CarType _type;

    public CarType Type
    {
        get => _type;
        set
        {
            if (!Enum.IsDefined(value))
                throw new Exception("Invalid car type");
            _type = value;
        }
    }
    
    private float _price;

    public float Price
    {
        get => _price;
        set
        {
            if (value <= 0) 
                throw new Exception("Price cannot be zero or negative");
            _price = value;
        }
    }
    private int _modelYear;

    public int ModelYear
    {
        get => _modelYear;
        set
        {
            if (value < 2000 || value > DateTime.Now.Year)
                throw new Exception("Model year must be between 2000 and current year");
            _modelYear = value;
        }
    }
    // Computed property with computes Car's price including 13.5% of tax
    public virtual double NetPrice => Price + (Price * 0.135);
    
    //ToString in order to display car's properties in ListView
    public override string ToString()
    {
        return $"{Vin}, {Make}, {Type}, {ModelYear}, {NetPrice}";
    }
    //constructor
    public Car(string vin, string make, CarType type, float price, int modelYear)
    {
        Vin = vin;
        Make = make;
        Type = type;
        Price = price;
        ModelYear = modelYear;
    }
}