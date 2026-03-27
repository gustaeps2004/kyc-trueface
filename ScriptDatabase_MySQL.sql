CREATE DATABASE KYC_TrueFace;

USE KYC_TrueFace;

CREATE TABLE Partner(
	ID 				INT 			NOT NULL AUTO_INCREMENT UNIQUE, 
    Code			BINARY(16)		NOT NULL PRIMARY KEY,
    IdNumber		VARCHAR(14)		NOT NULL,
    Name			VARCHAR(150)	NOT NULL,
    Email			VARCHAR(100)	NOT NULL,
    InclusionDt		DATETIME		NOT NULL,
    Situation		INT				NOT NULL
);

CREATE TABLE PartnerCredentials(
	ID 				INT 			NOT NULL AUTO_INCREMENT UNIQUE, 
    Code			BINARY(16)		NOT NULL PRIMARY KEY,
    CodePartner 	BINARY(16)		NOT NULL,
    ClientId		VARCHAR(100)		NOT NULL,
    ClientSecret	VARCHAR(100)	NOT NULL,
    GrantType		VARCHAR(100)	NOT NULL,
    Situation		INT				NOT NULL,
    
	CONSTRAINT FK_Partner_PartnerCredentials
    FOREIGN KEY (CodePartner) 
    REFERENCES Partner(Code)
);

CREATE TABLE User(
	ID 				INT 			NOT NULL AUTO_INCREMENT UNIQUE, 
    Code			BINARY(16)		NOT NULL PRIMARY KEY,
    CodePartner 	BINARY(16)		NOT NULL,
    Name			VARCHAR(150)	NOT NULL,
    IdNumber		VARCHAR(11)		NOT NULL,
    BirthDate		DATE			NOT NULL,
    Email			VARCHAR(100)	NOT NULL,
    Permission		INT				NOT NULL,
    Situation		INT				NOT NULL,
    InclusionDt		DATETIME		NOT NULL,
    MotherName		VARCHAR(150)	NULL,
    
	CONSTRAINT FK_Partner_User
    FOREIGN KEY (CodePartner) 
    REFERENCES Partner(Code)
);

CREATE TABLE UserAccess(
	ID 				INT 			NOT NULL AUTO_INCREMENT UNIQUE, 
    Code			BINARY(16)		NOT NULL PRIMARY KEY,
	Username		VARCHAR(150)	NOT NULL,
    Password 		VARCHAR(300) 	NOT NULL,
    Situation		INT				NOT NULL,
    Role			VARCHAR(150) 	NOT NULL,
    Scope			VARCHAR(200) 	NOT NULL,
    InclusionDt		DATETIME		NOT NULL
);

CREATE TABLE UserAccessLog(
	ID 				INT 			NOT NULL AUTO_INCREMENT UNIQUE, 
    Code			BINARY(16)		NOT NULL PRIMARY KEY,
    CodeUserAccess	BINARY(16)		NOT NULL UNIQUE,
    Situation		INT				NOT NULL,
    SituationDt		DATETIME		NOT NULL,
    Flow			INT				NOT NULL,
	Ip				VARCHAR(150)	NOT NULL,
    
	CONSTRAINT FK_UserAccess_UserAccessLog
    FOREIGN KEY (CodeUserAccess) 
    REFERENCES UserAccess(Code)
);

CREATE TABLE Onboarding(
	ID 				INT 			NOT NULL AUTO_INCREMENT UNIQUE, 
    Code			BINARY(16)		NOT NULL PRIMARY KEY,
    CodePartner 	BINARY(16)		NOT NULL,
    InclusionDt		DATETIME		NOT NULL,
    SituationDt		DATETIME		NOT NULL,
    Situation		INT				NOT NULL,
    PathDocument	VARCHAR(00)		NOT NULL,
    PathSelfie		VARCHAR(200)	NOT NULL,
    
	CONSTRAINT FK_Partner_Onboarding
    FOREIGN KEY (CodePartner) 
    REFERENCES Partner(Code)
);

CREATE TABLE OnboardingResult(
	ID 				INT 			NOT NULL AUTO_INCREMENT UNIQUE, 
    Code			BINARY(16)		NOT NULL PRIMARY KEY,
    CodeOnboarding 	BINARY(16)		NOT NULL,
    CodeUser	 	BINARY(16)		NOT NULL,
    InclusionDt		DATETIME		NOT NULL,
    Observation		VARCHAR(500)	NOT NULL,
    
	CONSTRAINT FK_Onboarding_OnboardingResult
    FOREIGN KEY (CodeOnboarding) 
    REFERENCES Onboarding(Code),
    
	CONSTRAINT FK_User_OnboardingResult
    FOREIGN KEY (CodeUser) 
    REFERENCES User(Code)
);
