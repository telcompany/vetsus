﻿/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

--Roles
INSERT INTO [Roles]
            (Id,Name)
VALUES      ('3YMZbxtOhiwTukZuZUp80a','Admin'),
            ('Gaig1ij2_WX-z81Yh_rvrU','User'),
            ('ij2_WX-z81YhGaig1_rvrU','Visitor')

--Permissions
INSERT INTO [Permissions]
            (Id,Name)
VALUES      ('YOfi2bZgwgIWq8RFxFWUOe','Read'),
            ('flaI5ThsXk2VfVKLyfacSA','List'),
            ('-6c7C5G0nU6Gr5ljbYZpTg','Write'),
            ('bZgwgIWq8RFxYOfi2FWUOe','Modify'),
            ('k2JQEVUgtxAwwaTAhd623d','Delete')

--RolePermissions
INSERT INTO [RolePermissions]
            (RoleId,PermissionId)
VALUES      ('3YMZbxtOhiwTukZuZUp80a','YOfi2bZgwgIWq8RFxFWUOe'),('3YMZbxtOhiwTukZuZUp80a','flaI5ThsXk2VfVKLyfacSA'),
            ('3YMZbxtOhiwTukZuZUp80a','-6c7C5G0nU6Gr5ljbYZpTg'),('3YMZbxtOhiwTukZuZUp80a','bZgwgIWq8RFxYOfi2FWUOe'),('3YMZbxtOhiwTukZuZUp80a','k2JQEVUgtxAwwaTAhd623d'),
            ('Gaig1ij2_WX-z81Yh_rvrU','YOfi2bZgwgIWq8RFxFWUOe'),('Gaig1ij2_WX-z81Yh_rvrU','flaI5ThsXk2VfVKLyfacSA'),('Gaig1ij2_WX-z81Yh_rvrU','bZgwgIWq8RFxYOfi2FWUOe'),
            ('ij2_WX-z81YhGaig1_rvrU','YOfi2bZgwgIWq8RFxFWUOe')

--Users
INSERT INTO [Users]
            (Id,Email,UserName,FirstName, LastName, PasswordHash, Created, CreatedBy)
VALUES      ('lkmaDSX4AUW-dl8NyiBYAw','admin@vetsus.com','admin','Admin', 'Boss', '$2a$11$AON5UqkZTQJtfNScBc8YE.05SpWrxoXrHuNI8OuSp13mKOkdEFO.G', GETDATE(), 'admin')

--UserRoles
INSERT INTO [UserRoles]
            (UserId,RoleId)
VALUES      ('lkmaDSX4AUW-dl8NyiBYAw', '3YMZbxtOhiwTukZuZUp80a')

--Species
INSERT INTO [Species]
            (Id,Name)
VALUES      ('BpLZ8uxy5kSbzslonnJeww', 'Perro'),
            ('D6kC6CNfYU6vRA7Z92LJTA', 'Gato'),
            ('HgB8mJor4ke9WJS26O-8Jg', 'Otros')