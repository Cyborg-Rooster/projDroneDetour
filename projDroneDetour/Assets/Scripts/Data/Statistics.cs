using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Statistics
{
    public static int BestScore = 0;
    public static int Death = 0;
    public static int Falls = 0;
    public static int Crashes = 0;
    public static int Clicks = 0;

    public static void ReturntoDefault()
    {
        BestScore = 0;
        Death = 0;
        Falls = 0;
        Crashes = 0;
        Clicks = 0;
    }
}
