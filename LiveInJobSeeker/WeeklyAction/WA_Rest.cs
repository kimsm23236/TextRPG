using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveInJobSeeker
{
    public class WA_Rest : WeeklyAction
    {
        private string descStr = "휴식을 취하였습니다.";
        private List<string> resultStr;
        public WA_Rest()
        {
            selectNumber = 0;
            resultStr = new List<string>();
        }
        public override void Init()
        {
            base.Init();

            descSB.AppendLine(descStr);
            TextBar.SetDesc(descSB.ToString());
            TextBar.SetOutputType(EOutputType.SEQ_LETTER);

            ExecuteAction();
        }
        public override void ExecuteAction()
        {
            if (!RunOnlyOnce)
                return;
            RunOnlyOnce = false;
            base.ExecuteAction();
            PRC_Action();
            ToNextScene_a_Second(2000);
        }
        public override void Update()
        {
            base.Update();
            rendersb.AppendLine(descStr);
            foreach(string str in resultStr)
            {
                rendersb.AppendLine(str);
            }
        }
        public override void PRC_Action()
        {
            base.PRC_Action();
            player.Status.hp = 100;
            string resStr = $"체력이 {player.Status.hp}으로 회복되었습니다.";
            resultSB.AppendLine(resStr);
            TextBar.SetRes(resultSB.ToString());
            TextBar.onOutputResultHandle();
        }
    }
}
