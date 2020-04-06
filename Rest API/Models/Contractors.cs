using System;
using System.Collections.Generic;

namespace TimesheetRestApi.Models
{
    public partial class Contractors
    {
        public Contractors()
        {
            Employees = new HashSet<Employees>();
            Timesheet = new HashSet<Timesheet>();
        }

        public int IdContractor { get; set; }
        public string CompanyName { get; set; }
        public string ContactPerson { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string VatId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<Employees> Employees { get; set; }
        public virtual ICollection<Timesheet> Timesheet { get; set; }
    }
}
