using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PathFind
{
    class QuadradoBranco
    {
        public Image imgVazio, imgInicio, imgFim, imgMuro;
        public Point p;
        public bool inicio = false, fim = false, muro = false;
        public int colun, line;

        public QuadradoBranco(Point point, int i, int j)
        {
            p = point;
            line = i;
            colun = j;
            imgVazio = Image.FromFile(@"..\..\Images\Vazio.png");
            imgFim   = Image.FromFile(@"..\..\Images\Fim.png");
            imgMuro  = Image.FromFile(@"..\..\Images\Muro.png");
            imgInicio= Image.FromFile(@"..\..\Images\Inicio.png");
        }

        public void DrawImage(Graphics e)
        {
            if (inicio && !fim && !muro)
            {
                e.DrawImage(imgInicio, p);
            }
            if (fim && !inicio && !muro)
            {
                e.DrawImage(imgFim, p);
            }
            if (muro && !inicio && !fim)
            {
                e.DrawImage(imgMuro, p);
            }
            if (muro == false && fim == false && inicio == false)
            {
                e.DrawImage(imgVazio, p);
            }
        }
    }
}