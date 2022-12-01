
using System;
using System.Diagnostics;

namespace ChefRisingStar.Models
{
    [DebuggerDisplay("{GetDebuggerDisplay}")]
    public class Language : IComparable
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public Language()
        {

        }
        public Language(string code, string name)
        {
            Code = code ?? throw new ArgumentNullException(nameof(code));
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public override string ToString()
        {
            return $"{Code} - {Name}";
        }

        public int CompareTo(object obj)
        {
            Language item = obj as Language;

            if (item == null)
                return -1;

            return item.Code.CompareTo(Code);
        }

        private string GetDebuggerDisplay()
        {
            return $"{Code} - {Name}";
        }
    }
}
