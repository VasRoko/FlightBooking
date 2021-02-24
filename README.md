# FlightBooking 

# Data Storage
To delete / reset data simply delete content at - FlightBooking.Console\bin\Debug\netcoreapp3.1\data.json

# Resources
* Project has been migrated to the latest .Net Core 3.1
* Mediator pattern - https://en.wikipedia.org/wiki/Mediator_pattern
* Command and Query Responsibility Segregation (CQRS) pattern - https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs
* Architecture in base on the book "Clean Architecture" by Robert C. Martin (Uncle Bob) - https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html

# Commands 
* Print Summary - prints all results & outcome.
* Add General {name} {age} - books general seat 
* Add Loyalty {name} {age} {points} {bool}- books loyalty seat with loyalty points & in the points are used.
* Add Airline {name} {age} - books airline emplyee seat.
