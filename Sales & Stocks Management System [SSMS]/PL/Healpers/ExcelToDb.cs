using System;
using System.Data;
using System.IO;
using ExcelDataReader;
using Microsoft.Data.SqlClient;

namespace PL.Healpers
{
    public class ExcelSheetData
    {
        public string SheetName { get; set; }
        public DataTable Content { get; set; }
    }
    public static class ExcelToDb

    {
        public static List<ExcelSheetData> ReadExcelSheets(string filePath)
        {
            // Register encoding provider (necessary for ExcelDataReader to work correctly)
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            var sheetDataList = new List<ExcelSheetData>();

            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    // Read all sheets into a DataSet
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true // Treat the first row as column headers
                        }
                    });

                    // Iterate through each table (sheet) in the DataSet
                    foreach (DataTable table in result.Tables)
                    {
                        var sheetData = new ExcelSheetData
                        {
                            SheetName = table.TableName,  // Sheet title (name)
                            Content = table               // Sheet content as DataTable
                        };

                        sheetDataList.Add(sheetData);
                    }
                }
            }

            return sheetDataList;
        }

        public static void ProcessExcelFile(string filePath)
        {
            var sheets = ReadExcelSheets(filePath);

            foreach (var sheet in sheets)
            {
                SaveDataToDatabase(sheet);
            }
        }

        public static void SaveDataToDatabase(ExcelSheetData dataTable)
        {
            using (var connection = new SqlConnection("Server = . ; Database = Stock_SalesDb ; Trusted_Connection = true;"))
            {
                connection.Open();
                bool alpha = false;
                foreach (DataRow row in dataTable.Content.Rows)
                {
                    if (alpha && row.ItemArray[2].ToString() is not null)
                    {
                        var command = new SqlCommand("INSERT INTO Products (Name, Category, StockQuantity) VALUES (@val1, @val2, @val3)", connection);

                        command.Parameters.AddWithValue("@val1", row.ItemArray[2]);
                        command.Parameters.AddWithValue("@val2", dataTable.SheetName);
                        command.Parameters.AddWithValue("@val3", 0);
                        try
                        {
                            command.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            File.AppendAllText("C:\\Users\\Mohamed\\Downloads\\Telegram Desktop\\output.txt", ex.Message);
                        }
                    }

                    if (row.ItemArray.Length <= 2 || row.ItemArray[2].ToString() != "اسم الصنف")
                        continue;
                    alpha = true;
                }
            }
        }

       


    }
}
