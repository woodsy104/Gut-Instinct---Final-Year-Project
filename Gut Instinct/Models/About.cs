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
    public string Message => "The aim of this app is to be an aid to people with Crohn's disease." +
        " Add your appointments, notes and foods to the personal sections of the app. " +
        "In the forum and toilet tracker you can see what has been submitted by others. " +
        "The toilet tracker displays toilets near you and you can answer or ask questions in the user forum!";
}
