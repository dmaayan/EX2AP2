using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC
{
    public enum Status { KeepConnection = 0, Disconnect, PrintAndContinue, PrintAndStop, Error, Close, CloseAndDeliver, Exit };

    public class Statues
    {
        private Status status;
        private string message;

        public void SetStatues(Status s, string m)
        {
            status = s;
            message = m;
        }

        public Status Stat
        {
            get { return status; }
        }

        public string Message
        {
            get { return message; }
        }

        public string ToJson()
        {
            return "\"status\":" + status.ToString() + "|" + "\"message\":" + message;
        }

        public static Statues FromJson(string s)
        {
            char[] spliters = { '|', ':' };
            string[] members = s.Split(spliters);
            Statues statues = new Statues();
            Status status = (Status)Enum.Parse(typeof(Status), members[1]);
            string message = "";
            for (int i = 3; i < members.Length; i++)
            {
                message += members[i];
            }
            statues.SetStatues(status, message);
            return statues;
        }
    }
}
