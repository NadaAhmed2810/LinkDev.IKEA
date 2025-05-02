using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Common.Entities
{
    public class BaseAuditableEntity<TKey> : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public /*required*/ string? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? LastModifiedBy { get; set; }
        public /*required*/DateTime LastModifiedOn { get; set; }
    }
}
