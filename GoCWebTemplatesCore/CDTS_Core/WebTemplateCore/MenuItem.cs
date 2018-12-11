﻿using System.Collections.Generic;

namespace CDTS_Core.WebTemplateCore
{
    public class MenuItem : Link
    {
        public bool OpenInNewWindow
        {
            get;
            set;
        }

        public List<MenuItem> SubItems
        {
            get;
            set;
        }

        public MenuItem()
        {
            SubItems = new List<MenuItem>();
            OpenInNewWindow = false;
        }

        public MenuItem(string href, string text)
            : base(href, text)
        {
            SubItems = new List<MenuItem>();
            OpenInNewWindow = false;
        }

        public MenuItem(string href, string text, MenuItem[] subItems)
            : base(href, text)
        {
            SubItems = new List<MenuItem>(subItems);
            OpenInNewWindow = false;
        }

        public MenuItem(string href, string text, bool openInNewWindow)
            : base(href, text)
        {
            SubItems = new List<MenuItem>();
            OpenInNewWindow = openInNewWindow;
        }

        public MenuItem(string href, string text, bool openInNewWindow, MenuItem[] subItems)
            : base(href, text)
        {
            SubItems = new List<MenuItem>(subItems);
            OpenInNewWindow = openInNewWindow;
        }
    }

}