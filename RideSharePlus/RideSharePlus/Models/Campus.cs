
using System.ComponentModel;

namespace RideSharePlus.Models
{
    public class Campus
    {
        [DisplayName("Campus")]
        public virtual int CampusId { get; set; }
        [DisplayName("Campus")]
        public virtual string Name { get; set; }
        public virtual string Address { get; set; }
        public virtual string City { get; set; }
    }
}