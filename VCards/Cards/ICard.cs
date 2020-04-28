using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VCards.Cards
{
    public interface ICard<T> where T : ICardProperties
    {
        event RoutedEventHandler OnClosedCalled;
        
        T CardProperties { get; set; }
    }
}
