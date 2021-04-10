using Common.DBTableModels;
using System;
using System.Collections.Generic;

namespace PromotionsSG.Presentation.WebPortal.Models
{
    public class CustomerProfileViewModel
    {
        public class Customer
        {
            public int customerProfileID { get; set; }
            public string customerFullName { get; set; }
            public string customerAddress { get; set; }
            public string customerEmail { get; set; }
            public string customerPhone { get; set; }
            public string customerType { get; set; }
            public string customerGender { get; set; }
            public bool customerActive { get; set; } //[CustomerActive] BIT NULL,
            public DateTime? customerDOB { get; set; }
            public string CreatedBy { get; set; }  //    [CreatedBy] VARCHAR(50) NULL,
            public DateTime? CreatedTime { get; set; }
            public string LastUpdatedBy { get; set; }
            public DateTime? LastUpdatedTime { get; set; }
            public int VersionNo { get; set; }
            public bool IsDeleted { get; set; }
        }

        public CustomerProfiles customerDto { get; set; }
        public int customerProfileID { get; set; }
        public string customerFullName { get; set; }
        public string customerAddress { get; set; }
        public string customerEmail { get; set; }
        public string customerPhone { get; set; }
        public string customerType { get; set; }
        public string customerGender { get; set; }
        public bool customerActive { get; set; } //[CustomerActive] BIT NULL,
        public DateTime? customerDOB { get; set; }
        public string CreatedBy { get; set; }  //    [CreatedBy] VARCHAR(50) NULL,
        public DateTime? CreatedTime { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedTime { get; set; }
        public int VersionNo { get; set; }
        public bool IsDeleted { get; set; }

        public List<Customer> allCustomers { get; set; }

    }
}
