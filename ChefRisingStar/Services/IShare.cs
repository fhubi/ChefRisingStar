using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChefRisingStar.Services
{
    public interface IShare
    {
        Task Share(string text, string imagePath, string title, ShareType shareType);
    }

    public enum ShareType
    {
        Resource,
        Web,
        Storage
    }
}
