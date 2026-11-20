-- ============================================================
--  Garbage Collection Monitoring System  -  garbage_collection_db
--  phpMyAdmin: Import tab (or paste into the SQL tab) and click Go.
--  Test logins:
--    juan.resident  / Resident123!   (Resident)
--    maria.official / Official123!   (Municipal Official)
--    marco.driver   / Driver123!     (Collection Driver)
--  Passwords are stored as SHA-256 hashes (the app hashes the same way).
-- ============================================================
CREATE DATABASE IF NOT EXISTS garbage_collection_db CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE garbage_collection_db;

SET FOREIGN_KEY_CHECKS = 0;
DROP TABLE IF EXISTS `User`;
DROP TABLE IF EXISTS Schedule;
DROP TABLE IF EXISTS Garbage_Collectors;
DROP TABLE IF EXISTS Garbage;
DROP TABLE IF EXISTS Place;
SET FOREIGN_KEY_CHECKS = 1;

CREATE TABLE Place (
    Location_id INT NOT NULL AUTO_INCREMENT,
    Street      VARCHAR(120) NOT NULL,
    Barangay    VARCHAR(80)  NOT NULL,
    PRIMARY KEY (Location_id)
) ENGINE=InnoDB;

CREATE TABLE Garbage (
    Garbage_id       INT NOT NULL AUTO_INCREMENT,
    Location_id      INT NOT NULL,
    Garbage_category ENUM('Biodegradable','Recyclable','Non-Biodegradable','Hazardous') NOT NULL,
    PRIMARY KEY (Garbage_id),
    CONSTRAINT fk_garbage_place FOREIGN KEY (Location_id) REFERENCES Place(Location_id)
        ON UPDATE CASCADE ON DELETE CASCADE
) ENGINE=InnoDB;

CREATE TABLE Garbage_Collectors (
    Batch_id    VARCHAR(50) NOT NULL,
    Garbage_id  INT NOT NULL,
    Location_id INT NOT NULL,
    First_name  VARCHAR(60) NOT NULL,
    Middle_name VARCHAR(60) NULL,
    Last_name   VARCHAR(60) NOT NULL,
    PRIMARY KEY (Batch_id),
    CONSTRAINT fk_gc_garbage FOREIGN KEY (Garbage_id)  REFERENCES Garbage(Garbage_id)  ON UPDATE CASCADE ON DELETE CASCADE,
    CONSTRAINT fk_gc_place   FOREIGN KEY (Location_id) REFERENCES Place(Location_id)   ON UPDATE CASCADE ON DELETE CASCADE
) ENGINE=InnoDB;

CREATE TABLE Schedule (
    Schedule_id      INT NOT NULL AUTO_INCREMENT,
    Batch_id         VARCHAR(50) NOT NULL,
    Location_id      INT NOT NULL,
    Garbage_id       INT NOT NULL,
    `Date`           DATE NOT NULL,
    `Time`           TIME NOT NULL,
    Status           ENUM('Scheduled','Ongoing','Completed','Missed') NOT NULL DEFAULT 'Scheduled',
    History          VARCHAR(255) NULL,
    Garbage_category ENUM('Biodegradable','Recyclable','Non-Biodegradable','Hazardous') NOT NULL,
    PRIMARY KEY (Schedule_id),
    CONSTRAINT fk_sched_batch   FOREIGN KEY (Batch_id)    REFERENCES Garbage_Collectors(Batch_id) ON UPDATE CASCADE ON DELETE CASCADE,
    CONSTRAINT fk_sched_place   FOREIGN KEY (Location_id) REFERENCES Place(Location_id)           ON UPDATE CASCADE ON DELETE CASCADE,
    CONSTRAINT fk_sched_garbage FOREIGN KEY (Garbage_id)  REFERENCES Garbage(Garbage_id)          ON UPDATE CASCADE ON DELETE CASCADE
) ENGINE=InnoDB;

CREATE TABLE `User` (
    User_id        INT NOT NULL AUTO_INCREMENT,
    Schedule_id    INT NULL,
    User_Role      ENUM('Resident','Municipal Official','Collection Driver') NOT NULL,
    First_name     VARCHAR(60)  NOT NULL,
    Middle_name    VARCHAR(60)  NULL,
    Last_name      VARCHAR(60)  NOT NULL,
    House_Number   VARCHAR(20)  NOT NULL,
    Street         VARCHAR(120) NOT NULL,
    Barangay       VARCHAR(80)  NOT NULL,
    Municipality   VARCHAR(80)  NOT NULL,
    Region         VARCHAR(80)  NOT NULL,
    Contact_number VARCHAR(30)  NOT NULL,
    Email_Address  VARCHAR(120) NOT NULL,
    Username       VARCHAR(50)  NOT NULL,
    Password       VARCHAR(255) NOT NULL,
    PRIMARY KEY (User_id),
    UNIQUE KEY uq_user_username (Username),
    CONSTRAINT fk_user_schedule FOREIGN KEY (Schedule_id) REFERENCES Schedule(Schedule_id)
        ON UPDATE CASCADE ON DELETE SET NULL
) ENGINE=InnoDB;

-- ---------------------------- SAMPLE DATA ----------------------------
INSERT INTO Place (Location_id, Street, Barangay) VALUES
(1,'Purok 1 - Riverside','San Isidro'),
(2,'Purok 4 - Market Road','San Isidro'),
(3,'Purok 2 - Matini St.','San Isidro'),
(4,'Health Center Area','San Isidro'),
(5,'Purok 3 - Sampaguita','San Isidro'),
(6,'Purok 6 - Town Plaza','San Isidro'),
(7,'Purok 5 - Quezon Ave.','San Isidro'),
(8,'Rizal Street','San Roque');

INSERT INTO Garbage (Garbage_id, Location_id, Garbage_category) VALUES
(1,1,'Biodegradable'),
(2,2,'Recyclable'),
(3,3,'Non-Biodegradable'),
(4,4,'Hazardous'),
(5,5,'Biodegradable'),
(6,6,'Recyclable'),
(7,7,'Non-Biodegradable'),
(8,8,'Biodegradable');

INSERT INTO Garbage_Collectors (Batch_id, Garbage_id, Location_id, First_name, Middle_name, Last_name) VALUES
('BCH-0244',5,5,'Ana','','Santos'),
('BCH-0245',6,6,'Joel','','Garcia'),
('BCH-0246',7,7,'Marco','Dizon','Reyes'),
('BCH-0247',1,1,'Ana','','Santos'),
('BCH-0248',1,1,'Marco','Dizon','Reyes'),
('BCH-0249',2,2,'Ana','','Santos'),
('BCH-0250',3,3,'Joel','','Garcia'),
('BCH-0251',4,4,'Marco','Dizon','Reyes');

-- Dates are relative to the day you import, so upcoming rows stay "upcoming".
INSERT INTO Schedule (Schedule_id, Batch_id, Location_id, Garbage_id, `Date`, `Time`, Status, History, Garbage_category) VALUES
(1,'BCH-0244',5,5, CURDATE() - INTERVAL 7 DAY,'07:30:00','Completed','Collection completed','Biodegradable'),
(2,'BCH-0245',6,6, CURDATE() - INTERVAL 6 DAY,'08:00:00','Completed','Collection completed','Recyclable'),
(3,'BCH-0246',7,7, CURDATE() - INTERVAL 5 DAY,'07:00:00','Missed','Collection missed','Non-Biodegradable'),
(4,'BCH-0247',1,1, CURDATE() - INTERVAL 4 DAY,'06:30:00','Completed','Collection completed','Biodegradable'),
(5,'BCH-0248',1,1, CURDATE() + INTERVAL 1 DAY,'07:30:00','Scheduled','','Biodegradable'),
(6,'BCH-0249',2,2, CURDATE(),'09:00:00','Ongoing','Collection started','Recyclable'),
(7,'BCH-0250',3,3, CURDATE() + INTERVAL 2 DAY,'07:00:00','Scheduled','','Non-Biodegradable'),
(8,'BCH-0251',4,4, CURDATE() + INTERVAL 3 DAY,'08:00:00','Scheduled','','Hazardous');

INSERT INTO `User` (User_id, Schedule_id, User_Role, First_name, Middle_name, Last_name, House_Number, Street, Barangay, Municipality, Region, Contact_number, Email_Address, Username, Password) VALUES
(1,NULL,'Resident','Juan','Santos','Dela Cruz','24','Matini Street','San Isidro','Santa Maria','Region IV-A','+63 912 345 6789','juan.delacruz@email.com','juan.resident',SHA2('Resident123!',256)),
(2,NULL,'Municipal Official','Maria','Reyes','Lopez','1','Municipal Hall Road','San Isidro','Santa Maria','Region IV-A','+63 917 111 2222','maria.lopez@email.com','maria.official',SHA2('Official123!',256)),
(3,NULL,'Collection Driver','Marco','Dizon','Reyes','12','Riverside Road','San Isidro','Santa Maria','Region IV-A','+63 918 333 4444','marco.reyes@email.com','marco.driver',SHA2('Driver123!',256));
