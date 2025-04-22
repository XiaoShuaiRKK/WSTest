using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SS1Winform
{
    public partial class CardControl : UserControl
    {
        private string title, subTitle, datetime;
        public CardControl(string title,string subTitle,string datetime)
        {
            this.title = title;
            this.subTitle = subTitle;
            this.datetime = datetime;
            InitializeComponent();
        }

        private void CardControl_Load(object sender, EventArgs e)
        {
            label1.Text = title;
            label2.Text = subTitle;
            label3.Text = datetime;
        }
    }
}
