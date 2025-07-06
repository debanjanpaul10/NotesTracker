BEGIN TRANSACTION;

BEGIN TRY

    IF NOT EXISTS (SELECT TOP 1 1 FROM dbo.BugReportsMapping WHERE StatusName='Open')
    BEGIN
        INSERT INTO BugReportsMapping (StatusName, StatusDescription)
        VALUES ('Open', 'The bug has been created by a user and yet to be picked up')
    END

    IF NOT EXISTS (SELECT TOP 1 1 FROM dbo.BugReportsMapping WHERE StatusName='InProgress')
    BEGIN
        INSERT INTO BugReportsMapping (StatusName, StatusDescription)
        VALUES ('InProgress', 'The bug is actively being worked on')
    END

    IF NOT EXISTS (SELECT TOP 1 1 FROM dbo.BugReportsMapping WHERE StatusName='Resolved')
    BEGIN
        INSERT INTO BugReportsMapping (StatusName, StatusDescription)
        VALUES ('Resolved', 'The bug fix has been given from DEV end')
    END

    IF NOT EXISTS (SELECT TOP 1 1 FROM dbo.BugReportsMapping WHERE StatusName='Closed')
    BEGIN
        INSERT INTO BugReportsMapping (StatusName, StatusDescription)
        VALUES ('Closed', 'The bug has been closed after confirmation with user')
    END

    IF @@TRANCOUNT > 0
        COMMIT TRANSACTION;

END TRY

BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;
END CATCH

GO