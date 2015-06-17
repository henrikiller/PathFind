using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PathFind
{
    class QuadradoManager
    {
        public QuadradoBranco[,] quadradoManager = new QuadradoBranco[10,5];
        int space = 100;

        public QuadradoManager()
        { 
            for(int i = 0; i <= 9; i++)
            {
                for (int j = 0; j <= 4; j++)
                {
                    quadradoManager[i, j] = new QuadradoBranco(new Point(1 + space * i, 1 + space * j), i, j);
                }
            }
        }

        public void Draw(Graphics e)
        {
            for (int i = 0; i <= 9; i++)
            {
                for (int j = 0; j <= 4; j++)
                {
                    quadradoManager[i, j].DrawImage(e);
                }
            }
        }

        public void Upadate()
        { 
            
        }
    }
}
