using System;

public abstract class Car
{
    // Properties
    public string Make { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public DateTime LastMaintenanceDate { get; set; }

    // Constructor
    public Car(string make, string model, int year, DateTime lastMaintenanceDate)
    {
        Make = make;
        Model = model;
        Year = year;
        LastMaintenanceDate = lastMaintenanceDate;
    }

    // General Method for Scheduling Maintenance
    public void ScheduleMaintenance()
    {
        DateTime nextMaintenanceDate = LastMaintenanceDate.AddMonths(6);
        Console.WriteLine($"Next Maintenance: {nextMaintenanceDate.ToShortDateString()}");
    }

    // Method for Displaying Car Details
    public void DisplayDetails()
    {
        Console.WriteLine($"Car: {Make} {Model} ({Year})");
        Console.WriteLine($"Last Maintenance: {LastMaintenanceDate.ToShortDateString()}");
    }

    // Abstract Method for Actions (Refuel or Charge)
    public abstract void PerformAction(DateTime actionDate);
}

public interface IFuelable
{
    void Refuel(DateTime timeOfRefuel);
}

public interface IChargable
{
    void Charge(DateTime timeOfCharge);
}

public class FuelCar : Car, IFuelable
{
    public FuelCar(string make, string model, int year, DateTime lastMaintenanceDate)
        : base(make, model, year, lastMaintenanceDate)
    {
    }

    // Implement PerformAction for FuelCar
    public override void PerformAction(DateTime actionDate)
    {
        Refuel(actionDate);
    }

    public void Refuel(DateTime timeOfRefuel)
    {
        Console.WriteLine($"FuelCar {Make} {Model} refueled on {timeOfRefuel.ToString("yyyy-MM-dd HH:mm")}");
    }
}

public class ElectricCar : Car, IChargable
{
    public ElectricCar(string make, string model, int year, DateTime lastMaintenanceDate)
        : base(make, model, year, lastMaintenanceDate)
    {
    }

    // Implement PerformAction for ElectricCar
    public override void PerformAction(DateTime actionDate)
    {
        Charge(actionDate);
    }

    public void Charge(DateTime timeOfCharge)
    {
        Console.WriteLine($"ElectricCar {Make} {Model} charged on {timeOfCharge.ToString("yyyy-MM-dd HH:mm")}");
    }
}

class Program
{
    static void Main()
    {
        // Input for Car Make and Model
        string make = GetInput("Enter car make: ");
        string model = GetInput("Enter car model: ");

        // Validate Year
        int year = GetValidYear();

        // Validate Last Maintenance Date
        DateTime lastMaintenanceDate = GetValidDate("Enter last maintenance date (yyyy-MM-dd): ");

        // Ask whether it is a FuelCar or ElectricCar
        string carType = GetCarType();

        // Create appropriate car object
        Car car = null;
        if (carType.ToUpper() == "F")
        {
            car = new FuelCar(make, model, year, lastMaintenanceDate);
        }
        else
        {
            car = new ElectricCar(make, model, year, lastMaintenanceDate);
        }

        // Display details and schedule maintenance
        car.DisplayDetails();
        car.ScheduleMaintenance();

        // Ask for refuel/charge action
        string actionResponse = GetInput("Do you want to refuel/charge? (Y/N): ");
        if (actionResponse.ToUpper() == "Y")
        {
            DateTime actionDate = GetValidDateTime("Enter refuel/charge date and time (yyyy-MM-dd HH:mm): ");
            car.PerformAction(actionDate);
        }
    }

    // Method to get user input for string values
    static string GetInput(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine();
    }

    // Method to get valid year input
    static int GetValidYear()
    {
        int year;
        while (true)
        {
            Console.Write("Enter car year (e.g., 2020): ");
            if (int.TryParse(Console.ReadLine(), out year) && year >= 1886 && year <= DateTime.Now.Year)
                return year;
            else
                Console.WriteLine("Invalid year! Please enter a valid year between 1886 and the current year.");
        }
    }

    // Method to get valid date input
    static DateTime GetValidDate(string prompt)
    {
        DateTime validDate;
        while (true)
        {
            Console.Write(prompt);
            if (DateTime.TryParse(Console.ReadLine(), out validDate))
                return validDate;
            else
                Console.WriteLine("Invalid date format! Please enter a valid date.");
        }
    }

    // Method to get valid DateTime input (for refuel/charge)
    static DateTime GetValidDateTime(string prompt)
    {
        DateTime validDateTime;
        while (true)
        {
            Console.Write(prompt);
            if (DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd HH:mm", null, System.Globalization.DateTimeStyles.None, out validDateTime))
                return validDateTime;
            else
                Console.WriteLine("Invalid date format! Please enter a valid date and time (yyyy-MM-dd HH:mm).");
        }
    }

    // Method to get valid car type (FuelCar or ElectricCar)
    static string GetCarType()
    {
        while (true)
        {
            Console.Write("Is this a FuelCar or ElectricCar? (F/E): ");
            string input = Console.ReadLine();
            if (input.ToUpper() == "F" || input.ToUpper() == "E")
                return input.ToUpper();
            else
                Console.WriteLine("Invalid input! Please enter 'F' for FuelCar or 'E' for ElectricCar.");
        }
    }
}
