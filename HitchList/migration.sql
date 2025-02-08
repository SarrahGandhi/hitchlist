Build started...
Build succeeded.
CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    PRIMARY KEY (`MigrationId`)
);

START TRANSACTION;
CREATE TABLE `AspNetRoles` (
    `Id` TEXT NOT NULL,
    `Name` TEXT NULL,
    `NormalizedName` TEXT NULL,
    `ConcurrencyStamp` TEXT NULL,
    PRIMARY KEY (`Id`)
);

CREATE TABLE `AspNetUsers` (
    `Id` TEXT NOT NULL,
    `UserName` TEXT NULL,
    `NormalizedUserName` TEXT NULL,
    `Email` TEXT NULL,
    `NormalizedEmail` TEXT NULL,
    `EmailConfirmed` INTEGER NOT NULL,
    `PasswordHash` TEXT NULL,
    `SecurityStamp` TEXT NULL,
    `ConcurrencyStamp` TEXT NULL,
    `PhoneNumber` TEXT NULL,
    `PhoneNumberConfirmed` INTEGER NOT NULL,
    `TwoFactorEnabled` INTEGER NOT NULL,
    `LockoutEnd` TEXT NULL,
    `LockoutEnabled` INTEGER NOT NULL,
    `AccessFailedCount` INTEGER NOT NULL,
    PRIMARY KEY (`Id`)
);

CREATE TABLE `AspNetRoleClaims` (
    `Id` INTEGER NOT NULL,
    `RoleId` TEXT NOT NULL,
    `ClaimType` TEXT NULL,
    `ClaimValue` TEXT NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `AspNetUserClaims` (
    `Id` INTEGER NOT NULL,
    `UserId` TEXT NOT NULL,
    `ClaimType` TEXT NULL,
    `ClaimValue` TEXT NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `AspNetUserLogins` (
    `LoginProvider` TEXT NOT NULL,
    `ProviderKey` TEXT NOT NULL,
    `ProviderDisplayName` TEXT NULL,
    `UserId` TEXT NOT NULL,
    PRIMARY KEY (`LoginProvider`, `ProviderKey`),
    CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `AspNetUserRoles` (
    `UserId` TEXT NOT NULL,
    `RoleId` TEXT NOT NULL,
    PRIMARY KEY (`UserId`, `RoleId`),
    CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `AspNetUserTokens` (
    `UserId` TEXT NOT NULL,
    `LoginProvider` TEXT NOT NULL,
    `Name` TEXT NOT NULL,
    `Value` TEXT NULL,
    PRIMARY KEY (`UserId`, `LoginProvider`, `Name`),
    CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
);

CREATE INDEX `IX_AspNetRoleClaims_RoleId` ON `AspNetRoleClaims` (`RoleId`);

CREATE UNIQUE INDEX `RoleNameIndex` ON `AspNetRoles` (`NormalizedName`);

CREATE INDEX `IX_AspNetUserClaims_UserId` ON `AspNetUserClaims` (`UserId`);

CREATE INDEX `IX_AspNetUserLogins_UserId` ON `AspNetUserLogins` (`UserId`);

CREATE INDEX `IX_AspNetUserRoles_RoleId` ON `AspNetUserRoles` (`RoleId`);

CREATE INDEX `EmailIndex` ON `AspNetUsers` (`NormalizedEmail`);

CREATE UNIQUE INDEX `UserNameIndex` ON `AspNetUsers` (`NormalizedUserName`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('00000000000000_CreateIdentitySchema', '9.0.1');

ALTER TABLE `AspNetUserTokens` MODIFY `Value` longtext NULL;

ALTER TABLE `AspNetUserTokens` MODIFY `Name` varchar(128) NOT NULL;

ALTER TABLE `AspNetUserTokens` MODIFY `LoginProvider` varchar(128) NOT NULL;

ALTER TABLE `AspNetUserTokens` MODIFY `UserId` varchar(255) NOT NULL;

ALTER TABLE `AspNetUsers` MODIFY `UserName` varchar(256) NULL;

ALTER TABLE `AspNetUsers` MODIFY `TwoFactorEnabled` tinyint(1) NOT NULL;

ALTER TABLE `AspNetUsers` MODIFY `SecurityStamp` longtext NULL;

ALTER TABLE `AspNetUsers` MODIFY `PhoneNumberConfirmed` tinyint(1) NOT NULL;

ALTER TABLE `AspNetUsers` MODIFY `PhoneNumber` longtext NULL;

ALTER TABLE `AspNetUsers` MODIFY `PasswordHash` longtext NULL;

ALTER TABLE `AspNetUsers` MODIFY `NormalizedUserName` varchar(256) NULL;

ALTER TABLE `AspNetUsers` MODIFY `NormalizedEmail` varchar(256) NULL;

ALTER TABLE `AspNetUsers` MODIFY `LockoutEnd` datetime NULL;

ALTER TABLE `AspNetUsers` MODIFY `LockoutEnabled` tinyint(1) NOT NULL;

ALTER TABLE `AspNetUsers` MODIFY `EmailConfirmed` tinyint(1) NOT NULL;

ALTER TABLE `AspNetUsers` MODIFY `Email` varchar(256) NULL;

ALTER TABLE `AspNetUsers` MODIFY `ConcurrencyStamp` longtext NULL;

ALTER TABLE `AspNetUsers` MODIFY `AccessFailedCount` int NOT NULL;

ALTER TABLE `AspNetUsers` MODIFY `Id` varchar(255) NOT NULL;

ALTER TABLE `AspNetUserRoles` MODIFY `RoleId` varchar(255) NOT NULL;

ALTER TABLE `AspNetUserRoles` MODIFY `UserId` varchar(255) NOT NULL;

ALTER TABLE `AspNetUserLogins` MODIFY `UserId` varchar(255) NOT NULL;

ALTER TABLE `AspNetUserLogins` MODIFY `ProviderDisplayName` longtext NULL;

ALTER TABLE `AspNetUserLogins` MODIFY `ProviderKey` varchar(128) NOT NULL;

ALTER TABLE `AspNetUserLogins` MODIFY `LoginProvider` varchar(128) NOT NULL;

ALTER TABLE `AspNetUserClaims` MODIFY `UserId` varchar(255) NOT NULL;

ALTER TABLE `AspNetUserClaims` MODIFY `ClaimValue` longtext NULL;

ALTER TABLE `AspNetUserClaims` MODIFY `ClaimType` longtext NULL;

ALTER TABLE `AspNetUserClaims` MODIFY `Id` int NOT NULL AUTO_INCREMENT;

ALTER TABLE `AspNetRoles` MODIFY `NormalizedName` varchar(256) NULL;

ALTER TABLE `AspNetRoles` MODIFY `Name` varchar(256) NULL;

ALTER TABLE `AspNetRoles` MODIFY `ConcurrencyStamp` longtext NULL;

ALTER TABLE `AspNetRoles` MODIFY `Id` varchar(255) NOT NULL;

ALTER TABLE `AspNetRoleClaims` MODIFY `RoleId` varchar(255) NOT NULL;

ALTER TABLE `AspNetRoleClaims` MODIFY `ClaimValue` longtext NULL;

ALTER TABLE `AspNetRoleClaims` MODIFY `ClaimType` longtext NULL;

ALTER TABLE `AspNetRoleClaims` MODIFY `Id` int NOT NULL AUTO_INCREMENT;

CREATE TABLE `Admin` (
    `User_Id` int NOT NULL AUTO_INCREMENT,
    `Username` varchar(50) NOT NULL,
    `Password_hash` varchar(255) NOT NULL,
    `User_phone` varchar(15) NOT NULL,
    `Category` varchar(10) NOT NULL,
    PRIMARY KEY (`User_Id`)
);

CREATE TABLE `Event` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(255) NOT NULL,
    `Date` datetime(6) NULL,
    `Location` varchar(255) NULL,
    `Category` varchar(10) NOT NULL,
    PRIMARY KEY (`Id`)
);

CREATE TABLE `Guest` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(255) NOT NULL,
    `InvitationStatus` tinyint(1) NOT NULL,
    `Category` varchar(10) NOT NULL,
    PRIMARY KEY (`Id`)
);

CREATE TABLE `Task` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `TaskName` varchar(255) NOT NULL,
    `TaskDescription` varchar(255) NOT NULL,
    `TaskDueDate` datetime(6) NOT NULL,
    `TaskCategory` varchar(10) NOT NULL,
    PRIMARY KEY (`Id`)
);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20250207232236_InitialCreate', '9.0.1');

COMMIT;


