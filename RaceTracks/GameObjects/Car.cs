using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Racetracks
{
    class Car : Body
    { 
        /// <summary>Creates a user controlled Car</summary>        
        public Car(Vector2 position) : base(position, "car")
        {
            offsetDegrees = -90;
        }

        /// <summary>Updates this Car</summary>        
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            // add smooth step to enhance driving experience
            Velocity = Vector2.SmoothStep(Velocity, Forward * Velocity.Length(), 0.5f);
        }

        /// <summary>Handle user input for this Car</summary>        
        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            // add corresponding keys to add forces to car     
            if (inputHelper.IsKeyDown(Keys.Left) || inputHelper.IsKeyDown(Keys.A))
                this.AddAngularForce(-3f);
            if (inputHelper.IsKeyDown(Keys.Right) || inputHelper.IsKeyDown(Keys.D))
                this.AddAngularForce(3f);
            if (inputHelper.IsKeyDown(Keys.Up) || inputHelper.IsKeyDown(Keys.W))
                this.AddForce(Forward * 5f);
            if (!inputHelper.IsKeyDown(Keys.Down) && !inputHelper.IsKeyDown(Keys.S))
                return;
            this.AddForce(-Forward * 5f);
        }
    }
}
