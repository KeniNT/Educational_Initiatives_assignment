# Design Patterns Exercise – Use Cases

## Overview
This repository contains six use cases demonstrating various software design patterns:

- **Behavioral Patterns** – Observer, Strategy  
- **Creational Patterns** – Singleton, Factory  
- **Structural Patterns** – Adapter, Decorator  

## Use Cases

### Behavioral Patterns
1. **Observer:** Weather monitoring system updates multiple displays automatically.  
2. **Strategy:** Payment system allows choosing payment methods at runtime.

### Creational Patterns
3. **Singleton:** Configuration manager ensures a single instance.  
4. **Factory:** Shape drawing app creates shapes without exposing instantiation logic.

### Structural Patterns
5. **Adapter:** Media player supports multiple audio formats through a single interface.  
6. **Decorator:** Text editor dynamically adds features like spell check and formatting.

## How to Run
1. Clone the repository:  
   ```bash
   git clone <repository_url>


# SmartOffice (Console App)

This repository contains a C# (.NET 8) console app implementing a Smart Office facility:
- configure rooms
- book / cancel bookings
- occupancy detection (>=2 people)
- auto-release of bookings if not occupied within configured minutes
- AC & Lights control (observer pattern)
- Uses Singleton, Observer & Command patterns.

## How to run (locally)

1. Install .NET 8 SDK: https://dotnet.microsoft.com/download
2. From repository root:
   ```bash
   cd src/SmartOffice
   dotnet run
