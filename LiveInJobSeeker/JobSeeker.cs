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
            hp = 0;
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
    public class JobSeeker
    {
        // 취준생 이름
        private string name;
        // 스탯
        private Status stat;

        public string Name
        {
            get { return name; }
        }
        public Status Status 
        { 
            get { return stat; } 
        }

        public JobSeeker()
        {
            name = string.Empty;
            // 스탯 초기화
            stat = new Status();
        }

        public void SetName(string newName)
        {
            name = newName;
        }
        public void SetStat(Status newStat)
        {
            stat = newStat;
        }
    }
}
