using System;
using Android.Content;
using Android.Util;
using Android.Views;
using Android.Views.Animations;

namespace Core.Android.Animations
{
#pragma warning disable XA0001 // Find issues with Android API usage
    public class FitToParentAnimation : Animation
#pragma warning restore XA0001 // Find issues with Android API usage
    {
        private int _childInitialWidth;
        private int _deltaWidth;
        private bool _hasToExpand;
        private int _parentInitialWidth;
        private View _resizableView;
        private View _resizableViewParentView;
        private bool _viewsInitialStatesFixed;

        public FitToParentAnimation(View resizableView)
        {
            InitViews(resizableView);
        }

        public FitToParentAnimation(View resizableView, Context context, IAttributeSet attrs) : base(context, attrs)
        {
            InitViews(resizableView);
        }

        private int ChildMeasuredWidth => _resizableView?.MeasuredWidth ?? 0;
        private int ParentMeasuredWidth => _resizableViewParentView?.MeasuredWidth ?? 0;

        public event EventHandler OnPostAnimationExecuted;

        private void InitViews(View childView)
        {
            _resizableView = childView;
            _resizableViewParentView = childView.Parent as View;
        }

        private void FixViewsInitialState()
        {
            _childInitialWidth = ChildMeasuredWidth;
            _parentInitialWidth = ParentMeasuredWidth;
            if (_parentInitialWidth == 0)
                throw new Exception("ParentView was not found.");
            _deltaWidth = Math.Abs(_parentInitialWidth - _childInitialWidth);
            _hasToExpand = _parentInitialWidth > _childInitialWidth;
            _viewsInitialStatesFixed = true;
        }


#pragma warning disable XA0001 // Find issues with Android API usage
        protected override void ApplyTransformation(float interpolatedTime, Transformation t)
#pragma warning restore XA0001 // Find issues with Android API usage
        {
            if (!_viewsInitialStatesFixed)
                FixViewsInitialState();
            t.TransformationType = TransformationTypes.Matrix;
            if (_hasToExpand)
                ExpandView(interpolatedTime);
            else
                CollapseView(interpolatedTime);
            _resizableView.RequestLayout();
            if (!HasEnded) return;
            _hasToExpand = !_hasToExpand;
            OnPostAnimationExecuted?.Invoke(_resizableView, null);
        }

        private void ExpandView(float interpolatedTime)
        {
            var viewGroup = _resizableView as ViewGroup;
            if (viewGroup != null)
                viewGroup.LayoutParameters.Width = _childInitialWidth + (int)(_deltaWidth * interpolatedTime);
        }

        private void CollapseView(float interpolatedTime)
        {
            var viewGroup = _resizableView as ViewGroup;
            if (viewGroup != null)
                viewGroup.LayoutParameters.Width = _parentInitialWidth - (int)(_deltaWidth * interpolatedTime);
        }

        public override bool WillChangeBounds()
        {
            return true;
        }
    }
}