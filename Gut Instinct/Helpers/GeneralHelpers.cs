using Microsoft.Maui.Graphics.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gut_Instinct.Helpers
{
    public static class GeneralHelpers
    {
        public static string UpperCaseFirst(string s) {
            if (string.IsNullOrEmpty(s)){
                return string.Empty;
            }
            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }
    }
    
}
