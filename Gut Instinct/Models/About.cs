using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gut_Instinct.Models;

internal class About
{
    public string Title => AppInfo.Name;
    public string Version => AppInfo.VersionString;
    public string MoreInfoUrl => "https://crohnscolitis.ie/";
    public string Message => "Filler text about me here...........";
}
