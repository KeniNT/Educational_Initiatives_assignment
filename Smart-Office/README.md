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
