using EduOMO.Base.Dto;
using EduOMO.Data.Base;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Xml;
using System.Xml.Serialization;

namespace EduOMO.Base;

public static class GenerateSiteMapHelper
{
    public static string POSTNAVIGATION = "post"; 
    public static string QUESTIONNAVIGATION = "question"; 
    public static string GetSiteMap(List<SitemapIndexNode> MyList)
    {
        XmlDocument doc = new XmlDocument();// create XML Documents
        XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null, null);// Create the head element
        doc.AppendChild(dec);

        XmlElement root = doc.CreateElement("sitemapindex");// Create the root element with attributes
        root.SetAttribute("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9");
        doc.AppendChild(root);

        foreach (var item in MyList)
        {
            XmlElement MySiteMap = doc.CreateElement("sitemap");

            XmlElement Loc = doc.CreateElement("loc");
            XmlElement Mod = doc.CreateElement("lastmod");

            Loc.InnerText = item.Loc;
            Mod.InnerText = item.LastMod.ToString("yyyy-MM-ddTHH:mm:ss+00:00");

            MySiteMap.AppendChild(Loc);
            MySiteMap.AppendChild(Mod);


            root.AppendChild(MySiteMap);
        }

        return doc.OuterXml;
    }

    public static string GetSiteMapRoot(string request)
    {
        List<string> MyList = new List<string>
        {
            string.Concat(request, "/", POSTNAVIGATION),
            string.Concat(request, "/", QUESTIONNAVIGATION),
        };

        XmlDocument doc = new XmlDocument();
        XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null, null);
        doc.AppendChild(dec);

        XmlElement root = doc.CreateElement("urlset");
        root.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
        root.SetAttribute("xmlns:xsd", "http://www.w3.org/2001/XMLSchema");
        root.SetAttribute("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9");
        doc.AppendChild(root);

        #region add priority 1.0
        XmlElement Url1 = doc.CreateElement("url");

        XmlElement Loc1 = doc.CreateElement("loc");
        XmlElement Freq1 = doc.CreateElement("changefreq");
        XmlElement Pri1 = doc.CreateElement("priority");

        Loc1.InnerText = request;
        Freq1.InnerText = "hourly";
        Pri1.InnerText = "1.0";

        Url1.AppendChild(Loc1);
        Url1.AppendChild(Freq1);
        Url1.AppendChild(Pri1);

        root.AppendChild(Url1);
        #endregion

        foreach (var item in MyList)
        {
            XmlElement MyUrl = doc.CreateElement("url");

            XmlElement Loc = doc.CreateElement("loc");
            XmlElement Freq = doc.CreateElement("changefreq");
            XmlElement Pri = doc.CreateElement("priority");

            Loc.InnerText = item;
            Freq.InnerText = "daily";
            Pri.InnerText = "0.85";

            MyUrl.AppendChild(Loc);
            MyUrl.AppendChild(Freq);
            MyUrl.AppendChild(Pri);


            root.AppendChild(MyUrl);
        }

        return doc.OuterXml;
    }

    public static string GetSiteMapElement<T>(string request, IEnumerable<T> MyList) where T : FullTrackEntity
    {
        /// Add namespace:
        ///using System.Xml;
        ///using System.Data;
        ///using System.Xml.Serialization;


        // create DataSet and DataTable if needed, or use List Array only
        /*
             DataTable DT = new DataTable();
             foreach (var array in MyList)
             {
                 DT.Rows.Add(array);
             }

         */
        XmlDocument doc = new XmlDocument();// create XML Documents
        XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null, null);// Create the head element
        doc.AppendChild(dec);

        XmlElement root = doc.CreateElement("urlset");// Create the root element with attributes
                                                      // default
        root.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
        root.SetAttribute("xmlns:xsd", "http://www.w3.org/2001/XMLSchema");
        root.SetAttribute("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9");
        doc.AppendChild(root);

        //foreach (DataRow DR in DT.Rows)// if use DataTable or
        foreach (var obj in MyList)// if use List only
        {
            XmlElement MyUrl = doc.CreateElement("url");//create child node for root node

            XmlElement Loc = doc.CreateElement("loc");//create child node for MyUrl node
            XmlElement LastMod = doc.CreateElement("lastmod");//create child node for MyUrl node
            XmlElement Freq = doc.CreateElement("changefreq");//create child node for MyUrl node
            XmlElement Pri = doc.CreateElement("priority");//create child node for MyUrl node
                                                           // create them element for <lastmod>2023-07-26</lastmod>

            Loc.InnerText = request + "/" + obj.Slug;//set value for 1. child node  Loc node
            LastMod.InnerText = obj.UpdatedAt.ToString("yyyy-MM-ddTHH:mm:ss+00:00"); ;//set value for 1. child node-Freq node
            Freq.InnerText = "hourly";//set value for 1. child node-Freq node
            Pri.InnerText = "0.85";//set value for 1. child node-Pri node

            MyUrl.AppendChild(Loc); //add child Loc node to MyUrl node
            MyUrl.AppendChild(LastMod); //add child Loc node to MyUrl node
            MyUrl.AppendChild(Freq);//add child Freq node to MyUrl node
            MyUrl.AppendChild(Pri);//add child Pri node to MyUrl node


            root.AppendChild(MyUrl);//add child MyUrl node to root node
        }

        return doc.OuterXml;
        //Response.Clear();
        //XmlSerializer xs = new XmlSerializer(typeof(XmlDocument));
        //Response.ContentType = "text/xml";
        //xs.Serialize(Response.Output, doc);
        //Response.End();
    }
}
