using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;

namespace SheetBuilder
{
    public class Tools
    {
        public static DataSet ConvertExcelToDataSet(string path)
        {
            Excel.Application excelApp = null;
            Excel.Workbook excelWorkBook = null;
            DataSet ds = new DataSet();
            try
            {
                excelApp = new Excel.Application();
                excelWorkBook = excelApp.Workbooks.Open(path);
                foreach (Excel.Worksheet worksheet in excelWorkBook.Worksheets)
                {
                    int rows = worksheet.UsedRange.Rows.Count;
                    int cols = worksheet.UsedRange.Columns.Count;
                    DataTable dt = new DataTable();
                    dt.TableName = worksheet.Name;
                    int noofrow = 1;
                    for (int c = 1; c <= cols; c++)
                    {
                        string colname = worksheet.Cells[1, c].Text;
                        dt.Columns.Add(colname);
                        noofrow = 2;
                    }
                    for (int r = noofrow; r <= rows; r++)
                    {
                        DataRow dr = dt.NewRow();
                        for (int c = 1; c <= cols; c++)
                        {
                            dr[c - 1] = worksheet.Cells[r, c].Text;
                        }
                        dt.Rows.Add(dr);
                    }
                    ds.Tables.Add(dt);
                }
                excelWorkBook.Close();
                excelApp.Quit();
            }
            catch (Exception ex)
            {
                excelWorkBook.Saved = true;
                excelWorkBook.Close();
                excelApp.Quit();
            }
            return ds;
        }
    }
}
