using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveInJobSeeker
{
    public class AAWindow : UI
    {
        public string curAA;
        public bool isRenderAA;
        public int tsx;
        public int tsy;
        public void SetTextPosition(int x, int y)
        {
            tsx = x;
            tsy = y;
        }

        public AAWindow() 
        { 
            curAA = string.Empty;
            IsThereBorder = true;
            bIsUpdated = true;
            renderSB = new StringBuilder();
        }

        public override void Init(int width, int height, int px, int py) // * 가로크기, 세로크기, x좌표, y좌표
        {
            base.Init(width, height, px, py);
            onUIUpdatedhandle = () => { bIsUpdated = true; };
            tsx = px + 1;
            tsy = py + 1;

        }

        public virtual void Update()
        {
            // renderSB.Append(curAA);
        }

        public virtual void Render()
        { 
            if (!bIsUpdated)
                return;

            base.Render();

            Console.SetCursorPosition(tsx, tsy);
            
            for(int i = 0; i < curAA.Length; i++)
            {
                if (curAA[i] == '\n')
                    Console.SetCursorPosition(tsx, Console.GetCursorPosition().Top + 1);
                else
                    Console.Write(curAA[i]);
            }

            bIsUpdated = false;
        }
        public virtual void RenderBorder()
        {
            Console.SetCursorPosition(position.X, position.Y);
            for (int y = 0; y < size.Height; y++)
            {
                for (int x = 0; x < size.Width; x++)
                {
                    if ((y == 0 && x == 0) || (y == size.Height - 1 && x == size.Width - 1) ||
                        (y == 0 && x == size.Width - 1) || (y == size.Height - 1 && x == 0)) // * 모서리 부분
                        Console.Write(grid1);
                    else if (x == 0 || x == size.Width - 1)
                        Console.Write(grid3);
                    else if (y == 0 || y == size.Height - 1)
                        Console.Write(grid2);
                    else
                        Console.Write(blank);
                }
            }
        }
        public void SetAA(string str)
        {
            curAA = str;
            isRenderAA = true;
            onUIUpdatedhandle();
        }
    }
}
