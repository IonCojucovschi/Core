using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Core.Android.MicrosoftServices.MicrosoftModels
{
   public class AnalisisVisionResultModel
    {
        public Description description { get; set; }
        public Metadata metadata { get; set; }
        public string requestId { get; set; }

    }

    public class Metadata
    {
        public string format { get; set; }
        public int height { get; set; }
        public int width { get; set; }
    }

    public class Description
    {
        public List<Caption> captions { get; set; }
        public List<string> tags { get; set; }

    }

    public class Caption
    {
        public double confidence { get; set; }
        public string text { get; set; }
    }
}