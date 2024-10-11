IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [KoiDesigns] (
    [DesignID] int NOT NULL IDENTITY,
    [DesignName] nvarchar(100) NOT NULL,
    [Description] nvarchar(500) NULL,
    [CostEstimate] decimal(18,2) NOT NULL,
    CONSTRAINT [PK__KoiDesig__32B8E17F671C8093] PRIMARY KEY ([DesignID])
);
GO

CREATE TABLE [MaintenanceServices] (
    [ServiceID] int NOT NULL IDENTITY,
    [ServiceName] nvarchar(100) NOT NULL,
    [Description] nvarchar(500) NULL,
    [Price] decimal(18,2) NOT NULL,
    CONSTRAINT [PK__Maintena__C51BB0EAF9178D2D] PRIMARY KEY ([ServiceID])
);
GO

CREATE TABLE [PaymentPolicies] (
    [PolicyID] int NOT NULL IDENTITY,
    [PolicyName] nvarchar(100) NOT NULL,
    [Description] nvarchar(500) NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK__PaymentP__2E13394406BD6B45] PRIMARY KEY ([PolicyID])
);
GO

CREATE TABLE [Promotions] (
    [PromotionID] int NOT NULL IDENTITY,
    [PromotionName] nvarchar(100) NOT NULL,
    [Description] nvarchar(500) NULL,
    [DiscountPercentage] decimal(5,2) NOT NULL,
    [StartDate] date NOT NULL,
    [EndDate] date NOT NULL,
    CONSTRAINT [PK__Promotio__52C42F2F325EA4D0] PRIMARY KEY ([PromotionID])
);
GO

CREATE TABLE [Roles] (
    [RoleID] int NOT NULL IDENTITY,
    [RoleName] nvarchar(50) NOT NULL,
    CONSTRAINT [PK__Roles__8AFACE3AC9B5849C] PRIMARY KEY ([RoleID])
);
GO

CREATE TABLE [Users] (
    [UserID] int NOT NULL IDENTITY,
    [FullName] nvarchar(100) NOT NULL,
    [Email] nvarchar(100) NOT NULL,
    [PasswordHash] nvarchar(255) NOT NULL,
    [PhoneNumber] nvarchar(15) NULL,
    [Address] nvarchar(255) NULL,
    [RoleID] int NULL,
    CONSTRAINT [PK__Users__1788CCAC0619AF3F] PRIMARY KEY ([UserID]),
    CONSTRAINT [FK__Users__RoleID__3A81B327] FOREIGN KEY ([RoleID]) REFERENCES [Roles] ([RoleID])
);
GO

CREATE TABLE [ConstructionRequests] (
    [RequestID] int NOT NULL IDENTITY,
    [CustomerID] int NULL,
    [DesignID] int NULL,
    [CustomDesignDescription] nvarchar(500) NULL,
    [Status] nvarchar(50) NOT NULL,
    [CreatedAt] datetime NOT NULL DEFAULT ((getdate())),
    [UpdatedAt] datetime NOT NULL DEFAULT ((getdate())),
    CONSTRAINT [PK__Construc__33A8519A9A183ECA] PRIMARY KEY ([RequestID]),
    CONSTRAINT [FK__Construct__Custo__3F466844] FOREIGN KEY ([CustomerID]) REFERENCES [Users] ([UserID]),
    CONSTRAINT [FK__Construct__Desig__403A8C7D] FOREIGN KEY ([DesignID]) REFERENCES [KoiDesigns] ([DesignID])
);
GO

CREATE TABLE [ServiceRequests] (
    [ServiceRequestID] int NOT NULL IDENTITY,
    [CustomerID] int NULL,
    [ServiceID] int NULL,
    [Status] nvarchar(50) NOT NULL,
    [CreatedAt] datetime NOT NULL DEFAULT ((getdate())),
    [UpdatedAt] datetime NOT NULL DEFAULT ((getdate())),
    CONSTRAINT [PK__ServiceR__790F6CAB08527AA3] PRIMARY KEY ([ServiceRequestID]),
    CONSTRAINT [FK__ServiceRe__Custo__4CA06362] FOREIGN KEY ([CustomerID]) REFERENCES [Users] ([UserID]),
    CONSTRAINT [FK__ServiceRe__Servi__4D94879B] FOREIGN KEY ([ServiceID]) REFERENCES [MaintenanceServices] ([ServiceID])
);
GO

CREATE TABLE [ConstructionProcess] (
    [ProcessID] int NOT NULL IDENTITY,
    [RequestID] int NULL,
    [Step] nvarchar(100) NOT NULL,
    [Status] nvarchar(50) NOT NULL,
    [AssignedStaffID] int NULL,
    [CreatedAt] datetime NOT NULL DEFAULT ((getdate())),
    [UpdatedAt] datetime NOT NULL DEFAULT ((getdate())),
    CONSTRAINT [PK__Construc__1B39A976DC28FE0E] PRIMARY KEY ([ProcessID]),
    CONSTRAINT [FK__Construct__Assig__45F365D3] FOREIGN KEY ([AssignedStaffID]) REFERENCES [Users] ([UserID]),
    CONSTRAINT [FK__Construct__Reque__44FF419A] FOREIGN KEY ([RequestID]) REFERENCES [ConstructionRequests] ([RequestID])
);
GO

CREATE TABLE [CustomerOrderHistory] (
    [HistoryID] int NOT NULL IDENTITY,
    [CustomerID] int NULL,
    [RequestID] int NULL,
    [CreatedAt] datetime NOT NULL DEFAULT ((getdate())),
    CONSTRAINT [PK__Customer__4D7B4ADD623FA230] PRIMARY KEY ([HistoryID]),
    CONSTRAINT [FK__CustomerO__Custo__5CD6CB2B] FOREIGN KEY ([CustomerID]) REFERENCES [Users] ([UserID]),
    CONSTRAINT [FK__CustomerO__Reque__5DCAEF64] FOREIGN KEY ([RequestID]) REFERENCES [ConstructionRequests] ([RequestID])
);
GO

CREATE TABLE [Feedback] (
    [FeedbackID] int NOT NULL IDENTITY,
    [CustomerID] int NULL,
    [RequestID] int NULL,
    [ServiceRequestID] int NULL,
    [Rating] int NULL,
    [Comment] nvarchar(500) NULL,
    [CreatedAt] datetime NOT NULL DEFAULT ((getdate())),
    CONSTRAINT [PK__Feedback__6A4BEDF672535547] PRIMARY KEY ([FeedbackID]),
    CONSTRAINT [FK__Feedback__Custom__5441852A] FOREIGN KEY ([CustomerID]) REFERENCES [Users] ([UserID]),
    CONSTRAINT [FK__Feedback__Reques__5535A963] FOREIGN KEY ([RequestID]) REFERENCES [ConstructionRequests] ([RequestID]),
    CONSTRAINT [FK__Feedback__Servic__5629CD9C] FOREIGN KEY ([ServiceRequestID]) REFERENCES [ServiceRequests] ([ServiceRequestID])
);
GO

CREATE INDEX [IX_ConstructionProcess_AssignedStaffID] ON [ConstructionProcess] ([AssignedStaffID]);
GO

CREATE INDEX [IX_ConstructionProcess_RequestID] ON [ConstructionProcess] ([RequestID]);
GO

CREATE INDEX [IX_ConstructionRequests_CustomerID] ON [ConstructionRequests] ([CustomerID]);
GO

CREATE INDEX [IX_ConstructionRequests_DesignID] ON [ConstructionRequests] ([DesignID]);
GO

CREATE INDEX [IX_CustomerOrderHistory_CustomerID] ON [CustomerOrderHistory] ([CustomerID]);
GO

CREATE INDEX [IX_CustomerOrderHistory_RequestID] ON [CustomerOrderHistory] ([RequestID]);
GO

CREATE INDEX [IX_Feedback_CustomerID] ON [Feedback] ([CustomerID]);
GO

CREATE INDEX [IX_Feedback_RequestID] ON [Feedback] ([RequestID]);
GO

CREATE INDEX [IX_Feedback_ServiceRequestID] ON [Feedback] ([ServiceRequestID]);
GO

CREATE INDEX [IX_ServiceRequests_CustomerID] ON [ServiceRequests] ([CustomerID]);
GO

CREATE INDEX [IX_ServiceRequests_ServiceID] ON [ServiceRequests] ([ServiceID]);
GO

CREATE INDEX [IX_Users_RoleID] ON [Users] ([RoleID]);
GO

CREATE UNIQUE INDEX [UQ__Users__A9D105349EE3CA92] ON [Users] ([Email]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241010080130_Init', N'8.0.10');
GO

COMMIT;
GO

