
select *from Employees


CREATE PROCEDURE sp_GetEmployees
AS
BEGIN
    SELECT Id, Name, Department, Salary FROM Employees;
END
GO

-- CREATE: Insert new employee
CREATE PROCEDURE sp_InsertEmployee
    @Name NVARCHAR(100),
    @Department NVARCHAR(50),
    @Salary DECIMAL(10,2)
AS
BEGIN
    INSERT INTO Employees (Name, Department, Salary)
    VALUES (@Name, @Department, @Salary);
END
GO

-- UPDATE: Modify employee
CREATE PROCEDURE sp_UpdateEmployee
    @Id INT,
    @Name NVARCHAR(100),
    @Department NVARCHAR(50),
    @Salary DECIMAL(10,2)
AS
BEGIN
    UPDATE Employees SET
        Name = @Name, Department = @Department, Salary = @Salary
    WHERE Id = @Id;
END
GO

-- DELETE: Remove employee
CREATE PROCEDURE sp_DeleteEmployee
    @Id INT
AS
BEGIN
    DELETE FROM Employees WHERE Id = @Id;
END
GO

CREATE PROCEDURE sp_GetEmployeeById
    @Id INT
AS
BEGIN
    SELECT Id, Name, Department, Salary 
    FROM Employees 
    WHERE Id = @Id;
END
GO

