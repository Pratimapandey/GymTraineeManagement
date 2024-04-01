using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gym.Models
{
    public class MonthlyFeeVoucher
    {
        [Key]
        public int MonthlyFeeVoucherID { get; set; }
        [DataType(DataType.Date)]
        public DateTime FeeDate { get; set; } = DateTime.Now;
        public string Remarks { get; set; }
        public string Status { get; set; }
        [ForeignKey("GymTrainee")]
        public int TraineeId { get; set; }
   
        public GymTrainee GymTrainee { get; set; }
    }
}
