using System;
using System.Collections.Generic;
using System.Linq;

namespace CarManagement
{
    class Bai1
    {
        enum CarType { Electric, Fuel }

        class Car
        {
            public string Make { get; set; }
            public string Model { get; set; }
            public int Year { get; set; }
            public CarType Type { get; set; }

            public Car(string make, string model, int year, CarType type)
            {
                Make = make;
                Model = model;
                Year = year;
                Type = type;
            }
        }

        static void Main(string[] args)
        {
            List<Car> cars = new List<Car>();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Car Management System");
                Console.WriteLine("1. Add a car");
                Console.WriteLine("2. View all cars");
                Console.WriteLine("3. Search car by Make");
                Console.WriteLine("4. Filter car by Type");
                Console.WriteLine("5. Remove a car by Model");
                Console.WriteLine("6. Exit");
                Console.Write("Enter your choice: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        AddCar(cars);
                        break;
                    case "2":
                        ViewAllCars(cars);
                        break;
                    case "3":
                        SearchCarByMake(cars);
                        break;
                    case "4":
                        FilterCarByType(cars);
                        break;
                    case "5":
                        RemoveCarByModel(cars);
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
            }
        }

        static void AddCar(List<Car> cars)
        {
            // Kiểm tra kiểu xe
            CarType type;
            while (true)
            {
                Console.Write("Enter Car type (Fuel/Electric): ");
                string typeInput = Console.ReadLine().Trim();

                if (Enum.TryParse(typeInput, true, out type) && Enum.IsDefined(typeof(CarType), type))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid car type. Please enter 'Fuel' or 'Electric'.");
                }
            }

            // Kiểm tra Make và Model không chứa ký tự đặc biệt và không trống
            string make;
            while (true)
            {
                Console.Write("Enter Make: ");
                make = Console.ReadLine().Trim();
                if (string.IsNullOrWhiteSpace(make) || make.Any(c => !Char.IsLetterOrDigit(c) && c != ' '))
                {
                    Console.WriteLine("Invalid input. Make cannot be empty or contain special characters.");
                }
                else
                {
                    break;
                }
            }

            string model;
            while (true)
            {
                Console.Write("Enter Model: ");
                model = Console.ReadLine().Trim();
                if (string.IsNullOrWhiteSpace(model) || model.Any(c => !Char.IsLetterOrDigit(c) && c != ' '))
                {
                    Console.WriteLine("Invalid input. Model cannot be empty or contain special characters.");
                }
                else if (cars.Any(car => car.Model.Equals(model, StringComparison.OrdinalIgnoreCase)))
                {

                    Console.WriteLine("Model already exists. Please enter a different model.");
                }
                else
                {
                    break;
                }
            }

            int year;
            while (true)
            {
                Console.Write("Enter Year: ");
                string yearInput = Console.ReadLine();

                if (int.TryParse(yearInput, out year) && year >= 1900 && year <= DateTime.Now.Year)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid year. Please enter a valid year (greater than or equal to 1900 and less than or equal to the current year).");
                }
            }

            Car newCar = new Car(make, model, year, type);
            cars.Add(newCar);

            Console.WriteLine("Car added successfully!");
            Console.ReadLine();
        }

        static void ViewAllCars(List<Car> cars)
        {
            Console.WriteLine("\nAll Cars:");

            if (cars.Count == 0)
            {
                Console.WriteLine("No cars available.");
            }
            else
            {
                foreach (var car in cars)
                {
                    Console.WriteLine($"Make: {car.Make}, Model: {car.Model}, Year: {car.Year}, Type: {car.Type}");
                }
            }

            Console.ReadLine();
        }

        static void SearchCarByMake(List<Car> cars)
        {
            Console.Write("\nEnter the Make to search for: ");
            string make = Console.ReadLine();

            var result = cars.Where(car => car.Make.Equals(make, StringComparison.OrdinalIgnoreCase)).ToList();

            if (result.Count == 0)
            {
                Console.WriteLine($"No cars found for Make: {make}");
            }
            else
            {
                Console.WriteLine($"Cars found for Make: {make}");
                foreach (var car in result)
                {
                    Console.WriteLine($"Model: {car.Model}, Year: {car.Year}, Type: {car.Type}");
                }
            }

            Console.ReadLine();
        }

        static void FilterCarByType(List<Car> cars)
        {
            Console.Write("\nEnter the Type to filter by (Electric/Fuel): ");
            CarType type = (CarType)Enum.Parse(typeof(CarType), Console.ReadLine(), true);

            var result = cars.Where(car => car.Type == type).ToList();

            if (result.Count == 0)
            {
                Console.WriteLine($"No cars found for Type: {type}");
            }
            else
            {
                Console.WriteLine($"Cars found for Type: {type}");
                foreach (var car in result)
                {
                    Console.WriteLine($"Make: {car.Make}, Model: {car.Model}, Year: {car.Year}");
                }
            }

            Console.ReadLine();
        }

        static void RemoveCarByModel(List<Car> cars)
        {
            Console.Write("\nEnter the Model of the car to remove: ");
            string model = Console.ReadLine();

            var carToRemove = cars.FirstOrDefault(car => car.Model.Equals(model, StringComparison.OrdinalIgnoreCase));

            if (carToRemove != null)
            {
                cars.Remove(carToRemove);
                Console.WriteLine($"Car with model {model} has been removed.");
            }
            else
            {
                Console.WriteLine($"No car found with model: {model}");
            }

            Console.ReadLine();
        }
    }
}
