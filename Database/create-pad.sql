IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [PadExecutionHistory] (
    [Id] bigint NOT NULL IDENTITY,
    [Identifier] uniqueidentifier NOT NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastUpdatedDate] datetimeoffset NOT NULL,
    [CreatedBy] nvarchar(255) NULL,
    [UpdatedBy] nvarchar(255) NULL,
    [UserId] uniqueidentifier NOT NULL,
    [PadIdentifier] uniqueidentifier NOT NULL,
    [ExecutionSummary] nvarchar(max) NULL,
    [Result] nvarchar(max) NULL,
    [Status] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_PadExecutionHistory] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Pads] (
    [Id] bigint NOT NULL IDENTITY,
    [Identifier] uniqueidentifier NOT NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastUpdatedDate] datetimeoffset NOT NULL,
    [CreatedBy] nvarchar(255) NULL,
    [UpdatedBy] nvarchar(255) NULL,
    [UserId] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [CurrentMaxSequenceId] int NOT NULL,
    [TriggerData] nvarchar(max) NULL,
    CONSTRAINT [PK_Pads] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [InstanceMapping] (
    [Id] bigint NOT NULL IDENTITY,
    [Identifier] uniqueidentifier NOT NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastUpdatedDate] datetimeoffset NOT NULL,
    [CreatedBy] nvarchar(255) NULL,
    [UpdatedBy] nvarchar(255) NULL,
    [UserId] uniqueidentifier NOT NULL,
    [PadId] bigint NULL,
    [FieldMappings] nvarchar(max) NULL,
    CONSTRAINT [PK_InstanceMapping] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_InstanceMapping_Pads_PadId] FOREIGN KEY ([PadId]) REFERENCES [Pads] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Node] (
    [Id] bigint NOT NULL IDENTITY,
    [Identifier] uniqueidentifier NOT NULL,
    [CreatedDate] datetimeoffset NOT NULL,
    [LastUpdatedDate] datetimeoffset NOT NULL,
    [CreatedBy] nvarchar(255) NULL,
    [UpdatedBy] nvarchar(255) NULL,
    [UserId] uniqueidentifier NOT NULL,
    [PadId] bigint NULL,
    [NodeId] int NOT NULL,
    [InNodes] nvarchar(max) NULL,
    [OutNodes] nvarchar(max) NULL,
    [MetaData] nvarchar(max) NULL,
    [NodeType] nvarchar(max) NULL,
    [Location] nvarchar(max) NULL,
    CONSTRAINT [PK_Node] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Node_Pads_PadId] FOREIGN KEY ([PadId]) REFERENCES [Pads] ([Id]) ON DELETE NO ACTION
);

GO



CREATE UNIQUE INDEX [IX_InstanceMapping_Identifier] ON [InstanceMapping] ([Identifier]);

GO

CREATE INDEX [IX_InstanceMapping_PadId] ON [InstanceMapping] ([PadId]);

GO

CREATE UNIQUE INDEX [IX_Node_Identifier] ON [Node] ([Identifier]);

GO

CREATE INDEX [IX_Node_PadId] ON [Node] ([PadId]);

GO

CREATE INDEX [IX_PadExecutionHistory_PadIdentifier] ON [PadExecutionHistory] ([PadIdentifier]);

GO

CREATE INDEX [IX_PadExecutionHistory_UserId] ON [PadExecutionHistory] ([UserId]);

GO

CREATE UNIQUE INDEX [IX_Pads_Identifier] ON [Pads] ([Identifier]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20181129041025_init', N'2.1.2-rtm-30932');

GO