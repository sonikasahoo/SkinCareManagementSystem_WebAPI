CREATE DATABASE SkinCareManagement;

use SkinCareManagement;

CREATE TABLE SkinCareProducts (
	ProductId INT PRIMARY KEY,
	Name VARCHAR(100),
	Brand VARCHAR(100),
	Price DECIMAL(10,2),
	SkinType VARCHAR(50),
	TargetArea VARCHAR(50)
);

INSERT INTO SkinCareProducts (ProductId,Name,Brand,Price,SkinType,TargetArea) VALUES (1,'Face Wash','Cetaphil',98.00,'Oily','Face');
INSERT INTO SkinCareProducts (ProductId,Name,Brand,Price,SkinType,TargetArea) VALUES (2,'Lip Balm','Love & Beauty',104.56,'Dry','Lips');
INSERT INTO SkinCareProducts (ProductId,Name,Brand,Price,SkinType,TargetArea) VALUES (3,'Body Lotion','Ive Steve',426.97,'Combined','Body');
INSERT INTO SkinCareProducts (ProductId,Name,Brand,Price,SkinType,TargetArea) VALUES (4,'Shower Gel','PamOlive',126.08,'Normal','Body');


SELECT * FROM SkinCareProducts;