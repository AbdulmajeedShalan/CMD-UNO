using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO
{
    public class Cards
    {
        public string CardTypes { get; set; }
        public string CardColors { get; set; }
        public Cards(string cardType, string cardColor)
        {
            CardTypes = cardType;
            CardColors = cardColor;



        }

    }


}
