using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Gym.Data;
using Gym.ViewModel;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Gym.Controllers
{
    public class ReportController : Controller
    {
        private readonly GymDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ReportController(GymDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult EmployeeReport(int Id)
        {
            var voucherViewModel = (from t in _dbContext.MonthlyFeeVouchers
                                    .Where(t => t.MonthlyFeeVoucherID == Id)
                                    join mfv in _dbContext.Trainees
                                    on t.TraineeId equals mfv.TraineeId
                                    into fee_Details
                                    from mfv in fee_Details.DefaultIfEmpty()
                                    select new GymTraineeDetailViewModel
                                    {
                                        monthlyFeeVoucher = t,
                                        gymTrainee = mfv
                                    }).FirstOrDefault();

            if (voucherViewModel == null)
            {
                return NotFound();
            }

            // Create the PDF document
            var document = new Document();
            using (var memoryStream = new MemoryStream())
            {
                var writer = PdfWriter.GetInstance(document, memoryStream);
                document.Open();

                // Add gym name (centered)
                var gymNameFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 24, BaseColor.BLACK);
                var gymName = new Paragraph("Fitness Club", gymNameFont);
                gymName.Alignment = Element.ALIGN_CENTER; // Center alignment
                document.Add(gymName);

                // Add title (centered)
                var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, BaseColor.DARK_GRAY);
                var title = new Paragraph("Monthly Fee Voucher", titleFont);
                title.Alignment = Element.ALIGN_CENTER; // Center alignment
                document.Add(title);
                document.Add(new Paragraph(Environment.NewLine));

                // Add date
                var dateFont = FontFactory.GetFont(FontFactory.HELVETICA, 12, BaseColor.BLACK);
                var date = new Paragraph(DateTime.Now.ToString("MMMM dd, yyyy"), dateFont);
                date.Alignment = Element.ALIGN_LEFT;
                document.Add(date);

                // Add image to the PDF (top-right)
                string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", voucherViewModel.gymTrainee.ImageName);
                if (System.IO.File.Exists(imagePath))
                {
                    using (var imageStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                    {
                        var image = iTextSharp.text.Image.GetInstance(imageStream);
                        image.ScaleToFit(150f, 100f); // Adjust the image size as needed
                        image.Alignment = iTextSharp.text.Image.ALIGN_RIGHT;
                        document.Add(image);
                    }
                }

                // Add data to the PDF in a structured format
                PdfPTable table = new PdfPTable(2);
                table.WidthPercentage = 100; // Table width 100% of page width
                table.SpacingBefore = 20; // Add space before the table to avoid overlapping

                // Add data cells
                table.AddCell(CreateCell("Name", voucherViewModel.gymTrainee.FirstName + " " + voucherViewModel.gymTrainee.LastName));
                table.AddCell(CreateCell("Address", voucherViewModel.gymTrainee.Address));
                table.AddCell(CreateCell("Gender", voucherViewModel.gymTrainee.Gender));
                table.AddCell(CreateCell("Age", voucherViewModel.gymTrainee.Age.ToString()));
                table.AddCell(CreateCell("Contact No", voucherViewModel.gymTrainee.ContactNo));
                table.AddCell(CreateCell("Monthly Fee", voucherViewModel.gymTrainee.MonthlyFee.ToString()));

                document.Add(table);

                document.Close();

                // Return the PDF file
                return File(memoryStream.ToArray(), "application/pdf", "report.pdf");
            }
        }

        // Helper method to create a PdfPCell with formatted text
        private PdfPCell CreateCell(string label, string value)
        {
            var cell = new PdfPCell(new Phrase(label + ":", FontFactory.GetFont(FontFactory.HELVETICA_BOLD)));
            cell.Border = Rectangle.NO_BORDER;
            var valueCell = new PdfPCell(new Phrase(value, FontFactory.GetFont(FontFactory.HELVETICA)));
            valueCell.Border = Rectangle.NO_BORDER;
            var table = new PdfPTable(1);
            table.AddCell(cell);
            table.AddCell(valueCell);
            return new PdfPCell(table);
        }
    }
}
