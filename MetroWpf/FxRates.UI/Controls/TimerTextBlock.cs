using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

namespace FxRates.UI.Controls
{
    public delegate void Invoker();

    public class TimerTextBlock : TextBlock
    {
        //private static Timer _UpdateTimer = new Timer(UpdateTimer, null, 1000, 1000);

        public static readonly DependencyProperty TimeSpanProperty =
            DependencyProperty.Register("TimeSpan", typeof (TimeSpan), typeof (TimerTextBlock),
                                        new UIPropertyMetadata(TimeSpan.Zero));

        public static readonly DependencyProperty IsStartedProperty =
            DependencyProperty.Register("IsStarted", typeof (bool), typeof (TimerTextBlock),
                                        new UIPropertyMetadata(false));

        public static readonly DependencyProperty TimeFormatProperty =
            DependencyProperty.Register("TimeFormat", typeof (string), typeof (TimerTextBlock),
                                        new UIPropertyMetadata(null));

        public static readonly DependencyProperty IsCountDownProperty =
            DependencyProperty.Register("IsCountDown", typeof (bool), typeof (TimerTextBlock),
                                        new UIPropertyMetadata(false));

        private Invoker _UpdateTimeInvoker;

        public TimerTextBlock()
        {
            Init();
        }

        public TimerTextBlock(Inline inline) : base(inline)
        {
            Init();
        }

        /// <summary>
        /// Represents the time remaining for the count down to complete if
        /// the control is initialized as a count down timer otherwise, it 
        /// represents the time elapsed since the timer has started.
        /// </summary>
        /// <exception cref="System.ArgumentException">
        /// </exception>
        public TimeSpan TimeSpan
        {
            get { return (TimeSpan) GetValue(TimeSpanProperty); }
            set
            {
                if (value < TimeSpan.Zero)
                    throw new ArgumentException();

                SetValue(TimeSpanProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for TimeSpan.  This enables animation, styling, binding, etc...

        public bool IsStarted
        {
            get { return (bool) GetValue(IsStartedProperty); }
            set { SetValue(IsStartedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsStarted. This enables animation, styling, binding, etc...

        /// <summary>
        /// Format string for displaying the time span value.
        /// </summary>
        public string TimeFormat
        {
            get { return (string) GetValue(TimeFormatProperty); }
            set { SetValue(TimeFormatProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TimeFormat. This enables animation, styling, binding, etc...

        /// <summary>
        /// Represents whether the control is a CountDown or regular timer.
        /// </summary>
        public bool IsCountDown
        {
            get { return (bool) GetValue(IsCountDownProperty); }
            set { SetValue(IsCountDownProperty, value); }
        }

        public event EventHandler OnCountDownComplete;

        private static event EventHandler OnTick;

        private void Init()
        {
            Loaded += TimerTextBlock_Loaded;
            Unloaded += TimerTextBlock_Unloaded;
        }

        ~TimerTextBlock()
        {
            Dispose();
        }

        public void Dispose()
        {
            OnTick -= TimerTextBlock_OnTick;
        }

        // Using a DependencyProperty as the backing store for IsCountDown.  This enables animation, styling, binding, etc...

        /// <summary>
        /// Sets the time span to zero and stops the timer.
        /// </summary>
        public void Reset()
        {
            IsStarted = false;
            TimeSpan = TimeSpan.Zero;
        }

        private static void UpdateTimer(object state)
        {
            EventHandler onTick = OnTick;
            if (onTick != null)
                onTick(null, EventArgs.Empty);
        }

        private void TimerTextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            var binding = new Binding("TimeSpan");
            binding.Source = this;
            binding.Mode = BindingMode.OneWay;
            binding.StringFormat = TimeFormat;

            SetBinding(TextProperty, binding);

            _UpdateTimeInvoker = UpdateTime;

            OnTick += TimerTextBlock_OnTick;
        }

        private void TimerTextBlock_Unloaded(object sender, RoutedEventArgs e)
        {
            OnTick -= TimerTextBlock_OnTick;
        }

        private void TimerTextBlock_OnTick(object sender, EventArgs e)
        {
            Dispatcher.Invoke(_UpdateTimeInvoker);
        }

        private void UpdateTime()
        {
            if (IsStarted)
            {
                TimeSpan step = TimeSpan.FromSeconds(1);
                if (IsCountDown)
                {
                    if (TimeSpan >= TimeSpan.FromSeconds(1))
                    {
                        TimeSpan -= step;
                        if (TimeSpan.TotalSeconds <= 0)
                        {
                            TimeSpan = TimeSpan.Zero;
                            IsStarted = false;
                            NotifyCountDownComplete();
                        }
                    }
                }
                else
                {
                    TimeSpan += step;
                }
            }
        }

        private void NotifyCountDownComplete()
        {
            EventHandler handler = OnCountDownComplete;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}