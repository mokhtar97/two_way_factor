using Authentication.Application.Common.Interfaces;
using Authentication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Common.Dtos
{
   public class SchoolDto
    {
        public int Id { get; set; }
        public string SchoolNumber { get; set; }
        public string SchoolName { get; set; }
        public int GovernorateId { get; set; }
    }
}
