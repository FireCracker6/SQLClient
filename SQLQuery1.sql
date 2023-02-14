
-- lägger till en adress och en kund med den adressen

DECLARE @FirstName nvarchar(50) SET @FirstName = 'Anna'
DECLARE @LastName nvarchar(50) SET @LastName = 'Svanström'
DECLARE @Email nvarchar(100) SET @Email = 'anna@domain.com'
DECLARE @PhoneNumber char(13) SET @PhoneNumber = '073-055 45 55'
DECLARE @AddressId int SET @AddressId = 3

DECLARE @StreetName nvarchar(100) SET @StreetName = 'Poseidongatan 16'
DECLARE @PostalCode char(6) SET @PostalCode = '723 58'
DECLARE @City nvarchar(100) SET @City = 'Västerås'

-- saves address to database if not exists else it and returns the id else it returns the id of the already inserted address 

IF NOT EXISTS ( SELECT Id FROM Addresses WHERE StreetName = @StreetName AND PostalCode = @PostalCode AND City = @City)
		INSERT INTO Addresses OUTPUT INSERTED.Id VALUES (@StreetName, @PostalCode, @City)
ELSE 
		( SELECT Id FROM Addresses WHERE StreetName = @StreetName AND PostalCode = @PostalCode AND City = @City)


-- saves the customer to the databases if no customer with the same email address already exists

IF NOT EXISTS (SELECT Id From Customers WHERE Email = @Email)
INSERT INTO Customers OUTPUT INSERTED.Id VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @AddressId)



-- hämtar alla adresses och hämtar alla kunder
SELECT * FROM Addresses
SELECT * FROM Customers

-- hämtar alla kunder och dess adress
SELECT 
 c.Id, c.FirstName, c.LastName, c.Email, c.PhoneNumber,
 a.StreetName, a.PostalCode, a.City

FROM Customers c 
JOIN Addresses a ON c.AddressId = a.Id





-- hämtar en specifik kund och adressen för kunden
SELECT 
 c.Id, c.FirstName, c.LastName, c.Email, c.PhoneNumber,
 a.StreetName, a.PostalCode, a.City

FROM Customers c 
JOIN Addresses a ON c.AddressId = a.Id
WHERE c.Email = @Email


