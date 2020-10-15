# PriceCalculator

Price Calculator is a demo app developed using .net core 3.1 
It is an api that calculates components of the Price, namely Gross Price, Net Price, and Vat Amount based on only one of those and a supplied Vat rate that is known to be valid. 

Solution is comprised of 3 projects  
-PriceCalculator.Web -> Contains nothing more then the controller and actions and swashbuckle to give decently easy way to test the api. 
-PriceCalculator.Services-> Contains the bussines logic ( services )
-PriceCalculator.Services.Tests -> Contains the unit tests for the Services Layer above. 

Before running this application please make sure you have installed .Net core 3.1 SDK.

Running the application locally for debug development is as easy as cloning the repo and opening the PriceCalculator.sln solution file with Visual Studio, make sure PriceCalculator.Web is set as startup projcet and you should start debugging with F5. 

In order to build for realase  : run the following command<code> dotnet build --configuration Release  </code>
To run the application by command line run the following command after building the projects, either by the command above or via Visual Studio :<code> dotnet run --configuration Release  --project PriceCalculator/PriceCalculator.Web.csproj </code>
The application should by default run on http://localhost:5001

Running the unit test from console line is as simple as typing  <code>dotnet test </code>