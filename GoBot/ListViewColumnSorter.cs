using System.Collections;
using System.Windows.Forms;
using System;

class Sorter : System.Collections.IComparer
{
    public int Column = 0;
    public System.Windows.Forms.SortOrder Order = SortOrder.Ascending;
    public int Compare(object x, object y) // IComparer Member
    {
        if (!(x is ListViewItem))
            return (0);
        if (!(y is ListViewItem))
            return (0);

        ListViewItem l1 = (ListViewItem)x;
        ListViewItem l2 = (ListViewItem)y;

        if (l1.ListView.Columns[Column].Tag == null)
        {
            l1.ListView.Columns[Column].Tag = "Text";
        }

        if (l1.ListView.Columns[Column].Tag.ToString() == "Numeric")
        {
            if (string.IsNullOrEmpty(l1.SubItems[Column].Text) || string.IsNullOrEmpty(l2.SubItems[Column].Text))
                return 0;
            try
            {
                if (l1.SubItems[Column].Text.Contains("/") && l2.SubItems[Column].Text.Contains("/"))
                {
                    float fl1 = float.Parse(l1.SubItems[Column].Text.Split('/')[0]);
                    float fl2 = float.Parse(l2.SubItems[Column].Text.Split('/')[0]);

                    if (Order == SortOrder.Ascending)
                    {
                        return fl1.CompareTo(fl2);
                    }
                    else
                    {
                        return fl2.CompareTo(fl1);
                    }
                }
                else
                {
                    float fl1 = float.Parse(l1.SubItems[Column].Text);
                    float fl2 = float.Parse(l2.SubItems[Column].Text);

                    if (Order == SortOrder.Ascending)
                    {
                        return fl1.CompareTo(fl2);
                    }
                    else
                    {
                        return fl2.CompareTo(fl1);
                    }

                }
               
            }
            catch (Exception)
            {
                return 0;
            }
        }
        else
        {
            string str1 = l1.SubItems[Column].Text;
            string str2 = l2.SubItems[Column].Text;

            if (Order == SortOrder.Ascending)
            {
                return str1.CompareTo(str2);
            }
            else
            {
                return str2.CompareTo(str1);
            }
        }
    }
}