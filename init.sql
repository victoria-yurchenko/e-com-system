ALTER LOGIN sa ENABLE;
ALTER LOGIN sa WITH PASSWORD = 'Strong@Passw0rd';

CREATE LOGIN myadmin WITH PASSWORD = 'Strong@Passw0rd';
CREATE USER myadmin FOR LOGIN myadmin;
EXEC sp_addsrvrolemember 'myadmin', 'sysadmin';

IF NOT EXISTS (
    SELECT * 
    FROM sys.databases 
    WHERE name = 'SubscriptionDb'
) 
BEGIN
    CREATE DATABASE SubscriptionDb;
    USE SubscriptionDb;
    CREATE USER sa FOR LOGIN sa;
    ALTER ROLE db_owner ADD MEMBER sa;
    GO
    PRINT 'Database SubscriptionDb created successfully';
END
ELSE 
BEGIN
    PRINT 'Database SubscriptionDb already exists';
END
GO
