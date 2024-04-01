using Gym.Data;
using Gym.Models;
using Microsoft.EntityFrameworkCore;

public class GymTraineeRepository : IGymTraineeRepository
{
    private readonly GymDbContext _dbContext;

    public GymTraineeRepository(GymDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddTrainee(GymTrainee gymTrainee)
    {
        await _dbContext.Trainees.AddAsync(gymTrainee);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<GymTrainee> GetTraineeById(int id)
    {
        return await _dbContext.Trainees.FindAsync(id);
    }

    public async Task<List<GymTrainee>> GetAllTrainees()
    {
        return await _dbContext.Trainees.ToListAsync();
    }

    public async Task UpdateTrainee(GymTrainee gymTrainee)
    {
        _dbContext.Trainees.Update(gymTrainee);
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
