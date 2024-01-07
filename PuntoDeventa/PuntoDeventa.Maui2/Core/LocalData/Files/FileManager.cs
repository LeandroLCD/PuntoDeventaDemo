using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using PuntoDeventa.Data.DTO.EmissionSystem;
using PuntoDeventa.Data.DTO.EmissionSystem.Dtes;
using PuntoDeventa.Data.DTO.Report;
using PuntoDeventa.Data.DTO.Sales;
using PuntoDeventa.Domain.Helpers;
using PuntoDeventa.UI.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Border = iText.Layout.Borders.Border;
using Cell = DocumentFormat.OpenXml.Spreadsheet.Cell;
using Cells = iText.Layout.Element.Cell;
using Image = iText.Layout.Element.Image;
using Path = System.IO.Path;
using Style = iText.Layout.Style;
using Table = iText.Layout.Element.Table;
using Text = iText.Layout.Element.Text;
using TextAlignment = iText.Layout.Properties.TextAlignment;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace PuntoDeventa.Core.LocalData.Files
{


    internal class FileManager : IFileManager
    {
        private static readonly string PathFiles = $"{FileSystem.CacheDirectory}/Files";

        public Task<string> CreatePdf(DteDTO documentDto, EmissionReposeDTO response, string regionalDirectionSii)
        {
            try
            {
                #region documentCreate

                ClearExistingFiles(PathFiles);
                var rznSoc = documentDto.Headers.Receiver.Name.Split(' ');

                var date = DateTime.Now;

                var invoiceNp = $"{date.Year:D2}{date.Month:D2}{date.Day:D2}{date.Hour:D2}{date.Minute:D2}";

                var nameDte = response.Folio.Equals(0)
                    ? $"NOTA DE PEDIDO #{invoiceNp}-{rznSoc[0]} {rznSoc[1]}.pdf"
                    : $"FACTURA_#{response.Folio}-{rznSoc[0]} {rznSoc[1]}.pdf";

                var filename = System.IO.Path.Combine(PathFiles, nameDte);



                var pdfWriter = new PdfWriter(filename);

                var pdfDocument = new PdfDocument(pdfWriter);

                pdfDocument.SetDefaultPageSize(new PageSize(PageSize.LETTER));

                var document = new Document(pdfDocument);

                document.SetMargins(57, 57, 57, 57);

                #endregion


                #region datos del Emisor
                var companyData = new Table(1, false).SetMaxWidth(312);

                //Datos del Emisnor.
                var issuingCompany = documentDto.Headers.IssuingCompany;

                var companyName = new Cells(1, 1).Add(new Paragraph(issuingCompany.Name)).SetFontSize(16).SetBold().SetBorder(Border.NO_BORDER);
                var companyTurn = new Cells(1, 1).Add(new Paragraph(issuingCompany.Turn)).SetFontSize(11).SetBorder(Border.NO_BORDER);
                var companyAddress = new Cells(1, 1).Add(new Paragraph(issuingCompany.Address)).SetFontSize(11).SetBorder(Border.NO_BORDER);

                companyData.AddCell(companyName);
                companyData.AddCell(companyTurn);
                companyData.AddCell(companyAddress);

                document.Add(companyData);

                var invoiceTable = new Table(1, false);
                var companyRut = new Cells(1, 1).Add(new Paragraph($"R.U.T.: {issuingCompany.Rut}")).SetFontSize(12).SetBold().SetBorder(Border.NO_BORDER);
                var dteString = response.Folio.Equals(0) ? "NOTA DE PEDIDO" : "FACTURA ELECTRÓNICA";
                var DteType = new Cells(1, 1).Add(new Paragraph(dteString)).SetFontSize(12).SetBold().SetBorder(Border.NO_BORDER);
                string folio = response.Folio.Equals(0) ? invoiceNp : response.Folio.ToString();
                var Folio = new Cells(1, 1).Add(new Paragraph($"N° {folio}")).SetFontSize(12).SetBold().SetBorder(Border.NO_BORDER);

                invoiceTable.AddCell(companyRut);
                invoiceTable.AddCell(DteType);
                invoiceTable.AddCell(Folio);

                invoiceTable.AddStyle(new Style().SetFontColor(ColorConstants.RED).SetTextAlignment(TextAlignment.CENTER).SetFixedPosition(383, 660, 169));

                document.Add(invoiceTable.SetBorder(new SolidBorder(ColorConstants.RED, 1)));

                var comuString = response.Folio.Equals(0) ? "Sin Validez Tributaria" : $"S.I.I. - {regionalDirectionSii}";

                var commune = new Paragraph(comuString);
                commune.AddStyle(new Style().SetFontColor(ColorConstants.RED).SetTextAlignment(TextAlignment.CENTER).SetFixedPosition(383, 640, 169));
                document.Add(commune);

                document.Add(new LineSeparator(new SolidLine()).SetRelativePosition(0, 10, 0, 0).SetHeight(1)).SetBackgroundColor(ColorConstants.BLACK);
                #endregion

                #region Datos del Resceptor

                var receptor = documentDto.Headers.Receiver;

                float[] pointColumnWidths = { 70F, 248F, 180F };

                var headerReceiver = new Table(pointColumnWidths)
                    .SetBorderCollapse(BorderCollapsePropertyValue.SEPARATE);

                var r0 = CornersRadiusLeft(new Cells(2, 1), 8, 5);

                r0.Add(new Paragraph(new Text("R.U.T:").SetBold()));

                r0.Add(new Paragraph(new Text("Señor (ES): ").SetBold()));

                r0.Add(new Paragraph(new Text("Giro:").SetBold()));

                r0.Add(new Paragraph(new Text("Direción:").SetBold()));

                r0.Add(new Paragraph(new Text("Comuna:").SetBold()));

                headerReceiver.AddCell(r0.SetFontSize(10));

                var r1 = CornersRadiusRight(new Cells(2, 1), 8, 5);

                r1.Add(new Paragraph(new Text(receptor.Rut)));

                var rs = receptor.Name.Length > 35 ? receptor.Name[..35] : receptor.Name;

                r1.Add(new Paragraph(new Text(rs)));

                var giro = receptor.Turn.Length > 35 ? receptor.Turn[..35] : receptor.Turn;

                r1.Add(new Paragraph(new Text(giro)));

                r1.Add(new Paragraph(new Text(receptor.Address)));

                r1.Add(new Paragraph(new Text(receptor.Commune)));
                headerReceiver.AddCell(r1.SetFontSize(10));
                #endregion

                #region Fecha de emision
                var r2 = new Cells(1, 1);
                r2.AddStyle(new Style().SetPadding(5).SetBorder(Border.NO_BORDER));
                date = Convert.ToDateTime(documentDto.Headers.IdDoc.FchEmis);
                Paragraph Date = new Paragraph($"{date.Day} de {date:MMMM}, del {date.Year}.").SetBold().SetTextAlignment(TextAlignment.RIGHT);
                r2.Add(Date);
                headerReceiver.AddCell(r2);
                #endregion

                #region Notas de Pago
                if (!response.Folio.Equals(0))
                {
                    var r3 = new Cells(1, 1).AddStyle(new Style().SetPadding(5).SetBorder(Border.NO_BORDER).SetVerticalAlignment(VerticalAlignment.BOTTOM));
                    var pay = new Paragraph($"Forma de pago:").SetTextAlignment(TextAlignment.RIGHT)
                        .Add(new Text(documentDto.Headers.IdDoc.FmaPago == 1 ? "Contado" : "Crédito").AddStyle(new Style().SetUnderline()));
                    r3.Add(pay);
                    pay = new Paragraph($"Tipo de venta:").SetTextAlignment(TextAlignment.RIGHT)
                       .Add(new Text("Ventas del Giro").AddStyle(new Style().SetUnderline()));
                    r3.Add(pay);
                    pay = new Paragraph($"Tipo Compra:").SetTextAlignment(TextAlignment.RIGHT)
                       .Add(new Text("Compras del Giro").AddStyle(new Style().SetUnderline()));
                    r3.Add(pay);
                    headerReceiver.AddCell(r3);
                }


                document.Add(headerReceiver.SetRelativePosition(0, 20, 0, 0).SetBorder(Border.NO_BORDER));
                #endregion

                #region Itms

                float[] columnWidths = { 50F, 288F, 25F, 60F, 75F };

                var ItemsTable = new Table(columnWidths).SetBorderCollapse(BorderCollapsePropertyValue.SEPARATE);

                #region Encabezado
                float height = 14F;
                Style styleHeader = new Style()
                    .SetBackgroundColor(ColorConstants.BLACK)
                    .SetFontColor(ColorConstants.WHITE)
                    .SetFontSize(8)
                    .SetHeight(height)
                    .SetBold();

                var cellHeader = new Cells(1, 1);

                cellHeader = new Cells(1, 1).AddStyle(styleHeader).Add(new Paragraph("Código"));
                ItemsTable.AddCell(cellHeader);
                cellHeader = new Cells(1, 1).AddStyle(styleHeader).Add(new Paragraph("Descripción"));
                ItemsTable.AddCell(cellHeader);
                cellHeader = new Cells(1, 1).AddStyle(styleHeader).Add(new Paragraph("Cant"));
                ItemsTable.AddCell(cellHeader);
                cellHeader = new Cells(1, 1).AddStyle(styleHeader).Add(new Paragraph("Precio")).SetTextAlignment(TextAlignment.RIGHT);
                ItemsTable.AddCell(cellHeader);
                cellHeader = new Cells(1, 1).AddStyle(styleHeader).Add(new Paragraph("Sub Total")).SetTextAlignment(TextAlignment.RIGHT);
                ItemsTable.AddCell(cellHeader);
                #endregion

                #region Cuerpo

                new Cells(1, 1);
                Style itmesStyle = new Style()
                    .SetBorderBottom(Border.NO_BORDER)
                    .SetBorderLeft(new SolidBorder(ColorConstants.BLACK, 1))
                    .SetBorderRight(Border.NO_BORDER)
                    .SetBorderTop(Border.NO_BORDER)
                    .SetFontSize(8);
                Style itemsStyleRight = new Style()
                    .SetBorderBottom(Border.NO_BORDER)
                    .SetBorderLeft(new SolidBorder(ColorConstants.BLACK, 1))
                    .SetBorderRight(new SolidBorder(ColorConstants.BLACK, 1))
                    .SetBorderTop(Border.NO_BORDER)
                .SetFontSize(8);

                var x = 1;
                foreach (var product in documentDto.Detalle)
                {
                    if (product == null) continue;
                    Cells itemsCell;
                    if (x != documentDto.Detalle.Count)
                    {
                        itemsCell = new Cells(1, 1).Add(new Paragraph(product.CdgItem[0].Value).SetTextAlignment(TextAlignment.CENTER));
                        ItemsTable.AddCell(itemsCell.AddStyle(itmesStyle));
                        itemsCell = new Cells(1, 1).Add(new Paragraph(product.NmbItem));
                        ItemsTable.AddCell(itemsCell.AddStyle(itmesStyle));
                        itemsCell = new Cells(1, 1).Add(new Paragraph(product.QtyItem.ToString()).SetTextAlignment(TextAlignment.CENTER));
                        ItemsTable.AddCell(itemsCell.AddStyle(itmesStyle));
                        itemsCell = new Cells(1, 1).Add(new Paragraph($"{product.PriceItem:N0} ").SetTextAlignment(TextAlignment.RIGHT));
                        ItemsTable.AddCell(itemsCell.AddStyle(itmesStyle));
                        itemsCell = new Cells(1, 1).Add(new Paragraph($"{product.Amount:N0} ").SetTextAlignment(TextAlignment.RIGHT));
                        ItemsTable.AddCell(itemsCell.AddStyle(itemsStyleRight));
                        x++;
                    }

                    else
                    {
                        itemsCell = new Cells(1, 1).Add(new Paragraph(product.CdgItem[0].Value).SetTextAlignment(TextAlignment.CENTER));
                        ItemsTable.AddCell(itemsCell.AddStyle(itmesStyle).SetHeight(height * (23 - x)));
                        itemsCell = new Cells(1, 1).Add(new Paragraph(product.NmbItem));
                        ItemsTable.AddCell(itemsCell.AddStyle(itmesStyle).SetHeight(height * (23 - x)));
                        itemsCell = new Cells(1, 1).Add(new Paragraph(product.QtyItem.ToString()).SetTextAlignment(TextAlignment.CENTER));
                        ItemsTable.AddCell(itemsCell.AddStyle(itmesStyle).SetHeight(height * (23 - x)));
                        itemsCell = new Cells(1, 1).Add(new Paragraph($"{product.PriceItem:N0} ").SetTextAlignment(TextAlignment.RIGHT));
                        ItemsTable.AddCell(itemsCell.AddStyle(itmesStyle).SetHeight(height * (23 - x)));
                        itemsCell = new Cells(1, 1).Add(new Paragraph($"{product.Amount:N0} ").SetTextAlignment(TextAlignment.RIGHT));
                        ItemsTable.AddCell(itemsCell.AddStyle(itemsStyleRight).SetHeight(height * (23 - x)));
                    }
                }


                #endregion

                document.Add(ItemsTable.SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 1)).SetRelativePosition(0, 30, 0, 0));
                #endregion

                #region Totales
                var totals = documentDto.Headers.Totals;
                if (!response.Folio.Equals(0))
                {
                    var fosterTimbre = new Table(1).SetBorderCollapse(BorderCollapsePropertyValue.SEPARATE).SetWidth(150F);

                    var fosterStyle = new Style()
                        .SetFontSize(6)
                        .SetBorder(Border.NO_BORDER)
                        .SetTextAlignment(TextAlignment.CENTER);

                    byte[] bytes = Convert.FromBase64String(response.Timbre);
                    PdfStream stream = new PdfStream(bytes);
                    Image timbre = new Image(ImageDataFactory.CreatePng(bytes));
                    timbre.SetWidth(UnitValue.CreatePercentValue(100));
                    //Timbre electronico del SII
                    fosterTimbre.AddCell(new Cells(3, 1).Add(timbre).AddStyle(fosterStyle));
                    var Notas = new Cells(1, 1).Add(new Paragraph("Timbre Electrónico S.I.I"));
                    Notas.Add(new Paragraph($"Res. {response.Resolution.Number} de {DateTime.Parse(response.Resolution.Date):yyyy}."));
                    Notas.Add(new Paragraph("Verifique documento en www.sii.cl"));
                    fosterTimbre.AddCell(Notas.AddStyle(fosterStyle));
                    document.Add(fosterTimbre.SetFixedPosition(57, 57, 100));
                }



                //Totales de la Factura.
                float[] colum = { 60F, 40F };
                Table FosterTotales = new Table(colum);
                Style TotalStyle = new Style().SetHeight(height).SetFontSize(8).SetBorder(new SolidBorder(ColorConstants.BLACK, 1));
                FosterTotales.AddCell(new Cells(1, 1).Add(new Paragraph("Monto Neto:").SetBold()).AddStyle(TotalStyle));
                FosterTotales.AddCell(new Cells(1, 1).Add(new Paragraph($"{totals.Net:N2} ")).AddStyle(TotalStyle)).SetTextAlignment(TextAlignment.RIGHT);
                FosterTotales.AddCell(new Cells(1, 1).Add(new Paragraph($"Vat {totals.VatRate} %:").SetBold()).AddStyle(TotalStyle));
                FosterTotales.AddCell(new Cells(1, 1).Add(new Paragraph($"{totals.Vat:N2} ")).AddStyle(TotalStyle)).SetTextAlignment(TextAlignment.RIGHT);
                FosterTotales.AddCell(new Cells(1, 1).Add(new Paragraph("Monto Total:").SetBold()).AddStyle(TotalStyle));
                FosterTotales.AddCell(new Cells(1, 1).Add(new Paragraph($"{totals.Amount:N2} ")).AddStyle(TotalStyle)).SetTextAlignment(TextAlignment.RIGHT);
                document.Add(FosterTotales.SetFixedPosition(415, 77, 140));

                #endregion

                document.Close();
                return Task.FromResult(filename);
            }
            catch (Exception ex)
            {
                return Task.FromException<string>(ex);
            }
        }

        private static Cells CornersRadiusLeft(Cells cell, int radius, int padding)
        {
            cell.SetPadding(padding);
            cell.SetBorderTopLeftRadius(new BorderRadius(radius));
            cell.SetBorderBottomLeftRadius(new BorderRadius(radius));

            cell.SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 1));
            cell.SetBorderLeft(new SolidBorder(ColorConstants.BLACK, 1));
            cell.SetBorderRight(Border.NO_BORDER);
            cell.SetBorderTop(new SolidBorder(ColorConstants.BLACK, 1));
            return cell;

        }
        private static Cells CornersRadiusRight(Cells cell, int radius, int padding)
        {
            cell.SetPadding(padding);
            cell.SetBorderBottomRightRadius(new BorderRadius(radius));
            cell.SetBorderTopRightRadius(new BorderRadius(radius));

            cell.SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 1));
            cell.SetBorderRight(new SolidBorder(ColorConstants.BLACK, 1));
            cell.SetBorderLeft(Border.NO_BORDER);
            cell.SetBorderTop(new SolidBorder(ColorConstants.BLACK, 1));
            return cell;

        }

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

        public async Task<string> CreateReportPdf(string fileName, List<ProductSalesDto> products)
        {
            try
            {
                ClearExistingFiles(PathFiles);


                var filePath = Path.Combine(PathFiles, fileName);


                #region Crear Pdf
                PdfWriter pdfWriter = new PdfWriter(filePath);

                PdfDocument pdfDocument = new PdfDocument(pdfWriter);

                pdfDocument.SetDefaultPageSize(new PageSize(PageSize.LETTER));

                Document document = new Document(pdfDocument);
                document.SetMargins(57, 57, 57, 57);



                #endregion

                #region Titulo
                Table title = new Table(1);
                Style styleTitle = new Style()
                    .SetFontColor(ColorConstants.BLACK)
                    .SetFontSize(14)
                    .SetHeight(25F)
                    .SetBorder(Border.NO_BORDER)
                    .SetWidth(498F)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetHorizontalAlignment(HorizontalAlignment.CENTER)
                    .SetBold();
                var cellTitle = new Cells(1, 1).AddStyle(styleTitle).Add(new Paragraph(fileName.Remove(fileName.Length - 4, 4)));
                title.AddCell(cellTitle);
                document.Add(title);
                #endregion

                #region Encabezado
                float[] columnWidths = { 50F, 263F, 60F, 75F, 50F };
                var itemsTable = new Table(columnWidths).SetBorderCollapse(BorderCollapsePropertyValue.SEPARATE);
                var height = 14F;
                Style styleHeader = new Style()
                    .SetBackgroundColor(ColorConstants.BLACK)
                    .SetFontColor(ColorConstants.WHITE)
                    .SetFontSize(8)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetHeight(height)
                    .SetBold();

                var cellHeader = new Cells(1, 1).AddStyle(styleHeader).Add(new Paragraph("Código"));
                itemsTable.AddHeaderCell(cellHeader);
                cellHeader = new Cells(1, 1).AddStyle(styleHeader).Add(new Paragraph("Producto"));
                itemsTable.AddHeaderCell(cellHeader);
                cellHeader = new Cells(1, 1).AddStyle(styleHeader).Add(new Paragraph("Precio"));
                itemsTable.AddHeaderCell(cellHeader);
                cellHeader = new Cells(1, 1).AddStyle(styleHeader).Add(new Paragraph("Precio + Iva"));
                itemsTable.AddHeaderCell(cellHeader);
                cellHeader = new Cells(1, 1).AddStyle(styleHeader).Add(new Paragraph("Cantidad"));
                itemsTable.AddHeaderCell(cellHeader);
                #endregion

                #region products

                var itemStyle = new Style()
                    .SetBorderBottom(Border.NO_BORDER)
                    .SetBorderLeft(new SolidBorder(ColorConstants.BLACK, 1))
                    .SetBorderRight(Border.NO_BORDER)
                    .SetBorderTop(Border.NO_BORDER)
                    .SetFontSize(8);
                var itemStyleRight = new Style()
                    .SetBorderBottom(Border.NO_BORDER)
                    .SetBorderLeft(new SolidBorder(ColorConstants.BLACK, 1))
                    .SetBorderRight(new SolidBorder(ColorConstants.BLACK, 1))
                    .SetBorderTop(Border.NO_BORDER)
                .SetFontSize(8);
                if (products != null)
                {
                    Cells itemCell;
                    foreach (var item in products)
                    {
                        var indexOf = products.IndexOf(item);
                        var background = indexOf % 2 == 0 ? new DeviceRgb(231, 230, 230) : ColorConstants.WHITE;
                        itemCell = new Cells(1, 1).Add(new Paragraph(item.SkuCode).SetTextAlignment(TextAlignment.CENTER));
                        itemsTable.AddCell(itemCell.AddStyle(itemStyle).SetBackgroundColor(background));
                        itemCell = new Cells(1, 1).Add(new Paragraph(item.Name));
                        itemsTable.AddCell(itemCell.AddStyle(itemStyle).SetBackgroundColor(background));
                        itemCell = new Cells(1, 1).Add(new Paragraph($"{item.Quantity} ").SetTextAlignment(TextAlignment.RIGHT));
                        itemsTable.AddCell(itemCell.AddStyle(itemStyle).SetBackgroundColor(background));
                        itemCell = new Cells(1, 1).Add(new Paragraph($"{item.PriceGross} ").SetTextAlignment(TextAlignment.RIGHT));
                        itemsTable.AddCell(itemCell.AddStyle(itemStyle).SetBackgroundColor(background));
                        itemCell = new Cells(1, 1).Add(new Paragraph($"{item.SubTotal}").SetTextAlignment(TextAlignment.CENTER));
                        itemsTable.AddCell(itemCell.AddStyle(itemStyleRight).SetBackgroundColor(background));
                    }


                    itemCell = new Cells(1, 3);
                    itemsTable.AddCell(itemCell.AddStyle(styleHeader));
                    itemCell = new Cells(1, 1).Add(new Paragraph($"Nro Productos:").SetTextAlignment(TextAlignment.RIGHT));
                    itemsTable.AddCell(itemCell.AddStyle(styleHeader));
                    itemCell = new Cells(1, 1).Add(new Paragraph($"{products.Sum(p => p.Quantity)}").SetTextAlignment(TextAlignment.CENTER));
                    itemsTable.AddCell(itemCell.AddStyle(styleHeader));
                }
                document.Add(itemsTable
                    .SetBorderBottom(new SolidBorder(ColorConstants.BLACK, 1))
                    .SetBorderTop(new SolidBorder(ColorConstants.BLACK, 1))
                    .SetRelativePosition(0, 20, 0, 0));
                #endregion

                document.Close();
                return filePath;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
                return string.Empty;
            }
        }


    }
}
