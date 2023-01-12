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
        private bool isSelected;

        private WeeklyAction selectedWA;

        private TextMenuUI TextBar;
        
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
            isSelected = false;
            selectedWA = new WeeklyAction();
            TextBar = new TextMenuUI();

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
            week = player.Turn;

            // UI 초기화
            renderSB.AppendLine($"{week}주차 ");
            renderSB.AppendLine($"{week}주차에는 무엇을 할지 선택해주세요. 현재 체력 : {player.Status.hp} ");
            TextBar.SetSB(renderSB.ToString());
            TextBar.SetHRZMenu(menu);
            TextBar.Init(160, 10, 0, 40, EOutputType.DEFAULT, renderSB.ToString());
            TextBar.IsThereBorder = true;
            // 컨트롤러 초기화
            controller = Controller.Instance;
            controller.InitDelegate();
            controller.leftarrowkeydownhandle = new F_LeftArrowKeyDownHandle(PressLeftArrowKey);
            controller.rightarrowkeydownhandle = new F_RightArrowKeyDownHandle(PressRightArrowKey);
            controller.zkeydownhandle = new F_ZKeyDownHandle(PressZKey);
        }
        public override void Update()
        {
            base.Update();
            // 프로토타입용 * 싹 바꿔야됨
            if(!isSelected)
            {
                controller.KbHit();
                menuUpdate();
            }
            else
            {
                selectedWA.Update();
                selectedWA.TextBar.Update();
            }
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
            TextBar.SelectMenu = selectNumber;
        }

        public override void Render()
        {
            base.Render();
            if(!isSelected)
            {
                TextBar.Render();
            }
            else
            {
                selectedWA.TextBar.Render();
            }
        }

        public override void Destroy()
        {
            base.Destroy();
        }

        public void PressLeftArrowKey() // 상위클래스 정의후 오버라이드로 바꿀수도 있음
        {
            selectNumber = Math.Clamp(selectNumber - 1, 0, menu.Length - 1);
            TextBar.onUIUpdatedhandle();
        }
        public void PressRightArrowKey()
        {
            selectNumber = Math.Clamp(selectNumber + 1, 0, menu.Length - 1);
            TextBar.onUIUpdatedhandle();
        }
        public void PressZKey()
        {
            SelectWA();
        }
        private void SelectWA()
        {
            WeeklyAction selectWeeklyAction;
            switch (selectMenu)
            {
                case EWAMenu.TRAINING:
                    selectWeeklyAction = new WA_Training();
                    break;
                case EWAMenu.APPLYJOB:
                    selectWeeklyAction = new WA_ApplyJob();
                    break;
                case EWAMenu.PARTTIMEJOB:
                    selectWeeklyAction = new WA_PartTimeJob();
                    break;
                case EWAMenu.REST:
                    selectWeeklyAction = new WA_Rest();
                    break;
                default:
                    selectWeeklyAction = new WeeklyAction();
                    break;
            }
            selectWeeklyAction.Init();
            selectWeeklyAction.SetRenderStringBuilder(renderSB);
            selectedWA = selectWeeklyAction;
            isSelected = true;
        }
    }
}
