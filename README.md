# 🚗 Garage Management System (C#)

A console-based Garage Management System built in C#, designed with clean object-oriented principles.  
The system lets you manage different types of vehicles, track their status, and perform basic garage operations.

---

## 🛠️ Features

- Add new vehicles to the garage (Car, Motorcycle, Truck)
- Update vehicle status (In Repair, Repaired, Paid)
- Inflate tires to maximum pressure
- Refuel or recharge vehicles
- Display all vehicle license numbers or by status
- Show detailed information for a specific vehicle
- Simple console menu interface

---

## 🧱 Technologies

- Language: C#
- Platform: .NET 6.0+
- Architecture: Object-Oriented Design (OOP)

---

## 📁 Project Structure

GarageManagementSystem/
├── GarageLogic/ # Business logic and models
│ ├── Vehicles/ # Car, Motorcycle, Truck classes
│ └── Energy/ # FuelEngine, ElectricEngine classes
├── GarageUI/ # Console user interface
└── Program.cs # Entry point


---

## 🚀 How to Run

### 1. Prerequisites

- [.NET 6.0 SDK or higher](https://dotnet.microsoft.com/en-us/download)

### 2. Build & Run

```bash
cd GarageManagementSystem
dotnet run
