
CREATE TABLE Employees (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Department NVARCHAR(50),
    Salary DECIMAL(10,2)
);
INSERT INTO Employees (Name, Department, Salary) VALUES
('John Doe', 'IT', 60000),
('Jane Smith', 'HR', 55000),
('Bob Johnson', 'IT', 50000);



