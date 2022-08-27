CREATE TABLE [dbo].[tbProduct] (
    [pid]          INT          IDENTITY (1, 1) NOT NULL,
    [pname]        VARCHAR (50) NOT NULL,
    [pqty]         INT          NOT NULL,
    [pprice]       INT          NOT NULL,
    [pdescription] VARCHAR (50) NULL,
    [pcategory]    VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([pid] ASC)
);

