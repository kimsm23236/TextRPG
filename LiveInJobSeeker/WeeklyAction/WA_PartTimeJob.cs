using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveInJobSeeker
{
    public enum EPARTTIMEJOB
    {
        NONE = 0,
        EASY, HARD,
    }
    public class WA_PartTimeJob : WeeklyAction
    {
        private List<string> descStr = new List<string>() {"아르바이트를 선택하세요." };
        private List<string> menu = new List<string>() {"쉬운 알바 (체력 감소 수치 및 돈 증가 수치 ↓)",
                                                        "힘든 알바 (체력 감소 수치 및 돈 증가 수치 ↑)"};
        private List<string> resultStr;
        private EPARTTIMEJOB selectedPTJ = EPARTTIMEJOB.NONE;

        private const int INCREASEVALUE_MONEY = 9620;
        private const int EASYMULTIPLIER = 3 * 5;
        private const int HARDMULTIPLIER = 8 * 5;

        public WA_PartTimeJob() 
        { 
            selectNumber = 0;
            resultStr = new List<string>();
        }
        public override void Init()
        {
            base.Init();
            controller.uparrowkeydownhandle = new F_UpArrowKeyDownHandle(PressUpArrowKey);
            controller.downArrowKeydownHandle = new F_DownArrowKeyDownHandle(PressDownArrowKey);
            controller.zkeydownhandle = new F_ZKeyDownHandle(PressZKey);
        }
        public override void ExecuteAction()
        {
            if (!RunOnlyOnce)
                return;
            RunOnlyOnce = false;
            base.ExecuteAction();
            PRC_Action();
            ToNextScene_a_Second(2000);
        }
        public override void PRC_Action()
        {
            base.PRC_Action();
            int increaseMoney = 0;
            int decreaseHp = 0;
            switch(selectedPTJ)
            {
                case EPARTTIMEJOB.NONE:
                    break;
                case EPARTTIMEJOB.EASY:
                    increaseMoney = INCREASEVALUE_MONEY * EASYMULTIPLIER;
                    break;
                case EPARTTIMEJOB.HARD:
                    increaseMoney = INCREASEVALUE_MONEY * HARDMULTIPLIER;
                    break;
                default: 
                    break;
            }
            player.IncreaseMoney(increaseMoney);
            resultStr.Add($"돈 {increaseMoney}원을 획득하였습니다.");
            player.DecreaseHP(decreaseHp);
            resultStr.Add($"체력 {decreaseHp}이 감소하였습니다.");
        }

        public override void Update()
        {
            base.Update();

            UpdateMenu();
            rendersb.AppendLine(descStr[0]);
            for (int i = 0; i < menu.Count; i++)
            {
                rendersb.Append(menu[i]);
                if (selectNumber == i)
                {
                    rendersb.Append('☜');
                }
                rendersb.AppendLine();
            }
            foreach(string res in resultStr)
            {
                rendersb.AppendLine(res);
            }
        }
        private void UpdateMenu()
        {
            switch(selectNumber)
            {
                case 0:
                    selectedPTJ = EPARTTIMEJOB.EASY;
                    break;
                case 1:
                    selectedPTJ = EPARTTIMEJOB.HARD;
                    break;
                default: 
                    break;
            }
        }
        public override void PressUpArrowKey()
        {
            base.PressUpArrowKey();
            selectNumber = Math.Clamp(selectNumber - 1, 0, 1);
        }
        public override void PressDownArrowKey()
        {
            base.PressDownArrowKey();
            selectNumber = Math.Clamp(selectNumber + 1, 0, 1);
        }
        public override void PressZKey()
        {
            base.PressZKey();
            ExecuteAction();
        }
    }
}
