using Gym.Data;
using Gym.Models;
using Gym.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualBasic;

namespace Gym.Controllers
{
    public class FeeVoucherController : Controller
    {

        private readonly GymDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private object _IWebhostEnvironment;

        public FeeVoucherController(GymDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            this._webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index(string selected_rbt, string selectedDate)
        {
            System.Diagnostics.Debug.WriteLine($"selected_rbt: {selected_rbt}, selectedDate: {selectedDate}");
            ViewBag.Selectedbutton = selected_rbt;
            ViewBag.SelectedDate = selectedDate;
            var result = GetGymeTraineeFeeStatus(selected_rbt, selectedDate);
            return View((IEnumerable<GymTraineeDetailViewModel>)result);
        }

        IEnumerable<GymTraineeDetailViewModel> GetGymeTraineeFeeStatus(string selected_rbt, string selectedDate)
        {
            if (selected_rbt == "list")
            {
                if (string.IsNullOrEmpty(selected_rbt))
                {
                    selected_rbt = "List";
                }
            }

            IEnumerable<GymTraineeDetailViewModel> result = null;

            if (selected_rbt == "Paid")
            {
                result = from t in _dbContext.Trainees
                         join mfv in _dbContext.MonthlyFeeVouchers on t.TraineeId equals mfv.TraineeId into fee_Details
                         from mfv in fee_Details.DefaultIfEmpty()
                         where (mfv.Status == selected_rbt)
                         select new GymTraineeDetailViewModel
                         {
                             monthlyFeeVoucher = mfv,
                             gymTrainee = t
                         };
            }
            else if (selected_rbt == "Un-Paid")
            {
                result = from t in _dbContext.Trainees
                         join mfv in _dbContext.MonthlyFeeVouchers on t.TraineeId equals mfv.TraineeId into fee_Details
                         from mfv in fee_Details.DefaultIfEmpty()
                         where (mfv.FeeDate == null)
                         select new GymTraineeDetailViewModel
                         {
                             monthlyFeeVoucher = mfv,
                             gymTrainee = t
                         };
            }
            if (selected_rbt == "list")
            {
                result = from t in _dbContext.Trainees
                         join mfv in _dbContext.MonthlyFeeVouchers on t.TraineeId equals mfv.TraineeId into fee_Details
                         from mfv in fee_Details.DefaultIfEmpty()

                         select new GymTraineeDetailViewModel
                         {
                             monthlyFeeVoucher = mfv,
                             gymTrainee = t
                         };
                if (string.IsNullOrEmpty(selected_rbt))
                {
                    result = from t in _dbContext.Trainees
                             join mfv in _dbContext.MonthlyFeeVouchers on t.TraineeId equals mfv.TraineeId into fee_Details

                             from mfv in fee_Details.DefaultIfEmpty()

                             select new GymTraineeDetailViewModel
                             {
                                 monthlyFeeVoucher = mfv,
                                 gymTrainee = t
                             };

                }
            }

            return result;
        }

        public async Task<IActionResult> Details(int? id)
        {
            FeeVoucherDetailsViewModel FeeVoucherDetailsViewModel = new FeeVoucherDetailsViewModel();


            if (id == null)
            {
                return NotFound();
            }



            FeeVoucherDetailsViewModel.gymTrainee = await _dbContext.Trainees
                .FirstOrDefaultAsync(m => m.TraineeId == id);

            if (FeeVoucherDetailsViewModel == null)
            {
                return NotFound();
            }

            return View(FeeVoucherDetailsViewModel);
        }




        [HttpGet]
        public async Task<IActionResult> PayMonthlyFee(int? Id)
        {
            FeeVoucherDetailsViewModel FeeVoucherDetailsViewModel = new FeeVoucherDetailsViewModel();
            if (Id == null)
            {
                return NotFound();
            }
            FeeVoucherDetailsViewModel.gymTrainee = await _dbContext.Trainees.FirstOrDefaultAsync(t => t.TraineeId == Id);
            if (FeeVoucherDetailsViewModel == null)
            {
                return NotFound();
            }
            return View(FeeVoucherDetailsViewModel);

        }
        [HttpPost]
        public async Task<IActionResult> PayMonthlyFee(FeeVoucherDetailsViewModel monthlyFeeVoucherObj, int traineeId)
        {
            try
            {
                MonthlyFeeVoucher monthlyFeeVoucher = new MonthlyFeeVoucher
                {

                    FeeDate = monthlyFeeVoucherObj.monthlyFeeVoucher.FeeDate,
                    TraineeId = traineeId,
                    Remarks = monthlyFeeVoucherObj.monthlyFeeVoucher.Remarks,
                    Status = "Paid"
                };

                _dbContext.Add(monthlyFeeVoucher);
                await _dbContext.SaveChangesAsync();

                return RedirectToAction(nameof(Index), "FeeVoucher");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while processing your request.");
                Console.WriteLine(ex.Message);
                return View(monthlyFeeVoucherObj);
            }
        }






        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("TraineeId, FirstName, LastName,Age,Height, Weight,Gender, Address,TrainingLevelID,ImageFile")] GymTrainee traineeObj)
        {
            if (ModelState.IsValid)
            {
                if (traineeObj.TraineeId == 0)
                {


                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(traineeObj.ImageFile.FileName);
                    string extension = Path.GetExtension(traineeObj.ImageFile.FileName);
                    traineeObj.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await traineeObj.ImageFile.CopyToAsync(fileStream);
                    }
                    _dbContext.Add(traineeObj);
                }
                else
                {
                    _dbContext.Update(traineeObj);

                }
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {

                List<SelectListItem> ObjTrainingLevelList = new List<SelectListItem>();
                var trainingLevels = _dbContext.TrainingLevels.ToList();
                foreach (var temp in trainingLevels)
                {
                    ObjTrainingLevelList.Add(new SelectListItem() { Text = temp.TrainingLevelName, Value = temp.TrainingLevelID.ToString() });
                }
                ViewBag.DPL_TL = ObjTrainingLevelList;

            }
            return View(traineeObj);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _dbContext.Trainees.FindAsync(id);
            if (pet == null)
            {
                return NotFound();
            }
            return View(pet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PetId,PetName,PetOwnerName,PetAddress,ImageName,ImageFile")] GymTrainee pet)
        {
            if (id != pet.TraineeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dbContext.Update(pet);
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PetExists(pet.TraineeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pet);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _dbContext.Trainees
                .FirstOrDefaultAsync(m => m.TraineeId == id);
            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pet = await _dbContext.MonthlyFeeVouchers.FindAsync(id);
            _dbContext.MonthlyFeeVouchers.Remove(pet);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }




        private bool PetExists(int id)
        {
            return _dbContext.Trainees.Any(e => e.TraineeId == id);
        }
    }
}

