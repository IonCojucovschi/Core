using Android.Views;
using Android.Views.Animations;
namespace Core.Android.Animations
{
    public static class CirclesFadeInOutAnimation
    {
        public static void AnimateViews(int duration = 300, params View[] views)
        {
            var fadeInAnims = new AlphaAnimation[views.Length];
            var fadeOutAnims = new AlphaAnimation[views.Length];
            for (var i = 0; i < views.Length; i++)
            {
                fadeInAnims[i] = new AlphaAnimation(0.0f, 1.0f);
                fadeOutAnims[i] = new AlphaAnimation(1.0f, 0.0f);

                fadeInAnims[i].Duration = duration;
                fadeOutAnims[i].Duration = duration;

                fadeInAnims[i].FillAfter = true;
                fadeOutAnims[i].FillAfter = true;
                views[i].Tag = i;
            }

            for (var i = 0; i < views.Length; i++)
            {
                if (i == 0)
                {
                    fadeInAnims[0].AnimationEnd += (sender, e) => { views[0].StartAnimation(fadeOutAnims[0]); };
                }
                else
                {
                    var pos = (int)views[i].Tag;
                    fadeInAnims[pos].AnimationEnd += (sender, e) =>
                    {
                        views[pos - 1].StartAnimation(fadeInAnims[pos - 1]);
                    };
                }

                if (i == views.Length - 1)
                {
                    fadeOutAnims[views.Length - 1].AnimationEnd += (sender, e) =>
                    {
                        views[views.Length - 1].StartAnimation(fadeInAnims[views.Length - 1]);
                    };
                }
                else
                {
                    var pos = (int)views[i].Tag;
                    fadeOutAnims[pos].AnimationEnd += (sender, e) =>
                    {
                        views[pos + 1].StartAnimation(fadeOutAnims[pos + 1]);
                    };
                }

                views[0].StartAnimation(fadeOutAnims[0]);
            }
        }
    }
}