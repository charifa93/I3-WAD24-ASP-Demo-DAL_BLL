using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class Cocktail
    {
        private Guid empty1;
        private DateOnly dateOnly;
        private Guid empty2;

        public Guid Cocktail_Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Instructions { get; set; }
        public DateOnly CreatedAt { get; set; }
        public Guid? CreatedBy { get; set; }

        public Cocktail(Guid cocktail_Id, string name, string? description, string instructions, DateTime createdAt, Guid? createdBy)
        {
            Cocktail_Id = cocktail_Id;
            Name = name;
            Description = description;
            Instructions = instructions;
            CreatedAt = createdAt;
            CreatedBy = createdBy;
        }

        public Cocktail(string name, string? description, string instructions, Guid? createdBy)
        {
            Name = name;
            Description = description;
            Instructions = instructions;
            //CreatedAt = createdAt;
            CreatedBy = createdBy;
        }

        public Cocktail(Guid empty1, string name, string? description, string instructions, DateOnly dateOnly, Guid empty2)
        {
            this.empty1 = empty1;
            Name = name;
            Description = description;
            Instructions = instructions;
            this.dateOnly = dateOnly;
            this.empty2 = empty2;
        }
    }
}
