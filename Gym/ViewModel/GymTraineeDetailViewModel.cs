using Gym.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gym.ViewModel
{
    public class GymTraineeDetailViewModel
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("gymTrainee")]
        public int TraineeId { get; set; }

        [ForeignKey("trainingLevel")]
        public int TrainingLevelID { get; set; }

        [ForeignKey("monthlyFeeVoucher")]
        public int MonthlyFeeVoucherID { get; set; }

        public GymTrainee gymTrainee { get; set; }
        public TrainingLevel trainingLevel { get; set; }
        public TrainingLevel TrainingLevel { get; set; }

        public MonthlyFeeVoucher monthlyFeeVoucher { get; set; }
        public string PageTitle { get; set; }
        public string PageHeader { get; set; }
    }
}
