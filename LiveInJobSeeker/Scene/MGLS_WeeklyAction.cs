using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveInJobSeeker
{
    enum EWAMenu
    {
        NONE = 0,
        TRAINING, APPLYJOB, PARTTIMEJOB, REST
    }
    public class MGLS_WeeklyAction : Scene
    {
        // MGLS : Main Game Loop Scene

        private JobSeeker player;
        private Controller controller;

        private string[] menu;
        private int selectNumber;
        private EWAMenu selectMenu;
        
        private int week;
        public int Week
        {
            get { return week; }
            set { week = value; }
        }

        public MGLS_WeeklyAction()
        {
            renderSB = new StringBuilder();
            selectNumber = 0;
            menu = new string[4] {  "☞ 훈련\t\t   회사지원\t\t   아르바이트\t\t   휴식",
                                    "   훈련\t\t☞ 회사지원\t\t   아르바이트\t\t   휴식",
                                    "   훈련\t\t   회사지원\t\t☞ 아르바이트\t\t   휴식",
                                    "   훈련\t\t   회사지원\t\t   아르바이트\t\t☞ 휴식"};
        }
        public override void Init()
        {
            base.Init();
            // 플레이어 초기화
            Game gameIns = Game.Instance;
            player = gameIns.Player;

            // 컨트롤러 초기화
            controller = Controller.Instance;
            controller.leftarrowkeydownhandle = new F_LeftArrowKeyDownHandle(PressLeftArrowKey);
            controller.rightarrowkeydownhandle = new F_RightArrowKeyDownHandle(PressRightArrowKey);
        }
        public override void Update()
        {
            base.Update();
            // 프로토타입용 * 싹 바꿔야됨
            controller.KbHit();
            menuUpdate();

            renderSB.AppendLine($"{week}주차                 ");
            renderSB.AppendLine($"{week}주차에는 무엇을 할지 선택해주세요. 현재 체력 : {player.Status.hp}");
            renderSB.AppendLine(menu[selectNumber]);
        }

        private void menuUpdate()
        {
            switch (selectNumber)
            {
                case 0:
                    selectMenu = EWAMenu.TRAINING;
                    break;
                case 1:
                    selectMenu = EWAMenu.APPLYJOB;
                    break;
                case 2:
                    selectMenu = EWAMenu.PARTTIMEJOB;
                    break;
                case 3:
                    selectMenu = EWAMenu.REST;
                    break;
                default:
                    break;
            }
        }

        public override void Render()
        {
            base.Render();
        }

        public override void Destroy()
        {
            base.Destroy();
        }

        public void PressLeftArrowKey() // 상위클래스 정의후 오버라이드로 바꿀수도 있음
        {
            selectNumber = Math.Clamp(selectNumber - 1, 0, menu.Length - 1);
        }
        public void PressRightArrowKey()
        {
            selectNumber = Math.Clamp(selectNumber + 1, 0, menu.Length - 1);
        }

    }
}
