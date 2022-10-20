using Authentication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authenticaion.Domain.Entities
{
    public class Product:BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
