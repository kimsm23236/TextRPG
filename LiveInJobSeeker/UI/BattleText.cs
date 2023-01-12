using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveInJobSeeker
{
    public delegate void F_ChangeCurLogHandle();
    public delegate void F_EndOutputLogHandle();
    public delegate void F_LogProcessEndHandle();
    public class BattleText : TextMenuUI
    {
        private List<BattleLog> battleLogs = new List<BattleLog>();
        // private StringBuilder renderSB = new StringBuilder();

        public F_ChangeCurLogHandle changecurloghandle;
        public F_EndOutputLogHandle endoutputloghandle;
        public F_LogProcessEndHandle logprocessendhandle;

        private BattleLog curOutputLog;

        // 로그 출력 중인지
        private bool IsOutputLog;
        // 몇번째 로그
        private int logidx;
        // 결과 출력 중인지
        private bool IsOutputRes;

        private int lineAppearSpeed;
        public int LineAppearSpeed
        {
            get { return lineAppearSpeed; }
            set { lineAppearSpeed = value; }
        }

        private int tsx;
        private int tsy;

        public BattleText()
        {
            renderSB = new StringBuilder();
            curOutputLog = new BattleLog();
            IsOutputLog = false;
            IsOutputRes = false;
            logidx = 0;
            lineAppearSpeed = 400;
            textAppearSpeed = 200;
        }

        public void SetLogs(List<BattleLog> newlogs)
        {
            battleLogs = newlogs;
            SetLog();
        }
        private void SetLog()
        {
            curOutputLog = battleLogs[logidx];
        }

        public void SetResultStr(string str)
        {
            resultStr = str;
        }

        public override void Init(int width, int height, int px, int py) // * 가로크기, 세로크기, x좌표, y좌표
        {
            UISize newSize = new UISize();
            newSize.Width = width;
            newSize.Height = height;
            UIPosition newPosition = new UIPosition();
            newPosition.X = px;
            newPosition.Y = py;
            _size = newSize;
            _position = newPosition;

            logidx = 0;
            tsx = newPosition.X + 1;
            tsy = newPosition.Y + 1;

            IsOutputLog = true;
            changecurloghandle = new F_ChangeCurLogHandle(SetLog);
            onUIUpdatedhandle = () => { bIsUpdated = true; };
            onOutputResultHandle = () =>
            {
                bIsUpdated = true;
                outputType = EOutputType.SEQ_LETTER;
                IsOutputRes = true;
            };
            endoutputloghandle = () =>
            {
                IsOutputLog = false;
                IsOutputRes = true;
                outputType = EOutputType.SEQ_LETTER;
            };

        }
        public void Init(int width, int height, int px, int py, List<BattleLog> logs)
        {
            Init(width, height, px, py);
            battleLogs = logs;
        }


        public override void Update()
        {
            base.Update();

            renderSB = new StringBuilder();

            if (IsOutputLog)
            {
                if (cntOutputLine >= 1)
                    renderSB.AppendLine(curOutputLog.playerStr);
                if (cntOutputLine >= 2)
                    renderSB.AppendLine(curOutputLog.atkDescStr);
                if (cntOutputLine >= 3 && curOutputLog.coteDescStr != string.Empty)
                    renderSB.AppendLine(curOutputLog.coteDescStr);
                if (cntOutputLine >= 4)
                    renderSB.AppendLine(curOutputLog.damageDescStr);
            }

            if(IsOutputRes)
            {
                for(int i = 0; i < resultStr.Length; i++)
                {
                    if (resultStr[i] == '\n')
                        renderSB.AppendLine();
                    else if (i <= cntOutputLetter)
                        renderSB.Append(resultStr[i]);
                }
            }

            
        }
        public override void Render()
        {
            // 출력 내용이 변경된 것이 없다면 렌더 X
            if (!bIsUpdated)
                return;

            if (IsThereBorder)
                RenderBorder();

            string outputStr = renderSB.ToString();

            Console.SetCursorPosition(tsx, tsy);
            if (IsOutputLog)
            {
                //for(int i = 0; i < outputStr.Length-1; i++)
                //{
                //    if (outputStr[i] == '\n')
                //        Console.SetCursorPosition(tsx, Console.CursorTop + 1);
                //    else
                //        Console.Write(outputStr);
                //}
                foreach(var ch in outputStr)
                {
                    if (ch == '\n')
                        Console.SetCursorPosition(tsx, Console.GetCursorPosition().Top + 1);
                    else
                        Console.Write(ch);
                }
            }

            if (IsOutputRes)
            {
                foreach(var ch in outputStr)
                {
                    if (ch == '\n')
                        Console.SetCursorPosition(tsx, Console.GetCursorPosition().Top + 1);
                    else
                        Console.Write(ch);
                }
                //for (int i = 0; i < resultStr.Length; i++)
                //{
                //    if (resultStr[i] == '\n')
                //    {
                //        Console.SetCursorPosition(tsx, Console.GetCursorPosition().Top + 1);
                //        continue;
                //    }
                //    if (i <= cntOutputLetter)
                //        Console.Write(resultStr[i]);
                //}
            }

            if (IsOutputRes)
            {
                // 결과 텍스트 출력
                //foreach(char ch in outputStr)
                //{
                //    if (ch == '\n')
                //        Console.SetCursorPosition(tsx, Console.GetCursorPosition().Top + 1);
                //    else
                //        Console.Write(ch);
                //}
                //for (int i = 0; i < resultStr.Length; i++)
                //{
                //    if (resultStr[i] == '\n')
                //    {
                //        Console.SetCursorPosition(tsx, Console.GetCursorPosition().Top + 1);
                //        continue;
                //    }
                //    if (i <= cntOutputLetter)
                //        Console.Write(resultStr[i]);
                //}
            }
            bIsUpdated = false;
        }

        public override void OnTimer()
        {
            IncreaseOutputLine();
        }
        private async void IncreaseOutputLine()
        {
            if (cntOutputLine >= curOutputLog.CntLine() + 5)
            {
                logidx = Math.Clamp(logidx + 1, 0, battleLogs.Count);
                if (logidx >= battleLogs.Count)
                {
                    endoutputloghandle();
                    cntOutputLetter = 0;
                    IncreaseOutputLetter();
                    return;
                }
                else if(logidx < battleLogs.Count)
                    changecurloghandle();
                

                cntOutputLine = 0;
                onUIUpdatedhandle();
                IncreaseOutputLine();

                return;
            }

            await Task.Delay(LineAppearSpeed);
            cntOutputLine++;
            IncreaseOutputLine();
            onUIUpdatedhandle();
        }

        protected override async void IncreaseOutputLetter()
        {
            if (cntOutputLetter >= resultStr.Length)
            {
                onUIUpdatedhandle();
                logprocessendhandle();
                return;
            }

            await Task.Delay(textAppearSpeed);
            cntOutputLetter++;
            IncreaseOutputLetter();
            onUIUpdatedhandle();
        }

        private void SetCurLog(BattleLog newLog)
        {
            curOutputLog = newLog;
        }
    }
}
