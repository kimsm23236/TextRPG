using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveInJobSeeker
{
    public class JobSeekerCreateScene : Scene
    {
        
        public JobSeekerCreateScene()
        {
            renderSB = new StringBuilder();
        }

        public override void Init()
        {
            base.Init();
            

        }
        public override void Update()
        {
            base.Update();
            InputUserName();
        }
        public override void Render()
        {
            base.Render();

        }

        public void InputUserName()
        {
            Console.WriteLine("캐릭터를 생성합니다.");
            Console.Write("캐릭터의 이름을 입력하세요 : ");
            string inputStr = Console.ReadLine();

            JobSeeker newJobSeeker = new JobSeeker();
            newJobSeeker.SetName(inputStr);

            Game gameIns = Game.Instance;
            gameIns.SetPlayer(newJobSeeker);
            // 나중에 예외처리
            MGLS_WeeklyAction nextScene = new MGLS_WeeklyAction();
            gameIns.shiftscenehandle.Invoke(nextScene);
        }
    }
}
