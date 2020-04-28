using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCards.Cards
{
    public interface ICardProperties
    {
        double Left { get; set; }
        double Top { get; set; }
        double Width { get; set; }
        double Height { get; set; }
    }
}
