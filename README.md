# Qif.Reader

This .Net Standart library provides parsing QIF files and streams.

## Getting Started

### Installation

    git clone https://github.com/AhmetCavus/Qif.Reader.git

    Load the library from the nuget package by entering "Qif.Reader" or include the DLL file into your project after you have compiled the library.

    For building the library from the command line:
    dotnet build

### Usage
    
```csharp
var qifService = new QifService(new QifReader(), new QifRepositoryContainer());
var transactions = await qifService.QueryFromFileAsync<NonInvestmentTransaction>("PATH TO THE QIF FILE");
```

    For more information check the integration test project.

### Test

    dotnet test

### License
Copyright (c) 2021 Ahmet Cavus  
Licensed under the MIT license.

See the [LICENSE](./LICENSE) file to get more information.