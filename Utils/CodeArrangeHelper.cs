using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenShoot
{
    public class CodeArrangeHelper
    {
        public static StringBuilder arrangeByGLAck(StringBuilder sbSrc)
        {
            string[] tags = sbSrc.ToString().
                Split(Environment.NewLine.ToCharArray());

            StringBuilder sbDst = new StringBuilder();
            sbDst.Append("//if (CurrentPage == \"\") {").
                AppendLine();

            int tagNumber = 0;
            foreach (string tag in tags)
            {
                if (tag == "")
                {
                    continue;
                }

                string strTemp = "    ack(" + tag.Replace(" ", "") + ");";
                sbDst.Append(strTemp).AppendLine();

                tagNumber++;
            }

            //sbDst.Append("    Unconfirmed = Unconfirmed - " + tagNumber.ToString() + ";").
            sbDst.Append("}").
                AppendLine();

            return sbDst;
        }
    }
}
