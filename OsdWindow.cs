using System;
using System.Drawing;
using System.Windows.Forms;

using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace PS3BluMote
{
    public partial class OsdWindow : Form
    {
        public enum OsdTextAlign
        {
            Left,
            Center,
            Right
        }
        public enum OsdVerticalAlign
        {
            Top,
            Middle,
            Bottom
        }

        public enum AnimateMode
        {
            Blend,
            SlideRightToLeft,
            SlideLeftToRight,
            SlideTopToBottom,
            SlideBottmToTop,
            RollRightToLeft,
            RollLeftToRight,
            RollTopToBottom,
            RollBottmToTop,
            ExpandCollapse
        }

        private Bitmap surfaceBitmap;
        // private Rectangle rScreen = Screen.PrimaryScreen.Bounds;
        private Rectangle rScreen;
        private Rectangle RScreen
        {
            get
            {
                rScreen = Screen.PrimaryScreen.Bounds;
                return rScreen;
            }
        }
        private StringFormat stringFormat;
        private System.Timers.Timer viewClock;

        private string text;
        private Font textFont;
        private SolidBrush brush;
        private Pen fontPath;
        private byte alpha;
        private AnimateMode mode;
        private uint time;

        private OsdTextAlign? align;
        private OsdVerticalAlign? valign;

        // ---
        public OsdWindow()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
        }

        delegate void ShowCallback(OsdTextAlign align, OsdVerticalAlign valign, byte alpha, Color textColor, Color pathColor, float pathWidth, Font textFont, int showTimeMSec, AnimateMode mode, uint time, string text);
        public void Show(Point pt, byte alpha, Color textColor, Color pathColor, float pathWidth, Font textFont, int showTimeMSec, AnimateMode mode, uint time, string text)
        {
            if (this.InvokeRequired)
            {
                ShowCallback d = new ShowCallback(Show);
                this.Invoke(d, new object[] { pt, alpha, textColor, pathColor, pathWidth, textFont, showTimeMSec, mode, time, text });
            }
            else
            {
                if (text == "") return;

                if (this.viewClock != null)
                {
                    this.viewClock.Stop();
                    this.viewClock.Dispose();
                }

                if (this.stringFormat == null)
                {
                    this.stringFormat = new StringFormat();
                    this.stringFormat.Alignment = StringAlignment.Near;
                    this.stringFormat.LineAlignment = StringAlignment.Near;
                    this.stringFormat.Trimming = StringTrimming.EllipsisWord;
                }

                this.Location = pt;
                this.alpha = alpha;
                if (this.brush != null)
                {
                    this.brush.Dispose();
                }
                this.brush = new SolidBrush(textColor);
                if (this.fontPath != null)
                {
                    this.fontPath.Dispose();
                }
                this.fontPath = new Pen(pathColor, pathWidth);
                this.textFont = textFont;
                this.text = text;
                this.mode = mode;
                this.time = time;

                PerformPaint();
                UpdateLayeredWindow();

                // if (this.time > 0) // always animate
                if (this.time > 0 && this.Visible == false) // cancel animate if OSD is shown (update text immediately)
                    this.ShowAnimate(this.mode, this.time);
                else
                    this.Show();

                GC.Collect();

                // start timer
                this.viewClock = new System.Timers.Timer();
                this.viewClock.Elapsed += new System.Timers.ElapsedEventHandler(ViewTimer);
                this.viewClock.Interval = showTimeMSec;
                this.viewClock.Start();
            }
        }

        delegate void ShowAlignCallback(OsdTextAlign align, OsdVerticalAlign valign, byte alpha, Color textColor, Color pathColor, float pathWidth, Font textFont, int showTimeMSec, AnimateMode mode, uint time, string text);
        public void Show(OsdTextAlign align, OsdVerticalAlign valign, byte alpha, Color textColor, Color pathColor, float pathWidth, Font textFont, int showTimeMSec, AnimateMode mode, uint time, string text)
        {
            if (this.InvokeRequired)
            {
                ShowAlignCallback d = new ShowAlignCallback(Show);
                this.Invoke(d, new object[] { align, valign, alpha, textColor, pathColor, pathWidth, textFont, showTimeMSec, mode, time, text });
            }
            else
            {
                if (text == "") return;

                if (this.viewClock != null)
                {
                    this.viewClock.Stop();
                    this.viewClock.Dispose();
                }

                if (this.stringFormat == null)
                {
                    this.stringFormat = new StringFormat();
                    this.stringFormat.Alignment = StringAlignment.Near;
                    this.stringFormat.LineAlignment = StringAlignment.Near;
                    this.stringFormat.Trimming = StringTrimming.EllipsisWord;
                }

                this.align = align;
                this.valign = valign;
                this.alpha = alpha;
                if (this.brush != null)
                {
                    this.brush.Dispose();
                }
                this.brush = new SolidBrush(textColor);
                if (this.fontPath != null)
                {
                    this.fontPath.Dispose();
                }
                this.fontPath = new Pen(pathColor, pathWidth);
                this.textFont = textFont;
                this.text = text;
                this.mode = mode;
                this.time = time;

                PerformPaint();
                UpdateLayeredWindow();

                // if (this.time > 0) // always animate
                if (this.time > 0 && this.Visible == false) // cancel animate if OSD is shown (update text immediately)
                    this.ShowAnimate(this.mode, this.time);
                else
                    this.Show();

                GC.Collect();

                // start timer
                viewClock = new System.Timers.Timer();
                viewClock.Elapsed += new System.Timers.ElapsedEventHandler(ViewTimer);
                viewClock.Interval = showTimeMSec;
                viewClock.Start();
            }
        }

        private void PerformPaint()
        {
            this.surfaceBitmap = new Bitmap(RScreen.Width, RScreen.Height, PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(this.surfaceBitmap))
            {
                g.Clear(Color.Transparent);
                g.SmoothingMode = SmoothingMode.HighQuality; // anti-alias
                g.CompositingQuality = CompositingQuality.HighQuality;

                using (GraphicsPath gp = new GraphicsPath())
                {
                    gp.AddString(this.text, this.textFont.FontFamily, (int)this.textFont.Style, this.textFont.SizeInPoints,
                        this.RScreen, this.stringFormat);
                    g.DrawPath(this.fontPath, gp);
                    g.FillPath(this.brush, gp);

                    RectangleF rectF = gp.GetBounds(new Matrix(), this.fontPath);
                    this.Width = (int)Math.Ceiling(rectF.Width);
                    this.Height = (int)Math.Ceiling(rectF.Height);
                }
            }

            if (this.align != null || this.valign != null)
            {
                int posX;
                if (align == OsdTextAlign.Left)
                    posX = 0;
                else if (align == OsdTextAlign.Center)
                    posX = (int)(this.RScreen.Width - this.Width) / 2;
                else
                    posX = (int)(this.RScreen.Width - this.Width);

                int posY;
                if (valign == OsdVerticalAlign.Top)
                    posY = 0;
                else if (valign == OsdVerticalAlign.Middle)
                    posY = (int)(this.RScreen.Height - this.Height) / 2;
                else
                    posY = (int)(this.RScreen.Height - this.Height) - 30;

                this.Location = new Point(posX, posY);
                this.align = null;
                this.valign = null;
            }
        }

        delegate void UpdateLayeredWindowCallback();
        public void UpdateLayeredWindow()
        {
            if (this.InvokeRequired)
            {
                UpdateLayeredWindowCallback d = new UpdateLayeredWindowCallback(UpdateLayeredWindow);
                this.Invoke(d, new object[] { });
            }
            else
            {
                Graphics g_sc = Graphics.FromHwnd(IntPtr.Zero);
                IntPtr hdc_sc = g_sc.GetHdc();
                Graphics g_bmp = Graphics.FromImage(this.surfaceBitmap);
                IntPtr hdc_bmp = g_bmp.GetHdc();
                IntPtr oldhbmp = SelectObject(hdc_bmp, this.surfaceBitmap.GetHbitmap(Color.FromArgb(0)));

                BLENDFUNCTION blend = new BLENDFUNCTION();
                blend.BlendOp = AC_SRC_OVER;
                blend.BlendFlags = 0;
                blend.SourceConstantAlpha = this.alpha;
                blend.AlphaFormat = AC_SRC_ALPHA;

                Point pos = this.Location;

                Size surfaceSize = new Size(this.Width, this.Height);
                Point surfacePos = new Point(0, 0);
                UpdateLayeredWindow(
                    this.Handle, hdc_sc, ref pos, ref surfaceSize,
                    hdc_bmp, ref surfacePos, 0, ref blend, ULW_ALPHA);

                DeleteObject(SelectObject(hdc_bmp, oldhbmp));
                g_sc.ReleaseHdc(hdc_sc);
                g_sc.Dispose();
                g_bmp.ReleaseHdc(hdc_bmp);
                g_bmp.Dispose();
            }
        }

        delegate void ShowAnimateCallback(AnimateMode mode, uint time);
        public virtual void ShowAnimate(AnimateMode mode, uint time)
        {
            if (this.InvokeRequired)
            {
                ShowAnimateCallback d = new ShowAnimateCallback(ShowAnimate);
                this.Invoke(d, new object[] { mode, time });
            }
            else
            {
                uint dwFlag = 0;
                switch (mode)
                {
                    case AnimateMode.Blend:
                        dwFlag = AW_BLEND;
                        break;
                    case AnimateMode.ExpandCollapse:
                        dwFlag = AW_CENTER;
                        break;
                    case AnimateMode.SlideLeftToRight:
                        dwFlag = AW_HOR_POSITIVE | AW_SLIDE;
                        break;
                    case AnimateMode.SlideRightToLeft:
                        dwFlag = AW_HOR_NEGATIVE | AW_SLIDE;
                        break;
                    case AnimateMode.SlideTopToBottom:
                        dwFlag = AW_VER_POSITIVE | AW_SLIDE;
                        break;
                    case AnimateMode.SlideBottmToTop:
                        dwFlag = AW_VER_NEGATIVE | AW_SLIDE;
                        break;
                    case AnimateMode.RollLeftToRight:
                        dwFlag = AW_HOR_POSITIVE;
                        break;
                    case AnimateMode.RollRightToLeft:
                        dwFlag = AW_HOR_NEGATIVE;
                        break;
                    case AnimateMode.RollBottmToTop:
                        dwFlag = AW_VER_NEGATIVE;
                        break;
                    case AnimateMode.RollTopToBottom:
                        dwFlag = AW_VER_POSITIVE;
                        break;
                }

                if ((dwFlag & AW_BLEND) != 0)
                    this.AnimateWithBlend(true, time);
                else
                    AnimateWindow(this.Handle, time, dwFlag);
            }
        }

        delegate void HideAnimateCallback(AnimateMode mode, uint time);
        public virtual void HideAnimate(AnimateMode mode, uint time)
        {
            if (this.InvokeRequired)
            {
                HideAnimateCallback d = new HideAnimateCallback(HideAnimate);
                this.Invoke(d, new object[] { mode, time });
            }
            else
            {
                if (this.Handle == IntPtr.Zero) return;

                uint dwFlag = 0;
                switch (mode)
                {
                    case AnimateMode.Blend:
                        dwFlag = AW_BLEND;
                        break;
                    case AnimateMode.ExpandCollapse:
                        dwFlag = AW_CENTER;
                        break;
                    case AnimateMode.SlideLeftToRight:
                        dwFlag = AW_HOR_POSITIVE | AW_SLIDE;
                        break;
                    case AnimateMode.SlideRightToLeft:
                        dwFlag = AW_HOR_NEGATIVE | AW_SLIDE;
                        break;
                    case AnimateMode.SlideTopToBottom:
                        dwFlag = AW_VER_POSITIVE | AW_SLIDE;
                        break;
                    case AnimateMode.SlideBottmToTop:
                        dwFlag = AW_VER_NEGATIVE | AW_SLIDE;
                        break;
                    case AnimateMode.RollLeftToRight:
                        dwFlag = AW_HOR_POSITIVE;
                        break;
                    case AnimateMode.RollRightToLeft:
                        dwFlag = AW_HOR_NEGATIVE;
                        break;
                    case AnimateMode.RollBottmToTop:
                        dwFlag = AW_VER_NEGATIVE;
                        break;
                    case AnimateMode.RollTopToBottom:
                        dwFlag = AW_VER_POSITIVE;
                        break;
                }
                dwFlag |= AW_HIDE;
                if ((dwFlag & AW_BLEND) != 0)
                    this.AnimateWithBlend(false, time);
                else
                    AnimateWindow(this.Handle, time, dwFlag);
                // this.Hide();
            }
        }

        delegate void AnimateWithBlendCallback(bool show, uint time);
        private void AnimateWithBlend(bool show, uint time)
        {
            if (this.InvokeRequired)
            {
                AnimateWithBlendCallback d = new AnimateWithBlendCallback(AnimateWithBlend);
                this.Invoke(d, new object[] { show, time });
            }
            else
            {
                byte originalAlpha = this.alpha;
                byte p = (byte)(originalAlpha / (time / 10));
                if (p == 0) p++;
                if (show)
                {
                    this.alpha = 0;
                    this.UpdateLayeredWindow();
                    ShowWindow(this.Handle, SW_SHOWNOACTIVATE);
                }
                for (byte i = show ? (byte)0 : originalAlpha; (show ? i <= originalAlpha : i >= (byte)0); i += (byte)(p * (show ? 1 : -1)))
                {
                    this.alpha = i;
                    this.UpdateLayeredWindow();
                    if ((show && i > originalAlpha - p) || (!show && i < p))
                        break;
                }
                this.alpha = originalAlpha;
                if (show) this.UpdateLayeredWindow();
            }
        }

        // timer ---
        delegate void ViewTimerCallback(object sender, System.EventArgs e);
        private void ViewTimer(object sender, System.EventArgs e)
        {
            if (this.InvokeRequired)
            {
                ViewTimerCallback d = new ViewTimerCallback(ViewTimer);
                this.Invoke(d, new object[] { sender, e });
            }
            else
            {
                this.viewClock.Stop();
                this.viewClock.Dispose();

                if (this.time > 0)
                    this.HideAnimate(this.mode, this.time);
                this.Hide();

                if (this.surfaceBitmap != null)
                {
                    this.surfaceBitmap.Dispose();
                }
                GC.Collect();
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style = unchecked((int)WS_POPUP);
                cp.ExStyle = WS_EX_TOPMOST | WS_EX_TOOLWINDOW | WS_EX_LAYERED | WS_EX_NOACTIVATE | WS_EX_TRANSPARENT | SW_SHOWNOACTIVATE;
                return cp;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (viewClock != null)
            {
                this.viewClock.Stop();
                this.viewClock.Dispose();
            }
            this.Hide();

            if (this.surfaceBitmap != null)
            {
                this.surfaceBitmap.Dispose();
            }
            GC.Collect();

            base.OnFormClosing(e);
        }

        // ---
        public const uint WS_POPUP = 0x80000000;
        public const int WS_EX_TOPMOST = 0x00000008;
        public const int WS_EX_TOOLWINDOW = 0x00000080;
        public const int WS_EX_LAYERED = 0x00080000;
        public const int WS_EX_NOACTIVATE = 0x08000000;
        public const int WS_EX_TRANSPARENT = 0x00000020;
        public const int SW_HIDE = 0x00000000;
        public const int SW_SHOWNOACTIVATE = 0x00000004;
        public const uint AW_HOR_POSITIVE = 0x00000001;
        public const uint AW_HOR_NEGATIVE = 0x00000002;
        public const uint AW_VER_POSITIVE = 0x00000004;
        public const uint AW_VER_NEGATIVE = 0x00000008;
        public const uint AW_CENTER = 0x00000010;
        public const uint AW_HIDE = 0x00010000;
        public const uint AW_ACTIVATE = 0x00020000;
        public const uint AW_SLIDE = 0x00040000;
        public const uint AW_BLEND = 0x00080000;

        public const byte AC_SRC_OVER = 0;
        public const byte AC_SRC_ALPHA = 1;
        public const int ULW_ALPHA = 2;

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct BLENDFUNCTION
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int DeleteObject(IntPtr hobject);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool AnimateWindow(IntPtr hWnd, uint dwTime, uint dwFlags);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern int ShowWindow(IntPtr hWnd, short cmdShow);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int UpdateLayeredWindow(
            IntPtr hwnd,
            IntPtr hdcDst,
            [System.Runtime.InteropServices.In()]
            ref Point pptDst,
            [System.Runtime.InteropServices.In()]
            ref Size psize,
            IntPtr hdcSrc,
            [System.Runtime.InteropServices.In()]
            ref Point pptSrc,
            int crKey,
            [System.Runtime.InteropServices.In()]
            ref BLENDFUNCTION pblend,
            int dwFlags);
    }
}
