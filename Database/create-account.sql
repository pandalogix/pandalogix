IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Accounts] (
    [Id] bigint NOT NULL IDENTITY,
    [Identifier] uniqueidentifier NOT NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastUpdatedDate] datetimeoffset NOT NULL,
    [CreatedBy] nvarchar(255) NULL,
    [UpdatedBy] nvarchar(255) NULL,
    [Email] nvarchar(max) NULL,
    [Tier] nvarchar(max) NULL,
    [FirstName] nvarchar(max) NULL,
    [LastName] nvarchar(max) NULL,
    [ActiveUntil] datetimeoffset NOT NULL,
    [ReachQuota] bit NOT NULL,
    [ActviatedDate] datetimeoffset NOT NULL,
    [Quota] bigint NOT NULL,
    [Active] bit NOT NULL,
    [StripeCustomerId] nvarchar(max) NULL,
    [ApiKey] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Accounts] PRIMARY KEY ([Id])
);

GO

CREATE UNIQUE INDEX [IX_Accounts_Identifier] ON [Accounts] ([Identifier]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20180831040012_init', N'2.1.2-rtm-30932');

GO

CREATE TABLE [AccountPad] (
    [Id] bigint NOT NULL IDENTITY,
    [UserId] uniqueidentifier NOT NULL,
    [PadId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_AccountPad] PRIMARY KEY ([Id])
);

GO

CREATE UNIQUE INDEX [IX_AccountPad_PadId] ON [AccountPad] ([PadId]);

GO

CREATE UNIQUE INDEX [IX_AccountPad_UserId] ON [AccountPad] ([UserId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20180831040243_accountpadref', N'2.1.2-rtm-30932');

GO

