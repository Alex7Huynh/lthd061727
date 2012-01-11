using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SumUpApp.RSS
{
    class Article
    {
        string _date;

        public string Date
        {
            get { return _date; }
            set { _date = value; }
        }
        string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        string _content;

        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }
        string _image;

        public string Image
        {
            get { return _image; }
            set { _image = value; }
        }
        string _url;

        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }
    }
}
