using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

public class Reservation
{
    public long CustomerId { get; set; }
    public long CarId { get; set; }
    public DateTime ReservationDate { get; set; }
    public DateTime ReservationTime { get; set; }
    public DateTime ReturnDate { get; set; }
    public DateTime ReturnTime { get; set; }
    public Decimal TotalAmount { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        // Create a Reservation object with sample data
        Reservation reservation = new Reservation
        {
            CustomerId = 123,
            CarId = 456,
            ReservationDate = DateTime.Today,
            ReservationTime = DateTime.Now,
            ReturnDate = DateTime.Today.AddDays(3),
            ReturnTime = DateTime.Now.AddHours(3),
            TotalAmount = 150.00m
        };

        // Path to the PDF file
        string filePath = "C:\\Users\\user\\Desktop\\Eng ko'p ishlatiladigan 170 ta gap.pdf";

        // Write reservation details to the PDF
        WriteReservationToPdf(filePath, reservation);

        Console.WriteLine($"Reservation details have been written to {filePath}");
    }

    static void WriteReservationToPdf(string filePath, Reservation reservation)
    {
        // Create a new document
        Document doc = new Document();

        try
        {
            // Create a PdfWriter that listens to the document and directs a PDF-stream to a file
            PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));

            // Open the document
            doc.Open();

            // Add reservation details to the document
            doc.Add(new Paragraph($"Customer ID: {reservation.CustomerId}"));
            doc.Add(new Paragraph($"Car ID: {reservation.CarId}"));
            doc.Add(new Paragraph($"Reservation Date: {reservation.ReservationDate.ToShortDateString()}"));
            doc.Add(new Paragraph($"Reservation Time: {reservation.ReservationTime.ToShortTimeString()}"));
            doc.Add(new Paragraph($"Return Date: {reservation.ReturnDate.ToShortDateString()}"));
            doc.Add(new Paragraph($"Return Time: {reservation.ReturnTime.ToShortTimeString()}"));
            doc.Add(new Paragraph($"Total Amount: {reservation.TotalAmount:C}"));
        }
        finally
        {
            // Close the document
            doc.Close();
        }
    }
}
