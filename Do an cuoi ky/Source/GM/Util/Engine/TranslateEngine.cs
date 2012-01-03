using System.Collections.Generic;
using System.Net;
using System.Xml;
using System.Text;

namespace CaroSocialNetwork.Util.Engine
{
    public class TranslateEngine
    {
        public static string Translate(string aContent, string aSource, string aDes)
        {            
            string url = "http://api.microsofttranslator.com/v2/Http.svc/Translate?appId="
                + KeyManager.BingID + "&text=" + aContent + "&from=" + aSource + "&to=" + aDes;
            WebClient c = new WebClient();
            string resultXML = Encoding.UTF8.GetString(c.DownloadData(url)).Replace("\r\n", "").Trim();
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(resultXML);
            string result = xDoc.DocumentElement.ChildNodes[0].Value;
            return result;
        }
        public static Dictionary<string, string> InitLanguage()
        {
            Dictionary<string, string> language = new Dictionary<string, string>();
            language.Add("English", "en");
            language.Add("Vietnamese", "vi");
            language.Add("Simplified Chinese", "zh-CHS");
            language.Add("Traditional Chinese", "zh-CHT");
            language.Add("Arabic", "ar");
            language.Add("Czech", "cs");
            language.Add("Danish", "da");
            language.Add("German", "de");
            language.Add("Estonian", "et");
            language.Add("Finnish", "fi");
            language.Add("French", "fr");
            language.Add("Dutch", "nl");
            language.Add("Greek", "el");
            language.Add("Hebrew", "he");
            language.Add("Haitian Creole", "ht");
            language.Add("Hungarian", "hu");
            language.Add("Indonesian", "id");
            language.Add("Italian", "it");
            language.Add("Japanese", "ja");
            language.Add("Korean", "ko");
            language.Add("Lithuanian", "lt");
            language.Add("Latvian", "lv");
            language.Add("Norwegian", "no");
            language.Add("Polish", "pl");
            language.Add("Portuguese", "pt");
            language.Add("Romanian", "ro");
            language.Add("Spanish", "es");
            language.Add("Russian", "ru");
            language.Add("Slovak", "sk");
            language.Add("Slovene", "sl");
            language.Add("Swedish", "sv");
            language.Add("Thai", "th");
            language.Add("Turkish", "tr");
            language.Add("Ukrainian", "uk");

            return language;
        }
    }
}