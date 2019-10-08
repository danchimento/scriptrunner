-- Insert rows into table 'Car' in schema '[dbo]'
INSERT INTO [dbo].[Car]
( -- Columns to insert data into
    [Make], [Model]
)
VALUES
( -- First row: values for the columns in the list above
 @Make, @Model
)
