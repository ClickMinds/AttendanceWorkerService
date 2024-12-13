-- Insert ShiftSchedules
INSERT INTO ShiftSchedules (Id, StartTime, EndTime)
VALUES
(1, '09:00:00', '18:00:00'),
(2, '10:00:00', '19:00:00'),
(3, '08:00:00', '17:00:00');

INSERT INTO AttendanceStatus (Id, StatusName)
VALUES
(1, 'Present'),
(2, 'Absent'),
(3, 'Incomplete'),
(4, 'Late');

-- Insert Employees
INSERT INTO Employees (Id, Name, ReportingManager, ShiftScheduleId)
VALUES
(1, 'Alok', 'Himanshu Shekhar', 1),
(2, 'Shubham Gupta', 'Himanshu Shekhar', 2),
(3, 'Shashank Pandey', 'Himanshu Shekhar', 1),
(4, 'Shashank Verma', 'Himanshu Shekhar', 3),
(5, 'Vinay', 'Himanshu Shekhar', 2),
(6, 'Nishant', 'Himanshu Shekhar', 1),
(7, 'Harshit', 'Himanshu Shekhar', 3),
(8, 'Rahul', 'Himanshu Shekhar', 2),
(9, 'Nitish', 'Himanshu Shekhar', 1),
(10, 'Rahul Mishra', 'Himanshu Shekhar', 3),
(11, 'Rajesh Sharma', 'Himanshu Shekhar', 1),
(12, 'Deeap Gupta', 'Himanshu Shekhar', 2),
(13, 'Ashish Mohan', 'Himanshu Shekhar', 3),
(14, 'Lalita Verma', 'Himanshu Shekhar', 1),
(15, 'Urmila', 'Himanshu Shekhar', 3),
(16, 'Pankaj B', 'Himanshu Shekhar', 2),
(17, 'Ruchi', 'Himanshu Shekhar', 1),
(18, 'Shweta', 'Himanshu Shekhar', 2),
(19, 'Atul', 'Himanshu Shekhar', 3),
(20, 'Surinder', 'Himanshu Shekhar', 1);

-- Insert AttendanceRecords
INSERT INTO AttendanceRecords (Id, EmployeeId, CheckInTime, CheckOutTime, TotalHoursWorked, AttendanceStatusId)
VALUES
(1, 1, '2024-12-01 09:15:00', '2024-12-01 18:00:00', 8, 4),
(2, 2, '2024-12-01 10:05:00', '2024-12-01 19:00:00', 8, 4),
(3, 3, '2024-12-01 09:00:00', '2024-12-01 17:00:00', 8, 3),
(4, 4, NULL, NULL, 0, 2),
(5, 5, '2024-12-01 10:00:00', '2024-12-01 17:00:00', 7, 3),
(6, 6, '2024-12-01 09:10:00', '2024-12-01 18:10:00', 9, 1),
(7, 7, NULL, NULL, 0, 2),
(8, 8, '2024-12-01 10:15:00', '2024-12-01 19:00:00', 8, 4),
(9, 9, '2024-12-01 09:00:00', '2024-12-01 18:00:00', 9, 1),
(10, 10, '2024-12-01 08:50:00', '2024-12-01 17:00:00', 8, 1),
-- Add more similar records up to 50
(11, 11, '2024-12-02 09:10:00', '2024-12-02 17:50:00', 8, 4),
(12, 12, NULL, NULL, 0, 2),
(13, 13, '2024-12-02 09:00:00', '2024-12-02 18:00:00', 9, 1),
(14, 14, '2024-12-02 08:45:00', '2024-12-02 16:45:00', 8, 3),
(15, 15, '2024-12-02 10:05:00', '2024-12-02 19:00:00', 8, 4),
(16, 16, '2024-12-02 09:00:00', '2024-12-02 18:00:00', 9, 1),
(17, 17, '2024-12-02 08:55:00', '2024-12-02 18:00:00', 9, 1),
(18, 18, '2024-12-02 10:15:00', '2024-12-02 19:00:00', 8, 4),
(19, 19, NULL, NULL, 0, 2),
(20, 20, '2024-12-02 09:00:00', '2024-12-02 18:00:00', 9, 1),
(21, 1, '2024-12-01 09:15:00', '2024-12-01 18:00:00', 8, 4),
(22, 2, '2024-12-01 10:05:00', '2024-12-01 19:00:00', 8, 4),
(23, 3, '2024-12-01 09:00:00', '2024-12-01 17:00:00', 8, 3),
(24, 4, NULL, NULL, 0, 2),
(25, 5, '2024-12-01 10:00:00', '2024-12-01 17:00:00', 7, 3);
