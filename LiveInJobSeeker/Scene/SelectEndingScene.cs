using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveInJobSeeker
{
    public class SelectEndingScene : Scene
    {
        private JobSeeker player;
        private Controller controller;

        private string[] menu;
        private int selectNumber;

        private bool isSelected;

        private ScrollMenu ScrollBar;

        private Game gameIns;
        private MGLS_WeeklyAction nextScene;
        public SelectEndingScene()
        { 
            renderSB = new StringBuilder();
            selectNumber = 0;
            isSelected = false;
            ScrollBar = new ScrollMenu();
        }
        public override void Init()
        {
            base.Init();
            gameIns = Game.Instance;
            player = gameIns.Player;
            renderSB.AppendLine("최종 합격한 회사가 있으시다면 입사하여 취준생 생활을 끝마칠 수 있습니다.\n 선택 : z, 계속 취준생 생활을 이어나가고 싶으시다면 : x");
            ScrollBar.SetSB(renderSB.ToString());
            ScrollBar.SetDesc(renderSB.ToString());
            if(player.WinningList.Count >= 1)
            {
                foreach(var item in player.WinningList) 
                {
                    ScrollBar.vtc_menu.Add(item);
                }
            }
            ScrollBar.Init(102, 10, 0, 40, EOutputType.SEQ_LETTER);
            ScrollBar.IsThereBorder = true;

            //
            controller = Controller.Instance;
            controller.InitDelegate();
            controller.uparrowkeydownhandle = new F_UpArrowKeyDownHandle(PressUpArrowKey);
            controller.downArrowKeydownHandle = new F_DownArrowKeyDownHandle(PressDownArrowKey);
            controller.zkeydownhandle = new F_ZKeyDownHandle(PressZKey);
            controller.xkeydownhandle = new F_XKeyDownHandle(PressXKey);
            //
            AAwindow.SetAA(AAData.Instance.AA_Training);
            
            /* 
             * Do Nothing 
             * Virtual Method
             */
        }

        public override void Update()
        {
            base.Update();
            AAwindow.Update();
            if(!isSelected)
            {
                controller.KbHit();
            }
        }
        public override void Render()
        {
            base.Render();
            AAwindow.Render();
            ScrollBar.Render();
        }
        public override void Destroy()
        {
            base.Destroy();
        }

        public void PressUpArrowKey()
        {
            selectNumber = Math.Clamp(selectNumber - 1, 0, menu.Length - 1);
            ScrollBar.onUIUpdatedhandle();
        }
        public void PressDownArrowKey()
        {
            selectNumber = Math.Clamp(selectNumber + 1, 0, menu.Length - 1);
            ScrollBar.onUIUpdatedhandle();
        }
        public void PressZKey()
        {
            if(player.WinningList.Count >= 1)
            {
                string str = $"당신은 {player.WinningList[selectNumber]}에 최종 합격하여 취준생을 졸업하였습니다.";
                ScrollBar.SetRes(str);
                ScrollBar.switchDescRes();

                ToNextScene();
            }
        }
        public void PressXKey()
        {
            nextScene = new MGLS_WeeklyAction();
            player.IncreaseTurn();
            gameIns.shiftscenehandle(nextScene);

        }

        public async void ToNextScene()
        {
            await Task.Delay(2000);
            TitleScene nextscene = new TitleScene();
            gameIns.shiftscenehandle(nextscene);
        }

    }
}
