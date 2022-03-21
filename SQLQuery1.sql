create TABLE Author
(Id INT PRIMARY KEY IDENTITY,
Name VARCHAR(100),
Email VARCHAR(100),
Mobile VARCHAR(20)
)

create TABLE Books
(Id INT PRIMARY KEY IDENTITY,
Title VARCHAR(100),
Price MONEY,
Quantity INT,
Published_On VARCHAR(100),
Author_Id INT FOREIGN KEY REFERENCES Author(Id)
)

INSERT INTO Author(Name,Email,Mobile)
VALUES
('Ankit','ama@gail.com','9723230023'),
('Ratnesh','Raa@gail.com','2323230023'),
('Shshi','Boma@gail.com','87443230023');


INSERT INTO Books(Title,Price,Quantity,Published_On,Author_Id) VALUES
('Angular','11',1200,400,grtData(),1),
('Reactjs','12',160,600,grtData(),2),

('Javascript','14',140,700,grtData(),1);
