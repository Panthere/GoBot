using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Drawing;

public static class Helpers
{
    public static Color C1_F = ColorFromHex("#007196");
    public static Color C1_B = ColorFromHex("#00BFFF");

    public static Color C2_F = ColorFromHex("#D15A4D");
    public static Color C2_B = ColorFromHex("#FFCFCA");

    public static Color C3_F = ColorFromHex("#54AB54");
    public static Color C3_B = ColorFromHex("#CAFFCA");

    public static Color C4_F = ColorFromHex("#ADA200");
    public static Color C4_B = ColorFromHex("#FCF47E");

    public static Color C5_F = ColorFromHex("#E800CD");
    public static Color C5_B = ColorFromHex("#FFBFF7");

    public static Color C6_F = ColorFromHex("#0EADAD");
    public static Color C6_B = ColorFromHex("#9CFFFF");

    public static Color ColorFromHex(string Hex)
    {
        return Color.FromArgb(Convert.ToInt32(long.Parse(string.Format("FFFFFFFFFF{0}", Hex.StartsWith("#") ? Hex.Substring(1) : Hex), System.Globalization.NumberStyles.HexNumber)));
    }
    public enum MouseState : byte
    {
        None = 0,
        Over = 1,
        Down = 2
    }

    public enum RoundingStyle : byte
    {
        All = 0,
        Top = 1,
        Bottom = 2,
        Left = 3,
        Right = 4,
        TopRight = 5,
        BottomRight = 6
    }

    public static GraphicsPath RoundRect(Rectangle Rect, int Rounding, RoundingStyle Style = RoundingStyle.All)
    {

        GraphicsPath GP = new GraphicsPath();
        int AW = Rounding * 2;

        GP.StartFigure();

        if (Rounding == 0)
        {
            GP.AddRectangle(Rect);
            GP.CloseAllFigures();
            return GP;
        }

        switch (Style)
        {
            case RoundingStyle.All:
                GP.AddArc(new Rectangle(Rect.X, Rect.Y, AW, AW), -180, 90);
                GP.AddArc(new Rectangle(Rect.Width - AW + Rect.X, Rect.Y, AW, AW), -90, 90);
                GP.AddArc(new Rectangle(Rect.Width - AW + Rect.X, Rect.Height - AW + Rect.Y, AW, AW), 0, 90);
                GP.AddArc(new Rectangle(Rect.X, Rect.Height - AW + Rect.Y, AW, AW), 90, 90);
                break;
            case RoundingStyle.Top:
                GP.AddArc(new Rectangle(Rect.X, Rect.Y, AW, AW), -180, 90);
                GP.AddArc(new Rectangle(Rect.Width - AW + Rect.X, Rect.Y, AW, AW), -90, 90);
                GP.AddLine(new Point(Rect.X + Rect.Width, Rect.Y + Rect.Height), new Point(Rect.X, Rect.Y + Rect.Height));
                break;
            case RoundingStyle.Bottom:
                GP.AddLine(new Point(Rect.X, Rect.Y), new Point(Rect.X + Rect.Width, Rect.Y));
                GP.AddArc(new Rectangle(Rect.Width - AW + Rect.X, Rect.Height - AW + Rect.Y, AW, AW), 0, 90);
                GP.AddArc(new Rectangle(Rect.X, Rect.Height - AW + Rect.Y, AW, AW), 90, 90);
                break;
            case RoundingStyle.Left:
                GP.AddArc(new Rectangle(Rect.X, Rect.Y, AW, AW), -180, 90);
                GP.AddLine(new Point(Rect.X + Rect.Width, Rect.Y), new Point(Rect.X + Rect.Width, Rect.Y + Rect.Height));
                GP.AddArc(new Rectangle(Rect.X, Rect.Height - AW + Rect.Y, AW, AW), 90, 90);
                break;
            case RoundingStyle.Right:
                GP.AddLine(new Point(Rect.X, Rect.Y + Rect.Height), new Point(Rect.X, Rect.Y));
                GP.AddArc(new Rectangle(Rect.Width - AW + Rect.X, Rect.Y, AW, AW), -90, 90);
                GP.AddArc(new Rectangle(Rect.Width - AW + Rect.X, Rect.Height - AW + Rect.Y, AW, AW), 0, 90);
                break;
            case RoundingStyle.TopRight:
                GP.AddLine(new Point(Rect.X, Rect.Y + 1), new Point(Rect.X, Rect.Y));
                GP.AddArc(new Rectangle(Rect.Width - AW + Rect.X, Rect.Y, AW, AW), -90, 90);
                GP.AddLine(new Point(Rect.X + Rect.Width, Rect.Y + Rect.Height - 1), new Point(Rect.X + Rect.Width, Rect.Y + Rect.Height));
                GP.AddLine(new Point(Rect.X + 1, Rect.Y + Rect.Height), new Point(Rect.X, Rect.Y + Rect.Height));
                break;
            case RoundingStyle.BottomRight:
                GP.AddLine(new Point(Rect.X, Rect.Y + 1), new Point(Rect.X, Rect.Y));
                GP.AddLine(new Point(Rect.X + Rect.Width - 1, Rect.Y), new Point(Rect.X + Rect.Width, Rect.Y));
                GP.AddArc(new Rectangle(Rect.Width - AW + Rect.X, Rect.Height - AW + Rect.Y, AW, AW), 0, 90);
                GP.AddLine(new Point(Rect.X + 1, Rect.Y + Rect.Height), new Point(Rect.X, Rect.Y + Rect.Height));
                break;
        }

        GP.CloseAllFigures();

        return GP;

    }

}

public class cTabControl : TabControl
{

    private Graphics G;

    private Rectangle Rect;
    public cTabControl()
    {
        DoubleBuffered = true;
        SizeMode = TabSizeMode.Fixed;
        Alignment = TabAlignment.Left;
        ItemSize = new Size(40, 170);
        SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque | ControlStyles.OptimizedDoubleBuffer, true);
    }


    protected override void OnHandleCreated(EventArgs e)
    {
        foreach (TabPage T in TabPages)
        {
            T.Font = new Font("Segoe UI", 9);
            T.ForeColor = Color.FromArgb(220, 220, 220);
            T.BackColor = Color.FromArgb(44, 44, 43);
        }

        base.OnHandleCreated(e);
    }


    protected override void OnPaint(PaintEventArgs e)
    {
        G = e.Graphics;

        G.Clear(Color.FromArgb(34, 34, 33));

        using (Pen Right = new Pen(Color.FromArgb(34, 34, 33)))
        {
            G.DrawLine(Right, ItemSize.Height + 3, 4, ItemSize.Height + 3, Height - 5);
        }


        for (int T = 0; T <= TabPages.Count - 1; T++)
        {
            Rect = GetTabRect(T);


            if (SelectedIndex == T)
            {
                using (SolidBrush Selection = new SolidBrush(Color.FromArgb(44, 44, 43)))
                {
                    G.FillRectangle(Selection, new Rectangle(Rect.X - 4, Rect.Y + 2, Rect.Width + 6, Rect.Height - 2));
                }

                using (SolidBrush TextBrush = new SolidBrush(Color.FromArgb(220, 220, 219)))
                {
                    using (Font TextFont = new Font("Segoe UI", 10))
                    {
                        G.DrawString(TabPages[T].Text, TextFont, TextBrush, new Point(Rect.X + 45, Rect.Y + 11));
                    }
                }

                using (Pen Line = new Pen(Color.FromArgb(34, 34, 33)))
                {
                    G.DrawLine(Line, Rect.X - 2, Rect.Y + 1, Rect.Width + 4, Rect.Y + 1);
                    G.DrawLine(Line, Rect.X - 2, Rect.Y + 39, Rect.Width + 4, Rect.Y + 39);
                }


            }
            else
            {
                using (SolidBrush TextBrush = new SolidBrush(Color.FromArgb(180, 180, 179)))
                {
                    using (Font TextFont = new Font("Segoe UI", 10))
                    {
                        G.DrawString(TabPages[T].Text, TextFont, TextBrush, new Point(Rect.X + 45, Rect.Y + 11));
                    }
                }

            }

            if ((ImageList != null))
            {
                if (!(TabPages[T].ImageIndex < 0))
                {
                    G.DrawImage(ImageList.Images[TabPages[T].ImageIndex], new Rectangle(Rect.X + 18, Rect.Y + 12, 16, 16));
                }
            }

        }

        base.OnPaint(e);
    }

}

public class cTextBox : Control
{

    private TextBox withEventsField_T;
    private TextBox T
    {
        get { return withEventsField_T; }
        set
        {
            withEventsField_T = value;
            if (withEventsField_T != null)
            {
                withEventsField_T.TextChanged += T_TextChanged;
                withEventsField_T.Enter += T_Enter;
            }
        }
    }
    private Helpers.MouseState State;

    private Graphics G;
    public Schemes Scheme { get; set; }

    public enum Schemes
    {
        Black = 0,
        Red = 1,
        Green = 2,
        Yellow = 3
    }

    public new string Text
    {
        get { return T.Text; }
        set
        {
            base.Text = value;
            T.Text = value;
            Invalidate();
        }
    }

    public new bool Enabled
    {
        get { return T.Enabled; }
        set
        {
            T.Enabled = value;
            Invalidate();
        }
    }

    public bool UseSystemPasswordChar
    {
        get { return T.UseSystemPasswordChar; }
        set
        {
            T.UseSystemPasswordChar = value;
            Invalidate();
        }
    }

    public int MaxLength
    {
        get { return T.MaxLength; }
        set
        {
            T.MaxLength = value;
            Invalidate();
        }
    }

    public new bool ReadOnly
    {
        get { return T.ReadOnly; }
        set
        {
            T.ReadOnly = value;
            Invalidate();
        }
    }

    protected override void OnGotFocus(EventArgs e)
    {
        T.Focus();
        base.OnGotFocus(e);
    }

    public cTextBox()
    {
        DoubleBuffered = true;

        T = new TextBox
        {
            BorderStyle = BorderStyle.None,
            BackColor = Color.FromArgb(34, 34, 33),
            ForeColor = Color.FromArgb(220, 220, 220),
            Location = new Point(7, 7),
            Multiline = false
        };

        Controls.Add(T);
    }


    protected override void OnPaint(PaintEventArgs e)
    {
        G = e.Graphics;
        G.SmoothingMode = SmoothingMode.HighQuality;

        using (SolidBrush Back = new SolidBrush(Color.FromArgb(34, 34, 33)))
        {
            G.FillPath(Back, Helpers.RoundRect(new Rectangle(0, 0, Width - 1, Height - 1), 1));
        }

        switch (Scheme)
        {

            case Schemes.Black:

                using (Pen Border = new Pen(Color.FromArgb(33, 33, 32)))
                {
                    G.DrawPath(Border, Helpers.RoundRect(new Rectangle(0, 0, Width - 1, Height - 1), 1));
                }


                break;
            case Schemes.Red:

                using (Pen Border = new Pen(Color.FromArgb(150, 50, 50)))
                {
                    G.DrawPath(Border, Helpers.RoundRect(new Rectangle(0, 0, Width - 1, Height - 1), 1));
                }


                break;
            case Schemes.Green:

                using (Pen Border = new Pen(Color.FromArgb(50, 150, 50)))
                {
                    G.DrawPath(Border, Helpers.RoundRect(new Rectangle(0, 0, Width - 1, Height - 1), 1));
                }


                break;
            case Schemes.Yellow:

                using (Pen Border = new Pen(Color.FromArgb(150, 120, 50)))
                {
                    G.DrawPath(Border, Helpers.RoundRect(new Rectangle(0, 0, Width - 1, Height - 1), 1));
                }


                break;


        }



        base.OnPaint(e);

    }

    protected override void OnEnter(EventArgs e)
    {
        State = Helpers.MouseState.Down;
        Invalidate();
        base.OnEnter(e);
    }

    protected override void OnLeave(EventArgs e)
    {
        State = Helpers.MouseState.None;
        Invalidate();
        base.OnLeave(e);
    }

    protected override void OnResize(EventArgs e)
    {
        Size = new Size(Width, 30);
        T.Size = new Size(Width - 10, Height - 14);
        base.OnResize(e);
    }

    private void T_TextChanged(object sender, EventArgs e)
    {
        base.OnTextChanged(EventArgs.Empty);
    }

    private void T_Enter(object sender, EventArgs e)
    {
        base.OnEnter(EventArgs.Empty);
    }

}

public class cCheckBox : CheckBox
{


    private Graphics G;
    public cCheckBox()
    {
        DoubleBuffered = true;
        Font = new Font("Segoe UI", 9);
        SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque | ControlStyles.OptimizedDoubleBuffer, true);
    }


    protected override void OnPaint(PaintEventArgs e)
    {
        G = e.Graphics;

        base.OnPaint(e);

        G.Clear(Parent.BackColor);


        if (Enabled)
        {
            using (SolidBrush Back = new SolidBrush(Color.FromArgb(34, 34, 33)))
            {
                G.FillPath(Back, Helpers.RoundRect(new Rectangle(0, 0, 16, 16), 1));
            }

            using (Pen Border = new Pen(Color.FromArgb(33, 33, 32)))
            {
                G.DrawPath(Border, Helpers.RoundRect(new Rectangle(0, 0, 16, 16), 1));
            }

            using (SolidBrush TextBrush = new SolidBrush(Color.FromArgb(220, 220, 219)))
            {
                using (Font TextFont = new Font("Segoe UI", 9))
                {
                    G.DrawString(Text, TextFont, TextBrush, new Point(22, 0));
                }
            }


            if (Checked)
            {
                using (SolidBrush TextBrush = new SolidBrush(Color.FromArgb(220, 220, 219)))
                {
                    using (Font TextFont = new Font("Marlett", 12))
                    {
                        G.DrawString("b", TextFont, TextBrush, new Point(-2, 1));
                    }
                }

            }


        }
        else
        {
            using (SolidBrush Back = new SolidBrush(Color.FromArgb(37, 37, 36)))
            {
                G.FillPath(Back, Helpers.RoundRect(new Rectangle(0, 0, 16, 16), 1));
            }

            using (Pen Border = new Pen(Color.FromArgb(36, 36, 35)))
            {
                G.DrawPath(Border, Helpers.RoundRect(new Rectangle(0, 0, 16, 16), 1));
            }

            using (SolidBrush TextBrush = new SolidBrush(Color.FromArgb(130, 130, 129)))
            {
                using (Font TextFont = new Font("Segoe UI", 9))
                {
                    G.DrawString(Text, TextFont, TextBrush, new Point(22, 0));
                }
            }


            if (Checked)
            {
                using (SolidBrush TextBrush = new SolidBrush(Color.FromArgb(130, 130, 129)))
                {
                    using (Font TextFont = new Font("Marlett", 12))
                    {
                        G.DrawString("b", TextFont, TextBrush, new Point(-2, 1));
                    }
                }

            }

        }


    }

}

public class cButton : Button
{


    private Graphics G;

    private Helpers.MouseState State;

    private Schemes SchemeClone = Schemes.Black;
    public Schemes Scheme
    {
        get { return SchemeClone; }
        set
        {
            SchemeClone = value;
            Invalidate();
        }
    }

    public enum Schemes : byte
    {
        Black = 0,
        Green = 1,
        Red = 2,
        Blue = 3
    }

    public cButton()
    {
        DoubleBuffered = true;
        SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque | ControlStyles.OptimizedDoubleBuffer, true);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        G = e.Graphics;

        base.OnPaint(e);

        G.Clear(Parent.BackColor);

        switch (Scheme)
        {

            case Schemes.Black:


                if (Enabled)
                {

                    if (State == Helpers.MouseState.None)
                    {
                        using (SolidBrush Background = new SolidBrush(Color.FromArgb(54, 54, 53)))
                        {
                            using (Pen Border = new Pen(Color.FromArgb(42, 42, 41)))
                            {
                                G.FillPath(Background, Helpers.RoundRect(new Rectangle(0, 0, Width - 1, Height - 1), 2));
                                G.DrawPath(Border, Helpers.RoundRect(new Rectangle(0, 0, Width - 1, Height - 1), 2));
                            }
                        }


                    }
                    else if (State == Helpers.MouseState.Over)
                    {
                        using (SolidBrush Background = new SolidBrush(Color.FromArgb(58, 58, 57)))
                        {
                            using (Pen Border = new Pen(Color.FromArgb(46, 46, 45)))
                            {
                                G.FillPath(Background, Helpers.RoundRect(new Rectangle(0, 0, Width - 1, Height - 1), 2));
                                G.DrawPath(Border, Helpers.RoundRect(new Rectangle(0, 0, Width - 1, Height - 1), 2));
                            }
                        }


                    }
                    else if (State == Helpers.MouseState.Down)
                    {
                        using (SolidBrush Background = new SolidBrush(Color.FromArgb(50, 50, 49)))
                        {
                            using (Pen Border = new Pen(Color.FromArgb(38, 38, 37)))
                            {
                                G.FillPath(Background, Helpers.RoundRect(new Rectangle(0, 0, Width - 1, Height - 1), 2));
                                G.DrawPath(Border, Helpers.RoundRect(new Rectangle(0, 0, Width - 1, Height - 1), 2));
                            }
                        }

                    }


                }
                else
                {
                    using (SolidBrush Background = new SolidBrush(Color.FromArgb(40, 40, 39)))
                    {
                        using (Pen Border = new Pen(Color.FromArgb(38, 38, 37)))
                        {
                            G.FillPath(Background, Helpers.RoundRect(new Rectangle(0, 0, Width - 1, Height - 1), 2));
                            G.DrawPath(Border, Helpers.RoundRect(new Rectangle(0, 0, Width - 1, Height - 1), 2));
                        }
                    }

                }

                break;
            case Schemes.Green:


                if (State == Helpers.MouseState.None)
                {
                    using (SolidBrush Background = new SolidBrush(Color.FromArgb(123, 164, 93)))
                    {
                        using (Pen Border = new Pen(Color.FromArgb(119, 160, 89)))
                        {
                            G.FillPath(Background, Helpers.RoundRect(new Rectangle(0, 0, Width - 1, Height - 1), 2));
                            G.DrawPath(Border, Helpers.RoundRect(new Rectangle(0, 0, Width - 1, Height - 1), 2));
                        }
                    }


                }
                else if (State == Helpers.MouseState.Over)
                {
                    using (SolidBrush Background = new SolidBrush(Color.FromArgb(127, 168, 97)))
                    {
                        using (Pen Border = new Pen(Color.FromArgb(123, 164, 93)))
                        {
                            G.FillPath(Background, Helpers.RoundRect(new Rectangle(0, 0, Width - 1, Height - 1), 2));
                            G.DrawPath(Border, Helpers.RoundRect(new Rectangle(0, 0, Width - 1, Height - 1), 2));
                        }
                    }


                }
                else if (State == Helpers.MouseState.Down)
                {
                    using (SolidBrush Background = new SolidBrush(Color.FromArgb(119, 160, 93)))
                    {
                        using (Pen Border = new Pen(Color.FromArgb(115, 156, 85)))
                        {
                            G.FillPath(Background, Helpers.RoundRect(new Rectangle(0, 0, Width - 1, Height - 1), 2));
                            G.DrawPath(Border, Helpers.RoundRect(new Rectangle(0, 0, Width - 1, Height - 1), 2));
                        }
                    }

                }

                break;
            case Schemes.Red:


                if (State == Helpers.MouseState.None)
                {
                    using (SolidBrush Background = new SolidBrush(Color.FromArgb(164, 93, 93)))
                    {
                        using (Pen Border = new Pen(Color.FromArgb(160, 89, 89)))
                        {
                            G.FillPath(Background, Helpers.RoundRect(new Rectangle(0, 0, Width - 1, Height - 1), 2));
                            G.DrawPath(Border, Helpers.RoundRect(new Rectangle(0, 0, Width - 1, Height - 1), 2));
                        }
                    }


                }
                else if (State == Helpers.MouseState.Over)
                {
                    using (SolidBrush Background = new SolidBrush(Color.FromArgb(168, 97, 97)))
                    {
                        using (Pen Border = new Pen(Color.FromArgb(164, 93, 93)))
                        {
                            G.FillPath(Background, Helpers.RoundRect(new Rectangle(0, 0, Width - 1, Height - 1), 2));
                            G.DrawPath(Border, Helpers.RoundRect(new Rectangle(0, 0, Width - 1, Height - 1), 2));
                        }
                    }


                }
                else if (State == Helpers.MouseState.Down)
                {
                    using (SolidBrush Background = new SolidBrush(Color.FromArgb(160, 89, 89)))
                    {
                        using (Pen Border = new Pen(Color.FromArgb(156, 85, 85)))
                        {
                            G.FillPath(Background, Helpers.RoundRect(new Rectangle(0, 0, Width - 1, Height - 1), 2));
                            G.DrawPath(Border, Helpers.RoundRect(new Rectangle(0, 0, Width - 1, Height - 1), 2));
                        }
                    }

                }

                break;
            case Schemes.Blue:


                if (State == Helpers.MouseState.None)
                {
                    using (SolidBrush Background = new SolidBrush(Color.FromArgb(93, 154, 164)))
                    {
                        using (Pen Border = new Pen(Color.FromArgb(89, 150, 160)))
                        {
                            G.FillPath(Background, Helpers.RoundRect(new Rectangle(0, 0, Width - 1, Height - 1), 2));
                            G.DrawPath(Border, Helpers.RoundRect(new Rectangle(0, 0, Width - 1, Height - 1), 2));
                        }
                    }


                }
                else if (State == Helpers.MouseState.Over)
                {
                    using (SolidBrush Background = new SolidBrush(Color.FromArgb(97, 160, 168)))
                    {
                        using (Pen Border = new Pen(Color.FromArgb(93, 154, 164)))
                        {
                            G.FillPath(Background, Helpers.RoundRect(new Rectangle(0, 0, Width - 1, Height - 1), 2));
                            G.DrawPath(Border, Helpers.RoundRect(new Rectangle(0, 0, Width - 1, Height - 1), 2));
                        }
                    }


                }
                else if (State == Helpers.MouseState.Down)
                {
                    using (SolidBrush Background = new SolidBrush(Color.FromArgb(89, 150, 160)))
                    {
                        using (Pen Border = new Pen(Color.FromArgb(85, 146, 156)))
                        {
                            G.FillPath(Background, Helpers.RoundRect(new Rectangle(0, 0, Width - 1, Height - 1), 2));
                            G.DrawPath(Border, Helpers.RoundRect(new Rectangle(0, 0, Width - 1, Height - 1), 2));
                        }
                    }

                }

                break;
        }


        if (Scheme == Schemes.Black)
        {
            if (Enabled)
            {
                using (SolidBrush TextBrush = new SolidBrush(Color.FromArgb(220, 220, 219)))
                {
                    using (Font TextFont = new Font("Segoe UI", 9))
                    {
                        using (StringFormat SF = new StringFormat { Alignment = StringAlignment.Center })
                        {
                            G.DrawString(Text, TextFont, TextBrush, new Rectangle(0, Height / 2 - 9, Width, Height), SF);
                        }
                    }
                }


            }
            else
            {
                using (SolidBrush TextBrush = new SolidBrush(Color.FromArgb(140, 140, 139)))
                {
                    using (Font TextFont = new Font("Segoe UI", 9))
                    {
                        using (StringFormat SF = new StringFormat { Alignment = StringAlignment.Center })
                        {
                            G.DrawString(Text, TextFont, TextBrush, new Rectangle(0, Height / 2 - 9, Width, Height), SF);
                        }
                    }
                }

            }


        }
        else
        {
            if (!Enabled)
            {
                Scheme = Schemes.Black;
            }

            using (SolidBrush TextBrush = new SolidBrush(Color.FromArgb(250, 250, 249)))
            {
                using (Font TextFont = new Font("Segoe UI", 9))
                {
                    using (StringFormat SF = new StringFormat { Alignment = StringAlignment.Center })
                    {
                        G.DrawString(Text, TextFont, TextBrush, new Rectangle(0, Height / 2 - 9, Width, Height), SF);
                    }
                }
            }

        }



    }

    protected override void OnMouseEnter(EventArgs e)
    {
        State = Helpers.MouseState.Over;
        Invalidate();
        base.OnMouseEnter(e);
    }

    protected override void OnMouseLeave(EventArgs e)
    {
        State = Helpers.MouseState.None;
        Invalidate();
        base.OnMouseLeave(e);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
        State = Helpers.MouseState.Over;
        Invalidate();
        base.OnMouseUp(e);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        State = Helpers.MouseState.Down;
        Invalidate();
        base.OnMouseDown(e);
    }

}

public class cStripRenderer : ToolStripRenderer
{

    public event PaintMenuBackgroundEventHandler PaintMenuBackground;
    public delegate void PaintMenuBackgroundEventHandler(object sender, ToolStripRenderEventArgs e);
    public event PaintMenuBorderEventHandler PaintMenuBorder;
    public delegate void PaintMenuBorderEventHandler(object sender, ToolStripRenderEventArgs e);
    public event PaintMenuImageMarginEventHandler PaintMenuImageMargin;
    public delegate void PaintMenuImageMarginEventHandler(object sender, ToolStripRenderEventArgs e);
    public event PaintItemCheckEventHandler PaintItemCheck;
    public delegate void PaintItemCheckEventHandler(object sender, ToolStripItemImageRenderEventArgs e);
    public event PaintItemImageEventHandler PaintItemImage;
    public delegate void PaintItemImageEventHandler(object sender, ToolStripItemImageRenderEventArgs e);
    public event PaintItemTextEventHandler PaintItemText;
    public delegate void PaintItemTextEventHandler(object sender, ToolStripItemTextRenderEventArgs e);
    public event PaintItemBackgroundEventHandler PaintItemBackground;
    public delegate void PaintItemBackgroundEventHandler(object sender, ToolStripItemRenderEventArgs e);
    public event PaintItemArrowEventHandler PaintItemArrow;
    public delegate void PaintItemArrowEventHandler(object sender, ToolStripArrowRenderEventArgs e);
    public event PaintSeparatorEventHandler PaintSeparator;
    public delegate void PaintSeparatorEventHandler(object sender, ToolStripSeparatorRenderEventArgs e);

    protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
    {
        if (PaintMenuBackground != null)
        {
            PaintMenuBackground(this, e);
        }
    }

    protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
    {
        if (PaintMenuImageMargin != null)
        {
            PaintMenuImageMargin(this, e);
        }
    }

    protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
    {
        if (PaintMenuBorder != null)
        {
            PaintMenuBorder(this, e);
        }
    }

    protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
    {
        if (PaintItemCheck != null)
        {
            PaintItemCheck(this, e);
        }
    }

    protected override void OnRenderItemImage(ToolStripItemImageRenderEventArgs e)
    {
        if (PaintItemImage != null)
        {
            PaintItemImage(this, e);
        }
    }

    protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
    {
        if (PaintItemText != null)
        {
            PaintItemText(this, e);
        }
    }

    protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
    {
        if (PaintItemBackground != null)
        {
            PaintItemBackground(this, e);
        }
    }

    protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
    {
        if (PaintItemArrow != null)
        {
            PaintItemArrow(this, e);
        }
    }

    protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
    {
        if (PaintSeparator != null)
        {
            PaintSeparator(this, e);
        }
    }

}

public class cContextMenuStrip : ContextMenuStrip
{

    private Rectangle Rect;

    private Graphics G;
    public cContextMenuStrip()
    {
        cStripRenderer Renderer = new cStripRenderer();
        Renderer.PaintMenuBackground += Renderer_PaintMenuBackground;
        Renderer.PaintMenuBorder += Renderer_PaintMenuBorder;
        Renderer.PaintItemImage += Renderer_PaintItemImage;
        Renderer.PaintItemText += Renderer_PaintItemText;
        Renderer.PaintItemBackground += Renderer_PaintItemBackground;
        Renderer.PaintItemArrow += Renderer_PaintItemArrow;
        base.Renderer = Renderer;
    }

    private void Renderer_PaintMenuBackground(object sender, ToolStripRenderEventArgs e)
    {
        G = e.Graphics;

        G.Clear(Color.FromArgb(44, 44, 43));
    }


    private void Renderer_PaintMenuBorder(object sender, ToolStripRenderEventArgs e)
    {
        G = e.Graphics;

        using (Pen Border = new Pen(Color.FromArgb(42, 42, 41)))
        {
            G.DrawRectangle(Border, new Rectangle(e.AffectedBounds.X, e.AffectedBounds.Y, e.AffectedBounds.Width - 1, e.AffectedBounds.Height - 1));
        }

    }


    private void Renderer_PaintItemArrow(object sender, ToolStripArrowRenderEventArgs e)
    {
        G = e.Graphics;

        using (Font TextFont = new Font("Marlett", 12))
        {
            using (SolidBrush TextBrush = new SolidBrush(Color.FromArgb(200, 200, 200)))
            {
                G.DrawString("8", TextFont, TextBrush, new Point(e.ArrowRectangle.X - 2, e.ArrowRectangle.Y + 1));
            }
        }

    }


    private void Renderer_PaintItemImage(object sender, ToolStripItemImageRenderEventArgs e)
    {
        G = e.Graphics;

        G.DrawImage(e.Image, new Rectangle(4, 1, 16, 16));

    }


    private void Renderer_PaintItemText(object sender, ToolStripItemTextRenderEventArgs e)
    {
        G = e.Graphics;

        using (Font ItemFont = new Font("Segoe UI", 9))
        {
            using (SolidBrush ItemBrush = new SolidBrush(Color.FromArgb(220, 220, 220)))
            {
                G.DrawString(e.Text, ItemFont, ItemBrush, new Point(e.TextRectangle.X, e.TextRectangle.Y));
            }
        }

    }


    private void Renderer_PaintItemBackground(object sender, ToolStripItemRenderEventArgs e)
    {
        G = e.Graphics;

        Rect = e.Item.ContentRectangle;


        if (e.Item.Selected)
        {
            using (SolidBrush Fill = new SolidBrush(Color.FromArgb(54, 54, 53)))
            {
                G.FillRectangle(Fill, new Rectangle(Rect.X - 1, Rect.Y - 1, Rect.Width + 4, Rect.Height - 1));
            }

        }

    }

}

public class BoosterComboBox : ComboBox
{

    private Rectangle Rect;

    private Graphics G;
    public BoosterComboBox()
    {
        ItemHeight = 20;
        DoubleBuffered = true;
        DropDownStyle = ComboBoxStyle.DropDownList;
        DrawMode = DrawMode.OwnerDrawFixed;
        SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque | ControlStyles.OptimizedDoubleBuffer, true);
    }


    protected override void OnPaint(PaintEventArgs e)
    {
        G = e.Graphics;

        G.Clear(Parent.BackColor);


        if (Enabled)
        {
            using (SolidBrush Fill = new SolidBrush(Color.FromArgb(34, 34, 33)))
            {
                G.FillRectangle(Fill, new Rectangle(0, 0, Width - 1, Height - 1));
            }

            using (Pen Border = new Pen(Color.FromArgb(32, 32, 31)))
            {
                G.DrawRectangle(Border, new Rectangle(0, 0, Width - 1, Height - 1));
            }

            using (Font ArrowFont = new Font("Marlett", 12))
            {
                using (SolidBrush ArrowBrush = new SolidBrush(Color.FromArgb(180, 180, 180)))
                {
                    G.DrawString("6", ArrowFont, ArrowBrush, new Point(Width - 23, 5));
                }
            }


            if (Items.Count > 0)
            {

                if (!(SelectedIndex == -1))
                {
                    using (SolidBrush TextBrush = new SolidBrush(Color.FromArgb(220, 220, 220)))
                    {
                        using (Font TextFont = new Font("Segoe UI", 9))
                        {
                            G.DrawString(GetItemText(Items[SelectedIndex]), TextFont, TextBrush, new Point(4, 4));
                        }
                    }


                }
                else
                {
                    using (SolidBrush TextBrush = new SolidBrush(Color.FromArgb(220, 220, 220)))
                    {
                        using (Font TextFont = new Font("Segoe UI", 9))
                        {
                            G.DrawString(GetItemText(Items[0]), TextFont, TextBrush, new Point(4, 4));
                        }
                    }

                }


            }


        }
        else
        {
            using (SolidBrush Fill = new SolidBrush(Color.FromArgb(28, 28, 27)))
            {
                G.FillRectangle(Fill, new Rectangle(0, 0, Width - 1, Height - 1));
            }

            using (Pen Border = new Pen(Color.FromArgb(26, 26, 25)))
            {
                G.DrawRectangle(Border, new Rectangle(0, 0, Width - 1, Height - 1));
            }

            using (Font ArrowFont = new Font("Marlett", 12))
            {
                using (SolidBrush ArrowBrush = new SolidBrush(Color.FromArgb(130, 130, 129)))
                {
                    G.DrawString("6", ArrowFont, ArrowBrush, new Point(Width - 23, 5));
                }
            }


            if (Items.Count > 0)
            {
                using (SolidBrush TextBrush = new SolidBrush(Color.FromArgb(130, 130, 129)))
                {
                    using (Font TextFont = new Font("Segoe UI", 9))
                    {
                        G.DrawString(GetItemText(Items[0]), TextFont, TextBrush, new Point(4, 4));
                    }
                }

            }

        }

        base.OnPaint(e);

    }


    protected override void OnDrawItem(DrawItemEventArgs e)
    {
        G = e.Graphics;

        Rect = e.Bounds;

        using (SolidBrush Back = new SolidBrush(Color.FromArgb(34, 34, 33)))
        {
            G.FillRectangle(Back, new Rectangle(e.Bounds.X - 4, e.Bounds.Y - 1, e.Bounds.Width + 4, e.Bounds.Height + 1));
        }


        if (!(e.Index == -1))
        {
            using (Font ItemsFont = new Font("Segoe UI", 9))
            {
                using (Pen Border = new Pen(Color.FromArgb(38, 38, 37)))
                {


                    if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                    {
                        using (SolidBrush HoverItemBrush = new SolidBrush(Color.FromArgb(220, 220, 220)))
                        {
                            using (SolidBrush HoverItemFill = new SolidBrush(Color.FromArgb(38, 38, 37)))
                            {
                                G.FillRectangle(HoverItemFill, new Rectangle(Rect.X - 1, Rect.Y + 2, Rect.Width + 1, Rect.Height - 4));
                                G.DrawString(GetItemText(Items[e.Index]), new Font("Segoe UI", 9), HoverItemBrush, new Point(Rect.X + 5, Rect.Y + 1));
                            }
                        }


                    }
                    else
                    {
                        using (SolidBrush DefaultItemBrush = new SolidBrush(Color.FromArgb(220, 220, 220)))
                        {
                            G.DrawString(GetItemText(Items[e.Index]), new Font("Segoe UI", 9), DefaultItemBrush, new Point(Rect.X + 5, Rect.Y + 1));
                        }

                    }

                }
            }

        }

        base.OnDrawItem(e);

    }

    protected override void OnSelectedItemChanged(EventArgs e)
    {
        Invalidate();
        base.OnSelectedItemChanged(e);
    }

}
