using System;
using System.Collections.Generic;

namespace TimesheetRestApi.Models
{
    public partial class Customers
    {
        public Customers()
        {
            Timesheet = new HashSet<Timesheet>();
            WorkAssignments = new HashSet<WorkAssignments>();
        }

        public int IdCustomer { get; set; }
        public string CustomerName { get; set; }
        public string ContactName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<Timesheet> Timesheet { get; set; }
        public virtual ICollection<WorkAssignments> WorkAssignments { get; set; }
    }
}
