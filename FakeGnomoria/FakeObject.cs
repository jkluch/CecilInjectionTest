using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeGnomoria
{
    public class FakeObject
    {
        private int _num;
        public int Num
        {
            get
            {
                return _num;
            }

            set
            {
                _num = value;
            }
        }

        public FakeObject(int number)
        {
            Num = number;
        }

        public void PasteNumbers()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.Out.Write(i);
            }
            Console.Out.WriteLine("");
            //getNumber(this);
        }
        public void PasteLetters()
        {
            for (int i = 97; i < 107; i++)
            {
                Console.Out.Write((char)i);
            }
            Console.Out.WriteLine("");
        }
//        public void getNumber(FakeObject fake)
//        {
//            Console.Out.WriteLine(fake.Num);
//        }
    }
}
