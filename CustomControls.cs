#nullable disable
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace GarbageCollectionMonitoringSystem
{
    /// <summary>Panel with rounded corners and an optional border (used for cards).</summary>
    public class RoundedPanel : Panel
    {
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public int Radius { get; set; } = 14;
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public Color BorderColor { get; set; } = Theme.Border;
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public bool DrawBorder { get; set; } = true;

        public RoundedPanel()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            BackColor = Color.White;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.Clear(Parent != null ? Parent.BackColor : SystemColors.Control);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            using (GraphicsPath path = Theme.RoundedRect(rect, Radius))
            using (SolidBrush b = new SolidBrush(BackColor))
            {
                g.FillPath(b, path);
                if (DrawBorder)
                    using (Pen pen = new Pen(BorderColor)) g.DrawPath(pen, path);
            }
        }
    }

    /// <summary>Flat rounded button with hover colour.</summary>
    public class RoundedButton : Button
    {
        private bool hover;
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public int Radius { get; set; } = 10;
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public Color FillColor { get; set; } = Theme.DarkGreen;
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public Color HoverColor { get; set; } = Color.Empty;
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public Color TextColor { get; set; } = Color.White;
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public Color BorderColor { get; set; } = Color.Empty;
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public bool LeftAligned { get; set; }

        public RoundedButton()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            Cursor = Cursors.Hand;
            Font = Theme.F(9.5f, FontStyle.Bold);
        }

        protected override void OnMouseEnter(EventArgs e) { hover = true; Invalidate(); base.OnMouseEnter(e); }
        protected override void OnMouseLeave(EventArgs e) { hover = false; Invalidate(); base.OnMouseLeave(e); }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Parent != null ? Parent.BackColor : SystemColors.Control);

            Color fill = FillColor;
            if (hover) fill = HoverColor != Color.Empty ? HoverColor : ControlPaint.Light(FillColor, 0.12f);

            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            using (GraphicsPath path = Theme.RoundedRect(rect, Radius))
            {
                using (SolidBrush b = new SolidBrush(fill)) g.FillPath(b, path);
                if (BorderColor != Color.Empty)
                    using (Pen pen = new Pen(BorderColor)) g.DrawPath(pen, path);
            }

            TextFormatFlags flags = TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine | TextFormatFlags.EndEllipsis |
                                    (LeftAligned ? TextFormatFlags.Left : TextFormatFlags.HorizontalCenter);
            Rectangle tr = LeftAligned ? new Rectangle(14, 0, Math.Max(1, Width - 14), Height) : ClientRectangle;
            TextRenderer.DrawText(g, Text, Font, tr, TextColor, flags);
        }
    }

    /// <summary>Sidebar navigation button with an "active" (white pill) state.</summary>
    public class NavButton : Button
    {
        private bool hover;
        private bool active;

        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public bool Active
        {
            get { return active; }
            set { active = value; Invalidate(); }
        }

        public NavButton()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            Cursor = Cursors.Hand;
            Size = new Size(208, 40);
            Margin = new Padding(0, 2, 0, 2);
        }

        protected override void OnMouseEnter(EventArgs e) { hover = true; Invalidate(); base.OnMouseEnter(e); }
        protected override void OnMouseLeave(EventArgs e) { hover = false; Invalidate(); base.OnMouseLeave(e); }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Parent != null ? Parent.BackColor : Theme.DarkGreen);

            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            if (active || hover)
            {
                Color fill = active ? Color.White : ColorTranslator.FromHtml("#14291F");
                using (GraphicsPath path = Theme.RoundedRect(rect, 9))
                using (SolidBrush b = new SolidBrush(fill)) g.FillPath(b, path);
            }

            Color fore = active ? Theme.DarkGreen : ColorTranslator.FromHtml("#CFE5DB");
            Font f = Theme.F(9.5f, active ? FontStyle.Bold : FontStyle.Regular);
            TextRenderer.DrawText(g, Text, f, new Rectangle(14, 0, Width - 14, Height), fore,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Left | TextFormatFlags.SingleLine);
            f.Dispose();
        }
    }
}
