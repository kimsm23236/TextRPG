using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveInJobSeeker
{
    public delegate void F_LeftArrowKeyDownHandle();
    public delegate void F_RightArrowKeyDownHandle();
    public delegate void F_UpArrowKeyDownHandle();
    public delegate void F_DownArrowKeyDownHandle();
    public delegate void F_ZKeyDownHandle();
    public delegate void F_XKeyDownHandle();

    public class Controller
    {
        private bool bisKeyDown;

        public F_LeftArrowKeyDownHandle     leftarrowkeydownhandle;
        public F_RightArrowKeyDownHandle    rightarrowkeydownhandle;
        public F_UpArrowKeyDownHandle       uparrowkeydownhandle;
        public F_DownArrowKeyDownHandle     downArrowKeydownHandle;
        public F_ZKeyDownHandle             zkeydownhandle;
        public F_XKeyDownHandle             xkeydownhandle;

        public bool IsKeyDown
        {
            get { return bisKeyDown; }
        }
        private ConsoleKeyInfo cki;
        public ConsoleKeyInfo CKI 
        { 
            get { return cki; } 
        }

        private static Controller _instance;
        public static Controller Instance
        {
            get
            {
                if(_instance == null)
                    _instance = new Controller();
                return _instance;
            }
        }

        private Controller()
        {
            InitDelegate();
        }

        public void KbHit() // 키입력을 받는 곳에서 써주기
        {
            if(Console.KeyAvailable) 
            {
                cki = Console.ReadKey();
                bisKeyDown = true;
            }
            else
            {
                bisKeyDown = false;
                cki = new ConsoleKeyInfo();
            }

            if(bisKeyDown)
            {
                switch(cki.Key)
                {
                    case ConsoleKey.LeftArrow:
                        leftarrowkeydownhandle.Invoke();
                        break; 
                    case ConsoleKey.RightArrow:
                        rightarrowkeydownhandle.Invoke();
                        break;
                    case ConsoleKey.UpArrow:
                        uparrowkeydownhandle.Invoke();
                        break;
                    case ConsoleKey.DownArrow:
                        downArrowKeydownHandle.Invoke();
                        break;
                    case ConsoleKey.Z:
                        zkeydownhandle.Invoke();
                        break;
                    case ConsoleKey.X: 
                        xkeydownhandle.Invoke();
                        break;
                }
            }
        }

        public void InitDelegate()
        {
            leftarrowkeydownhandle = new F_LeftArrowKeyDownHandle(LeftArrowKeyDown);
            rightarrowkeydownhandle = new F_RightArrowKeyDownHandle(RightArrowKeyDown);
            uparrowkeydownhandle = new F_UpArrowKeyDownHandle(UpArrowKeyDown);
            downArrowKeydownHandle = new F_DownArrowKeyDownHandle(DownArrowKeyDown);
            zkeydownhandle = new F_ZKeyDownHandle(ZKeyDown);
            xkeydownhandle = new F_XKeyDownHandle(XKeyDown);
        }

        private void LeftArrowKeyDown()
        {
            Console.WriteLine("왼쪽 방향키 눌렸음");
        }
        public void RightArrowKeyDown() 
        {
            Console.WriteLine("오른쪽 방향키 눌렸음");
        }
        public void UpArrowKeyDown()
        {
            Console.WriteLine("위쪽 방향키 눌렸음");
        }
        public void DownArrowKeyDown()
        {
            Console.WriteLine("아래쪽 방향키 눌렸음");
        }

        public void ZKeyDown()
        {
            Console.WriteLine("Z키 눌렸음");
        }

        public void XKeyDown()
        {
            Console.WriteLine("X키 눌렸음");
        }
    }
}
