using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveInJobSeeker
{
    public class Status
    {
        /*
         * 스탯
         */
        // 체력
        public int hp;                             // 체력
        // 회사 지원 스탯
        public int specPower;                      // 서류력
        // 코딩 테스트 관련 스탯
        public int codePower;                      // 코딩력
        public int algoPower;                      // 알고력
        // * 속성 방어력
        public int agp_Brf;                        // 알고력_완전탐색
        public int agp_DP;                         // 알고력_다이나믹프로그래밍
        public int agp_BDFS;                       // 알고력_BFS, DFS
        public int agp_Dijk;                       // 알고력_다익스트라
        public int agp_DivC;                       // 알고력_분할정복
        // 면접력
        public int intvPower;                      // * interview

        public Status()
        {
            hp = 100;
            specPower = 0;
            codePower = 0;
            algoPower = 0;
            agp_Brf = 0;
            agp_DP = 0;
            agp_BDFS = 0;
            agp_Dijk = 0;
            agp_DivC = 0;
            intvPower = 0;
        }
    }

    public enum EAttack
    {
        NONE = 0,
        SPECATTACK, COTEATTACK, INTVATTACK
    }
    public class JobSeeker
    {
        // 취준생 이름
        private string name;
        // 스탯
        private Status stat;
        // 돈
        private int money;
        // 턴 수
        private int cntTurn;

        public string Name
        {
            get { return name; }
        }
        public Status Status 
        { 
            get { return stat; } 
        }
        public int Money
        {
            get { return money; }
        }

        public int Turn
        {
            get { return cntTurn; }
            set { cntTurn = value; }
        }

        public JobSeeker()
        {
            name = string.Empty;
            // 스탯 초기화
            stat = new Status();
            cntTurn = 1;
        }

        public void SetName(string newName)
        {
            name = newName;
        }
        public void SetStat(Status newStat)
        {
            stat = newStat;
        }

        public void ResetHP()
        {
            Status.hp = 100;
        }
        public void IncreaseHP(int value)
        {
            stat.hp += value;
        }
        public void DecreaseHP(int value)
        {
            stat.hp -= value;
        }

        public bool IsDead()
        {
            return stat.hp <= 0;
        }

        // 데미지 처리 함수
        public int TakeDamage(int dmgValue, EAttack Atk)
        {
            // 데미지 계산식
            // 각각 공격력 - 그에 맞는 플레이어 스탯
            int firstDamage = dmgValue;
            int firstDef = 0;
            switch(Atk)
            {
                case EAttack.SPECATTACK:
                    firstDef = stat.specPower;
                    break;
                case EAttack.COTEATTACK:
                    /* Do Nothing */
                    break;
                case EAttack.INTVATTACK:
                    firstDef = stat.intvPower;
                    break;
                default:
                    break;
            }
            int finalDamage = Math.Clamp(firstDamage - firstDef, 0, 100);
            DecreaseHP(finalDamage);
            return finalDamage;
        }
        public int TakeDamage(int dmgValue, EAttack Atk, EAlgorithm Algo) // 코테 데미지 처리
        {
            int firstDamage = dmgValue;
            int firstDef = 0;
            switch (Algo)
            {
                case EAlgorithm.BRUTEFORCE:
                    firstDef = stat.agp_Brf;
                    break;
                case EAlgorithm.DP:
                    firstDef = stat.agp_DP;
                    break;
                case EAlgorithm.BDFS:
                    firstDef = stat.agp_BDFS;
                    break;
                case EAlgorithm.DIJKSTRA:
                    firstDef = stat.agp_Dijk;
                    break;
                case EAlgorithm.DIVIDEANDCONQUER:
                    firstDef = stat.agp_DivC;
                    break;
            }
            int finalDamage = Math.Clamp(firstDamage - firstDef, 0, 100);
            DecreaseHP(finalDamage);
            return finalDamage;
        }

        public void IncreaseTurn()
        {
            cntTurn++;
        }

        public void IncreaseMoney(int m)
        {
            money += m;
        }
        public void DecreaseMoney(int m)
        {
            money -= m;
        }
    }
}
