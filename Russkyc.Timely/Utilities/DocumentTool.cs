using System.Collections.Immutable;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Russkyc.Timely.Models.Entities;

namespace Russkyc.Timely.Utilities;

public static class DocumentTool
{
    public static byte[] GenerateDateTimeRecord(IEnumerable<TimeEntry> timeEntries, DateTime currentTimestamp)
    {
        var enumerable = timeEntries as TimeEntry[] ?? timeEntries.ToArray();
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(4.25f, 13, Unit.Inch);
                page.Content()
                    .Padding(7, Unit.Millimetre)
                    .Column(column =>
                    {
                        column.Spacing(11);
                        column.Item()
                            .Text("Civil Service Form No. 48")
                            .FontSize(9.5f)
                            .Bold()
                            .Italic();
                        column.Item()
                            .Column(column =>
                            {
                                column.Spacing(-2);
                                column.Item()
                                    .AlignCenter()
                                    .Text("DAILY TIME RECORD")
                                    .Black();
                                column.Item()
                                    .AlignCenter()
                                    .Text("-----o0o-----")
                                    .FontSize(9.5f);
                            });
                        column.Item()
                            .Column(column =>
                            {
                                column.Item()
                                    .PaddingTop(8)
                                    .LineHorizontal(0.75f);
                                column.Item()
                                    .AlignCenter()
                                    .Text("(Name)")
                                    .Bold()
                                    .Italic()
                                    .FontSize(9.5f);
                            });
                        column.Item()
                            .PaddingBottom(4)
                            .Column(column =>
                            {
                                column.Item().Row(row =>
                                {
                                    row.AutoItem()
                                        .AlignCenter()
                                        .Text("For the month of")
                                        .Italic()
                                        .FontSize(9.5f);
                                    row.RelativeItem()
                                        .PaddingTop(11.45f)
                                        .PaddingLeft(4)
                                        .LineHorizontal(0.65f);
                                    row.AutoItem()
                                        .AlignCenter()
                                        .Text(currentTimestamp.ToString("MMMM, yyyy"))
                                        .Underline()
                                        .Italic()
                                        .Bold()
                                        .FontSize(9.5f);
                                    row.RelativeItem()
                                        .PaddingTop(11.45f)
                                        .LineHorizontal(0.65f);
                                });
                                column.Item().Row(row =>
                                {
                                    row.Spacing(64);
                                    row.AutoItem()
                                        .Column(column =>
                                        {
                                            column.Item()
                                                .AlignCenter()
                                                .Text("Official hours for arrival")
                                                .Italic()
                                                .FontSize(9.5f);
                                            column.Item()
                                                .AlignCenter()
                                                .Text("and departure")
                                                .Italic()
                                                .FontSize(9.5f);
                                        });
                                    row.RelativeItem()
                                        .Column(column =>
                                        {
                                            column.Item()
                                                .Row(row =>
                                                {
                                                    row.AutoItem()
                                                        .AlignCenter()
                                                        .Text("Regular Days")
                                                        .Italic()
                                                        .FontSize(9.5f);
                                                    row.RelativeItem()
                                                        .PaddingTop(10)
                                                        .PaddingLeft(4)
                                                        .LineHorizontal(0.75f);
                                                });
                                            column.Item()
                                                .Row(row =>
                                                {
                                                    row.AutoItem()
                                                        .AlignCenter()
                                                        .Text("Saturdays")
                                                        .Italic()
                                                        .FontSize(9.5f);
                                                    row.RelativeItem()
                                                        .PaddingTop(10)
                                                        .PaddingLeft(4)
                                                        .LineHorizontal(0.75f);
                                                });
                                        });
                                });
                            });
                        column.Item()
                            .MinimalBox()
                            .Border(1)
                            .Table(table =>
                            {
                                IContainer DefaultCellStyle(IContainer container, string backgroundColor)
                                {
                                    return container
                                        .Border(0.75f)
                                        .AlignCenter()
                                        .PaddingVertical(3)
                                        .PaddingHorizontal(2)
                                        .AlignMiddle();
                                }

                                IContainer RowCellStyle(IContainer container, string backgroundColor)
                                {
                                    return container
                                        .Border(0.75f)
                                        .AlignCenter()
                                        .PaddingVertical(0.5f)
                                        .PaddingHorizontal(2)
                                        .AlignMiddle();
                                }

                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                table.Header(header =>
                                {
                                    header.Cell()
                                        .RowSpan(2)
                                        .Element(CellStyle)
                                        .ExtendHorizontal()
                                        .AlignCenter()
                                        .Text("Day")
                                        .FontSize(10)
                                        .Bold();
                                    header.Cell()
                                        .ColumnSpan(2)
                                        .Element(CellStyle)
                                        .Text("A.M.")
                                        .FontSize(10)
                                        .Black();

                                    header.Cell()
                                        .ColumnSpan(2)
                                        .Element(CellStyle)
                                        .Text("P.M.")
                                        .FontSize(10)
                                        .Black();
                                    header.Cell()
                                        .ColumnSpan(2)
                                        .Element(CellStyle)
                                        .Text("Undertime")
                                        .FontSize(10)
                                        .Black();
                                    header.Cell()
                                        .Element(CellStyle)
                                        .ExtendHorizontal()
                                        .AlignCenter()
                                        .Text("Arrival")
                                        .FontSize(9.5f)
                                        .Black();
                                    header.Cell()
                                        .Element(CellStyle)
                                        .ExtendHorizontal()
                                        .AlignCenter()
                                        .Text("Depar- ture")
                                        .LineHeight(0.9f)
                                        .FontSize(9.5f)
                                        .Black();
                                    header.Cell()
                                        .Element(CellStyle)
                                        .ExtendHorizontal()
                                        .AlignCenter()
                                        .Text("Arrival")
                                        .FontSize(9.5f)
                                        .Black();
                                    header.Cell()
                                        .Element(CellStyle)
                                        .ExtendHorizontal()
                                        .AlignCenter()
                                        .Text("Depar- ture")
                                        .LineHeight(0.9f)
                                        .FontSize(9.5f)
                                        .Black();
                                    header.Cell()
                                        .Element(CellStyle)
                                        .ExtendHorizontal()
                                        .AlignCenter()
                                        .Text("Hours")
                                        .FontSize(9.5f)
                                        .Black();
                                    header.Cell()
                                        .Element(CellStyle)
                                        .ExtendHorizontal()
                                        .AlignCenter()
                                        .Text("Minu- tes")
                                        .LineHeight(0.9f)
                                        .FontSize(9.5f)
                                        .Black();

                                    IContainer CellStyle(IContainer container) =>
                                        DefaultCellStyle(container, Colors.Grey.Lighten3);
                                });
                                
                                for (var day = 1; day <= 31; day++)
                                {
                                    table.Cell()
                                        .Element(RowStyle)
                                        .ExtendHorizontal()
                                        .AlignCenter()
                                        .Text(day)
                                        .Black();

                                    var dayRecord = enumerable.FirstOrDefault(entry => entry.Day == day);
                                    var recordAmInIsValid = dayRecord is not null && dayRecord.AmIn != default;

                                    // AM In
                                    table.Cell()
                                        .Element(RowStyle)
                                        .ExtendHorizontal()
                                        .AlignCenter()
                                        .Text(recordAmInIsValid
                                            ? dayRecord!.AmIn.ToString("hh:mm")
                                            : String.Empty)
                                        .Bold()
                                        .Italic();

                                    var recordAmOutIsValid = dayRecord is not null && dayRecord.AmOut != default;
                                    
                                    // AM Out
                                    table.Cell()
                                        .Element(RowStyle)
                                        .ExtendHorizontal()
                                        .AlignCenter()
                                        .Text(recordAmOutIsValid
                                            ? dayRecord!.AmOut.ToString("hh:mm")
                                            : String.Empty)
                                        .Bold()
                                        .Italic();
                                    
                                    var recordPmInIsValid = dayRecord is not null && dayRecord.PmIn != default;

                                    // Pm In
                                    table.Cell()
                                        .Element(RowStyle)
                                        .ExtendHorizontal()
                                        .AlignCenter()
                                        .Text(recordPmInIsValid
                                            ? dayRecord!.PmIn.ToString("hh:mm")
                                            : String.Empty)
                                        .Bold()
                                        .Italic();

                                    var recordPmOutIsValid = dayRecord is not null && dayRecord.PmOut != default;

                                    // Pm Out
                                    table.Cell()
                                        .Element(RowStyle)
                                        .ExtendHorizontal()
                                        .AlignCenter()
                                        .Text(recordPmOutIsValid
                                            ? dayRecord!.PmOut.ToString("hh:mm")
                                            : String.Empty)
                                        .Bold()
                                        .Italic();

                                    // Under Hours
                                    table.Cell()
                                        .Element(RowStyle)
                                        .ExtendHorizontal()
                                        .AlignLeft()
                                        .Text(String.Empty);

                                    // Under Minutes
                                    table.Cell()
                                        .Element(RowStyle)
                                        .ExtendHorizontal()
                                        .AlignLeft()
                                        .Text(String.Empty);

                                    IContainer CellStyle(IContainer container) =>
                                        DefaultCellStyle(container, Colors.White)
                                            .ShowOnce();

                                    IContainer RowStyle(IContainer container) =>
                                        RowCellStyle(container, Colors.White)
                                            .ShowOnce();
                                }
                            });
                        column.Item()
                            .Column(column =>
                            {
                                column.Spacing(24);
                                column.Item()
                                    .AlignLeft()
                                    .PaddingHorizontal(4)
                                    .Text(
                                        "I certify on my honor that the above is a true and correct report of the hours of work performed, record of which was made daily at te time of arrival and departure from office.")
                                    .Italic()
                                    .Bold()
                                    .FontSize(8.5f);
                                column.Item()
                                    .Column(column =>
                                    {
                                        column.Spacing(4);
                                        column.Item()
                                            .LineHorizontal(0.75f);
                                        column.Item()
                                            .AlignLeft()
                                            .PaddingHorizontal(4)
                                            .Text("VERIFIED as to the prescribed office hours:")
                                            .Italic()
                                            .Bold()
                                            .FontSize(8.5f);
                                    });
                                column.Item()
                                    .Column(column =>
                                    {
                                        column.Item()
                                            .LineHorizontal(1.5f);
                                        column.Item()
                                            .AlignCenter()
                                            .PaddingHorizontal(4)
                                            .Text("In Charge")
                                            .Italic()
                                            .Bold()
                                            .FontSize(9f);
                                    });
                            });
                    });
            });
        });

        return document.GeneratePdf();
    }
}