using Gym.Models;
using Gym.ViewModel;

public interface IGymTraineeService
{
    Task SaveTraineeInfo(GymTrainee gymTrainee);
    Task<GymTrainee> GetTraineeById(int id);
    Task<List<GymTraineeDetailViewModel>> GetAllTraineeDetails();
    Task UpdateTrainee(GymTrainee gymTrainee);
    Task<bool> TraineeExists(int id);
    Task DeleteTrainee(int id);
}
