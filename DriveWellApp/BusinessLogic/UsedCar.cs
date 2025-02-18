namespace DriveWellApp.BusinessLogic;

public class UsedCar : Car
{
    private int Kilometers { get; set; } 

    public UsedCar(string vin, string make, CarType carType, float price, int modelYear, int kilometers)
        : base(vin, make, carType, price, modelYear)
    {
        Kilometers = kilometers;
    }

    
    private float Depreciation
    {
        get
        {
            int carAge = DateTime.Now.Year - ModelYear; 
            float depreciationFromAge = 0.10f * carAge; 
            float depreciationFromKilometers = 0.009f * (Kilometers / 10000); 

            
            return Price * (depreciationFromAge + depreciationFromKilometers);
        }
    }

    
    public override double NetPrice
    {
        get
        {
            float depreciation = Depreciation;
            float tax = 0.13f * (Price - depreciation); 
            return Price - depreciation + tax; 
        }
    }
}
