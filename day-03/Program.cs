using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day_03
{
  class Program
  {
    static void Main(string[] args)
    {
      int input = 289326;
      //for (input = 1; input <= 1024; input++)
      //{

        for (int lengthOfSide = 1; lengthOfSide < 1000; lengthOfSide += 2)
        {
          if (lengthOfSide * lengthOfSide >= input)
          {
            int smallerSide = lengthOfSide - 1;
            int insideCorner = (lengthOfSide - 2) * (lengthOfSide - 2);

            int x = 0;
            int y = 0;

            if (input <= insideCorner + smallerSide)
            {
              x = smallerSide / 2;
              y = -smallerSide / 2 + (input - insideCorner);
            }
            else if (input <= insideCorner + 2 * smallerSide)
            {
              x = smallerSide / 2 - (input - insideCorner - smallerSide);
              y = smallerSide / 2;
            } else if (input <= insideCorner + 3 * smallerSide)
            {
              x = -smallerSide / 2;
              y = smallerSide / 2 - (input - insideCorner - 2 * smallerSide);
            } else
            {
              x = smallerSide / 2 - (input - insideCorner - 3 * smallerSide);
              y = smallerSide / 2;
            }

            Console.WriteLine("{3} ({0}, {1}) -> distance {2}", x, y, Math.Abs(x) + Math.Abs(y), input);
            break;
          }
//        }
      }
    }
  }
}
