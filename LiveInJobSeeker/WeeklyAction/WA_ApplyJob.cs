using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LiveInJobSeeker
{
    public enum EApplyJobPhase
    {
        NONE = 0,
        SEARCHJP, BATTLE, REWARD
    }
    public class WA_ApplyJob : WeeklyAction
    {
        private EApplyJobPhase currentPhase;
        private Controller controller;

        // 적 생성 클래스
        private JobPostingCreater enemyCreater;
        // 적 리스트
        private List<JobPosting> enemies;
        // 적 리스트 개체 수
        private int enemyCount;

        // 전투 관리자
        private BattleManager battleManager;
        // 전투 로그
        private List<BattleLog> logs;
        // 출력 로그
        private List<string> outputLogs;

        private BattleText battleTextBar;

        private int logIdx;
        private int outputLineNum;
        public EApplyJobPhase Phase
        {
            get
            {
                return currentPhase;
            }
        }
        public WA_ApplyJob() 
        {
            enemyCount = 0;
            logIdx = 0;
            outputLineNum = 0;
            outputLogs = new List<string>();
            battleTextBar = new BattleText();

            descStr = new List<string>() { "지원 가능한 공고 목록" };
            resultStr = new List<string>();
            menu = new List<string>();

        }
        public override void Init()
        {
            base.Init();
            controller = Controller.Instance;
            controller.uparrowkeydownhandle = new F_UpArrowKeyDownHandle(PressUpArrowKey);
            controller.downArrowKeydownHandle = new F_DownArrowKeyDownHandle(PressDownArrowKey);
            controller.zkeydownhandle = new F_ZKeyDownHandle(PressZKey);
            enemyCreater = JobPostingCreater.Instance;
            battleManager = new BattleManager();
            SetupEnemyList();
            currentPhase = EApplyJobPhase.SEARCHJP;

            // TextBar.Init(160, 10, 0, 40);
            descSB.AppendLine(descStr[0]);
            TextBar.SetDesc(descSB.ToString());
            TextBar.SetOutputType(EOutputType.DEFAULT);
            TextBar.SetVTCMenu(menu);

            aaWindow.SetAA(AAData.Instance.AA_ApplyJob);
            aaWindow.SetTextPosition(15, 0);

            battleTextBar.Init(102, 10, 0, 40);
            battleTextBar.IsThereBorder = true;
        }
        public override void ExecuteAction()
        {
            base.ExecuteAction();
            if (!RunOnlyOnce)
                return;
            RunOnlyOnce = false;
            base.ExecuteAction();
            PRC_Action();

        }
        public override void PRC_Action()
        {
            base.PRC_Action();
            ToNextPhase();

            if (currentPhase == EApplyJobPhase.BATTLE)
            {
                logs = battleManager.ExecuteBattle();
                battleTextBar.SetLogs(logs);
                battleTextBar.logprocessendhandle = new F_LogProcessEndHandle(BattleEnd);
                TextBar = battleTextBar;
                TextBar.SetOutputType(EOutputType.SEQ_LINE);
                TextBar.SetRes(battleManager.ResultStr);

                //LogsToOutputLogs();
                //Output_a_Second(800);
                //IncreaseLogIdx_a_Second(6000);
            }
        }

        public override void Update()
        {
            base.Update();

            // 페이즈에 따라 다른 처리
            /*
            switch(Phase)
            {
                case EApplyJobPhase.SEARCHJP:   // 서치
                    rendersb.AppendLine("지원 가능한 공고 목록");
                    for(int i = 0; i < enemies.Count; i++)
                    {
                        rendersb.Append(enemies[i].Name);
                        if(selectNumber == i)
                        {
                            rendersb.Append(" ☜");
                        }
                        rendersb.AppendLine();
                    }
                    break;
                case EApplyJobPhase.BATTLE:     // 배틀
                    for(int i = 0; i < outputLogs.Count; i++)
                    {
                        if (outputLineNum >= i)
                            rendersb.AppendLine(outputLogs[i]);
                    }
                    //if (outputLineNum <= 1)
                    //    break;
                    //rendersb.AppendLine(logs[logIdx].playerStr);
                    //if (outputLineNum <= 2)
                    //    break;
                    //rendersb.AppendLine(logs[logIdx].atkDescStr);
                    //if (outputLineNum <= 3)
                    //    break;
                    //if (logs[logIdx].coteDescStr != string.Empty)
                    //    rendersb.AppendLine(logs[logIdx].coteDescStr);
                    //if(outputLineNum <= 4)
                    //rendersb.AppendLine(logs[logIdx].damageDescStr);
                    //if (outputLineNum <= 6)
                    //    break;
                    //if (!logs[logIdx].isAlive)
                    //{
                    //    rendersb.AppendLine($"{player.Name}은 눈 앞이 깜깜해졌다!");
                    //}
                    break;
                case EApplyJobPhase.REWARD:     // 보상
                    rendersb.AppendLine("Press Z Key");
                    break;
                default:
                    break;
            }*/
        }
        private void PressUpArrowKey()
        {
            selectNumber = Math.Clamp(selectNumber - 1, 0, enemyCount - 1);
            //
            TextBar.SelectMenu = selectNumber;
            TextBar.onUIUpdatedhandle();
        }
        private void PressDownArrowKey()
        {
            selectNumber = Math.Clamp(selectNumber + 1, 0, enemyCount - 1);
            //
            TextBar.SelectMenu = selectNumber;
            TextBar.onUIUpdatedhandle();
        }
        private void PressZKey()
        {
            base.PressZKey();
            ExecuteAction();
        }
        private void ToNextPhase()
        {
            switch(Phase)
            {
                case EApplyJobPhase.SEARCHJP:
                    // 서치한 적 선택 후 배틀 매니져에 플레이어, 적 넣어주기
                    battleManager.SetPlayer(player);
                    battleManager.SetEnemy(enemies[selectNumber]);
                    // 컨트롤러 델리게이트에 묶인 함수들 초기화 * 입력 안받겠다는 것
                    controller.InitDelegate();
                    currentPhase = EApplyJobPhase.BATTLE;
                    break;
                case EApplyJobPhase.BATTLE:
                    currentPhase = EApplyJobPhase.REWARD;
                    break;
                case EApplyJobPhase.REWARD:
                    currentPhase = EApplyJobPhase.NONE;
                    ToNextScene_a_Second(500);
                    // 다음 씬 시프트 작업
                    break;
            }
        }
        private void SetupEnemyList()
        {
            enemies = enemyCreater.CreateRandomEnemyList();
            foreach(var enemy in enemies)
            {
                menu.Add(enemy.name);
            }
            enemyCount = enemies.Count;
        }

        private void BattleEnd()
        {
            ToNextScene_a_Second(2000);
        }

        private async void Output_a_Second(int sec)
        {
            await Task.Delay(sec);
            outputLineNum++;
            Output_a_Second(sec);
        }
        protected async void IncreaseLogIdx_a_Second(int sec) 
        {
            if (logIdx >= logs.Count - 1)
            {
                RunOnlyOnce = true;
                keyBind();
                ToNextPhase();
                return;
            }
            
            await Task.Delay(sec);
            
            logIdx = Math.Clamp(logIdx + 1, 0, logs.Count - 1);
            outputLineNum = 0;
            outputLogs = new List<string>();
            Console.Clear();
            LogsToOutputLogs();
            IncreaseLogIdx_a_Second(sec);
        }

        protected void keyBind()
        {
            controller.uparrowkeydownhandle = new F_UpArrowKeyDownHandle(PressUpArrowKey);
            controller.downArrowKeydownHandle = new F_DownArrowKeyDownHandle(PressDownArrowKey);
            controller.zkeydownhandle = new F_ZKeyDownHandle(PressZKey);
        }

        private void LogsToOutputLogs()
        {
            outputLogs.Add(logs[logIdx].playerStr);
            outputLogs.Add(logs[logIdx].atkDescStr);
            if (logs[logIdx].coteDescStr != string.Empty)
                outputLogs.Add(logs[logIdx].coteDescStr);
            outputLogs.Add(logs[logIdx].damageDescStr);
        }
    }
}
