using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LiveInJobSeeker
{
    public class Scene
    {
        protected StringBuilder renderSB;
        public  AAWindow AAwindow;
        public Scene() 
        {
            /* 
             * Do Nothing 
             * Virtual Method
             */
        }

        public virtual void Init()
        {
            AAwindow = new AAWindow();
            AAwindow.Init(102, 41, 0, 0);
            AAwindow.IsThereBorder = false;
        }

        public virtual void Update() 
        {
            
        }
        public virtual void Render()
        {
            // Console.SetCursorPosition(0, 0);
            Thread.Sleep(50);
            // renderSB.Clear();
        }
        public virtual void Destroy() 
        {
            /* Do Nothing */
        }


    }
}
