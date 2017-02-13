using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RideSharePlus.Models
{
    public class Ride
    {
        [DisplayName("Ride")]
        public virtual int RideId { get; set; }
        [DisplayName("Campus")]
        [Required(ErrorMessage = "Need to specify a campus")]
        public virtual int CampusId { get; set; }
        [DisplayName("Student Email")]
        [DataType(DataType.EmailAddress)]
        public virtual string StudentEmail { get; set; }
        [DisplayName("Starting Crossroads")]
        public virtual string StartingCrossroads { get; set; }
        [DisplayName("Starting Town")]
        [Required(ErrorMessage = "Need to specify a starting town")]
        public virtual string StartingTown { get; set; }
        [DisplayName("Day of Week")]
        [Required(ErrorMessage = "Need to specify a day of the week")]
        [RegularExpression(@"(Mon|Tues|Wednes|Thurs|Fri|Satur|Sun)day", 
            ErrorMessage="Need to specify a valid day of the week")]
        public virtual string DayOfWeek { get; set; }
        [DisplayName("Start Time")]
        public virtual TimeSpan TimeStart { get; set; }
        [DisplayName("End Time")]
        public virtual TimeSpan TimeEnd { get; set; }
        [DataType(DataType.MultilineText)]
        public virtual string Requirements { get; set; }
        public virtual Campus Campus { get; set; }
    }
}