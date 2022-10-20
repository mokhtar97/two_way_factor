using System;

namespace Authentication.Domain.Entities
{
    public abstract class BaseEntity 
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedDate  { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedDate { get; set; }
    }
}