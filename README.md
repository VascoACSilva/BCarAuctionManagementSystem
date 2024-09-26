# BCarAuctionManagementSystem

## Project Overview

The **BCarAuctionManagementSystem** is a console-based system designed to manage car auctions. It supports adding vehicles to an inventory, searching for vehicles based on various parameters, starting and closing auctions, and placing bids. The project focuses on demonstrating object-oriented design principles in C# along with unit testing using xUnit.

## Features

- Add vehicles to the auction inventory (Hatchback, Sedan, SUV, Truck)
- Search vehicles by type, manufacturer, model, or year
- Start auctions, ensuring only one active auction per vehicle at a time
- Place bids on active auctions with proper validation

## Design Decisions

### Vehicle Types and Attributes
Each type of vehicle (Hatchback, Sedan, SUV, Truck) has specific attributes such as:
- **Hatchback**: Number of doors, manufacturer, model, year, and starting bid.
- **Sedan**: Number of doors, manufacturer, model, year, and starting bid.
- **SUV**: Number of seats, manufacturer, model, year, and starting bid.
- **Truck**: Load capacity, manufacturer, model, year, and starting bid.

### Unique Identifier
Each vehicle must have a unique identifier. This is validated using a custom data annotation attribute to ensure no duplicate vehicle identifiers exist.

### Auction Management
- Auctions can only be started for vehicles that exist in the inventory.
- Auctions must be closed before placing bids.

## Error Handling

Several error cases are handled in the system:
- Duplicate vehicle identifiers
- Attempting to start an auction for a vehicle that doesn't exist or is already in an auction
- Placing bids lower than the current highest bid or for auctions that aren't active

## Unit Testing

Unit tests have been written for all critical operations using xUnit. The tests cover:
- Valid vehicle addition to the inventory
- Various edge cases, including invalid input for adding vehicles and auction management
- Searching vehicles with different combinations of parameters

## Setup Instructions

### Prerequisites
- .NET 6.0 SDK
- Visual Studio (or your preferred IDE)

### Running the Project
1. Clone the repository
2. Open the solution in Visual Studio
3. Build the project
4. Run the application

### Running Unit Tests
1. Open the solution in Visual Studio
2. Open the Test Explorer window
3. Run all tests

## Conclusion

This project demonstrates a simple yet scalable car auction system with strong emphasis on clean, object-oriented design and comprehensive unit testing.
