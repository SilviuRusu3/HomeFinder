using HomeFinder.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace HomeFinder.HelpClass
{
    public class HomeComparer : IComparer<Home>
    {
        
        //Class used with Sort to sort Homes in reverse order by GeneralGrade
        public int Compare([AllowNull] Home firstHome, [AllowNull] Home secondHome)
        {
            if (firstHome.GeneralGrade == null)
            {
                if (secondHome.GeneralGrade == null)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                if (secondHome.GeneralGrade == null)
                {
                    return -1;
                }
                else
                {
                    double gradeDifference = (double)firstHome.GeneralGrade - (double)secondHome.GeneralGrade;
                    if (gradeDifference > 0)
                    {
                        return -1;
                    }
                    else if (gradeDifference < 0)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }
    }
}
