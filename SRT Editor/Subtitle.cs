using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRT_Editor
{
    public class Subtitle
    {
        public string timestamp { get; set; }
        public string num { get; set; }
        public List<String> lines { get; set; }

        public Subtitle(string num, string timestamp, List<string> lines)
        {
            this.num = num;
            this.lines = lines;
            this.timestamp = timestamp;
        }

        public string getToString()
        {
            string str = num + "\n" + timestamp + "\n";
            foreach (string s in lines)
            {
                str += s + "\n";
            }
            return str;
        }

        public string getLines()
        {
            string str = "";
            foreach (string s in lines)
            {
                str += s + "\n";
            }
            return str.Substring(0, str.Length-1);
        }

        public void add()
        {
            string[] times = timestamp.Split(" --> ");
            string from = times[0];
            string to = times[1];
            //00:00:34,425 --> 00:00:35,992
            string[] froms = from.Split(":");
            string[] tos = to.Split(":");

            string fromS = getTimeString(getTime(froms)+100);

            string toS = getTimeString(getTime(tos) + 100);

            this.timestamp = fromS + " --> " + toS;
        }
        public void subtract()
        {
            string[] times = timestamp.Split(" --> ");
            string from = times[0];
            string to = times[1];
            //00:00:34,425 --> 00:00:35,992
            string[] froms = from.Split(":");
            string[] tos = to.Split(":");

            string fromS = getTimeString(getTime(froms) - 100);

            string toS = getTimeString(getTime(tos) - 100);

            this.timestamp = fromS + " --> " + toS;
        }
        public long getTime(string [] times)
        {
            //00:00:35,992
            //h : m : s , ms
            Console.WriteLine(times.ToString());
            long h = Convert.ToInt64(times[0]);
            long m = Convert.ToInt64(times[1]);
            string[] sms = times[2].Split(",");
            long s = Convert.ToInt64(sms[0]);
            long ms = Convert.ToInt64(sms[1]);
            m = m * 60 * 1000;
            h = h * 60 * 60 * 1000;
            s = s * 1000;
            return h + m + s + ms;
        }
        public string getTimeString(long time)
        {
            if (time <= 0)
                time = 0;
            long ms = time % 1000;
            time /= 1000;
            long s = time % 60;
            time /= 60;
            long m = time % 60;
            long h = time / 60;
            return h.ToString("00") + ":" + m.ToString("00") + ":" + s.ToString("00") + "," + ms.ToString("000");
        }
    }
}
