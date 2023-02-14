SELECT Email
FROM Customers
IF EXISTS ( SELECT * FROM Customers) 
PRINT 'Records found'
ELSE 
PRINT 'No such record'
DELETE FROM Customers Where Email = @Email


SELECT * FROM Customers

