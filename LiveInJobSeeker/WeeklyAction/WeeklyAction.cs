using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveInJobSeeker
{
    /*
     * WeeklyAction Class
     * 주간 행동 클래스
     * 한 턴(주)에 플레이어가 선택할 수 있는 행동 클래스의 상위 클래스
     * 4개의 행동 클래스가 상속 받으며 각각의 행동을 실행할 수 있음
     */
    public class WeeklyAction
    {
        protected JobSeeker player;
        public WeeklyAction()
        { 
            player = new JobSeeker();
        }  
        public virtual void Init()
        {
            Game gameIns = Game.Instance;
            player = gameIns.Player;
        }
        public virtual void ExecuteAction()
        {
            /*
             * Do Nothing? 
             * 턴을 증가시키는 내용이 들어갈수도있음
             * 오버라이드 해서 자식들이 각각 다른 행동을 실행하게
             */
        }

    }
}
