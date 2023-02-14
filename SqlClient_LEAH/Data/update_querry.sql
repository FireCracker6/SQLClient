SELECT 
 c.Id, c.FirstName, c.LastName, c.Email, c.PhoneNumber,
 a.StreetName, a.PostalCode, a.City

FROM Customers c 

JOIN Addresses a ON c.AddressId = a.Id


SELECT 
 c.Id, c.FirstName, c.LastName, c.Email, c.PhoneNumber,
 a.StreetName, a.PostalCode, a.City

FROM Customers c 

JOIN Addresses a ON c.AddressId = a.Id
 WHERE c.Id = 3




IF EXISTS (SELECT Id From Customers WHERE Email= 'saxe@domain.com')
UPDATE c SET FirstName = 'Leia', LastName = 'Saxe', PhoneNumber = '070-555 55 56'
FROM Customers c 
JOIN 
Addresses a ON c.AddressId = a.Id
WHERE c.Email = 'saxe@domain.com'

UPDATE a SET StreetName = 'Grusgatan 1', PostalCode = '676 66',  City = 'Leipzig'
FROM Addresses a 
JOIN 
Customers c ON a.Id = c.AddressId

WHERE c.Email = 'saxe@domain.com'



IF EXISTS (SELECT Id From Customers WHERE Email= @Email) UPDATE c SET FirstName = @FirstName, LastName =@LastName, PhoneNumber = @PhoneNumber FROM Customers c JOIN Addresses a ON c.AddressId = a.Id WHERE c.Email = @Email UPDATE a SET StreetName = @StreetName, PostalCode = @PostalCode,  City = @City FROM Addresses a JOIN Customers c ON a.Id = c.AddressId WHERE c.Email = @Email




