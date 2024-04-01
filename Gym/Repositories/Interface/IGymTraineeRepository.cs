using Gym.Models;

public interface IGymTraineeRepository
{
    Task AddTrainee(GymTrainee gymTrainee);
    Task<GymTrainee> GetTraineeById(int id);
    Task<List<GymTrainee>> GetAllTrainees();
    Task UpdateTrainee(GymTrainee gymTrainee);
    Task<bool> TraineeExists(int id);
    Task DeleteTrainee(int id);
}
