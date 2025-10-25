-- Script para criar tabela LogEntry no banco de dados

CREATE TABLE IF NOT EXISTS `LogEntry` (
    `Id` char(36) NOT NULL,
    `Timestamp` datetime(6) NOT NULL,
    `Level` varchar(50) NULL,
    `Category` varchar(200) NULL,
    `Message` longtext NULL,
    `Exception` longtext NULL,
    `HttpMethod` varchar(10) NULL,
    `Path` varchar(500) NULL,
    `StatusCode` int NULL,
    `Duration` int NULL,
    `UserId` varchar(100) NULL,
    `UserName` varchar(100) NULL,
    `IpAddress` varchar(50) NULL,
    `AdditionalInfo` longtext NULL,
    `UsuarioCriacao` varchar(100) NULL,
    `DataCriacao` datetime(6) NOT NULL,
    `UsuarioModificacao` varchar(100) NULL,
    `DataModificacao` datetime(6) NOT NULL,
    PRIMARY KEY (`Id`),
    INDEX `IX_LogEntry_Timestamp` (`Timestamp`),
    INDEX `IX_LogEntry_Level` (`Level`),
    INDEX `IX_LogEntry_UserName` (`UserName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
