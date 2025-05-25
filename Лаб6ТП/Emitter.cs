using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаб6ТП
{
    public class Emitter
    {
        List<Particle> particles = new List<Particle>();
        public int MousePositionX;
        public int MousePositionY;
        public float GravitationX = 1;
        public float GravitationY = 0;
        public int ParticlesCount = 300;
        public void UpdateState()
        {
            foreach (var particle in particles)
            {
                particle.Life -= 1;
                if (particle.Life < 0)
                {
                    ResetParticle(particle);
                }
                else
                {
                    //var directionInRadians = particle.Direction / 180 * Math.PI;
                    //particle.X += (float)(particle.Speed * Math.Cos(directionInRadians));
                    //particle.Y -= (float)(particle.Speed * Math.Sin(directionInRadians));
                    particle.SpeedX += GravitationX;
                    particle.SpeedY += GravitationY;
                    particle.X += particle.SpeedX;
                    particle.Y += particle.SpeedY;
                }
            }
            for (var i = 0; i < 10; ++i)
            {
                if (particles.Count < ParticlesCount)
                {
                    var particle = new ParticleColorful();
                    particle.FromColor = Color.White;
                    particle.ToColor = Color.FromArgb(0, Color.DarkGray);
                    //particle.X = MousePositionX;
                    //particle.Y = MousePositionY;
                    ResetParticle(particle);
                    particles.Add(particle);
                }
                else
                {
                    break;
                }
            }
        }
        public void Render(Graphics g)
        {
            foreach (var particle in particles)
            {
                particle.Draw(g);
            }
        }
        public virtual void ResetParticle(Particle particle)
        {
            particle.Life = 20 + Particle.rand.Next(100);
            //particle.X = picDisplay.Image.Width / 2;
            //particle.Y = picDisplay.Image.Height / 2;
            particle.X = MousePositionX;
            particle.Y = MousePositionY;
            var direction = (double)Particle.rand.Next(360);
            var speed = 1 + Particle.rand.Next(10);
            particle.SpeedX = (float)(Math.Cos(direction / 180 * Math.PI) * speed);
            particle.SpeedY = -(float)(Math.Sin(direction / 180 * Math.PI) * speed);
            particle.Radius = 2 + Particle.rand.Next(10);
        }
    }
    public class LeftEmitter : Emitter
    {
        public int Width;

        public override void ResetParticle(Particle particle)
        {
            base.ResetParticle(particle);

            particle.X = 0;
            particle.Y = 100 + Particle.rand.Next(Width - 300);

            particle.SpeedY = Particle.rand.Next(-2,2);//поменять на 0 за игры
            particle.SpeedX = 1; 
        }
    }

}
