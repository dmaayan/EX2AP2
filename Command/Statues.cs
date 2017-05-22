using System;

namespace MVC
{
    /// <summary>
    /// enum Status has 8 status, represent the current condotion
    /// </summary>
    public enum Status { KeepConnection = 0, Disconnect, PrintAndContinue,
                    CloseGame, Error, Close, CloseAndDeliver, Finish, Play };

    /// <summary>
    /// Statues has Status and string 
    /// </summary>
    public class Statues
    {
        /// <summary>
        /// the enum status
        /// </summary>
        private Status status;
        /// <summary>
        /// message to deliver
        /// </summary>
        private string message;

        /// <summary>
        /// SetStatues
        /// </summary>
        /// <param name="s">is the Status </param>
        /// <param name="m">is the string</param>
        public void SetStatues(Status s, string m)
        {
            status = s;
            message = m;
        }

        /// <summary>
        /// a property of stat 
        /// </summary>
        public Status Stat
        {
            get { return status; }
        }

        /// <summary>
        /// a property of message 
        /// </summary>
        public string Message
        {
            get { return message; }
        }

        /// <summary>
        /// convert Statues to Json
        /// </summary>
        /// <returns>as string </returns>
        public string ToJson()
        {
            return "\"status\"~" + status.ToString() + "|" + "\"message\"~" + message;
        }

        /// <summary>
        /// FromJson is static method convert from Json to Statues
        /// </summary>
        /// <param name="s">is the string represented at Json</param>
        /// <returns>the Statues</returns>
        public static Statues FromJson(string s)
        {
            // split the string
            char[] spliters = { '|', '~' };
            string[] members = s.Split(spliters);
            Statues statues = new Statues();
            // parse the status back
            Status status = (Status)Enum.Parse(typeof(Status), members[1]);
            string message = "";
            // build the string
            for (int i = 3; i < members.Length; i++)
            {
                message += members[i];  
            }
            statues.SetStatues(status, message);
            return statues;
        }
    }
}
