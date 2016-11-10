CREATE TABLE [dbo].[AspNetRoles] (
    [Id]   NVARCHAR (128) NOT NULL,
    [Name] NVARCHAR (256) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[AspNetUsers] (
    [Id]                   NVARCHAR (128) NOT NULL,
    [Hometown]             NVARCHAR (MAX) NULL,
    [Email]                NVARCHAR (256) NULL,
    [EmailConfirmed]       BIT            NOT NULL,
    [PasswordHash]         NVARCHAR (MAX) NULL,
    [SecurityStamp]        NVARCHAR (MAX) NULL,
    [PhoneNumber]          NVARCHAR (MAX) NULL,
    [PhoneNumberConfirmed] BIT            NOT NULL,
    [TwoFactorEnabled]     BIT            NOT NULL,
    [LockoutEndDateUtc]    DATETIME       NULL,
    [LockoutEnabled]       BIT            NOT NULL,
    [AccessFailedCount]    INT            NOT NULL,
    [UserName]             NVARCHAR (256) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[AspNetUsersExt] (
    [UserId]             NVARCHAR (128) NOT NULL,
    [SecurityQuestion]   NVARCHAR (256) NULL,
    [SecurityAnswer]     NVARCHAR (128) NULL,
    [SecurityAnswerSalt] NVARCHAR (128) NULL,
    CONSTRAINT [PK_AspNetUsersExt] PRIMARY KEY CLUSTERED ([UserId] ASC),
    CONSTRAINT [FK_AspNetUserExt_AspNetUsers] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE [dbo].[AspNetUserClaims] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [UserId]     NVARCHAR (128) NOT NULL,
    [ClaimType]  NVARCHAR (MAX) NULL,
    [ClaimValue] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [dbo].[AspNetUserLogins] (
    [LoginProvider] NVARCHAR (128) NOT NULL,
    [ProviderKey]   NVARCHAR (128) NOT NULL,
    [UserId]        NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED ([LoginProvider] ASC, [ProviderKey] ASC, [UserId] ASC),
    CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [dbo].[AspNetUserRoles] (
    [UserId] NVARCHAR (128) NOT NULL,
    [RoleId] NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC),
    CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);


CREATE TABLE [dbo].[Checkins] (
    [Id]                         INT            IDENTITY (1, 1) NOT NULL,
    [UserId]                     NVARCHAR (128) NOT NULL,
    [Weight]                     REAL           NULL,
    [Heartrate]                  REAL           NULL,
    [SystolicBloodPressure]      REAL           NULL,
    [DiastolicBloodPressure]     REAL           NULL,
    [PhysicalFeelingRating]      REAL           NULL,
    [PsychologicalFeelingRating] REAL           NULL,
    [ExerciseRating]             REAL           NULL,
    [Notes]                      NVARCHAR (MAX) NULL,
    [TimeAdded]                  DATETIME       NOT NULL,
    [UpdateTime]                 DATETIME       NOT NULL,
    CONSTRAINT [PK_dbo.Checkins] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Medicines] (
    [Id]                  INT            IDENTITY (1, 1) NOT NULL,
    [UserId]              NVARCHAR (128) NOT NULL,
    [Name]                NVARCHAR (500) NOT NULL,
    [NumberOfTimesPerDay] INT            NOT NULL,
    [IsActive]            BIT            NOT NULL,
    [StartDate]           DATETIME       NULL,
    [EndDate]             DATETIME       NULL,
    [TimeAdded]           DATETIME       NOT NULL,
    [UpdateTime]          DATETIME       NOT NULL,
    CONSTRAINT [PK_dbo.Medicines] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[MedicineTakens] (
    [Id]           INT      IDENTITY (1, 1) NOT NULL,
    [MedicineId]   INT      NOT NULL,
    [DateAddedFor] DATETIME NOT NULL,
    [TimeAdded]    DATETIME NOT NULL,
    CONSTRAINT [PK_dbo.MedicineTakens] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.MedicineTakens_dbo.Medicines_MedicineId] FOREIGN KEY ([MedicineId]) REFERENCES [dbo].[Medicines] ([Id]) ON DELETE CASCADE
);