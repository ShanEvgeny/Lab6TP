using System.Numerics;
using Лаб6ТП.Objects;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Лаб6ТП
{
    public partial class Form1 : Form
    {
        Emitter emitter;
        CometEmmiter cometEmmiter;
        List<Comet> objects = new ();
        public Form1()
        {
            InitializeComponent();
            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);
            /*for (var i = 0; i < 100; ++i)
            {
                var particle = new Particle();
                particle.X = picDisplay.Image.Width / 2;
                particle.Y = picDisplay.Image.Height / 2;
                particles.Add(particle);
            }*/
            emitter = new LeftEmitter
            {
                Width = picDisplay.Width - 300,
                GravitationX = 0.25f
            };
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            emitter.UpdateState();
            using (var g = Graphics.FromImage(picDisplay.Image))
            {
                g.Clear(Color.Black);
                //emitter.Render(g);
                var is_spawn = new Random().Next(100);
                if (is_spawn <= 5 && objects.Count < 5)
                {
                    Comet newComet = new Comet(-20, new Random().Next(50, this.Height - 80), 0);
                    objects.Add(newComet);
                }
                foreach (var obj in objects.ToList())
                {
                    //updateComet((Comet)obj);
                    //obj.Render(g);
                    var comet = (Comet)obj;
                    comet.Update();
                    if (comet.X > picDisplay.Width)
                    {
                        comet.X = -20;
                        comet.Y = new Random().Next(picDisplay.Height);
                    }
                }
                foreach (var obj in objects)
                {
                    g.Transform = obj.GetTransform();
                    obj.Render(g);
                }
            }
            picDisplay.Invalidate();
        }
        private void picDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            emitter.MousePositionX = e.X;
            emitter.MousePositionY = e.Y;
        }

        private void picDisplay_Paint(object sender, PaintEventArgs e)
        {
            //var g = e.Graphics;
            //g.Clear(Color.Black);
            
        }
        private void updateComet(Comet comet)
        {
            comet.Update();
            if (comet.X > picDisplay.Width)
            {
                comet.X = -20;
                comet.Y = new Random().Next(picDisplay.Height);
            }
            //if (comet.X == picDisplay.Width - 50)
            //{
                //comet.X = 0;
                //comet.Y = new Random().Next(picDisplay.Height);
            //}
            //comet.X += 5;
        }
    }
}