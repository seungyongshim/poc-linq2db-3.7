CREATE DATABASE Sample1234;
USE Sample1234;
CREATE TABLE `History`
(
        `Id`        BIGINT       AUTO_INCREMENT NOT NULL,
        `DateTime`  TIMESTAMP(6)                NOT NULL,
        `EventType` CHAR                        NOT NULL,
        `TraceId`   VARCHAR(255)                    NULL,

        CONSTRAINT `PK_History` PRIMARY KEY CLUSTERED (`Id`)
)