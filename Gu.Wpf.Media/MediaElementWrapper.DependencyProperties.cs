namespace Gu.Wpf.Media
{
    using System;
    using System.Windows;

    /// <summary>
    /// Dependency properties.
    /// </summary>
    public partial class MediaElementWrapper
    {
        /// <summary>Identifies the <see cref="State"/> dependency property.</summary>
        public static readonly DependencyProperty StateProperty = DependencyProperty.Register(
            nameof(State),
            typeof(MediaState),
            typeof(MediaElementWrapper),
            new FrameworkPropertyMetadata(
                MediaState.Stop,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnStateChanged));

        /// <summary>Identifies the <see cref="IsPlaying"/> dependency property.</summary>
        public static readonly DependencyProperty IsPlayingProperty = DependencyProperty.Register(
            nameof(IsPlaying),
            typeof(bool),
            typeof(MediaElementWrapper),
            new FrameworkPropertyMetadata(
                default(bool),
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnIsPlayingChanged));

        /// <summary>Identifies the <see cref="Position"/> dependency property.</summary>
        public static readonly DependencyProperty PositionProperty = DependencyProperty.Register(
            nameof(Position),
            typeof(TimeSpan?),
            typeof(MediaElementWrapper),
            new FrameworkPropertyMetadata(
                default(TimeSpan?),
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnPositionChanged,
                CoercePosition));

        private static readonly DependencyPropertyKey LengthPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(Length),
            typeof(TimeSpan?),
            typeof(MediaElementWrapper),
            new PropertyMetadata(default(TimeSpan?)));

        /// <summary>Identifies the <see cref="Length"/> dependency property.</summary>
        public static readonly DependencyProperty LengthProperty = LengthPropertyKey.DependencyProperty;

        private static readonly DependencyPropertyKey CanPausePropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(CanPause),
            typeof(bool?),
            typeof(MediaElementWrapper),
            new PropertyMetadata(default(bool?)));

        /// <summary>Identifies the <see cref="CanPause"/> dependency property.</summary>
        public static readonly DependencyProperty CanPauseProperty = CanPausePropertyKey.DependencyProperty;

        private static readonly DependencyPropertyKey NaturalVideoHeightPropertyKey = DependencyProperty.RegisterAttachedReadOnly(
            nameof(NaturalVideoHeight),
            typeof(int?),
            typeof(MediaElementWrapper),
            new PropertyMetadata(default(int?)));

        /// <summary>Identifies the <see cref="NaturalVideoHeight"/> dependency property.</summary>
        public static readonly DependencyProperty NaturalVideoHeightProperty = NaturalVideoHeightPropertyKey.DependencyProperty;

        private static readonly DependencyPropertyKey NaturalVideoWidthPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(NaturalVideoWidth),
            typeof(int?),
            typeof(MediaElementWrapper),
            new PropertyMetadata(default(int?)));

        /// <summary>Identifies the <see cref="NaturalVideoWidth"/> dependency property.</summary>
        public static readonly DependencyProperty NaturalVideoWidthProperty = NaturalVideoWidthPropertyKey.DependencyProperty;

        private static readonly DependencyPropertyKey HasAudioPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(HasAudio),
            typeof(bool?),
            typeof(MediaElementWrapper),
            new PropertyMetadata(default(bool?)));

        /// <summary>Identifies the <see cref="HasAudio"/> dependency property.</summary>
        public static readonly DependencyProperty HasAudioProperty = HasAudioPropertyKey.DependencyProperty;

        private static readonly DependencyPropertyKey HasVideoPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(HasVideo),
            typeof(bool?),
            typeof(MediaElementWrapper),
            new PropertyMetadata(default(bool?)));

        /// <summary>Identifies the <see cref="HasVideo"/> dependency property.</summary>
        public static readonly DependencyProperty HasVideoProperty = HasVideoPropertyKey.DependencyProperty;

        private static readonly DependencyPropertyKey HasMediaPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(HasMedia),
            typeof(bool),
            typeof(MediaElementWrapper),
            new PropertyMetadata(default(bool)));

        /// <summary>Identifies the <see cref="HasMedia"/> dependency property.</summary>
        public static readonly DependencyProperty HasMediaProperty = HasMediaPropertyKey.DependencyProperty;

        /// <summary>Identifies the <see cref="SpeedRatio"/> dependency property.</summary>
        public static readonly DependencyProperty SpeedRatioProperty = DependencyProperty.Register(
            nameof(SpeedRatio),
            typeof(double),
            typeof(MediaElementWrapper),
            new PropertyMetadata(default(double), (d, e) => ((MediaElementWrapper)d).mediaElement.SpeedRatio = (double)e.NewValue));

        private static readonly DependencyPropertyKey IsBufferingPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(IsBuffering),
            typeof(bool),
            typeof(MediaElementWrapper),
            new PropertyMetadata(default(bool)));

        /// <summary>Identifies the <see cref="IsBuffering"/> dependency property.</summary>
        public static readonly DependencyProperty IsBufferingProperty = IsBufferingPropertyKey.DependencyProperty;

        private static readonly DependencyPropertyKey DownloadProgressPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(DownloadProgress),
            typeof(double),
            typeof(MediaElementWrapper),
            new PropertyMetadata(default(double)));

        /// <summary>Identifies the <see cref="DownloadProgress"/> dependency property.</summary>
        public static readonly DependencyProperty DownloadProgressProperty = DownloadProgressPropertyKey.DependencyProperty;

        private static readonly DependencyPropertyKey BufferingProgressPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(BufferingProgress),
            typeof(double),
            typeof(MediaElementWrapper),
            new PropertyMetadata(default(double)));

        /// <summary>Identifies the <see cref="BufferingProgress"/> dependency property.</summary>
        public static readonly DependencyProperty BufferingProgressProperty = BufferingProgressPropertyKey.DependencyProperty;

        /// <summary>Identifies the <see cref="VolumeIncrement"/> dependency property.</summary>
        public static readonly DependencyProperty VolumeIncrementProperty = DependencyProperty.Register(
            nameof(VolumeIncrement),
            typeof(double),
            typeof(MediaElementWrapper),
            new PropertyMetadata(0.05));

        /// <summary>Identifies the <see cref="SkipIncrement"/> dependency property.</summary>
        public static readonly DependencyProperty SkipIncrementProperty = DependencyProperty.Register(
            nameof(SkipIncrement),
            typeof(TimeSpan),
            typeof(MediaElementWrapper),
            new PropertyMetadata(TimeSpan.FromSeconds(1)));

        /// <summary>Identifies the <see cref="VideoFormats"/> dependency property.</summary>
        public static readonly DependencyProperty VideoFormatsProperty = DependencyProperty.Register(
            nameof(VideoFormats),
            typeof(string),
            typeof(MediaElementWrapper),
            new PropertyMetadata(FileFormats.DefaultVideoFormats));

        /// <summary>Identifies the <see cref="AudioFormats"/> dependency property.</summary>
        public static readonly DependencyProperty AudioFormatsProperty = DependencyProperty.Register(
            nameof(AudioFormats),
            typeof(string),
            typeof(MediaElementWrapper),
            new PropertyMetadata(FileFormats.DefaultAudioFormats));

        /// <summary>Identifies the <see cref="LoadedBehavior"/> dependency property.</summary>
        public static readonly DependencyProperty LoadedBehaviorProperty = DependencyProperty.Register(
            nameof(LoadedBehavior),
            typeof(MediaState),
            typeof(MediaElementWrapper),
            new PropertyMetadata(MediaState.Play));

        /// <summary>
        /// Gets or sets the current <see cref="MediaState"/>.
        /// </summary>
        public MediaState State
        {
            get => (MediaState)this.GetValue(StateProperty);
            set => this.SetValue(StateProperty, value);
        }

        /// <summary>
        /// Sets <see cref="State"/> to <see cref="MediaState.Pause"/> if <see cref="MediaState.Play"/> and to <see cref="MediaState.Play"/> if <see cref="MediaState.Pause"/>.
        /// </summary>
        public bool IsPlaying
        {
            get => (bool)this.GetValue(IsPlayingProperty);
            set => this.SetValue(IsPlayingProperty, value);
        }

        /// <summary>
        /// Gets or sets the current position in the media.
        /// Null if no media.
        /// </summary>
        public TimeSpan? Position
        {
            get => (TimeSpan?)this.GetValue(PositionProperty);
            set => this.SetValue(PositionProperty, value);
        }

        /// <summary>
        /// The length of the current media.
        /// </summary>
        public TimeSpan? Length
        {
            get => (TimeSpan?)this.GetValue(LengthProperty);
            protected set => this.SetValue(LengthPropertyKey, value);
        }

        /// <summary>
        /// Returns whether the given media can be paused. This is only valid
        /// after the MediaOpened event has fired.
        /// </summary>
        public bool? CanPause
        {
            get => (bool?)this.GetValue(CanPauseProperty);
            protected set => this.SetValue(CanPausePropertyKey, value);
        }

        /// <summary>
        /// Returns the natural height of the media in the video. Only valid after
        /// the MediaOpened event has fired.
        /// </summary>
        public int? NaturalVideoHeight
        {
            get => (int?)this.GetValue(NaturalVideoHeightProperty);
            protected set => this.SetValue(NaturalVideoHeightPropertyKey, value);
        }

        /// <summary>
        /// Returns the natural width of the media in the video. Only valid after
        /// the MediaOpened event has fired.
        /// </summary>
        public int? NaturalVideoWidth
        {
            get => (int?)this.GetValue(NaturalVideoWidthProperty);
            protected set => this.SetValue(NaturalVideoWidthPropertyKey, value);
        }

        /// <summary>
        /// Returns whether the given media has audio, null if no media.
        /// </summary>
        public bool? HasAudio
        {
            get => (bool?)this.GetValue(HasAudioProperty);
            protected set => this.SetValue(HasAudioPropertyKey, value);
        }

        /// <summary>
        /// Returns whether the given media has video, null if no media.
        /// </summary>
        public bool? HasVideo
        {
            get => (bool?)this.GetValue(HasVideoProperty);
            protected set => this.SetValue(HasVideoPropertyKey, value);
        }

        /// <summary>
        /// Returns whether the given media has video.
        /// </summary>
        public bool HasMedia
        {
            get => (bool)this.GetValue(HasMediaProperty);
            protected set => this.SetValue(HasMediaPropertyKey, value);
        }

        /// <summary>
        /// Allows the speed ration of the media to be controlled.
        /// </summary>
        public double SpeedRatio
        {
            get => (double)this.GetValue(SpeedRatioProperty);
            set => this.SetValue(SpeedRatioProperty, value);
        }

        /// <summary>
        /// Returns whether the given media is currently being buffered. This
        /// applies to network accessed media only.
        /// </summary>
        public bool IsBuffering
        {
            get => (bool)this.GetValue(IsBufferingProperty);
            protected set => this.SetValue(IsBufferingPropertyKey, value);
        }

        /// <summary>
        /// Returns the download progress of the media.
        /// </summary>
        public double DownloadProgress
        {
            get => (double)this.GetValue(DownloadProgressProperty);
            protected set => this.SetValue(DownloadProgressPropertyKey, value);
        }

        /// <summary>
        /// Returns the buffering progress of the media.
        /// </summary>
        public double BufferingProgress
        {
            get => (double)this.GetValue(BufferingProgressProperty);
            protected set => this.SetValue(BufferingProgressPropertyKey, value);
        }

        /// <summary>
        /// The increment by which the <see cref="IncreaseVolume(object)"/> and <see cref="DecreaseVolume(object)"/> changes current volume if null is passed in.
        /// </summary>
        public double VolumeIncrement
        {
            get => (double)this.GetValue(VolumeIncrementProperty);
            set => this.SetValue(VolumeIncrementProperty, value);
        }

        /// <summary>
        /// The increment by which the skip commands changes current position if null is passed in.
        /// If an integer is passed in the <see cref="SkipIncrement"/> is multiplied by this value.
        /// </summary>
        public TimeSpan SkipIncrement
        {
            get => (TimeSpan)this.GetValue(SkipIncrementProperty);
            set => this.SetValue(SkipIncrementProperty, value);
        }

        /// <summary>
        /// Gets or sets a list with video file formats.
        /// This is a convenience for use in <see cref="Microsoft.Win32.OpenFileDialog"/>
        /// https://support.microsoft.com/en-us/kb/316992.
        /// </summary>
        public string VideoFormats
        {
            get => (string)this.GetValue(VideoFormatsProperty);
            set => this.SetValue(VideoFormatsProperty, value);
        }

        /// <summary>
        /// Gets or sets a list with audio file formats.
        /// This is a convenience for use in <see cref="Microsoft.Win32.OpenFileDialog"/>
        /// https://support.microsoft.com/en-us/kb/316992.
        /// </summary>
        public string AudioFormats
        {
            get => (string)this.GetValue(AudioFormatsProperty);
            set => this.SetValue(AudioFormatsProperty, value);
        }

        /// <summary>
        /// Specifies the initial state when media is loaded.
        /// Unlike MediaElement.LoadedBehavior this does not affect controlling playback later.
        /// </summary>
        public MediaState LoadedBehavior
        {
            get => (MediaState)this.GetValue(LoadedBehaviorProperty);
            set => this.SetValue(LoadedBehaviorProperty, value);
        }

        private static void OnIsPlayingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var wrapper = (MediaElementWrapper)d;
            if ((bool)e.NewValue)
            {
                wrapper.SetCurrentValue(StateProperty, MediaState.Play);
            }
            else if (wrapper.State == MediaState.Play)
            {
                wrapper.SetCurrentValue(StateProperty, MediaState.Pause);
            }
        }

        private static object CoerceIsMuted(DependencyObject d, object baseValue)
        {
            var wrapper = (MediaElementWrapper)d;
            if (wrapper.Volume <= 0)
            {
                return true;
            }

            return baseValue;
        }
    }
}
