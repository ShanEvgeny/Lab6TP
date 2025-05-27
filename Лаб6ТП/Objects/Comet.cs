using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаб6ТП.Objects
{
    public class Comet : BaseSpaceObject
    {
        public float diametr;
        public Action<Comet> DecreaseToZero;
        public Random rand = new Random();
        public int SpeedX = 5;
        public int Life;
        public CometEmmiter cometEmmiter;
        public Comet(float x, float y, float angle) : base(x, y, angle)
        {
            diametr = 20 + rand.Next(40);
            cometEmmiter = new CometEmmiter(this);
        }
        public override void Render(Graphics g)
        {
            if (diametr <= 30)
            {
                Life = 1;
            }
            else if(diametr <= 35)
            {
                Life = 2;
            }
            else
            {
                Life = 3;
            }
            g.FillEllipse(new SolidBrush(Color.LightBlue), -diametr / 2, -diametr / 2, diametr, diametr);
            cometEmmiter.UpdateState();
            cometEmmiter.Render(g);
        }
        public void Update()
        {
            X += SpeedX;
            cometEmmiter.UpdateState();
        }
        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-diametr / 2, -diametr / 2, diametr, diametr);
            return path;
        }
    }
}
