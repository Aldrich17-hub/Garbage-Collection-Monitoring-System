#nullable disable
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace GarbageCollectionMonitoringSystem
{
    public static class Theme
    {
        public static readonly Color DarkGreen = ColorTranslator.FromHtml("#0A1914");
        public static readonly Color Mint = ColorTranslator.FromHtml("#A9DFCA");
        public static readonly Color MintSoft = ColorTranslator.FromHtml("#E8F6F0");
        public static readonly Color Background = ColorTranslator.FromHtml("#F4F7F6");
        public static readonly Color Border = ColorTranslator.FromHtml("#E0E8E4");
        public static readonly Color TextDark = ColorTranslator.FromHtml("#0F1F1A");
        public static readonly Color TextMuted = ColorTranslator.FromHtml("#6B7C75");
        public static readonly Color Accent = ColorTranslator.FromHtml("#0F8A63");
        public static readonly Color Danger = ColorTranslator.FromHtml("#C62828");

        public static Font F(float size, FontStyle style = FontStyle.Regular)
        {
            return new Font("Segoe UI", size, style, GraphicsUnit.Point);
        }

        public static GraphicsPath RoundedRect(Rectangle r, int radius)
        {
            int d = Math.Max(2, radius * 2);
            GraphicsPath p = new GraphicsPath();
            p.AddArc(r.X, r.Y, d, d, 180, 90);
            p.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            p.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            p.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            p.CloseFigure();
            return p;
        }

        public static Label MakeLabel(string text, float size, FontStyle style, Color fore, Color back, int x, int y)
        {
            Label l = new Label();
            l.Text = text;
            l.AutoSize = true;
            l.Font = F(size, style);
            l.ForeColor = fore;
            l.BackColor = back;
            l.Location = new Point(x, y);
            return l;
        }

        public static TextBox MakeTextBox(int x, int y, int w, string placeholder, bool password = false)
        {
            TextBox t = new TextBox();
            t.Location = new Point(x, y);
            t.Width = w;
            t.Font = F(10f);
            t.BorderStyle = BorderStyle.FixedSingle;
            t.BackColor = Color.White;
            t.ForeColor = TextDark;
            t.PlaceholderText = placeholder;
            t.UseSystemPasswordChar = password;
            return t;
        }

        public static ComboBox MakeCombo(int x, int y, int w)
        {
            ComboBox c = new ComboBox();
            c.Location = new Point(x, y);
            c.Width = w;
            c.Font = F(10f);
            c.DropDownStyle = ComboBoxStyle.DropDownList;
            c.BackColor = Color.White;
            c.ForeColor = TextDark;
            return c;
        }

        public static RoundedButton MakeButton(string text, int x, int y, int w, int h, Color fill, Color textColor, Color border)
        {
            RoundedButton b = new RoundedButton();
            b.Text = text;
            b.Bounds = new Rectangle(x, y, w, h);
            b.FillColor = fill;
            b.TextColor = textColor;
            b.BorderColor = border;
            return b;
        }

        public static RadioButton MakeChip(string text, int x, int y, int w, int h)
        {
            RadioButton r = new RadioButton();
            r.Appearance = Appearance.Button;
            r.FlatStyle = FlatStyle.Flat;
            r.TextAlign = ContentAlignment.MiddleCenter;
            r.Text = text;
            r.Font = F(8.75f, FontStyle.Bold);
            r.BackColor = Color.White;
            r.ForeColor = TextDark;
            r.Cursor = Cursors.Hand;
            r.Bounds = new Rectangle(x, y, w, h);
            r.FlatAppearance.BorderColor = Border;
            r.FlatAppearance.CheckedBackColor = MintSoft;
            r.FlatAppearance.MouseOverBackColor = ColorTranslator.FromHtml("#F3FAF7");
            return r;
        }
    }
}
