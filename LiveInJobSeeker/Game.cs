
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LiveInJobSeeker
{
    public delegate void FShiftSceneDelegate(Scene nextScene);
    public delegate void FOnShiftSceneDelegate();
    public class Game
    {
        /*
         * 멤버 변수
         */
        // 게임이 끝나는지 체크할 변수
        // * 메인 게임 루프 반복 용도
        private bool bisGameEnd = false;

        // 콘솔창 크기
        private int width;
        private int height;

        // 씬 관련
        // 현재 씬
        private Scene currentScene;
        public FShiftSceneDelegate shiftscenehandle;
        public FOnShiftSceneDelegate onshiftscenehandle;

        // 플레이어
        private JobSeeker currentJobSeeker;
        public JobSeeker Player
        {
            get { return currentJobSeeker; }
        }
        public void SetPlayer(JobSeeker newplayer)
        {
            currentJobSeeker = newplayer;
        }

        // 싱글턴으로 만들어서 어디서든 참조 가능
        private static Game _instance;
        public static Game Instance
        {
            get 
            {
                if (_instance == null)
                {
                    _instance = new Game();
                }
                return _instance; 
            }
        }
        private Game()
        {
            // 현재 씬을 씬 객체 생성 후 타이틀 화면 씬으로 대입;
            currentScene = new TitleScene();
            
            // 델리게이트 초기화
            shiftscenehandle = new FShiftSceneDelegate(ShiftScene);
            onshiftscenehandle = new FOnShiftSceneDelegate(OnShiftScene);

            //  씬 초기화
            onshiftscenehandle.Invoke();
        }

        public void Start()
        {
            while(!bisGameEnd)
            {
                Update();
                Render();
                Thread.Sleep(50);
            }
        }

        public void Exit()
        {
            bisGameEnd = true;
        }

        private void Update()
        {
            currentScene.Update();
        }

        private void Render()
        {
            currentScene.Render();
        }

        private void ShiftScene(Scene nextScene)
        {
            currentScene = nextScene;
            onshiftscenehandle.Invoke();
        }
        private void OnShiftScene()
        {
            Console.Clear();
            currentScene.Init();
        }
    }
}
