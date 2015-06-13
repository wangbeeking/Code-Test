using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Threading;
using Vlc.DotNet.Forms;
namespace VLC_WPF_TEST
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer(); //计时器
        readonly public int SecondScanTime = 1000; //当前播放时间刷新间隔ms
        public bool bLeftButtonDown = false;//判断是否由鼠标拖动触发slider值修改
        public MainWindow()
        {
            InitializeComponent();
            myControl.MediaPlayer.VlcLibDirectoryNeeded += OnVlcControlNeedsLibDirectory;//load vlc相关的库文件
            myControl.MediaPlayer.Playing +=MediaPlayer_Playing; //相应开始播放消息，此消息会在调用播放之后再触发。仅第一次播放时触发
            myControl.MediaPlayer.EndReached += MediaPlayer_EndReached; //播放完成响应
            timer.Tick += SecondChange; //计时器响应
            timer.Interval = TimeSpan.FromMilliseconds(SecondScanTime);//计时器时间设置
        }
        /// <summary>
        /// 播放完成响应，停止计时器，清理Slider
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MediaPlayer_EndReached(object sender, Vlc.DotNet.Core.VlcMediaPlayerEndReachedEventArgs e)
        {
            timer.Stop();//停止计时器
            this.Dispatcher.Invoke(new Action(() => //清理slider
            {
                TimeSlider.Value = 0;
                TimeSlider.Maximum = 0;
            }));
        }
        /// <summary>
        /// 播放事件响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediaPlayer_Playing(object sender, Vlc.DotNet.Core.VlcMediaPlayerPlayingEventArgs e)
        {
            SetAllTime(myControl.MediaPlayer.Length);//设置总时间
            this.Dispatcher.Invoke(new Action(() =>  //设置Slider与开始计时器
            {
                TimeSlider.Maximum = myControl.MediaPlayer.Length / 1000;
                TimeSlider.Value = 0;
                timer.Start();
            }));
        }
        /// <summary>
        /// 计时器响应，主要用来控制slider滑块的移动与当前时间显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SecondChange(object sender, EventArgs e)
        {
            SetNowTime(myControl.MediaPlayer.Time);//设置当前时间
            this.Dispatcher.Invoke(new Action(() =>//设置滑块位置。滑块总长为总秒数
            {
                TimeSlider.Value = myControl.MediaPlayer.Time / 1000;
            }));
        }
        /// <summary>
        /// 初始化Lib，需要输入lib文件夹路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnVlcControlNeedsLibDirectory(object sender, VlcLibDirectoryNeededEventArgs e)
        {
            var currentAssembly = Assembly.GetEntryAssembly();
            var currentDirectory = new FileInfo(currentAssembly.Location).DirectoryName;
            if (currentDirectory == null)
                return;
            if (AssemblyName.GetAssemblyName(currentAssembly.Location).ProcessorArchitecture == ProcessorArchitecture.X86)
                e.VlcLibDirectory = new DirectoryInfo(Path.Combine(currentDirectory, @"lib\x86\"));
            else
                e.VlcLibDirectory = new DirectoryInfo(Path.Combine(currentDirectory, @"lib\x64\"));
        }
        /// <summary>
        /// 添加播放文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayClick(object sender, RoutedEventArgs e)
        {
            //播放流媒体请使用：myControl.MediaPlayer.Play(new Uri("http://download.blender.org/peach/bigbuckbunny_movies/big_buck_bunny_480p_surround-fix.avi"));
            //本地播放请使用：myControl.MediaPlayer.Play(new FileInfo(@"D:\gcjd1.mp4"));
            //由于未找到明确的变量判断是否已加载了文件，所以直接访问文件长度，如果有exception表示无文件加载
            bool bflag = true; 
            try
            {
                long temp = myControl.MediaPlayer.Length;
            }
            catch
            {
                bflag = false;
            }
            if(bflag == false)//无文件加载，加载新文件
                myControl.MediaPlayer.Play(new FileInfo(@PathText.Text));
            myControl.MediaPlayer.Play();//有文件加载，继续播放
        }
        /// <summary>
        /// 暂停播放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PauseClick(object sender, RoutedEventArgs e)
        {
            myControl.MediaPlayer.Pause();//暂停播放
            timer.Stop();//停止计时
        }
        /// <summary>
        /// 停止播放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StopClick(object sender, RoutedEventArgs e)
        {
            myControl.MediaPlayer.Stop();//停止播放
            //清理显示
            allTime.Text = "";
            nowTime.Text = "";
            TimeSlider.Value = 0;
            TimeSlider.Maximum = 0;
            //还原播放速度
            this.Dispatcher.Invoke(new Action(() =>
            {
                playSpeed.Text = "1x";
                myControl.MediaPlayer.Rate = 1;
            }));
        }
        /// <summary>
        /// 减慢播放速度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SlowerClick(object sender, RoutedEventArgs e)
        {
            if (myControl.MediaPlayer.Rate == 0.125)//不得小于1/8倍
                return;
            myControl.MediaPlayer.Rate /= 2;//以2倍为阶梯
            this.Dispatcher.Invoke(new Action(() =>
            {
                playSpeed.Text = myControl.MediaPlayer.Rate.ToString() + "x";//设置显示
            }));
        }
        /// <summary>
        /// 设置播放速度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FasterClick(object sender, RoutedEventArgs e)
        {
            if (myControl.MediaPlayer.Rate == 8)//判断当前倍速，不得大于8倍速
                return;
            myControl.MediaPlayer.Rate *= 2;//以两倍为阶梯
            this.Dispatcher.Invoke(new Action(() =>
            {
                playSpeed.Text = myControl.MediaPlayer.Rate.ToString() + "x";//设置播放速度显示
            }));
        }
        /// <summary>
        /// 鼠标左键抬起消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimeSlider_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //鼠标左键放开，继续播放，继续计时器，还原标记
            bLeftButtonDown = false;
            timer.Start();
            myControl.MediaPlayer.Play();
        }
        /// <summary>
        /// 鼠标左键按下消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimeSlider_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //如果按下，设置标记，暂停播放，停止计时器
            bLeftButtonDown = true;
            timer.Stop();
            myControl.MediaPlayer.Pause();
        }
        /// <summary>
        /// Slider滑块移动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimeSlider_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (!bLeftButtonDown)//如果鼠标并未按下，表示是并未移动滑块，不做处理
                return;
            myControl.MediaPlayer.Time = (long)(TimeSlider.Value * 1000);//由于Slider总长为总秒数，因此需要计算当前滑块位置代表的总毫秒数
            SetNowTime(myControl.MediaPlayer.Time);
        }
        /// <summary>
        /// 设置当前时间，通过视频的播放时间设置
        /// </summary>
        /// <param name="ticketTime"></param>
        public void SetNowTime(long ticketTime)
        {
            long hour = ticketTime / (1000 * 60 * 60);
            long minute = (ticketTime - (hour * 1000 * 60 * 60)) / (1000 * 60);
            long second = (ticketTime - (hour * 1000 * 60 * 60) - minute * (1000 * 60)) / (1000);
            this.Dispatcher.Invoke(new Action(() =>
            {
                nowTime.Text = hour.ToString("00") + ":" + minute.ToString("00") + ":" + second.ToString("00");
            }));
        }
        /// <summary>
        /// 设置总时间，通过视频总长度设置
        /// </summary>
        /// <param name="ticketTime"></param>
        public void SetAllTime(long ticketTime)
        {
            long hour = ticketTime / (1000 * 60 * 60);
            long minute = (ticketTime - (hour * 1000 * 60 * 60)) / (1000 * 60);
            long second = (ticketTime - (hour * 1000 * 60 * 60) - minute * (1000 * 60)) / (1000);
            this.Dispatcher.Invoke(new Action(() =>
            {
                allTime .Text = hour.ToString("00") + ":" + minute.ToString("00") + ":" + second.ToString("00");
            }));
        }
    }
}
