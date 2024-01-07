using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using PuntoDeVenta.Maui.Data.DTO.EmissionSystem;
using PuntoDeVenta.Maui.Data.DTO.EmissionSystem.Dtes;
using PuntoDeVenta.Maui.Data.DTO.Report;
using PuntoDeVenta.Maui.Data.DTO.Sales;
using PuntoDeVenta.Maui.Domain.Helpers;
using PuntoDeVenta.Maui.UI.Utilities;
using Cell = DocumentFormat.OpenXml.Spreadsheet.Cell;
using Path = System.IO.Path;

namespace PuntoDeVenta.Maui.Core.LocalData.Files
{


    internal class FileManager : IFileManager
    {
        private static readonly string PathFiles = "";//$"{FileSystem.CacheDirectory}/Files";

       
        //private static Cells CornersRadiusLeft(Cells cell, int radius, int padding)
        //{
        //    cell.SetPadding(padding);
        //    cell.SetBorderTopLeftRadius(new BorderRadius(radius));
        //    cell.SetBorderBottomLeftRadius(new BorderRadius(radius));

        //    cell.SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 1));
        //    cell.SetBorderLeft(new SolidBorder(ColorConstants.BLACK, 1));
        //    cell.SetBorderRight(Border.NO_BORDER);
        //    cell.SetBorderTop(new SolidBorder(ColorConstants.BLACK, 1));
        //    return cell;

        //}
        //private static Cells CornersRadiusRight(Cells cell, int radius, int padding)
        //{
        //    cell.SetPadding(padding);
        //    cell.SetBorderBottomRightRadius(new BorderRadius(radius));
        //    cell.SetBorderTopRightRadius(new BorderRadius(radius));

        //    cell.SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 1));
        //    cell.SetBorderRight(new SolidBorder(ColorConstants.BLACK, 1));
        //    cell.SetBorderLeft(Border.NO_BORDER);
        //    cell.SetBorderTop(new SolidBorder(ColorConstants.BLACK, 1));
        //    return cell;

        //}

        private Cell ConstructCell(string value, CellValues dataType) =>
           new Cell()
           {
               CellValue = new CellValue(value),
               DataType = new EnumValue<CellValues>(dataType)
           };

        private void ClearExistingFiles(string directoryPath)
        {
            if (Directory.Exists(directoryPath))
            {
                Directory.GetFiles(directoryPath).ToList().ForEach(File.Delete);
            }
            else
            {
                Directory.CreateDirectory(directoryPath);
            }
        }

        public async Task<string> CreateReportExcel(string fileName, ExcelDataDto data)
        {

            ClearExistingFiles(PathFiles);

            var filePath = Path.Combine(PathFiles, fileName);

            var document = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook);

            var wbPart = document.AddWorkbookPart();

            wbPart.Workbook = new Workbook();

            var part = wbPart.AddNewPart<WorksheetPart>();

            part.Worksheet = new Worksheet(new SheetData());

            var sheets = wbPart.Workbook.AppendChild(new Sheets());

            var sheet = new Sheet()
            {
                Id = wbPart.GetIdOfPart(part),
                SheetId = 1,
                Name = "Datos"
            };

            sheets.Append(sheet);
            await InsertDataIntoSheet(wbPart, data);
            await Task.Run(() => wbPart.Workbook.Save());

            return filePath;
        }

        private async Task InsertDataIntoSheet(WorkbookPart wbPart, ExcelDataDto data)
        {

            if (wbPart.IsNull())
                throw new CustomException(545, "No se logro crear el reporte en excel");

            var sheets = wbPart.Workbook.GetFirstChild<Sheets>();

            var sheet = sheets!.Elements<Sheet>().FirstOrDefault();
            sheet!.Name = "Datos";

            var part = wbPart.WorksheetParts.First();
            var lstColumns = sheets.GetFirstChild<Columns>() ?? new Columns();

            lstColumns.Append(new Column() { Min = 1, Max = 1, Width = 12, CustomWidth = true });
            lstColumns.Append(new Column() { Min = 2, Max = 2, Width = 48, CustomWidth = true });
            lstColumns.Append(new Column() { Min = 3, Max = 3, Width = 15, CustomWidth = true });
            lstColumns.Append(new Column() { Min = 4, Max = 4, Width = 15, CustomWidth = true });
            lstColumns.Append(new Column() { Min = 5, Max = 5, Width = 15, CustomWidth = true });
            lstColumns.Append(new Column() { Min = 6, Max = 6, Width = 15, CustomWidth = true });

            var sheetData = part.Worksheet.Elements<SheetData>().First();

            sheetData.InsertBeforeSelf(lstColumns);

            var headerRow = sheetData.AppendChild(new Row());

            foreach (var cell in data.Headers.Select(header => ConstructCell(header, CellValues.String)))
            {
                headerRow.Append(cell);
            }

            foreach (var value in data.Values)
            {
                var dataRow = sheetData.AppendChild(new Row());

                foreach (var dataElement in value)
                {
                    var cell = double.TryParse(dataElement, out var result)
                        ? ConstructCell(dataElement, CellValues.Number)
                        : ConstructCell(dataElement, CellValues.String);

                    dataRow.Append(cell);
                }
            }


            await Task.Run(() => wbPart.Workbook.Save());
        }

        
        public Task<string> CreatePdf(DteDTO documentDto, EmissionReposeDTO response, string regionalDirectionSii)
        {
            throw new NotImplementedException();
        }

        public Task<string> CreateReportPdf(string fileName, List<ProductSalesDto> products)
        {
            throw new NotImplementedException();
        }
    }
}
