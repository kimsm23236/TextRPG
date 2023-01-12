using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveInJobSeeker
{
    public class ScrollMenu : UI
    {
        protected int cntOutputLetter;
        public EOutputType outputType;

        protected string curOutputStr;

        public List<string> vtc_menu;
        private bool IsVtcMenu;
        private int selectMenuNumb;

        public int SelectMenu
        {
            get { return selectMenuNumb; }
            set { selectMenuNumb = value; }
        }

        private string descStr;
        private bool IsOutputDesc;

        private bool bisAllOutput;

        protected string resultStr;
        private bool IsOutputResult;
        // public F_OnOutputResultHandle onOutputResultHandle;

        public void SetDesc(string str)
        {
            descStr = str;
            curOutputStr = descStr;
        }
        public void SetRes(string str)
        {
            resultStr = str;
            curOutputStr = resultStr;
        }

        protected int textAppearSpeed;
        public int TextAppearSpeed
        {
            get { return textAppearSpeed; }
            set { textAppearSpeed = value; }
        }

        private int tsx;
        private int tsy;

        public ScrollMenu()
        {
            cntOutputLetter = 0;
            outputType = EOutputType.DEFAULT;
            descStr = string.Empty;
            resultStr = string.Empty;
            curOutputStr = string.Empty;
            textAppearSpeed = 80;
            vtc_menu = new List<string>();
            IsVtcMenu = false;
            IsOutputDesc = false;
            IsOutputResult = false;
            bIsUpdated = true;

            // 델리게이트 바인딩
            onUIUpdatedhandle = () => { bIsUpdated = true; };
        }

        public override void Init(int width, int height, int px, int py)
        {
            base.Init(width, height, px, py);
            IsOutputDesc = true;
            tsx = px + 1;
            tsy = py + 1;
        }
        public void Init(int width, int height, int px, int py, EOutputType type)
        {
            Init(width, height, px, py);
            outputType = type;
            outputInit();
        }
        public void Init(int width, int height, int px, int py, EOutputType type, string desctext)
        {
            Init(width, height, px, py, type);
            descStr = desctext;
            curOutputStr = descStr;
        }

        public void SetOutputType(EOutputType type)
        {
            outputType = type;
            outputInit();
        }

        protected void outputInit()
        {
            switch (outputType)
            {
                case EOutputType.DEFAULT:
                    cntOutputLetter = 10000;
                    break;
                case EOutputType.SEQ_LINE:
                    break;
                case EOutputType.SEQ_LETTER:
                    cntOutputLetter = 0;
                    OnTimer();
                    bisAllOutput = false;
                    break;
            }
            onUIUpdatedhandle();
        }
        public void SetVTCMenu(List<string> newMenu)
        {
            vtc_menu = newMenu;
            IsVtcMenu = true;
        }

        public void SetSB(string str)
        {
            renderSB.Append(str);
            curOutputStr = str;
        }
        public override void Update()
        {
            base.Update();
        }
        public override void Render()
        {
            base.Render();
            // 출력 내용이 변경된 것이 없다면 렌더 X
            if (!bIsUpdated)
                return;

            Console.SetCursorPosition(tsx, tsy);
            if (IsOutputDesc)
            {
                for (int i = 0; i < descStr.Length; i++)
                {
                    if (descStr[i] == '\n')
                    {
                        Console.SetCursorPosition(tsx, Console.GetCursorPosition().Top + 1);
                        continue;
                    }
                    if (i <= cntOutputLetter)
                        Console.Write(descStr[i]);
                }
            }
            if (IsVtcMenu && bisAllOutput)
            {
                // 수직 메뉴 출력 로직 작성
                if(vtc_menu.Count <= 7)
                    for (int i = 0; i < vtc_menu.Count; i++)
                    {
                        Console.Write(vtc_menu[i]);
                        if (selectMenuNumb == i)
                            Console.Write("  ☜");
                        Console.SetCursorPosition(tsx, Console.GetCursorPosition().Top + 1);
                    }
                else if(vtc_menu.Count > 7)
                {
                    int i = Math.Clamp(selectMenuNumb - 6, 0, vtc_menu.Count - 1);
                    for(;i < vtc_menu.Count; i++)
                    {
                        Console.Write(vtc_menu[i]);
                        if (selectMenuNumb == i)
                            Console.Write("  ☜");
                        Console.SetCursorPosition(tsx, Console.GetCursorPosition().Top + 1);
                    }
                }
            }
            if (IsOutputResult)
            {
                // 결과 텍스트 출력
                for (int i = 0; i < resultStr.Length; i++)
                {
                    if (resultStr[i] == '\n')
                    {
                        Console.SetCursorPosition(tsx, Console.GetCursorPosition().Top + 1);
                        continue;
                    }
                    if (i <= cntOutputLetter)
                        Console.Write(resultStr[i]);
                }
            }
            bIsUpdated = false;
        }
        public virtual void OnTimer()
        {
            IncreaseOutputLetter();
        }
        protected virtual async void IncreaseOutputLetter()
        {
            if (cntOutputLetter >= curOutputStr.Length)
            {
                bisAllOutput = true;
                onUIUpdatedhandle();
                return;
            }

            await Task.Delay(textAppearSpeed);
            cntOutputLetter++;
            IncreaseOutputLetter();
            onUIUpdatedhandle();
        }

    }
}
