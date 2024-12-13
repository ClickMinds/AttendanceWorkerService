-- Create ShiftSchedules Table
CREATE TABLE ShiftSchedules (
    Id INT PRIMARY KEY,
    StartTime TIME NOT NULL,
    EndTime TIME NOT NULL
);

CREATE TABLE AttendanceStatus (
    Id INT PRIMARY KEY,
    StatusName NVARCHAR(10) NOT NULL
);

-- Create Employees Table
CREATE TABLE Employees (
    Id INT PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    ReportingManager NVARCHAR(50) NULL,
    ShiftScheduleId INT NOT NULL,
    FOREIGN KEY (ShiftScheduleId) REFERENCES ShiftSchedules(Id)
);

-- Create AttendanceRecords Table
CREATE TABLE AttendanceRecords (
    Id INT PRIMARY KEY,
    EmployeeId INT NOT NULL,
    CheckInTime DATETIME NULL,
    CheckOutTime DATETIME NULL,
    TotalHoursWorked DECIMAL(5, 2) NOT NULL DEFAULT 0,
    AttendanceStatusId INT NOT NULL,
    FOREIGN KEY (EmployeeId) REFERENCES Employees(Id),
    FOREIGN KEY (AttendanceStatusId) REFERENCES AttendanceStatus(Id) 
);
