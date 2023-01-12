using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveInJobSeeker
{
    public enum ETraining
    {
        NONE = 0,
        SPEC, CODING, INTERVIEW,
        ALGO_BF,
        ALGO_DP,
        ALGO_BDFS,
        ALGO_DIJK,
        ALGO_DIVC
    }
    public class WA_Training : WeeklyAction
    {
        private ETraining selectedTraining;

        //private List<string> descStr = new List<string>() { "훈련을 선택하세요.", "공부할 알고리즘을 선택하세요." };
        //private List<string> resultStr = new List<string>();
        private List<string> menu2;// = new List<List<string>>()
        //{
        //    new List<string>            {
        //                                "자기 계발\t",
        //                                "코딩 연습\t",
        //                                "알고리즘 공부\t",
        //                                "면접 대비\t"
        //                                },
        //    new List<string>            {
        //                                "완전탐색   ",
        //                                "DP        ",
        //                                "BFS, DFS  ",
        //                                "다익스트라 ",
        //                                "분할정복   ",
        //                                }
        //};

        private bool isAllSelected;
        
        public WA_Training() 
        { 
            selectedTraining = ETraining.NONE;
            menuLevel = 0;
            isAllSelected = false;

            descStr = new List<string>() { "훈련을 선택하세요.", "공부할 알고리즘을 선택하세요." };
            resultStr = new List<string>();
            menu = new List<string>() 
            {
                                            "자기 계발\t",
                                            "코딩 연습\t",
                                            "알고리즘 공부\t",
                                            "면접 대비\t"
            };
            menu2 = new List<string>()
            {
                                            "완전탐색\t",
                                            "DP\t\t",
                                            "BFS, DFS\t",
                                            "다익스트라\t",
                                            "분할정복\t"
            };
        }
        public override void Init()
        {
            base.Init();
            controller.uparrowkeydownhandle = new F_UpArrowKeyDownHandle(PressUpArrowKey);
            controller.downArrowKeydownHandle = new F_DownArrowKeyDownHandle(PressDownArrowKey);
            controller.zkeydownhandle = new F_ZKeyDownHandle(PressZKey);
            controller.xkeydownhandle = new F_XKeyDownHandle(PressXKey);

            descSB.AppendLine(descStr[0]);
            TextBar.SetDesc(descSB.ToString());
            TextBar.SetOutputType(EOutputType.DEFAULT);
            TextBar.SetVTCMenu(menu);
            aaWindow.SetAA(AAData.Instance.AA_Training);
            aaWindow.SetTextPosition(15, 5);
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
        public override void PRC_Action()   // 선택한 주간 행동에 따른 능력치 증감 처리
        {
            base.PRC_Action();
            
            string Training = string.Empty;
            // 증가 수치 * 임시
            int increasingValue = 4;
            if (player.Status.hp <= 0)
                increasingValue = 0;
            int decreasingValue = 20;
            switch(selectedTraining) // 임시 수치 증가 * 밸런스 조절때 수정 해야됨
            {
                case ETraining.NONE:
                    /* Do Nothing */
                    break;
                case ETraining.SPEC:
                    player.Status.specPower += increasingValue;
                    Training = $"스펙력이 {increasingValue} 증가하였습니다.";
                    break;
                case ETraining.CODING:
                    player.Status.codePower += increasingValue;
                    Training = $"코딩력이 {increasingValue} 증가하였습니다.";
                    break;
                case ETraining.ALGO_BF:
                    player.Status.algoPower += 1;
                    player.Status.agp_Brf += increasingValue;
                    Training = $"알고력이 1, 완전탐색 숙련도가 {increasingValue} 증가하였습니다.";
                    break;
                case ETraining.ALGO_DP:
                    player.Status.algoPower += 1;
                    player.Status.agp_DP += increasingValue;
                    Training = $"알고력이 1, DP 숙련도가 {increasingValue} 증가하였습니다.";
                    break;
                case ETraining.ALGO_BDFS:
                    player.Status.algoPower += 1;
                    player.Status.agp_BDFS += increasingValue;
                    Training = $"알고력이 1, BFS, DFS 숙련도가 {increasingValue} 증가하였습니다.";
                    break;
                case ETraining.ALGO_DIJK:
                    player.Status.algoPower += 1;
                    player.Status.agp_Dijk += increasingValue;
                    Training = $"알고력이 1, 다익스트라 숙련도가 {increasingValue} 증가하였습니다.";
                    break;
                case ETraining.ALGO_DIVC:
                    player.Status.algoPower += 1;
                    player.Status.agp_DivC += increasingValue;
                    Training = $"알고력이 1, 분할정복 숙련도가 {increasingValue} 증가하였습니다.";
                    break;
                case ETraining.INTERVIEW:
                    player.Status.intvPower += increasingValue;
                    Training = $"면접력이 {increasingValue} 증가하였습니다.";
                    break;
                default: 
                    break;
            }

            player.DecreaseHP(decreasingValue);
            resultSB.AppendLine(Training);
            resultSB.AppendLine($"체력이 {decreasingValue} 만큼 감소하였습니다.");
            TextBar.SetRes(resultSB.ToString());
            TextBar.onOutputResultHandle();
            // resultStr.Add(Training);
        }

        public override void Update()
        {
            base.Update();
            MenuUpdate();
        }

        public void MenuUpdate()
        {
            //selectedTraining 설정
            if (menuLevel == 0)
            {
                switch (selectNumber)
                {
                    case 0:
                        selectedTraining = ETraining.SPEC;
                        break;
                    case 1:
                        selectedTraining = ETraining.CODING;
                        break;
                    case 2:
                        break;
                    case 3:
                        selectedTraining = ETraining.INTERVIEW;
                        break;
                    default:
                        break;
                }
            }
            else if (menuLevel == 1)
            {
                switch (selectNumber)
                {
                    case 0:
                        selectedTraining = ETraining.ALGO_BF;
                        break;
                    case 1:
                        selectedTraining = ETraining.ALGO_DP;
                        break;
                    case 2:
                        selectedTraining = ETraining.ALGO_BDFS;
                        break;
                    case 3:
                        selectedTraining = ETraining.ALGO_DIJK;
                        break;
                    case 4:
                        selectedTraining = ETraining.ALGO_DIVC;
                        break;
                    default:
                        break;
                }
            }
        }
        public override void PressUpArrowKey() 
        {
            base.PressUpArrowKey();
            if (menuLevel == 0)
                selectNumber = Math.Clamp(selectNumber - 1, 0, menu.Count - 1);
            else if(menuLevel == 1)
                selectNumber = Math.Clamp(selectNumber - 1, 0, menu2.Count - 1);
            //
            TextBar.SelectMenu = selectNumber;
            TextBar.onUIUpdatedhandle();
        }
        public override void PressDownArrowKey()
        {
            base.PressDownArrowKey();
            if (menuLevel == 0)
                selectNumber = Math.Clamp(selectNumber + 1, 0, menu.Count - 1);
            else if (menuLevel == 1)
                selectNumber = Math.Clamp(selectNumber + 1, 0, menu2.Count - 1);
            //
            TextBar.SelectMenu = selectNumber;
            TextBar.onUIUpdatedhandle();
        }
        public override void PressZKey()
        {
            base.PressZKey();
            descSB.Clear();
            if (menuLevel == 0) // 훈련 선택의 경우
            {
                if(selectNumber == 2)
                {
                    menuLevel = Math.Clamp(menuLevel + 1, 0, 1);
                    descSB.AppendLine(descStr[1]);
                    TextBar.SetDesc(descSB.ToString());
                    TextBar.SetVTCMenu(menu2);
                }
                else
                {
                    ExecuteAction();
                }
            }
            else if(menuLevel == 1) // 알고리즘 공부 선택의 경우
            {
                ExecuteAction();
            }
            selectNumber = 0;
            TextBar.SelectMenu = selectNumber;
            TextBar.onUIUpdatedhandle();
        }
        public override void PressXKey()
        {
            base.PressXKey();
            descSB.Clear();
            menuLevel = Math.Clamp(menuLevel - 1, 0, 1);
            descSB.AppendLine(descStr[0]);
            TextBar.SetDesc(descSB.ToString());
            TextBar.SetVTCMenu(menu);
            selectNumber = 0;
        }
    }
}
