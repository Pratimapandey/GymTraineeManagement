using Gym.Data;
using Gym.Models;
using Gym.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GymMang.Controllers
{
    public class GymTraineeController : Controller
    {
        private readonly IGymTraineeService _gymTraineeService;

        public GymTraineeController(IGymTraineeService gymTraineeService)
        {
            _gymTraineeService = gymTraineeService;
        }

        public async Task<IActionResult> SaveTraineeInfoAsync(int Id)
        {
            if (Id == 0)
            {
           
                return View(new GymTrainee());
            }
            else
            {
                
                var gymTrainee = await _gymTraineeService.GetTraineeById(Id);
                return View(gymTrainee);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveTraineeInfo(GymTrainee gymTrainee)
        {
           
            await _gymTraineeService.SaveTraineeInfo(gymTrainee);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gymTrainee = await _gymTraineeService.GetTraineeById(id);
            if (gymTrainee == null)
            {
                return NotFound();
            }

            return View("SaveTraineeInfo", gymTrainee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GymTrainee gymTrainee)
        {
            if (id != gymTrainee.TraineeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   
                    if (!await _gymTraineeService.TraineeExists(gymTrainee.TraineeId))
                    {
                        return NotFound();
                    }

                    await _gymTraineeService.UpdateTrainee(gymTrainee);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _gymTraineeService.TraineeExists(gymTrainee.TraineeId))
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
            return View("SaveTraineeInfo", gymTrainee);
        }


        public async Task<IActionResult> Index()
        {
            var traineeDetailsList = await _gymTraineeService.GetAllTraineeDetails();
            return View(traineeDetailsList);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int Id)
        {
            await _gymTraineeService.DeleteTrainee(Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
