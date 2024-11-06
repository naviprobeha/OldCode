using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Navipro.Backoffice.Web.Models
{
    public class ProfileMenuItem
    {
        public string caption { get; set; }
        public string controller { get; set; }
        public string action { get; set; }
        public string parameters { get; set; }

        public string icon { get; set; }

        public List<ProfileMenuItem> subMenuItems;

        public ProfileMenuItem(string caption, string controller, string action, string parameters, string icon)
        {
            this.caption = caption;
            this.controller = controller;
            this.action = action;
            this.parameters = parameters;
            this.icon = icon;
            subMenuItems = new List<ProfileMenuItem>();
        }
    }
}