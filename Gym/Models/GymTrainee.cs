using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Gym.Models
{
    public class GymTrainee
    {

        [Key]
        public int TraineeId { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required]

        [DisplayName("ContactNo")]
        public string ContactNo { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        [DisplayName("Age")]
        public string Age { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        [DisplayName("Height")]

        public string Height { get; set; }

        [Required]
        [Column(TypeName = "int")]
        [DisplayName("Weight")]
        public int Weight { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        [DisplayName("Gender")]
        public string Gender { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        [DisplayName("Address")]
        public string Address { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Image Name")]
        public string ImageName { get; set; }
        public DateTime CreationDate { get; set; }

        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile ImageFile { get; set; }


     






        public int TrainingLevelID { get; set; }
        public virtual TrainingLevel TrainingLevel { get; set; }


        public int MonthlyFee { get; set; }
        private string _feepaid_status = "unknown";

        [NotMapped]
        public string Feepaid_Status
        {
            get
            {
                return _feepaid_status;
            }
            set
            {
                _feepaid_status = value;
            }
        }



    }

    public enum feefilter
    {
        Paid,
        UnPaid
    }
}




