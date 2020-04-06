using System;
using System.Collections.Generic;

namespace TimesheetRestApi.Models
{
    public partial class WorkAssignments
    {
        public WorkAssignments()
        {
            Timesheet = new HashSet<Timesheet>();
        }

        public int IdWorkAssignment { get; set; }
        public int? IdCustomer { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? Deadline { get; set; }
        public bool? InProgress { get; set; }
        public DateTime? WorkStartedAt { get; set; }
        public bool? Completed { get; set; }
        public DateTime? CompletedAt { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool? Active { get; set; }

        public virtual Customers IdCustomerNavigation { get; set; }
        public virtual ICollection<Timesheet> Timesheet { get; set; }
    }
}
