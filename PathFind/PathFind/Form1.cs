using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace PathFind
{
    public partial class Form1 : Form
    {
        QuadradoManager q;
        
        bool selecionandoInicio = false;
        bool selecionandoFim = false;
        bool fimselecionado;
        bool selecionandoMuro = false;

        int xB, yB;

        List<QuadradoBranco> posicoes = new List<QuadradoBranco>();

        public Form1()
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            InitializeComponent();
            CenterToScreen();

            q = new QuadradoManager();

            Timer timer = new Timer();
            timer.Interval = 1;
            timer.Start();
            timer.Tick += new EventHandler(this.Update);

            KeyUp += new KeyEventHandler(this.GetKeyUp);

            MouseDown += new MouseEventHandler(this.GetMouseDown);

            Paint += new PaintEventHandler(this.onDraw);
        }

        void onDraw(object sender, PaintEventArgs PaintNow)
        {
            q.Draw(PaintNow.Graphics);
            this.Invalidate();
        }

        void Update(object sender, EventArgs e)
        {
            
        }

        void GetKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                selecionandoInicio = true;
            }
            if (e.KeyCode == Keys.B)
            {
                selecionandoFim = true;
            }
            if (e.KeyCode == Keys.M)
            {
                selecionandoMuro = true;
            }



            if (e.KeyCode == Keys.Space)
            {
                CalcularAdjacentes(posicoes[posicoes.Count-1].line,posicoes[posicoes.Count-1].colun);
            }

            if (e.KeyCode == Keys.M)
            {
                Console.WriteLine(posicoes[0].colun + "-----" + posicoes[0].line);
            }

        }

        void GetMouseDown(object sender, MouseEventArgs e)
        {
            #region Selecionando o Inicio
            if (selecionandoInicio == true)
            {
                selecionandoInicio = false;                

                 for (int i = 0; i < 10; i++)
                 {
                     for (int j = 0; j < 5; j++)
                     {
                         if (e.X > q.quadradoManager[i, j].p.X && e.X < q.quadradoManager[i, j].p.X + 90 &&
                            e.Y > q.quadradoManager[i, j].p.Y && e.Y < q.quadradoManager[i, j].p.Y + 90)
                         {
                             for (int a = 0; a < 10; a++)
                             {
                                 for (int b = 0; b < 5; b++)
                                 {
                                     q.quadradoManager[a, b].inicio = false;
                                     q.quadradoManager[i, j].inicio = true;

                                     posicoes.Add(q.quadradoManager[i,j]);
                                 }
                             }
                         }
                     }
                 }

            }
            #endregion

            #region Selecionando o Fim
            if (selecionandoFim == true)
            {
                selecionandoFim = false;

                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (e.X > q.quadradoManager[i, j].p.X && e.X < q.quadradoManager[i, j].p.X + 90 &&
                           e.Y > q.quadradoManager[i, j].p.Y && e.Y < q.quadradoManager[i, j].p.Y + 90)
                        {
                            for (int a = 0; a < 10; a++)
                            {
                                for (int b = 0; b < 5; b++)
                                {
                                    q.quadradoManager[a, b].fim = false;
                                    q.quadradoManager[i, j].fim = true;

                                    xB = i;
                                    yB = j;

                                    fimselecionado = true;
                                }
                            }
                        }
                    }
                }

            }
            #endregion

            #region Selecionando o Muro
            if (selecionandoMuro == true)
            {
                selecionandoMuro = false;

                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (e.X > q.quadradoManager[i, j].p.X && e.X < q.quadradoManager[i, j].p.X + 90 &&
                           e.Y > q.quadradoManager[i, j].p.Y && e.Y < q.quadradoManager[i, j].p.Y + 90)
                        {
                            q.quadradoManager[i, j].muro = true;
                        }
                    }
                }

            }
            #endregion
        }

        int CalculeCusto(int x, int y)
        {
            int custo = 0;

            if (fimselecionado == true)
            {
                int a = Math.Abs(x - xB);
                int b = Math.Abs(y - yB);

                custo = (a + b) * 10;
            }

            return custo;
        }

        void CalcularAdjacentes(int x,int y)
        {
            int[] bsort = new int[8];

            bsort[0] = CalculeCusto(x,y-1);
            bsort[1] = CalculeCusto(x,y+1);
            bsort[2] = CalculeCusto(x-1,y);
            bsort[3] = CalculeCusto(x+1,y);

            bsort[4] = CalculeCusto(x - 1, y - 1);
            bsort[5] = CalculeCusto(x+1,y+1);
            bsort[6] = CalculeCusto(x - 1, y + 1);
            bsort[7] = CalculeCusto(x + 1, y - 1);

            int index = bsort[0];
            for (int i = 0; i < 7; i++ )
            {
                if (index > bsort[i])
                {
                    index = bsort[i];
                }
            }

            #region Horizontal e Vertical
            if (bsort[0] == index)
            {
                posicoes[posicoes.Count-1].inicio = false;
                posicoes.Add(q.quadradoManager[x,y-1]);
                posicoes[posicoes.Count - 1].inicio = true;
            }
            else if(bsort[1] == index)
            {
                posicoes[posicoes.Count - 1].inicio = false;
                posicoes.Add(q.quadradoManager[x, y + 1]);
                posicoes[posicoes.Count - 1].inicio = true;
            }
            else if(bsort[2] == index)
            {
                posicoes[posicoes.Count - 1].inicio = false;
                posicoes.Add(q.quadradoManager[x - 1, y]);
                posicoes[posicoes.Count - 1].inicio = true;
            }
            else if(bsort[3] == index)
            {
                posicoes[posicoes.Count - 1].inicio = false;
                posicoes.Add(q.quadradoManager[x + 1, y]);
                posicoes[posicoes.Count - 1].inicio = true;
            }
            #endregion

            #region Diagonal
            else if(bsort[4] == index)
            {
                posicoes[posicoes.Count - 1].inicio = false;
                posicoes.Add(q.quadradoManager[x - 1, y - 1]);
                posicoes[posicoes.Count - 1].inicio = true;
            }
            else if(bsort[5] == index)
            {
                posicoes[posicoes.Count - 1].inicio = false;
                posicoes.Add(q.quadradoManager[x + 1, y + 1]);
                posicoes[posicoes.Count - 1].inicio = true;
            }
            else if(bsort[6] == index)
            {
                posicoes[posicoes.Count - 1].inicio = false;
                posicoes.Add(q.quadradoManager[x - 1, y + 1]);
                posicoes[posicoes.Count - 1].inicio = true;
            }
            else if(bsort[7] == index)
            {
                posicoes[posicoes.Count - 1].inicio = false;
                posicoes.Add(q.quadradoManager[x + 1, y - 1]);
                posicoes[posicoes.Count - 1].inicio = true;
            }
            #endregion

        }
    }
}
