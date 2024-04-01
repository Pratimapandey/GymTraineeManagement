using Gym.Models;

namespace Gym.ViewModel
{
    public class FeeVoucherDetailsViewModel
    {
        public int TraineeId { get; set; }
        public GymTrainee gymTrainee { get; set; }
        public MonthlyFeeVoucher monthlyFeeVoucher { get; set; }


        public IEnumerable<GymTrainee> list_GymTrainee { get; set; }

        public IEnumerable<MonthlyFeeVoucher> list_MonthlyFeeVoucher { get; set; }
    }
}
