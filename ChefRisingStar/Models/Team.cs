
using System.Collections.Generic;
using System.Diagnostics;

namespace ChefRisingStar.Models
{
    [DebuggerDisplay("{GetDebuggerDisplay}")]
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CaptainId { get; set; }
        public bool IsActive { get; set; }
        public List<int> Members { get; set; }

        public Team()
        {

        }

        public Team(int id, string name, int captainId)
        {
            Id = id;
            Name = name;
            CaptainId = captainId;
            IsActive = true;
        }

        public override string ToString()
        {
            return $"{Id} : {Name}";
        }

        private string GetDebuggerDisplay()
        {
            return $"{Id} : {Name}";
        }
    }
}
