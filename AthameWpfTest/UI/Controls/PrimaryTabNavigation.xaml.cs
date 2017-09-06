using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AthameWPF.UI.Controls
{
    /// <summary>
    /// Interaction logic for SlideTabNavigation.xaml
    /// </summary>
    [ContentProperty(nameof(Tabs))]
    public partial class PrimaryTabNavigation
    {
        private const int SwitcherAnimTimeMs = 200;
        private PathToggleButton activeTab;

        public PrimaryTabNavigation()
        {
            InitializeComponent();
            Tabs = TabsPanel.Children;
        }

        public static readonly DependencyProperty TabsProperty = DependencyProperty.Register(
            "Tabs", typeof(UIElementCollection), typeof(PrimaryTabNavigation), new PropertyMetadata(default(UIElementCollection)));

        public UIElementCollection Tabs
        {
            get { return (UIElementCollection) GetValue(TabsProperty); }
            private set { SetValue(TabsProperty, value); }
        }

        public static readonly DependencyProperty ActiveTabProperty = DependencyProperty.Register(
            "ActiveTab", typeof(PathToggleButton), typeof(PrimaryTabNavigation), new PropertyMetadata(default(PathToggleButton)));

        public PathToggleButton ActiveTab
        {
            get { return (PathToggleButton) GetValue(ActiveTabProperty); }
            set { SetValue(ActiveTabProperty, value); }
        }

        private static double GetWidthIncludingMargin(FrameworkElement element)
        {
            return element.ActualWidth + element.Margin.Left + element.Margin.Right;
        }

        private void SwitcherButtonOnChecked(object sender, RoutedEventArgs e)
        {
            var radioButton = (PathToggleButton)sender;
            activeTab = radioButton;
            LineUpIndicatorToButton(true);
        }

        private void LineUpIndicatorToButton(bool doAnimation)
        {
            if (MainSwitcherSelectionIndicator == null || activeTab == null) return;
            var widthIncludingMargin = GetWidthIncludingMargin(activeTab);
            var parent = VisualTreeHelper.GetParent(activeTab) as FrameworkElement;
            // Left margin = X position of selected radio button relative to parent
            var leftMargin = activeTab.TranslatePoint(new Point(0, 0), parent).X;

            // Before we animated the indicator's margin, we need to align it to the radio button's hori alignment
            // Before we do that, however, we need to calculate the empty space in the parent to the left and right of the indicator,
            // so that when we set the alignment it doesn't align itself before we begin the transition
            var relLeft = MainSwitcherSelectionIndicator.TranslatePoint(new Point(0, 0), parent).X;
            var preparedMargin = new Thickness(relLeft, 0, parent.ActualWidth - (relLeft + MainSwitcherSelectionIndicator.Width), 0);
            MainSwitcherSelectionIndicator.Margin = preparedMargin;
            MainSwitcherSelectionIndicator.HorizontalAlignment = activeTab.HorizontalAlignment;

            // Calculate the new margin to animate to.
            var rightMargin = parent.ActualWidth - (leftMargin + MainSwitcherSelectionIndicator.Width);
            var newMargin = new Thickness(leftMargin, 0, rightMargin, 0);

            if (doAnimation)
            {
                var widthAnimation = new DoubleAnimation(widthIncludingMargin,
                    TimeSpan.FromMilliseconds(SwitcherAnimTimeMs))
                {
                    EasingFunction = new CubicEase()
                };

                var marginAnimation = new ThicknessAnimation(newMargin,
                    TimeSpan.FromMilliseconds(SwitcherAnimTimeMs), FillBehavior.Stop)
                {
                    EasingFunction = new CubicEase()
                };
                marginAnimation.Completed += (sender, args) =>
                {

                    ResetIndicatorMargin(newMargin);
                };
                MainSwitcherSelectionIndicator.BeginAnimation(WidthProperty, widthAnimation);
                MainSwitcherSelectionIndicator.BeginAnimation(MarginProperty, marginAnimation);

            }
            else
            {
                MainSwitcherSelectionIndicator.Width = widthIncludingMargin;
                MainSwitcherSelectionIndicator.Margin = newMargin;
                ResetIndicatorMargin(newMargin);
            }
        }

        private void ResetIndicatorMargin(Thickness target)
        {

            MainSwitcherSelectionIndicator.HorizontalAlignment = activeTab.HorizontalAlignment;
            if (MainSwitcherSelectionIndicator.HorizontalAlignment == HorizontalAlignment.Right)
            {
                MainSwitcherSelectionIndicator.Margin = new Thickness(0, 0,
                    target.Right, 0);
            }
            else
            {
                MainSwitcherSelectionIndicator.Margin = new Thickness(target.Left, 0, 0, 0);
            }
        }

        private void MainSwitcherSelectionIndicator_Loaded(object sender, RoutedEventArgs e)
        {
            LineUpIndicatorToButton(false);
        }
    }
}
