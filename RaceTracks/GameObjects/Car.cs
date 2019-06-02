using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Racetracks
{
    class Car : Body
    {
        private float AccelerationForce = 5f;
        private float RotationForce = 3f;
        private float DriftForce = 0.5f;

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
            Vector2 TempVelocity = Vector2.SmoothStep(Velocity, Forward * Velocity.Length(), DriftForce);
            // added clamp for max speed
            velocity.X = MathHelper.Clamp(TempVelocity.X, -300, 300);
            velocity.Y = MathHelper.Clamp(TempVelocity.Y, -300, 300);
        }

        /// <summary>Handle user input for this Car</summary>        
        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);
            // add corresponding keys to add forces to car     
            if (inputHelper.IsKeyDown(Keys.Left) || inputHelper.IsKeyDown(Keys.A))
            { 
                AddAngularForce(-RotationForce);
            }
            if (inputHelper.IsKeyDown(Keys.Right) || inputHelper.IsKeyDown(Keys.D))
            { 
                AddAngularForce(RotationForce);
            }
            if (inputHelper.IsKeyDown(Keys.Up) || inputHelper.IsKeyDown(Keys.W))
            {
                AddForce(Forward * AccelerationForce);
            }
            if (inputHelper.IsKeyDown(Keys.Down) && inputHelper.IsKeyDown(Keys.S))
            {
                AddForce(-Forward * AccelerationForce);
            }
        }
    }
}
