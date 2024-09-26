using Xunit;
using BCarAuctionManagementSystem.Managers;
using BCarAuctionManagementSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace BCarAuctionManagementSystem.UnitTests
{
    public class AuctionManagerTests
    {
        private readonly AuctionManager _auctionManager;

        public AuctionManagerTests()
        {
            _auctionManager = new AuctionManager();
        }

        [Fact]
        public void AddVehicle_ValidHatchback_AddsToInventory()
        {
            // Arrange - Add a valid hatchback
            _auctionManager.AddVehicle("Hatchback", "HATCH1", "Honda", "Civic", 2020, 15000, 5, null, null);

            // Act - Check if the vehicle was added to the inventory
            var vehicle = _auctionManager.inventory.FirstOrDefault(v => v.Identifier == "HATCH1");

            // Assert - Ensure the vehicle was added and has the correct attributes
            Assert.NotNull(vehicle);
            Assert.Equal("HATCH1", vehicle?.Identifier);
            Assert.Equal("Hatchback", vehicle?.VehicleType);
        }

        [Fact]
        public void AddVehicle_HatchbackMissingNumberOfDoors_ThrowsValidationException()
        {
            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() =>
                _auctionManager.AddVehicle("Hatchback", "HAT123", "Honda", "Civic", 2020, 15000, null, null, null)
            );

            Assert.Contains("Validation failed", exception.Message);
        }

        [Fact]
        public void AddVehicle_ValidSedan_AddsToInventory()
        {
            // Arrange - Add a valid sedan
            _auctionManager.AddVehicle("Sedan", "SEDAN1", "Toyota", "Camry", 2021, 20000, 4, null, null);

            // Act - Check if the vehicle was added to the inventory
            var vehicle = _auctionManager.inventory.FirstOrDefault(v => v.Identifier == "SEDAN1");

            // Assert - Ensure the vehicle was added and has the correct attributes
            Assert.NotNull(vehicle);
            Assert.Equal("SEDAN1", vehicle?.Identifier);
            Assert.Equal("Sedan", vehicle?.VehicleType);
        }

        [Fact]
        public void AddVehicle_SedanMissingNumberOfDoors_ThrowsValidationException()
        {
            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() =>
                _auctionManager.AddVehicle("Sedan", "SED123", "Toyota", "Camry", 2019, 18000, null, null, null)
            );

            Assert.Contains("Validation failed", exception.Message);
        }

        [Fact]
        public void AddVehicle_ValidSUV_AddsToInventory()
        {
            // Arrange - Add a valid SUV
            _auctionManager.AddVehicle("SUV", "SUV001", "Ford", "Explorer", 2022, 30000, null, 7, null);

            // Act - Check if the vehicle was added to the inventory
            var vehicle = _auctionManager.inventory.FirstOrDefault(v => v.Identifier == "SUV001");

            // Assert - Ensure the vehicle was added and has the correct attributes
            Assert.NotNull(vehicle);
            Assert.Equal("SUV001", vehicle?.Identifier);
            Assert.Equal("SUV", vehicle?.VehicleType);
        }

        [Fact]
        public void AddVehicle_SUVMissingNumberOfSeats_ThrowsValidationException()
        {
            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() =>
                _auctionManager.AddVehicle("SUV", "SUV789", "Ford", "Explorer", 2021, 25000, null, null, null)
            );

            Assert.Contains("Validation failed", exception.Message);
        }

        [Fact]
        public void AddVehicle_ValidTruck_AddsToInventory()
        {
            // Arrange - Add a valid truck
            _auctionManager.AddVehicle("Truck", "TRUCK1", "Chevrolet", "Silverado", 2020, 40000, null, null, 5000);

            // Act - Check if the vehicle was added to the inventory
            var vehicle = _auctionManager.inventory.FirstOrDefault(v => v.Identifier == "TRUCK1");

            // Assert - Ensure the vehicle was added and has the correct attributes
            Assert.NotNull(vehicle);
            Assert.Equal("TRUCK1", vehicle?.Identifier);
            Assert.Equal("Truck", vehicle?.VehicleType);
        }

        [Fact]
        public void AddVehicle_TruckMissingLoadCapacity_ThrowsValidationException()
        {
            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() =>
                _auctionManager.AddVehicle("Truck", "TRK123", "Chevrolet", "Silverado", 2020, 30000, null, null, null)
            );

            Assert.Contains("Validation failed", exception.Message);
        }

        [Fact]
        public void AddVehicle_InvalidVehicleDuplicateIdentifier_ThrowsValidationException()
        {
            // Arrange - First, add a vehicle with a certain identifier
            _auctionManager.AddVehicle("SUV", "DUP123", "Toyota", "RAV4", 2020, 20000, null, 5, null);

            // Act & Assert - Try to add another vehicle with the same identifier
            var exception = Assert.Throws<ValidationException>(() =>
                _auctionManager.AddVehicle("SUV", "DUP123", "Toyota", "Highlander", 2021, 25000, null, 7, null)
            );

            Assert.Contains("A vehicle with identifier DUP123 already exists.", exception.Message);
        }

        [Theory]
        [InlineData("", "SUV123", "Toyota", "RAV4", 2020, 20000, null, 5, null)]// Missing vehicle type
        [InlineData("SUV", "", "Toyota", "RAV4", 2020, 20000, null, 5, null)]// Missing identifier
        [InlineData("SUV", "SUV123", "", "RAV4", 2020, 20000, null, 5, null)]// Missing manufacturer
        [InlineData("SUV", "SUV123", "Toyota", "", 2020, 20000, null, 5, null)]// Missing model
        [InlineData("SUV", "SUV123", "Toyota", "RAV4", null, 20000, null, 5, null)]// Missing year
        [InlineData("SUV", "SUV123", "Toyota", "RAV4", 2020, null, null, 5, null)]// Missing starting bid
        [InlineData("SUVMARINE", "SUV123", "Toyota", "RAV4", 2020, 20000, null, 5, null)]  // Invalid vehicle type
        [InlineData("SUV", "SUV1234", "Toyota", "RAV4", 2020, 20000, null, 5, null)]  // Invalid identifier length (too long)
        [InlineData("SUV", "SUV12", "Toyota", "RAV4", 2020, 20000, null, 5, null)]  // Invalid identifier length (too short)
        [InlineData("SUV", "$U<12£", "Toyota", "RAV4", 2020, 20000, null, 5, null)]  // Invalid identifier format (should be uppercase alphanumeric)
        [InlineData("SUV", "SUV123", "Toyota Motor Corporation, the Japanese multinational automotive manufacturer headquartered in Toyota City", "RAV4", 2020, 20000, null, 5, null)]  // Invalid manufacturer length
        [InlineData("SUV", "SUV123", "Toyota", "RAV4 also known as Toyota Vanguard (Japan, 2005–2016), Toyota Wildlander (China, 2020–present), Suzuki Across (Europe, 2020–present)", 2020, 20000, null, 5, null)]  // Invalid model length
        [InlineData("SUV", "SUV123", "Toyota", "RAV4", 1924, 20000, null, 5, null)]  // Invalid year too low
        [InlineData("SUV", "SUV123", "Toyota", "RAV4", 3024, 20000, null, 5, null)]  // Invalid year too high 
        [InlineData("SUV", "SUV123", "Toyota", "RAV4", 2020, 0, null, 5, null)]  // Invalid starting bid (not positive value)
        [InlineData("SUV", "SUV123", "Toyota", "RAV4", 2020, 20000.005, null, 5, null)]  // Invalid starting bid format (more than 2 decimal places)
        public void AddVehicle_InvalidParameters_ThrowsValidationException(
    string vehicleType, string identifier, string manufacturer, string model, int year, decimal startingBid, int? numberOfDoors, int? numberOfSeats, decimal? loadCapacity)
        {
            // Act & Assert
            var exception = Assert.Throws<ValidationException>(() =>
                _auctionManager.AddVehicle(vehicleType, identifier, manufacturer, model, year, startingBid, numberOfDoors, numberOfSeats, loadCapacity)
            );

            Assert.Contains("Validation failed", exception.Message);
        }

        [Fact]
        public void SearchVehicles_ByVehicleType_ReturnsCorrectResults()
        {
            // Arrange - Add vehicles to the inventory
            _auctionManager.AddVehicle("Hatchback", "MZ2589", "Mazda", "Mazda 6 Wagon 2.0 MZR-CD", 2005, 100000, 5, null, null);
            _auctionManager.AddVehicle("Sedan", "1144AA", "Ford", "Focus", 2009, 5000, 5, null, null);

            // Act - Search for Hatchbacks
            var results = _auctionManager.SearchVehicles(vehicleType: "Hatchback", manufacturer: null, model: null, year: null);

            // Assert - Ensure only the Hatchback is returned
            Assert.Single(results);
            Assert.Equal("MZ2589", results.First().Identifier);
            Assert.Equal("Hatchback", results.First().VehicleType);
        }

        [Fact]
        public void SearchVehicles_ByManufacturer_ReturnsCorrectResults()
        {
            // Arrange - Add vehicles to the inventory
            _auctionManager.AddVehicle("Hatchback", "MZ2589", "Mazda", "Mazda 6 Wagon 2.0 MZR-CD", 2005, 100000, 5, null, null);
            _auctionManager.AddVehicle("Sedan", "1144AA", "Ford", "Focus", 2009, 5000, 5, null, null);

            // Act - Search for vehicles by manufacturer Mazda
            var results = _auctionManager.SearchVehicles(vehicleType: null, manufacturer: "Mazda", model: null, year: null);

            // Assert - Ensure only Mazda vehicles are returned
            Assert.Single(results);
            Assert.Equal("MZ2589", results.First().Identifier);
            Assert.Equal("Mazda", results.First().Manufacturer);
        }

        [Fact]
        public void SearchVehicles_ByModel_ReturnsCorrectResults()
        {
            // Arrange - Add vehicles to the inventory
            _auctionManager.AddVehicle("Hatchback", "MZ2589", "Mazda", "Mazda 6 Wagon 2.0 MZR-CD", 2005, 100000, 5, null, null);
            _auctionManager.AddVehicle("Sedan", "1144AA", "Ford", "Focus", 2009, 5000, 5, null, null);

            // Act - Search for vehicles by model "Focus"
            var results = _auctionManager.SearchVehicles(vehicleType: null, manufacturer: null, model: "Focus", year: null);

            // Assert - Ensure only the Focus is returned
            Assert.Single(results);
            Assert.Equal("1144AA", results.First().Identifier);
            Assert.Equal("Focus", results.First().Model);
        }

        [Fact]
        public void SearchVehicles_ByYear_ReturnsCorrectResults()
        {
            // Arrange - Add vehicles to the inventory
            _auctionManager.AddVehicle("Hatchback", "MZ2589", "Mazda", "Mazda 6 Wagon 2.0 MZR-CD", 2005, 100000, 5, null, null);
            _auctionManager.AddVehicle("Sedan", "1144AA", "Ford", "Focus", 2009, 5000, 5, null, null);

            // Act - Search for vehicles from the year 2005
            var results = _auctionManager.SearchVehicles(vehicleType: null, manufacturer: null, model: null, year: 2005);

            // Assert - Ensure only the 2005 vehicle is returned
            Assert.Single(results);
            Assert.Equal("MZ2589", results.First().Identifier);
            Assert.Equal(2005, results.First().Year);
        }

        [Theory]
        [InlineData("SUV", null, null, null, new[] { "AA1122", "33BB44" })] // Search by vehicle type
        [InlineData(null, "Honda", null, null, new[] { "AA1122", "5566CC" })] // Search by manufacturer
        [InlineData(null, null, "Accord", null, new[] { "5566CC", "33BB44" })] // Search by model
        [InlineData(null, null, null, 2010, new[] { "AA1122", "DD7788" })] // Search by year
        public void SearchVehicles_MultipleMatches_ReturnsAllMatchingResults(string? vehicleType, string? manufacturer, string? model, int? year, string[] expectedIdentifiers)
        {
            // Arrange - Add 4 vehicles with overlapping attributes
            _auctionManager.AddVehicle("SUV", "AA1122", "Honda", "Civic", 2010, 15000, null, 5, null);
            _auctionManager.AddVehicle("SUV", "33BB44", "Ford", "Accord", 2011, 20000, null, 5, null);
            _auctionManager.AddVehicle("Sedan", "5566CC", "Honda", "Accord", 2009, 18000, 4, null, null);
            _auctionManager.AddVehicle("Hatchback", "DD7788", "Mazda", "Mazda 3", 2010, 16000, 5, null, null);

            // Act - Perform the search with the given criteria
            var results = _auctionManager.SearchVehicles(vehicleType, manufacturer, model, year);

            // Assert - Ensure the correct number of vehicles are returned
            Assert.Equal(2, results.Count);

            // Assert - Ensure the expected vehicles are returned based on identifiers
            foreach (var expectedIdentifier in expectedIdentifiers)
            {
                Assert.Contains(results, v => v.Identifier == expectedIdentifier);
            }
        }

        [Fact]
        public void StartAuction_ValidIdentifierAndAuctionState_StartsAuction()
        {
            // Arrange - Add a vehicle with NotStarted auction state
            _auctionManager.AddVehicle("SUV", "SUV123", "Toyota", "RAV4", 2020, 20000, null, 5, null);

            // Act - Start the auction for the vehicle
            _auctionManager.StartAuction("SUV123");

            // Assert - Ensure the auction state is set to Active
            var vehicle = _auctionManager.inventory.FirstOrDefault(v => v.Identifier == "SUV123");
            Assert.NotNull(vehicle);
            Assert.Equal(AuctionState.Active, vehicle?.AuctionState);
        }

        [Fact]
        public void StartAuction_VehicleIdentifierDoesNotExist_ThrowsInvalidOperationException()
        {
            // Act & Assert - Attempt to start an auction for a non-existent vehicle
            var exception = Assert.Throws<ArgumentException>(() =>
                _auctionManager.StartAuction("NONEXISTENT123")
            );

            Assert.Equal("Vehicle with identifier NONEXISTENT123 does not exist.", exception.Message);
        }

        [Theory]
        [InlineData(AuctionState.Active, "Auction for vehicle SUV123 is already active.")]
        [InlineData(AuctionState.Closed, "Auction for vehicle SUV123 has already concluded.")]
        public void StartAuction_InvalidVehicleAuctionState_ThrowsInvalidOperationException(AuctionState initialState, string expectedMessage)
        {
            // Arrange - Add a vehicle with a specific auction state (Active or Closed)
            _auctionManager.AddVehicle("SUV", "SUV123", "Toyota", "RAV4", 2020, 20000, null, 5, null);
            var vehicle = _auctionManager.inventory.FirstOrDefault(v => v.Identifier == "SUV123");
            vehicle.AuctionState = initialState;

            // Act & Assert - Attempt to start the auction and check for the correct exception
            var exception = Assert.Throws<InvalidOperationException>(() =>
                _auctionManager.StartAuction("SUV123")
            );

            Assert.Equal(expectedMessage, exception.Message);
        }

        [Fact]
        public void CloseAuction_ValidIdentifierAndAuctionState_ClosesAuction()
        {
            // Arrange - Add a vehicle and set its auction state to Active
            _auctionManager.AddVehicle("SUV", "SUV123", "Toyota", "RAV4", 2020, 20000, null, 5, null);
            var vehicle = _auctionManager.inventory.FirstOrDefault(v => v.Identifier == "SUV123");
            vehicle.AuctionState = AuctionState.Active;

            // Act - Close the auction for the vehicle
            _auctionManager.CloseAuction("SUV123");

            // Assert - Ensure the auction state is set to Closed
            Assert.NotNull(vehicle);
            Assert.Equal(AuctionState.Closed, vehicle?.AuctionState);
        }

        [Fact]
        public void CloseAuction_VehicleIdentifierDoesNotExist_ThrowsInvalidOperationException()
        {
            // Act & Assert - Attempt to close an auction for a non-existent vehicle
            var exception = Assert.Throws<ArgumentException>(() =>
                _auctionManager.CloseAuction("NONEXISTENT123")
            );

            Assert.Equal("Vehicle with identifier NONEXISTENT123 does not exist.", exception.Message);
        }

        [Theory]
        [InlineData(AuctionState.NotStarted, "Auction for vehicle SUV123 is not currently active.")]
        [InlineData(AuctionState.Closed, "Auction for vehicle SUV123 is not currently active.")]
        public void CloseAuction_InvalidVehicleAuctionState_ThrowsInvalidOperationException(AuctionState initialState, string expectedMessage)
        {
            // Arrange - Add a vehicle with a specific auction state (NotStarted or Closed)
            _auctionManager.AddVehicle("SUV", "SUV123", "Toyota", "RAV4", 2020, 20000, null, 5, null);
            var vehicle = _auctionManager.inventory.FirstOrDefault(v => v.Identifier == "SUV123");
            vehicle.AuctionState = initialState;

            // Act & Assert - Attempt to close the auction and check for the correct exception
            var exception = Assert.Throws<InvalidOperationException>(() =>
                _auctionManager.CloseAuction("SUV123")
            );

            Assert.Equal(expectedMessage, exception.Message);
        }

        [Fact]
        public void PlaceBid_ValidIdentifierAuctionStateAndBidValue_PlacesBid()
        {
            // Arrange - Add a vehicle with Active auction state and a starting bid
            _auctionManager.AddVehicle("SUV", "SUV123", "Toyota", "RAV4", 2020, 20000, null, 5, null);
            var vehicle = _auctionManager.inventory.FirstOrDefault(v => v.Identifier == "SUV123");
            vehicle.AuctionState = AuctionState.Active;
            vehicle.HighestBid = 20000m;

            // Act - Place a valid bid higher than the current highest bid
            _auctionManager.PlaceBid("SUV123", 25000m);

            // Assert - Ensure the bid was placed and the highest bid is updated
            Assert.Equal(25000m, vehicle?.HighestBid);
        }

        [Fact]
        public void PlaceBid_VehicleIdentifierDoesNotExist_ThrowsInvalidOperationException()
        {
            // Act & Assert - Attempt to place a bid on a non-existent vehicle
            var exception = Assert.Throws<ArgumentException>(() =>
                _auctionManager.PlaceBid("NONEXISTENT123", 25000m)
            );

            Assert.Equal("Vehicle with identifier NONEXISTENT123 does not exist.", exception.Message);
        }

        [Theory]
        [InlineData(AuctionState.NotStarted, "Auction for vehicle SUV123 is not currently active.")]
        [InlineData(AuctionState.Closed, "Auction for vehicle SUV123 is not currently active.")]
        public void PlaceBid_InvalidVehicleAuctionState_ThrowsInvalidOperationException(AuctionState initialState, string expectedMessage)
        {
            // Arrange - Add a vehicle with a specific auction state (NotStarted or Closed)
            _auctionManager.AddVehicle("SUV", "SUV123", "Toyota", "RAV4", 2020, 20000, null, 5, null);
            var vehicle = _auctionManager.inventory.FirstOrDefault(v => v.Identifier == "SUV123");
            vehicle.AuctionState = initialState;

            // Act & Assert - Attempt to place a bid and check for the correct exception
            var exception = Assert.Throws<InvalidOperationException>(() =>
                _auctionManager.PlaceBid("SUV123", 25000m)
            );

            Assert.Equal(expectedMessage, exception.Message);
        }

        [Theory]
        [InlineData(20000)]
        [InlineData(2000)]
        [InlineData(-2)]
        public void PlaceBid_NewBidNotHigherThanCurrentHighest_ThrowsInvalidOperationException(decimal newBid)
        {
            // Arrange - Add a vehicle with Active auction state and a highest bid of 20000
            _auctionManager.AddVehicle("SUV", "SUV123", "Toyota", "RAV4", 2020, 20000, null, 5, null);
            var vehicle = _auctionManager.inventory.FirstOrDefault(v => v.Identifier == "SUV123");
            vehicle.AuctionState = AuctionState.Active;
            vehicle.HighestBid = 20000;

            // Act & Assert - Attempt to place a bid lower or equal to the current highest bid
            var exception = Assert.Throws<InvalidOperationException>(() =>
                _auctionManager.PlaceBid("SUV123", newBid)
            );

            Assert.Equal($"New bids for vehicle SUV123 must be higher than the current highest bid: 20000€.", exception.Message);
        }

    }
}