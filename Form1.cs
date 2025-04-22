namespace SS1Winform
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.WrapContents = true;
            LoadCard();
        }

        private void LoadCard()
        {
            flowLayoutPanel1.Controls.Clear();
            for (int i = 0; i < 10; i++)
            {

                var card = new CardControl($"¿¨Æ¬{i}", $"¿¨Æ¬Æ¬{i}", "2025-05-25")
                {
                    Margin = new Padding(10),
                    Width = 200,
                    Height = 100
                };
                flowLayoutPanel1.Controls.Add(card);

            }
        }
    }
}
