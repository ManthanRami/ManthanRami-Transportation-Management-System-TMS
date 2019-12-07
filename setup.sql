DROP DATABASE TMS;
CREATE DATABASE IF NOT EXISTS TMS;
USE TMS;

DROP USER IF EXISTS 'tms'@'localhost';
CREATE USER 'tms'@'localhost' IDENTIFIED BY 'tmsPassword*2019';
GRANT SELECT, INSERT, UPDATE, DELETE, DROP ON `tms`.* TO 'tms'@'localhost';

CREATE TABLE IF NOT EXISTS `User` (
	UserID			INT UNSIGNED NOT NULL AUTO_INCREMENT,
	Username		VARCHAR(32) NOT NULL UNIQUE,
    `Password`		VARCHAR(64) NOT NULL,
    Email			VARCHAR(32) NOT NULL,
    FirstName		VARCHAR(32) NOT NULL,
    LastName		VARCHAR(32) NOT NULL,
    UserType		TINYINT DEFAULT 0,
    
    PRIMARY KEY (UserID)
);

CREATE TABLE IF NOT EXISTS Carrier (
	CarrierID		INT UNSIGNED NOT NULL AUTO_INCREMENT,
	`Name`			VARCHAR(32) NOT NULL,
    DepotCity		VARCHAR(32) NOT NULL,
    FtlAvailability	INT NOT NULL,
    LtlAvailability	INT NOT NULL,
    
    PRIMARY KEY (CarrierID)
);

CREATE TABLE IF NOT EXISTS FTLRate (
	CarrierID	INT UNSIGNED NOT NULL UNIQUE,
    Rate		FLOAT NOT NULL,
    
    PRIMARY KEY (CarrierID),
    FOREIGN KEY (CarrierID) REFERENCES Carrier(CarrierID)
);

CREATE TABLE IF NOT EXISTS LTLRate (
	CarrierID	INT UNSIGNED NOT NULL UNIQUE,
    Rate		FLOAT NOT NULL,
    
    PRIMARY KEY (CarrierID),
    FOREIGN KEY (CarrierID) REFERENCES Carrier(CarrierID)
);

CREATE TABLE IF NOT EXISTS ReeferCharge (
	CarrierID	INT UNSIGNED NOT NULL UNIQUE,
    Charge		FLOAT NOT NULL,
    
    PRIMARY KEY (CarrierID),
    FOREIGN KEY (CarrierID) REFERENCES Carrier(CarrierID)
);

CREATE TABLE IF NOT EXISTS Customer (
	CustomerID		INT UNSIGNED NOT NULL AUTO_INCREMENT,
    CustomerName	VARCHAR(48) NOT NULL,
    
    PRIMARY KEY (CustomerID)
);

CREATE TABLE IF NOT EXISTS Contract (
	ContractID	INT UNSIGNED NOT NULL AUTO_INCREMENT,
    CarrierID	INT UNSIGNED NOT NULL,
    CustomerID	INT UNSIGNED NOT NULL,
	`Status`	TINYINT	NOT NULL,
    Quantity	INT NOT NULL,
    LoadType	TINYINT NOT NULL,
    VanType		TINYINT NOT NULL,
    OriginCity	VARCHAR(32) NOT NULL,
    DestCity	VARCHAR(32) NOT NULL,
    
    PRIMARY KEY (ContractID),
    FOREIGN KEY (CarrierID) REFERENCES Carrier(CarrierID),
    FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID)
);

/* INSERT CARRIERS */
INSERT INTO Carrier VALUES (NULL, "Planet Express", "Windsor", 50, 640); -- 1
INSERT INTO FTLRate VALUE (1, 5.21);
INSERT INTO LTLRate VALUE (1, 0.3621);
INSERT INTO ReeferCharge VALUE (1, 0.08);

INSERT INTO Carrier VALUES (NULL, "Planet Express", "Hamilton", 50, 640); -- 2
INSERT INTO FTLRate VALUE (2, 5.21);
INSERT INTO LTLRate VALUE (2, 0.3621);
INSERT INTO ReeferCharge VALUE (2, 0.08);

INSERT INTO Carrier VALUES (NULL, "Planet Express", "Oshawa", 50, 640); -- 3
INSERT INTO FTLRate VALUE (3, 5.21);
INSERT INTO LTLRate VALUE (3, 0.3621);
INSERT INTO ReeferCharge VALUE (3, 0.08);

INSERT INTO Carrier VALUES (NULL, "Planet Express", "Belleville", 50, 640); -- 4
INSERT INTO FTLRate VALUE (4, 5.21);
INSERT INTO LTLRate VALUE (4, 0.3621);
INSERT INTO ReeferCharge VALUE (4, 0.08);

INSERT INTO Carrier VALUES (NULL, "Planet Express", "Ottawa", 50, 640); -- 5
INSERT INTO FTLRate VALUE (5, 5.21);
INSERT INTO LTLRate VALUE (5, 0.3621);
INSERT INTO ReeferCharge VALUE (5, 0.08);

INSERT INTO Carrier VALUES (NULL, "Schooner's", "London", 18, 98); -- 6
INSERT INTO FTLRate VALUE (6, 5.05);
INSERT INTO LTLRate VALUE (6, 0.3434);
INSERT INTO ReeferCharge VALUE (6, 0.07);

INSERT INTO Carrier VALUES (NULL, "Schooner's", "Toronto", 18, 98); -- 7
INSERT INTO FTLRate VALUE (7, 5.05);
INSERT INTO LTLRate VALUE (7, 0.3434);
INSERT INTO ReeferCharge VALUE (7, 0.07);

INSERT INTO Carrier VALUES (NULL, "Schooner's", "Kingston", 18, 98); -- 8
INSERT INTO FTLRate VALUE (8, 5.05);
INSERT INTO LTLRate VALUE (8, 0.3434);
INSERT INTO ReeferCharge VALUE (8, 0.07);

INSERT INTO Carrier VALUES (NULL, "Tillman Transport", "Windsor", 24, 35); -- 9
INSERT INTO FTLRate VALUE (9, 5.11);
INSERT INTO LTLRate VALUE (9, 0.3012);
INSERT INTO ReeferCharge VALUE (9, 0.09);

INSERT INTO Carrier VALUES (NULL, "Tillman Transport", "London", 18, 45); -- 10
INSERT INTO FTLRate VALUE (10, 5.11);
INSERT INTO LTLRate VALUE (10, 0.3012);
INSERT INTO ReeferCharge VALUE (10, 0.09);

INSERT INTO Carrier VALUES (NULL, "Tillman Transport", "Hamilton", 18, 45); -- 11
INSERT INTO FTLRate VALUE (11, 5.11);
INSERT INTO LTLRate VALUE (11, 0.3012);
INSERT INTO ReeferCharge VALUE (11, 0.09);

INSERT INTO Carrier VALUES (NULL, "We Haul", "Ottawa", 11, 0); -- 12
INSERT INTO FTLRate VALUE (12, 5.2);
INSERT INTO LTLRate VALUE (12, 0);
INSERT INTO ReeferCharge VALUE (12, 0.065);

INSERT INTO Carrier VALUES (NULL, "We Haul", "Toronto", 11, 0); -- 13
INSERT INTO FTLRate VALUE (13, 5.2);
INSERT INTO LTLRate VALUE (13, 0);
INSERT INTO ReeferCharge VALUE (13, 0.065);

/*SELECT
c.`Name`,
c.DepotCity,
c.FtlAvailability,
c.LtlAvailability,
ftlr.Rate AS FtlRate,
ltlr.Rate AS LtlRate,
rc.Charge AS ReeferCharge
FROM Carrier c
INNER JOIN FTLRate ftlr ON ftlr.CarrierID = c.CarrierID
INNER JOIN LTLRate ltlr ON ltlr.CarrierID = c.CarrierID
INNER JOIN ReeferCharge rc ON rc.CarrierID = c.CarrierID;*/