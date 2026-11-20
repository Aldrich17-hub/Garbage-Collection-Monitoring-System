#nullable disable
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Windows.Forms;

namespace GarbageCollectionMonitoringSystem
{
    /// <summary>Creates and paints the styled schedule DataGridView (pills for category / status, driver avatar).</summary>
    public static class ScheduleGrid
    {
        private static readonly Font PillFont = Theme.F(7.75f, FontStyle.Bold);
        private static readonly Font AvatarFont = Theme.F(7f, FontStyle.Bold);
        private static readonly Font NameFont = Theme.F(9f);

        public static DataGridView Create()
        {
            DataGridView g = new DataGridView();
            g.AllowUserToAddRows = false;
            g.AllowUserToDeleteRows = false;
            g.AllowUserToResizeRows = false;
            g.AllowUserToResizeColumns = false;
            g.ReadOnly = true;
            g.RowHeadersVisible = false;
            g.BorderStyle = BorderStyle.None;
            g.BackgroundColor = Color.White;
            g.GridColor = Theme.Border;
            g.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            g.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            g.EnableHeadersVisualStyles = false;
            g.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            g.MultiSelect = false;
            g.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            g.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            g.ColumnHeadersHeight = 38;
            g.RowTemplate.Height = 58;

            typeof(DataGridView).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(g, true, null);

            Color headBack = ColorTranslator.FromHtml("#F1F8F5");
            g.ColumnHeadersDefaultCellStyle.BackColor = headBack;
            g.ColumnHeadersDefaultCellStyle.SelectionBackColor = headBack;
            g.ColumnHeadersDefaultCellStyle.ForeColor = Theme.TextMuted;
            g.ColumnHeadersDefaultCellStyle.SelectionForeColor = Theme.TextMuted;
            g.ColumnHeadersDefaultCellStyle.Font = Theme.F(7.5f, FontStyle.Bold);
            g.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 0, 0);

            g.DefaultCellStyle.BackColor = Color.White;
            g.DefaultCellStyle.ForeColor = Theme.TextDark;
            g.DefaultCellStyle.SelectionBackColor = Theme.MintSoft;
            g.DefaultCellStyle.SelectionForeColor = Theme.TextDark;
            g.DefaultCellStyle.Font = Theme.F(9f);
            g.DefaultCellStyle.Padding = new Padding(10, 0, 0, 0);

            AddColumn(g, "colBatch", "BATCH ID", 13);
            AddColumn(g, "colWhen", "DATE & TIME", 17);
            AddColumn(g, "colCategory", "CATEGORY", 17);
            AddColumn(g, "colArea", "COLLECTION AREA", 21);
            AddColumn(g, "colDriver", "ASSIGNED DRIVER", 18);
            AddColumn(g, "colStatus", "STATUS", 14);
            g.Columns[0].DefaultCellStyle.Font = Theme.F(9f, FontStyle.Bold);
            g.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            g.CellPainting += CellPaint;
            return g;
        }

        private static void AddColumn(DataGridView g, string name, string header, float weight)
        {
            DataGridViewTextBoxColumn c = new DataGridViewTextBoxColumn();
            c.Name = name;
            c.HeaderText = header;
            c.FillWeight = weight;
            c.SortMode = DataGridViewColumnSortMode.NotSortable;
            g.Columns.Add(c);
        }

        public static void Populate(DataGridView g, List<ScheduleRow> rows)
        {
            g.Rows.Clear();
            foreach (ScheduleRow r in rows)
                g.Rows.Add(r.BatchId, r.WhenText, r.Category, r.Area, r.Driver, r.Status);
            g.ClearSelection();
        }

        private static void CategoryColors(string c, out Color back, out Color fore)
        {
            switch (c)
            {
                case "Recyclable": back = ColorTranslator.FromHtml("#DCEAFB"); fore = ColorTranslator.FromHtml("#1F5FBF"); break;
                case "Non-Biodegradable": back = ColorTranslator.FromHtml("#EBDFFA"); fore = ColorTranslator.FromHtml("#6A2DB8"); break;
                case "Hazardous": back = ColorTranslator.FromHtml("#FDE6D3"); fore = ColorTranslator.FromHtml("#C2410C"); break;
                default: back = ColorTranslator.FromHtml("#DDF3E8"); fore = ColorTranslator.FromHtml("#0E7A52"); break;
            }
        }

        private static void StatusColors(string s, out Color back, out Color fore)
        {
            switch (s)
            {
                case "Ongoing": back = ColorTranslator.FromHtml("#DCEAFB"); fore = ColorTranslator.FromHtml("#1F5FBF"); break;
                case "Completed": back = ColorTranslator.FromHtml("#DDF3E8"); fore = ColorTranslator.FromHtml("#0E7A52"); break;
                case "Missed": back = ColorTranslator.FromHtml("#FCE0E0"); fore = ColorTranslator.FromHtml("#C62828"); break;
                default: back = ColorTranslator.FromHtml("#FEF1D6"); fore = ColorTranslator.FromHtml("#B45309"); break;
            }
        }

        private static string Initials(string name)
        {
            string[] parts = (name ?? "").Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0) return "?";
            string s = parts[0].Substring(0, 1);
            if (parts.Length > 1) s += parts[parts.Length - 1].Substring(0, 1);
            return s.ToUpperInvariant();
        }

        private static void CellPaint(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || (e.ColumnIndex != 2 && e.ColumnIndex != 4 && e.ColumnIndex != 5)) return;

            bool selected = (e.State & DataGridViewElementStates.Selected) != 0;
            e.PaintBackground(e.CellBounds, selected);
            e.Paint(e.CellBounds, DataGridViewPaintParts.Border);

            string text = Convert.ToString(e.Value);
            if (!string.IsNullOrEmpty(text))
            {
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                if (e.ColumnIndex == 4)
                {
                    int d = 26;
                    Rectangle circle = new Rectangle(e.CellBounds.X + 10, e.CellBounds.Y + (e.CellBounds.Height - d) / 2, d, d);
                    using (SolidBrush b = new SolidBrush(Theme.MintSoft)) g.FillEllipse(b, circle);
                    TextRenderer.DrawText(g, Initials(text), AvatarFont, circle, Theme.Accent,
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);
                    Rectangle tr = new Rectangle(circle.Right + 8, e.CellBounds.Y, Math.Max(10, e.CellBounds.Width - d - 22), e.CellBounds.Height);
                    TextRenderer.DrawText(g, text, NameFont, tr, Theme.TextDark,
                        TextFormatFlags.VerticalCenter | TextFormatFlags.Left | TextFormatFlags.EndEllipsis | TextFormatFlags.SingleLine);
                }
                else
                {
                    Color back, fore;
                    string label = text;
                    if (e.ColumnIndex == 2) CategoryColors(text, out back, out fore);
                    else { StatusColors(text, out back, out fore); label = "\u25CF " + text; }

                    Size sz = TextRenderer.MeasureText(g, label, PillFont, new Size(int.MaxValue, int.MaxValue), TextFormatFlags.NoPadding);
                    Rectangle pill = new Rectangle(e.CellBounds.X + 10, e.CellBounds.Y + (e.CellBounds.Height - 22) / 2, sz.Width + 20, 22);
                    using (GraphicsPath path = Theme.RoundedRect(pill, 11))
                    using (SolidBrush b = new SolidBrush(back)) g.FillPath(b, path);
                    TextRenderer.DrawText(g, label, PillFont, pill, fore,
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);
                }
            }
            e.Handled = true;
        }
    }
}
