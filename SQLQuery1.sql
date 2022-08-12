SELECT Fname, Lname, Pname, Phone, Emai, o.Id OrderId,[Date],ProductId,Clientid FROM [Clients] c  
JOIN [Orders] o  
ON c.Id = o.Clientid  
WHERE c.Id = 1