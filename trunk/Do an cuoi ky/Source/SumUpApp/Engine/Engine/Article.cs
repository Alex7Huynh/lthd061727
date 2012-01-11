using System;

namespace SumUpApp.Util.Engine
{
    public class Article
    {
        private string title;
        private string link;
        private string description;
        private string source;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        public string Link
        {
            get { return link; }
            set { link = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public string Source
        {
            get { return source; }
            set { source = value; }
        }

        public Article()
        {
            title = String.Empty;
        }
        public Article(string aTitle, string aLink, string aDescription, string aSource)
        {
            title = aTitle;
            link = aLink;
            description = aDescription;
            source = aSource;
        }
    }
}