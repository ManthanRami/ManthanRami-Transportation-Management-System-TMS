CREATE DATABASE IF NOT EXISTS TMS;
USE TMS;

CREATE TABLE IF NOT EXISTS `User` (
	UserID			INT UNSIGNED NOT NULL AUTO_INCREMENT,
	Username		VARCHAR(32) NOT NULL,
    `Password`		VARCHAR(64) NOT NULL,
    Email			VARCHAR(32) NOT NULL,
    UserType		TINYINT DEFAULT 0,
    
    PRIMARY KEY (UserID)
);

CREATE TABLE IF NOT EXISTS Carrier (
	CarrierID		INT UNSIGNED NOT NULL AUTO_INCREMENT,
    DepotCity		VARCHAR(32) NOT NULL,
    FtlAvailability	SMALLINT NOT NULL,
    LtlAvailability	SMALLINT NOT NULL,
    
    PRIMARY KEY (CarrierID)
);

CREATE TABLE IF NOT EXISTS FTLRate (
	CarrierID	INT UNSIGNED NOT NULL UNIQUE REFERENCES Carrier(CarrierID),
    Rate		FLOAT NOT NULL,
    
    PRIMARY KEY (CarrierID)
);

CREATE TABLE IF NOT EXISTS LTLRate (
	CarrierID	INT UNSIGNED NOT NULL UNIQUE REFERENCES Carrier(CarrierID),
    Rate		FLOAT NOT NULL,
    
    PRIMARY KEY (CarrierID)
);

CREATE TABLE IF NOT EXISTS ReeferCharge (
	CarrierID	INT UNSIGNED NOT NULL UNIQUE REFERENCES Carrier(CarrierID),
    Charge		FLOAT NOT NULL,
    
    PRIMARY KEY (CarrierID)
);

CREATE TABLE IF NOT EXISTS Contract (
	ContractID	INT UNSIGNED NOT NULL AUTO_INCREMENT,
    CarrierID	INT UNSIGNED NOT NULL REFERENCES Carrier(CarrierID),
    `Client`	VARCHAR(64) NOT NULL,
    Quantity	INT UNSIGNED NOT NULL,
    LoadType	TINYINT NOT NULL,
    VanType		TINYINT NOT NULL,
    
    PRIMARY KEY (ContractID)
);