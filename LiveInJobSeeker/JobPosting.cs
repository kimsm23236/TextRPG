using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveInJobSeeker
{
    public enum EAlgorithm
    {
        NONE = 0,
        BRUTEFORCE, DP, BDFS, DIJKSTRA, DIVIDEANDCONQUER
    }
    public class CodingTest
    {
        private EAlgorithm eAlgorithm;
        private int level;
        public EAlgorithm Algorithm 
        { 
            get { return eAlgorithm; } 
            set { eAlgorithm = value; }
        }
        public int Level
        {
            get { return level; }
            set { level = value; }
        }
        public CodingTest(int lv, int algo)
        {
            level = lv;
            eAlgorithm = (EAlgorithm)algo;
        }
    }

    public class JobPostStatus
    {       
        private int specAtk;
        private int coteAtk;
        private int intvAtk;
        public int SpecAtk 
        {
            get { return specAtk; }
            set { specAtk = value; }
        }
        public int CoteAtk
        {
            get { return coteAtk; }
            set { coteAtk = value; }
        }
        public int IntvAtk
        {
            get { return intvAtk; }
            set { intvAtk = value; }
        }
        public JobPostStatus(int sa, int ca, int ia) 
        {
            specAtk = sa;
            coteAtk = ca;
            intvAtk = ia;
        }
    }
    public class JobPosting
    {
        /*
        * 멤버
        * 개인 번호
        * 이름  * ex) 넥슨 공채, 넥슨 인턴 
        * 스탯
        *   스펙 공격력
        *   코테 공격력
        *   면접 공격력
        * 코딩테스트 여러개
        * 코테 데미지는 코테 공격력 * 코테 추가 보정치
        */
        private int id;
        private string name;
        private JobPostStatus status;
        private List<CodingTest> coteList;

        public int ID
        {
            get { return id; }
        }
        public string Name
        {
            get { return name; }
        }
        public JobPostStatus Status
        {
            get { return status; }
        }
        public List<CodingTest> CoteList
        {
            get { return coteList; }
        }
        public JobPosting()
        {
            coteList = new List<CodingTest>();
        }
        public JobPosting(int id, string name, JobPostStatus status)
        {
            this.id = id;
            this.name = name;
            this.status = status;
            coteList = new List<CodingTest>();
        }
        public void SetupCodingTest()
        {
            /*
             * 코테 랜덤 세팅
             * 코테 수 ( 2 ~ 4 ) 
             * 난이도 ( 1 ~ 5 )
             * 알고리즘 종류 ( 0 ~ 5 )
             * 수에 따른 난이도?(최소 난이도 : , 최대 난이도) 
             * 우선 싹다 랜덤 박고 완성되면 밸런스 조정
             */
            Random random = new Random();
            int cntCodingTest = random.Next(2, 4+1);
            for(int i = 0; i < cntCodingTest; i++)
            {
                Random rd = new Random();
                int rlevel = rd.Next(1, 5+1);
                int ralgo = rd.Next(0, 5+1);
                CodingTest ct = new CodingTest(rlevel, ralgo);
                coteList.Add(ct);
            }
        }
    } // JopPosting
    public class JobPostingCreater
    {
        private int cntJobPosting;

        // 임시로 이름 배열에서 적 이름 붙여주기
        private List<string> enemyNames = new List<string>();

        /*
         * 싱글턴
         */
        private static JobPostingCreater _instance;
        public static JobPostingCreater Instance
        {
            get 
            {
                if(_instance == null)
                {
                    _instance = new JobPostingCreater();
                }
                return _instance;
            }
        }
        private JobPostingCreater()
        {
            // 임시
            enemyNames.Add("넥슨 공채");
            enemyNames.Add("엔씨 공채");
            enemyNames.Add("넷마블 공채");
            enemyNames.Add("스마일게이트 공채");
            enemyNames.Add("크래프톤 공채");
            enemyNames.Add("네오위즈 공채");
            enemyNames.Add("넥슨 인턴");
            enemyNames.Add("엔씨 인턴");
            enemyNames.Add("넷마블 인턴");
            enemyNames.Add("스마일게이트 인턴");
            enemyNames.Add("크래프톤 인턴");
            enemyNames.Add("네오위즈 인턴");
        }

        public List<JobPosting> CreateEnemyList()
        {
            // 일단 전부 랜덤으로
            List<JobPosting> newEnemyList = new List<JobPosting>();

            Random random = new Random();
            int cntEnemy = random.Next(1, 5+1);
            for(int i = 0; i < cntEnemy; i++)
            {
                // id 설정
                int id = ++cntJobPosting;
                // 이름 설정
                int rdname = random.Next(0, 11+1);
                string name = enemyNames[rdname];
                // 스탯 설정
                int rspatk = random.Next(20, 30 + 1);
                int rctatk = random.Next(30, 50 + 1);
                int ritatk = random.Next(20, 40 + 1);
                JobPostStatus enemyStatus;
                enemyStatus= new JobPostStatus(rspatk, rctatk, ritatk);
                JobPosting newJP = new JobPosting(id, name, enemyStatus);
                newJP.SetupCodingTest();
                newEnemyList.Add(newJP);
            }

            return newEnemyList;
        }

        
    }
}
