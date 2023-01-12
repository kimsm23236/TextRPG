using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveInJobSeeker
{
    public struct UIPosition
    {
        public int X; 
        public int Y;
    }
    public struct UISize
    {
        public int Width; 
        public int Height;
    }

    public delegate void F_OnUIUpdatedHandle();
    public class UI
    {
        public StringBuilder renderSB;
        protected UIPosition _position;
        protected UISize _size;
        protected bool bIsThereBorder;

        // 업데이트 관련
        public F_OnUIUpdatedHandle onUIUpdatedhandle;
        protected bool bIsUpdated;

        // 유니티?
        protected string grid1 = "+";
        protected string grid2 = "-";
        protected string grid3 = "|";
        protected string blank = " ";
        public UIPosition position
        {
            get { return _position; }
        }
        public UISize size
        { 
            get { return _size; } 
        }
        public bool IsThereBorder
        {
            get { return bIsThereBorder; }
            set { bIsThereBorder = value; }
        }
        public UI() 
        {
            renderSB = new StringBuilder();
            bIsThereBorder = false;
        }

        public virtual void Init(int width, int height, int px, int py) // * 가로크기, 세로크기, x좌표, y좌표
        {
            UISize newSize = new UISize();
            newSize.Width = width;
            newSize.Height = height;
            UIPosition newPosition = new UIPosition();
            newPosition.X = px;
            newPosition.Y = py;
            _size = newSize;
            _position = newPosition;
        }
        public virtual void Update() 
        { 

        }

        public virtual void Render()
        {
            if (!bIsUpdated)
                return;
            if(IsThereBorder)
                RenderBorder();
        }
        public virtual void RenderBorder()
        {
            Console.SetCursorPosition(position.X, position.Y);
            for (int y = 0; y < size.Height; y++)
            {
                for(int x = 0; x < size.Width; x++)
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
    }
}
