using LinkDev.IKEA.DAL.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Models_DTOS_.Department
{
    public record DepartmentDetailsDTO
    (int Id,string Code, string Name,
     string? Description ,DateOnly CreationDate ,
     string? CreatedBy ,DateTime CreatedOn , 
     string? LastModifiedBy,DateTime LastModifiedOn
     );
}
