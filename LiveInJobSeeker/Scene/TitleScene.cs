using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveInJobSeeker
{
    enum EMenu
    {
        NONE,
        GAMESTART,
        EXIT
    }
    public class TitleScene : Scene
    {
        // 플레이어 선택 변수
        private int selectNumber;
        private EMenu selectMenu;
        // 타이틀 씬 메뉴 목록
        private string[] menu;
        
        private Controller controller;


        public TitleScene()
        {
            selectNumber= 0;
            renderSB = new StringBuilder();
            menu = new string[2] {"☞ 게임 시작     게임 종료", "   게임 시작  ☞ 게임 종료" }; 
        }

        public override void Init()
        {
            base.Init();
            // 컨트롤러 객체 가져와서
            controller = Controller.Instance;
            // 컨트롤러 키다운 핸들러 초기화
            controller.leftarrowkeydownhandle = new F_LeftArrowKeyDownHandle(PressLeftArrowKey);
            controller.rightarrowkeydownhandle = new F_RightArrowKeyDownHandle(PressRightArrowKey);
            controller.zkeydownhandle = new F_ZKeyDownHandle(PressZKey);

        }
        public override void Update()
        {
            base.Update();
            controller.KbHit();
            menuUpdate();
            
            renderSB.Append(menu[selectNumber]);
        }
        public override void Render()
        {
            base.Render();

        }
        private void menuUpdate()
        {
            switch(selectNumber)
            {
                case 0:
                    selectMenu = EMenu.GAMESTART;
                    break;
                case 1:
                    selectMenu = EMenu.EXIT;
                    break;
                default: 
                    break;
            }
        }
        public void PressLeftArrowKey()
        {
            selectNumber = Math.Clamp(selectNumber - 1, 0, menu.Length - 1);
        }
        public void PressRightArrowKey()
        {
            selectNumber = Math.Clamp(selectNumber + 1, 0, menu.Length - 1);
        }
        public void PressZKey()
        {
            Game gameIns = Game.Instance;
            switch (selectMenu)
            {
                case EMenu.GAMESTART:
                    JobSeekerCreateScene nextScene = new JobSeekerCreateScene();
                    gameIns.shiftscenehandle.Invoke(nextScene);
                    break;
                case EMenu.EXIT:
                    gameIns.Exit();
                    break;
                default:
                    break;
            }
        }

    }
}
