# BCarAuctionManagementSystem

## Introduction
This project is a **Car Auction Management System** designed to meet the requirements specified in the problem statement. The system is built in **C#** and adheres to object-oriented principles, with a focus on **modularity**, **scalability**, and **robust error handling**. It includes functionality to add different vehicle types, manage auctions, place bids, and search for vehicles based on various attributes.

## Design Decisions
The design of this system was guided by several key principles, particularly **modularity**, **scalability**, and **extensibility**, with the goal of creating a solution that is **easy to understand**, **maintain**, and **extend** in the future. Below is a breakdown of some of the key design decisions:

### 1. **Use of Data Annotations for Validation**
- To simplify validation logic and ensure consistency, I used **data annotations** extensively. This allowed the system to handle input validation declaratively, rather than through manually written validation logic within methods.
- For example, vehicle attributes like the **Identifier**, **Manufacturer**, and **Model** are validated using annotations such as `[StringLength]`, `[Required]`, and `[RegularExpression]`, ensuring they adhere to the specified requirements.
- Custom validation attributes like `TwoDecimalPlacesAttribute` were implemented to enforce business-specific validation rules, such as ensuring that bid values do not exceed two decimal places.

### 2. **Modular and Scalable Architecture**
- The code was divided into logically independent layers, such as **Factories**, **Managers**, and **Models**, each serving a distinct purpose.
- The **VehicleFactory** was implemented using a simple factory pattern to handle the creation of different vehicle types (Sedan, SUV, Hatchback, Truck). This pattern makes it easy to add new vehicle types in the future without changing the core auction logic.
- The **AuctionManager** serves as the main class for managing the auction system, centralizing operations like adding vehicles, starting and closing auctions, and placing bids.

### 3. **Extensibility for Future States**
- When designing methods such as `StartAuction` and `CloseAuction`, I used enums like `AuctionState` to represent the current state of an auction. This approach ensures that more states (such as “Cancelled” or “Paused”) can be easily added without major changes to the codebase.
- The switch case handling auction states was built with future expansion in mind, allowing for clean error handling if additional states are added.

### 4. **Separation of Concerns**
- The system was designed to ensure that each class has a **single responsibility**. For example, the **Vehicle** class only contains properties related to the vehicle itself, while **AuctionManager** handles operations such as starting or closing auctions.
- This separation makes it easier to modify one part of the system without affecting others, contributing to long-term maintainability.

### 5. **Error Handling and Edge Cases**
- Extensive error handling was built into the system, including checks for:
  - Duplicate vehicle identifiers when adding vehicles.
  - Invalid bids that are lower than the current highest bid.
  - Starting an auction for a vehicle that is already in an active or closed state.
  - Searching for vehicles that do not exist or match the given criteria.
- These validations ensure the system gracefully handles invalid operations, preventing the system from entering an inconsistent state.

### 6. **Test-Driven Development Approach**
- The system was built and tested using **xUnit**, ensuring that each feature is covered by comprehensive unit tests. This includes not only positive cases (where operations succeed) but also negative cases (where errors are thrown).
- Test coverage includes:
  - Adding vehicles with both valid and invalid parameters.
  - Ensuring auctions can only start or close when in the correct state.
  - Validating bid logic, ensuring bids are higher than the current highest bid and have no more than two decimal places.
  - Searching for vehicles based on type, manufacturer, model, and year, with handling for multiple and no results.

## Conclusion / Personal Note
In this project I hope to demonstrate my understanding of object-oriented design principles, focusing on **modularity**, **scalability**, and **robust error handling**. I attempted to design it to be easily extensible for future requirements, such as adding new vehicle types or auction states, and to provide a **clean** and **testable** codebase. By using data annotations, separation of concerns, and best practices in error handling, I built this solution with both the present functionality and future growth in mind.
