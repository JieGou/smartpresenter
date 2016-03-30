using SmartPresenter.BO.Common.Enums;
using SmartPresenter.UI.Controls.SlideShow;
using SmartPresenter.UI.RenderEngine.WPF.ViewModel;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace SmartPresenter.UI.RenderEngine.WPF
{
    /// <summary>
    /// Interaction logic for OutputWindow.xaml
    /// </summary>
    public partial class OutputWindow : Window
    {
        /// <summary>
        /// The view model.
        /// </summary>
        private OutputWindowViewModel _viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="OutputWindow" /> class.
        /// </summary>
        public OutputWindow()
        {
            InitializeComponent();

            DataContext = _viewModel = new OutputWindowViewModel();
            _viewModel.PropertyChanged += ViewModel_PropertyChanged;            
        }

        void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "CurrentSlide":
                    if (_viewModel.CurrentSlide != null)
                    {
                        Storyboard storyboard = null;
                        switch (_viewModel.CurrentSlide.Transition.Type)
                        {
                            case TransitionTypes.None:
                                storyboard = FindResource("NoneTransition") as Storyboard;
                                break;
                            case TransitionTypes.Cut:
                                //storyboard = FindResource("CutTransition") as Storyboard;
                                break;
                            case TransitionTypes.Fade:
                                storyboard = FindResource("FadeTransition") as Storyboard;
                                break;
                            case TransitionTypes.Push:
                                storyboard = FindResource("PushTransition") as Storyboard;
                                if (_viewModel.CurrentSlide.Transition.TransitionOption == TransitionOptions.From_Top)
                                {
                                    ((ThicknessAnimation)storyboard.Children[0]).From = new Thickness(0);
                                    ((ThicknessAnimation)storyboard.Children[0]).To = new Thickness(0, _viewModel.CurrentSlide.Height, 0, 0);

                                    ((ThicknessAnimation)storyboard.Children[1]).From = new Thickness(0, _viewModel.CurrentSlide.Height * -1, 0, 0);
                                    ((ThicknessAnimation)storyboard.Children[1]).To = new Thickness(0);
                                }
                                if (_viewModel.CurrentSlide.Transition.TransitionOption == TransitionOptions.From_Bottom)
                                {
                                    ((ThicknessAnimation)storyboard.Children[0]).From = new Thickness(0);
                                    ((ThicknessAnimation)storyboard.Children[0]).To = new Thickness(0, _viewModel.CurrentSlide.Height * -1, 0, 0);

                                    ((ThicknessAnimation)storyboard.Children[1]).From = new Thickness(0, _viewModel.CurrentSlide.Height, 0, 0);
                                    ((ThicknessAnimation)storyboard.Children[1]).To = new Thickness(0);
                                }
                                if (_viewModel.CurrentSlide.Transition.TransitionOption == TransitionOptions.From_Left)
                                {
                                    ((ThicknessAnimation)storyboard.Children[0]).From = new Thickness(0);
                                    ((ThicknessAnimation)storyboard.Children[0]).To = new Thickness(_viewModel.CurrentSlide.Width, 0, 0, 0);

                                    ((ThicknessAnimation)storyboard.Children[1]).From = new Thickness(_viewModel.CurrentSlide.Width * -1, 0, 0, 0);
                                    ((ThicknessAnimation)storyboard.Children[1]).To = new Thickness(0);
                                }
                                if (_viewModel.CurrentSlide.Transition.TransitionOption == TransitionOptions.From_Right)
                                {
                                    ((ThicknessAnimation)storyboard.Children[0]).From = new Thickness(0);
                                    ((ThicknessAnimation)storyboard.Children[0]).To = new Thickness(_viewModel.CurrentSlide.Width * -1, 0, 0, 0);

                                    ((ThicknessAnimation)storyboard.Children[1]).From = new Thickness(_viewModel.CurrentSlide.Width, 0, 0, 0);
                                    ((ThicknessAnimation)storyboard.Children[1]).To = new Thickness(0);
                                }
                                break;
                            case TransitionTypes.Wipe:
                                storyboard = FindResource("WipeTransition") as Storyboard;
                                break;
                            case TransitionTypes.Split:
                                storyboard = FindResource("SplitTransition") as Storyboard;
                                break;
                            case TransitionTypes.Reveal:
                                storyboard = FindResource("RevealTransition") as Storyboard;
                                break;
                            case TransitionTypes.RandomBars:
                                storyboard = FindResource("RandomBarsTransition") as Storyboard;
                                break;
                            case TransitionTypes.Shape:
                                storyboard = FindResource("ShapeTransition") as Storyboard;
                                break;
                            case TransitionTypes.Uncover:
                                storyboard = FindResource("UncoverTransition") as Storyboard;
                                break;
                            case TransitionTypes.Cover:
                                storyboard = FindResource("CoverTransition") as Storyboard;
                                break;
                            case TransitionTypes.Flash:
                                storyboard = FindResource("FlashTransition") as Storyboard;
                                break;
                            case TransitionTypes.Dissolve:
                                storyboard = FindResource("DissolveTransition") as Storyboard;
                                break;
                        }
                        if (storyboard != null)
                        {
                            foreach (Timeline animation in storyboard.Children)
                            {
                                animation.Duration = TimeSpan.FromSeconds(_viewModel.CurrentSlide.Transition.Duration);
                            }
                            storyboard.Begin();
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// Handles the PreviewKeyDown event of the Window control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    SlideShowManager.Instance.StopSlideShow();
                    this.Hide();
                    break;
                case Key.Left:
                case Key.Up:
                    SlideShowManager.Instance.PlayPreviousSlide();
                    break;
                case Key.Right:
                case Key.Down:
                    SlideShowManager.Instance.PlayNextSlide();
                    break;
            }
        }
    }
}
