using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonManagerService.Domain.Models
{
    public class PersonSkill
    {
        public Guid PersonSkillId { get; set; }
        public string Name { get; set; }
        public Person Person { get; set; }
        public Guid PersonId { get; set; }
    }
}
