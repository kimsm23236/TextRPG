using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveInJobSeeker
{
    public class SelectEndingScene : Scene
    {
        private Game gameIns;
        private MGLS_WeeklyAction nextScene;
        public SelectEndingScene()
        { 

        }
        public virtual void Init()
        {
            gameIns = Game.Instance;
            nextScene= new MGLS_WeeklyAction();
            /* 
             * Do Nothing 
             * Virtual Method
             */
        }

        public override void Update()
        {
            gameIns.shiftscenehandle(nextScene);
        }
        public override void Render()
        {
            
        }
        public override void Destroy()
        {
            /* Do Nothing */
        }
    }
}
