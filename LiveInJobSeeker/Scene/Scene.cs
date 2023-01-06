using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveInJobSeeker
{
    public class Scene
    {
        protected StringBuilder renderSB;

        public Scene() 
        {
            /* 
             * Do Nothing 
             * Virtual Method
             */
        }

        public virtual void Init()
        {
            /* 
             * Do Nothing 
             * Virtual Method
             */
        }

        public virtual void Update() 
        {
            
        }
        public virtual void Render()
        {
            Console.SetCursorPosition(0, 0);
            Console.Write(renderSB.ToString());
            renderSB.Clear();
        }
        public virtual void Destroy() 
        {
            /* Do Nothing */
        }


    }
}
