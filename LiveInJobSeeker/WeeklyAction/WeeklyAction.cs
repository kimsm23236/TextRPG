using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveInJobSeeker
{
    public delegate void F_ActionEndHandle();
    /*
     * WeeklyAction Class
     * 주간 행동 클래스
     * 한 턴(주)에 플레이어가 선택할 수 있는 행동 클래스의 상위 클래스
     * 4개의 행동 클래스가 상속 받으며 각각의 행동을 실행할 수 있음
     */
    public class WeeklyAction
    {
        protected int selectNumber;
        protected Controller controller;
        protected JobSeeker player;
        protected StringBuilder rendersb;
        protected int menuLevel;

        protected bool RunOnlyOnce;
        protected bool isEndAllAction;
        protected F_ActionEndHandle actionEndHandle;
        public WeeklyAction()
        { 
            player = new JobSeeker();
            isEndAllAction= false;
        }  
        public virtual void Init()
        {
            Game gameIns = Game.Instance;
            player = gameIns.Player;
            controller = Controller.Instance;
            controller.InitDelegate();
            isEndAllAction = false;
            RunOnlyOnce = true;
            selectNumber = 0;
        }
        public virtual void SetRenderStringBuilder(StringBuilder newSB)
        {
            rendersb = newSB;
        }
        public virtual void ExecuteAction()
        {
            /* Do Nothing */
        }
        public virtual void PRC_Action()
        {
            Console.WriteLine("PRC Action Checking");
            /* Do Nothing */
        }
        public virtual void Update()
        {
            controller.KbHit();
            if(isEndAllAction)
            {
                // 다음 씬으로
                SelectEndingScene nextScene = new SelectEndingScene();
                Game gameIns = Game.Instance;
                gameIns.shiftscenehandle.Invoke(nextScene);
            }
        }

        protected virtual async void ToNextScene_a_Second(int sec)
        {
            await Task.Delay(sec);
            ToNextScene();
        }
        protected virtual void ToNextScene()
        {
            Game gameIns = Game.Instance;
            MGLS_WeeklyAction nextScene = new MGLS_WeeklyAction();
            gameIns.shiftscenehandle(nextScene);
        }
        protected virtual async void ToNextPrc_a_Second(int sec)
        {
            await Task.Delay(sec);
            // next process
        }

        public virtual void PressLeftArrowKey()
        {
            /* Do Nothing */
        }
        public virtual void PressRightArrowKey()
        {
            /* Do Nothing */
        }
        public virtual void PressUpArrowKey()
        {
            /* Do Nothing */
        }
        public virtual void PressDownArrowKey()
        {
            /* Do Nothing */
        }
        public virtual void PressZKey() 
        {
            /* Do Nothing */
        }
        public virtual void PressXKey()
        {
            /* Do Nothing */
        }
    }
}
