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

        // string
        protected List<string> descStr = new List<string>();
        protected List<string> menu = new List<string>();
        protected List<string> resultStr = new List<string>();

        protected StringBuilder descSB= new StringBuilder();
        protected StringBuilder menuSB = new StringBuilder();
        protected StringBuilder resultSB = new StringBuilder();

        protected bool RunOnlyOnce;
        protected bool isEndAllAction;
        protected F_ActionEndHandle actionEndHandle;
        public TextMenuUI TextBar;

        public WeeklyAction()
        { 
            player = new JobSeeker();
            TextBar = new TextMenuUI();
            isEndAllAction = false;
        }  
        public virtual void Init()
        {
            Game gameIns = Game.Instance;
            player = gameIns.Player;
            controller = Controller.Instance;
            controller.InitDelegate();

            //
            TextBar.Init(160, 10, 0, 40, EOutputType.SEQ_LETTER);
            TextBar.IsThereBorder = true;
            //

            isEndAllAction = false;
            RunOnlyOnce = true;
            selectNumber = 0;
        }
        public virtual void SetTextBar(TextMenuUI ui)
        {
            this.TextBar = ui;
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
            player.IncreaseTurn();
            gameIns.shiftscenehandle(nextScene);
            Console.Clear();
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
