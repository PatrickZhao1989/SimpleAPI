# SimpleAPI
This is a simple API to run CRUD on Product

# Pre-requisite 
- [.NET 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0), tested with 6.0.202, see `global.json`
- VSCode configuration provided in `.vscode`
- If you are on any other IDE, please figure it out yourself or ask Patrick

# Debug in VSCOde 
```shell
cd src/SampleApi
```
**Press F5 on your keyboard**

# Run
```shell
cd src/SampleApi
dotnet run
```
Navigate to `https://localhost:7280/swagger/index.html`

Follow the Swagger instructions

Have fun!

# Testing

```shell
cd src/Tests/SampleApiTests
dotnet test
```

# Cloud Setup and deployment

```shell
cd src/SampleAPi
docker build 
docker run -d -p 8080:80
```

Try cloud version here: 
https://samplewebappfortest.azurewebsites.net/swagger/index.html 
