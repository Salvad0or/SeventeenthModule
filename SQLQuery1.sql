SELECT c.Id Id, Fname, Lname, Pname , Phone, Emai EMail, o.Id OrderId, [Date], ProductId, Clientid FROM CLIENTS c
JOIN ORDERS O
ON c.Id = o.Clientid
WHERE C.Fname = 'Alex'

SELECT * FROM Clients c 
JOIN Orders o  
ON c.Id = o.Clientid
WHERE c.Id = 1


INSERT Orders
VALUES
(GETDATE(), 4, 1)