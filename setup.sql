DROP DATABASE TMS;

CREATE DATABASE IF NOT EXISTS TMS;
USE TMS;

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

CREATE TABLE IF NOT EXISTS Contract (
	ContractID	INT UNSIGNED NOT NULL AUTO_INCREMENT,
    CarrierID	INT UNSIGNED NOT NULL,
    `Client`	VARCHAR(64) NOT NULL,
    Quantity	INT UNSIGNED NOT NULL,
    LoadType	TINYINT NOT NULL,
    VanType		TINYINT NOT NULL,
    
    PRIMARY KEY (ContractID),
    FOREIGN KEY (CarrierID) REFERENCES Carrier(CarrierID)
);

/* TEST DATA */
INSERT INTO `User` VALUES (NULL, 'admin', 'password', 'admin@test.com', 'testing', 'testerson', 2);
SELECT * FROM `User`;

INSERT INTO `Carrier` VALUES (NULL, "Windsor", 100, 283);
INSERT INTO `FTLRate` VALUES (1, 1.89);

SELECT * FROM `Carrier`;
UPDATE `Carrier` SET `Carrier`.`FtlAvailability` = 50 WHERE `Carrier`.`CarrierID` = LAST_INSERT_ID();

SELECT LAST_INSERT_ID() as CarrierID;



/*
DELETE FROM `FTLRate` WHERE `FTLRate`.`CarrierID` = 1;
DELETE FROM `Carrier` WHERE `Carrier`.`CarrierID` = 1;
*/

SELECT * FROM ReeferCharge;
