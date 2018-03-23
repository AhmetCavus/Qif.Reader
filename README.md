# Qif.Reader

A .Net Standard library parser for the Qif format

## Getting Started

### Installation

    git clone https://github.com/AhmetCavus/Qif.Reader.git

    Load the library from the nuget package by entering "Qif.Reader" or include the DLL file into your project after you have compiled the library.

    For building the library from the command line:
    dotnet build

### Usage
    
    ```csharp
    ITransactionService service = new QifService();
    ITransactionDetail result = service.QueryFromFile<NonInvestmentTransaction>(_path + "/export.qif");
    ```

    For more information check the unit test project folder.

### Test

    dotnet test

### License
Copyright (c) 2018 Ahmet Cavus  
Licensed under the MIT license.
