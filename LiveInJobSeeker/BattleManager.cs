using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace LiveInJobSeeker
{
    public enum EBattlePhase
    {
        NONE = 0,
        SPECCHECK, CODINGTEST, INTERVIEW
    }
    public class BattleLog
    {
        public string playerStr;
        public string atkDescStr;
        public string coteDescStr;
        public string damageDescStr;
        public bool isAlive;

        public BattleLog(string pstr, string atk, string cote, string dmg)
        {
            playerStr = pstr;
            atkDescStr = atk;
            coteDescStr = cote;
            damageDescStr = dmg;
        }
        public BattleLog(string pstr, string atk, string dmg)
        {
            playerStr = pstr;
            atkDescStr = atk;
            coteDescStr = string.Empty;
            damageDescStr = dmg;
        }
        public BattleLog()
        {
            playerStr= string.Empty;
            atkDescStr= string.Empty;
            coteDescStr= string.Empty;
            damageDescStr= string.Empty;
        }
        public int CntLine()
        {
            int num = 4;
            if (playerStr == string.Empty)
                num--;
            if (atkDescStr == string.Empty)
                num--;
            if (coteDescStr == string.Empty)
                num--;
            if (damageDescStr == string.Empty)
                num--;
            return num;
        }
        public void SetAlive(bool alive)
        {
            this.isAlive = alive;
        }
    }

    public class BattleManager
    {
        /*
         * 전투 관리 클래스
         * 플레이어와 적을 멤버로 가지고
         * 플레이어 데미지 처리,
         * 출력 처리등을 담당
         */
        private JobSeeker player;
        private JobPosting enemy;
        private int CoteIdx;

        private string resultStr; // * 합격 하였다!, 서류 광탈 하였다!, 코테 불합 하였다! 이런거
        public string ResultStr
        {
            get { return resultStr; }
        }

        private EBattlePhase phase;

        private List<BattleLog> logs;

        private bool isRunOnce;
        // private bool isRunOnce
        public BattleManager() 
        {
            isRunOnce = false;
            logs = new List<BattleLog>();
        }
        public void SetPlayer(JobSeeker p)
        {
            player = p;
        }
        public void SetEnemy(JobPosting e)
        {
            enemy = e;
            CoteIdx = 0;
            resultStr = $"{enemy.Name}\n";
        }
        public List<BattleLog> ExecuteBattle()
        {
            if (!IsCorrentSetup())
                return new List<BattleLog>();

            // 배틀 실행
            Attack_SpecCheck();
            Attack_CodingTest();
            Attack_Interview();

            return logs;
        }
        public bool IsCorrentSetup()
        {
            bool isPlayerValid = player != null;
            bool isEnemyValid = enemy != null;
            return isPlayerValid && isEnemyValid;
        }

        private void Attack_SpecCheck()
        {
            // 데미지 처리
            int hpBeforeDamage = player.Status.hp;
            int damage = enemy.Status.SpecAtk;
            int finaldmg = player.TakeDamage(damage, EAttack.SPECATTACK);

            // 출력 로그 처리
            string playerStr = $"{player.Name}의 현재 체력 : {hpBeforeDamage}";
            string atkDescStr = $"{enemy.Name}의 서류 전형!";
            string damageDescStr = $"{player.Name}은 {finaldmg}의 데미지를 받았다!";
            BattleLog bLog = new BattleLog(playerStr, atkDescStr, damageDescStr);

            bool bIsAlive = true;
            if(player.IsDead())
            {
                bIsAlive = false;
                resultStr += "서류 광탈 하였다!";
            }
                

            bLog.SetAlive(bIsAlive);
            logs.Add(bLog);
        }
        private void Attack_CodingTest()
        {
            if (player.IsDead())
                return;

            for(int i = 0; i < enemy.CoteList.Count; i++)
            {
                CodingTest cote = enemy.CoteList[i];
                int hpBeforeDamage = player.Status.hp;
                int damage = enemy.Status.CoteAtk + cote.Level * 5;
                int finaldmg = player.TakeDamage(damage, EAttack.COTEATTACK, cote.Algorithm);
                string algo = string.Empty;
                string atkDescStr = $"{enemy.Name}의 코딩 테스트 공격!";
                switch (cote.Algorithm)
                {
                    case EAlgorithm.NONE:
                        algo = "구현";
                        break;
                    case EAlgorithm.BRUTEFORCE:
                        algo = "완전탐색";
                        break;
                    case EAlgorithm.DP:
                        algo = "DP";
                        break;
                    case EAlgorithm.BDFS:
                        algo = "BFS, DFS";
                        break;
                    case EAlgorithm.DIJKSTRA:
                        algo = "다익스트라";
                        break;
                    case EAlgorithm.DIVIDEANDCONQUER:
                        algo = "분할정복";
                        break;
                    default:
                        break;
                }
                string playerStr = $"{player.Name}의 현재 체력 : {hpBeforeDamage}";
                string coteDescStr = $"{algo} 문제가 출제되었다!";
                string damageDescStr = $"{player.Name}은 {finaldmg}의 데미지를 받았다!";
                BattleLog bLog = new BattleLog(playerStr, atkDescStr, coteDescStr, damageDescStr);
                bool isAlive = true;
                if (player.IsDead())
                {
                    isAlive = false;
                    resultStr += "코딩 테스트에 불합격하였다!";
                }
                bLog.SetAlive(isAlive);
                logs.Add(bLog);

                if (player.IsDead())
                    break;
            }  
        }
        private void Attack_Interview()
        {
            if (player.IsDead())
                return;

            // 데미지 처리
            int hpBeforeDamage = player.Status.hp;
            int damage = enemy.Status.IntvAtk;
            int finaldmg = player.TakeDamage(damage, EAttack.INTVATTACK);

            // 출력 로그 처리
            string playerStr = $"{player.Name}의 현재 체력 : {hpBeforeDamage}";
            string atkDescStr = $"{enemy.Name}의 면접 공격!";
            string damageDescStr = $"{player.Name}은 {finaldmg}의 데미지를 받았다!";
            BattleLog bLog = new BattleLog(playerStr, atkDescStr, damageDescStr);

            bool isAlive = true;
            if(player.IsDead())
            {
                isAlive = false;
                resultStr += "면접에서 탈락하였다!";
            }
            else
            {
                resultStr += "최종 합격!";
            }
            bLog.SetAlive(isAlive);
            logs.Add(bLog);
        }
    }
}
