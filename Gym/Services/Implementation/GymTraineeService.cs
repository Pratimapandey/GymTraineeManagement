using AutoMapper;
using Gym.Data;
using Gym.Models;
using Gym.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public class GymTraineeService : IGymTraineeService
{
    private readonly GymDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public GymTraineeService(GymDbContext dbContext, IMapper mapper, IWebHostEnvironment webHostEnvironment)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task SaveTraineeInfo(GymTrainee gymTrainee)
    {
        if (gymTrainee.ImageFile != null)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(gymTrainee.ImageFile.FileName);
            string extension = Path.GetExtension(gymTrainee.ImageFile.FileName);
            gymTrainee.ImageName = fileName + DateTime.Now.ToString("yymmssff") + extension;
            string path = Path.Combine(wwwRootPath, "Images", gymTrainee.ImageName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await gymTrainee.ImageFile.CopyToAsync(fileStream);
            }

            gymTrainee.CreationDate = DateTime.Now;
            _dbContext.Add(gymTrainee);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<GymTrainee> GetTraineeById(int id)
    {
        return await _dbContext.Trainees.FindAsync(id);
    }

    public async Task<List<GymTraineeDetailViewModel>> GetAllTraineeDetails()
    {
        var traineesList = await _dbContext.Trainees.Include(t => t.TrainingLevel).ToListAsync();

        return _mapper.Map<List<GymTraineeDetailViewModel>>(traineesList);
    }

    public async Task UpdateTrainee(GymTrainee gymTrainee)
    {
        _dbContext.Attach(gymTrainee).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> TraineeExists(int id)
    {
        return await _dbContext.Trainees.AnyAsync(t => t.TraineeId == id);
    }

    public async Task DeleteTrainee(int id)
    {
        var trainee = await _dbContext.Trainees.FindAsync(id);
        _dbContext.Trainees.Remove(trainee);
        await _dbContext.SaveChangesAsync();
    }
}
