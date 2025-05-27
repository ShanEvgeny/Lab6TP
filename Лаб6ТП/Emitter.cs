using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Лаб6ТП.Objects;

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
                    var particleComet = new ParticleColorful();
                    particleComet.FromColor = Color.LightBlue;
                    particleComet.ToColor = Color.FromArgb(0, Color.White);
                    ResetParticle(particleComet);
                    particles.Add(particle);
                    particles.Add(particleComet);
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
        //public int ParticleCount = 50;
        public override void ResetParticle(Particle particle)
        {
            base.ResetParticle(particle);

            particle.X = 0;
            particle.Y = 100 + Particle.rand.Next(Width - 300);

            particle.SpeedY = Particle.rand.Next(-2,2);//поменять на 0 за игры
            particle.SpeedX = 1; 
        }
    }
    public class CometEmmiter : Emitter
    {
        //public float GravitationX = -1;
        //public float GravitationY = 0;
        public Comet comet;
        //public int ParticleCount = 50;
        public CometEmmiter(Comet comet)
        {
           this.comet = comet;
           this.ParticlesCount = 150;
           this.GravitationX = -1;
           this.GravitationY = 0;
        }
        public override void ResetParticle(Particle particle)
        {
            base.ResetParticle(particle);
            particle.X = comet.X + comet.diametr/2;
            particle.Y = comet.Y;
            var direction = (double)Particle.rand.Next(360);
            var speed = 5;
            //particle.SpeedX = (float)(Math.Cos(direction / 180 * Math.PI) * speed);
            //particle.SpeedY = -(float)(Math.Sin(direction / 180 * Math.PI) * speed);
            particle.Radius = 1 + Particle.rand.Next(5);
            particle.Life = 10 + Particle.rand.Next(30);
            particle.SpeedY = Particle.rand.Next(-2, 2);//поменять на 0 за игры
            particle.SpeedX = (float)0.5;
        }
    }

}
