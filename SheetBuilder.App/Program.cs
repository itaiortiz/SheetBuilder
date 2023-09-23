using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SheetBuilder;
using SheetBuilder.Models;
using System.Data;

namespace BuilderApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string excelFilePath = string.Empty;
            if (args.Length == 0)
            {
                Console.WriteLine("Enter excel file location:");
                excelFilePath = Console.ReadLine();
                //Remove extra '/' caracters at start and end of the path
                if (excelFilePath[0] == '/')
                    excelFilePath = excelFilePath.Substring(1, excelFilePath.Length - 1);
            }
            else {
                excelFilePath = args[0];
            }

            DataSet ds = Tools.ConvertExcelToDataSet(excelFilePath);

            List<Paper> pagesList = new List<Paper>();

            foreach (DataTable table in ds.Tables)
            {
                Paper page = new Paper();
                page.Fecha = table.TableName;

                foreach (DataRow row in table.Rows)
                {
                    Asignacion asignacion = new Asignacion() {
                        Asignado = row["Asignado"].ToString(),
                        Ayudante = row["Ayudante"].ToString(),
                        Lectura = row["Lectura"].ToString() != string.Empty,
                        PrimeraConversacion = row["Primera Conversacion"].ToString() != string.Empty,
                        PrimeraRevisita = row["Primera Revisita"].ToString() != string.Empty,
                        SegundaRevisita = row["Segunda Revisita"].ToString() != string.Empty,
                        TerceraRevisita = row["Tercera Revisita"].ToString() != string.Empty,
                        CursoBiblico = row["Curso biblico"].ToString() != string.Empty,
                        Discurso = row["Discurso"].ToString() != string.Empty,
                        Leccion = row["Leccion"].ToString(),
                        SalaPrincipal = row["Sala Principal"].ToString() != string.Empty,
                        SalaAux1 = row["Sala Aux. Num. 1"].ToString() != string.Empty,
                        SalaAux2 = row["Sala Aux. Num. 2"].ToString() != string.Empty
                    };

                    page.Asignaciones.Add(asignacion);
                }

                pagesList.Add(page);
            }

            var filename = Path.GetFileName(excelFilePath);
            Factory.GenerateFile(Factory.Build(pagesList), Path.GetDirectoryName(excelFilePath), $"{Path.GetFileNameWithoutExtension(excelFilePath)}.html");

        }

    }
}
