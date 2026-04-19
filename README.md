# Acme Corporation
This is a small project for a company that would held a draw based on people's insertion of serial numbers, which they will get for buying the company's product. 

Each serial number is made for a Guid, which should make it sure that its uniqueness and predictability is unquestionable impossible.

## Project structure
The project are built as an .net 10, Blazor web page with an API backend. 
it contains 
- Acme.Web      (contains both api and Blazor)
- Acme.Logic    (Cover most logic in the project)
- Acme.Repo     (Cover database logic with entity framework)
- Acme.Model    (Act as a collective of important models shared between the other projects)


## Database
The database has 3 table's, one for Customer, Users and Serial Number, I didn't get around to add users, and even though Blazor provide some excellent code setup for that part. I would like to treat this as little code in Blazor as possible, without a direct relation to the database. 

The serialnumber to customer table has a restriction on max 2 customer per serial number. which is why I didn't create a fully many-to-many relation database. so this is embedded all the way down at the database level. both things can be done, but this should provide the cleanest and efficient way without room for accidents. 

## How to import Database
The database was build on the lates sql from microsoft, installed via docker. in case there will be any trouble, get docker, setup with this image: https://hub.docker.com/r/microsoft/mssql-server

Remember, on setup even though you can pass it through, microsoft are still very strong in their password politics, make sure you follow them, you will be able to create it, but it will never log you in!

I have attached an sql backup in the project folder. 

## Serial Numbers
In this task i was asked to create 100 serial numbers, I have done so and added it in the project folder, but as a man behind software I have also created an easy way to both add and download those serial number via. swaggers.

on project startup, navigate to swagger (can only be accessed via dev code); and use the Generate100SerialNumbers.
for a list of every serial numbers use the GetAllSerialNumbers. this is only possible in dev, so the code is still secure in release. my intention was to only allow sign in users with the right role to have access to that feature. 

## Dummy data
If any case you wish to have those 100 serial numbers I have created a list for in the database, with a decent bunch of customer data as well, then I have created a series of sql script that adds each set of data.

1. Add SerialNumbers. --This is not necessary to run unless you only wants the serial numbers.
2. Add Customers.
3. Add SerialNumbers with Customers. 

OBS: with this dummy data, you might want to remove an connection or two by declaring the null, otherwise you can't add any more customers to a serial number. or just use the first script.


use as you like :)

## Test
I have made a small test project that should cover test in the logic project of the code.
For that I have used Unit test and Moq.  
