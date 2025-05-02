using LinkDev.IKEA.DAL.Common;
using LinkDev.IKEA.DAL.Common.Entities;
using LinkDev.IKEA.DAL.Entities.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Entities.Departments
{
    public class Department:BaseAuditableEntity<int>
    {
        public required string Name { get; set; }
        public required string Code { get; set; }
        public string? Description  { get; set; }
        //[JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly CreationDate{ get; set; }
        public  ICollection<Employee> Employees { get; set; }=new HashSet<Employee>();
        public  int? ManagerId   { get; set; }
        public  Employee? Manager { get; set; }
    }
}
