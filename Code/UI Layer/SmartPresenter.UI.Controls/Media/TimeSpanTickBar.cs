using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace SmartPresenter.UI.Controls.Media
{
    /// <summary>
    /// A Tickbar to show time on a timeline.
    /// </summary>
    public class TimeSpanTickBar : TickBar
    {
        /// <summary>
        /// Draws the tick marks for a <see cref="T:System.Windows.Controls.Slider" /> control.
        /// </summary>
        /// <param name="dc">The <see cref="T:System.Windows.Media.DrawingContext" /> that is used to draw the ticks.</param>
        protected override void OnRender(DrawingContext dc)
        {
            Size size = new Size(this.ActualWidth, this.ActualHeight);
            int oneTickLength = (int)(this.ActualWidth / 75);

            int tickCount = (int)((this.Maximum - this.Minimum) / this.TickFrequency);
            tickCount = tickCount > oneTickLength ? oneTickLength : tickCount;
            int tickInterval = (int)((this.Maximum - this.Minimum) / tickCount);

            Double tickFrequencySize;
            // Calculate tick's setting
            tickFrequencySize = (size.Width / tickInterval);
            string text = "";
            FormattedText formattedText = null;
            double num = this.Maximum - this.Minimum;
            double xTick = 0;
            int count = -1;
            // Draw each tick text
            for (int i = 0; i <= num; i += (tickInterval / 10))
            {
                if (count == 9)
                {
                    count = 0;
                    if (i != 0 && i != num)
                    {
                        text = GetTimeText(text, i);
                        formattedText = new FormattedText(text, CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, new Typeface("Verdana"), 8, Brushes.Black);
                        formattedText.TextAlignment = TextAlignment.Center;
                        dc.DrawText(formattedText, new Point(xTick, 3));
                        dc.DrawLine(new Pen(Brushes.Black, 1), new Point(xTick, 15), new Point(xTick, 25));
                        xTick += size.Width / (num / (tickInterval / 10));
                    }
                }
                else
                {
                    dc.DrawLine(new Pen(Brushes.Black, 1), new Point(xTick, 20), new Point(xTick, 25));
                    xTick += size.Width / (num / (tickInterval / 10));
                    count++;
                }
            }
        }

        /// <summary>
        /// Gets the time text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="i">The i.</param>
        /// <returns></returns>
        private string GetTimeText(string text, int i)
        {
            TimeSpan time = TimeSpan.FromSeconds(i + this.Minimum);
            if (time.TotalSeconds < 60)
            {
                text = Convert.ToString(Convert.ToInt32(i), 10);
            }
            else if (time.TotalSeconds > 59 && time.TotalSeconds < 3600)
            {
                if (time.Minutes > 9)
                {
                    text = time.Minutes.ToString("00") + ":" + time.Seconds.ToString("00");
                }
                else
                {
                    text = time.Minutes.ToString("0") + ":" + time.Seconds.ToString("00");
                }
            }
            else if (time.TotalSeconds > 3599)
            {
                if (time.Hours > 9)
                {
                    text = time.Hours.ToString("00") + ":" + time.Minutes.ToString("00") + ":" + time.Seconds.ToString("00");
                }
                else
                {
                    text = time.Hours.ToString("0") + ":" + time.Minutes.ToString("00") + ":" + time.Seconds.ToString("00");
                }
            }
            return text;
        }
    }
}
