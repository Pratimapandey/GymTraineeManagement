using AutoMapper;
using Gym.Models;
using Gym.ViewModel;

namespace Gym.Automapper
{
    public class ApplicationMapper:Profile
    {
        public ApplicationMapper()
        {
            CreateMap<GymTrainee, GymTraineeDetailViewModel>()
             .ForMember(dest => dest.gymTrainee, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.trainingLevel, opt => opt.MapFrom(src => src.TrainingLevel));

        }
    }
}
