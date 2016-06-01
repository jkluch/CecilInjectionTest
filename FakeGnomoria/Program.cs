using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeGnomoria
{
    class Program
    {
        static void Main(string[] args)
        {
            FakeObject fake = new FakeObject(42);
            fake.PasteNumbers();
            fake.PasteLetters();
            Console.ReadKey();
        }
    }
}
