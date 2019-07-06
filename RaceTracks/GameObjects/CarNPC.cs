using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Racetracks
{
    class CarNPC : Body
    {       
        private Waypoints waypoints;
        private float offset;
        private float speed;
        private const float SteeringFactor = 0.1f;
        private const float SpeedMultiplier = 6f;
        private float maxSpeed = 300.0f;

        /// <summary>Creates a waypoint driven Car</summary>        
        public CarNPC(Vector2 position, float speed, float offset) : base(position, "car2")
        {
            offsetDegrees = -90;
            waypoints = new Waypoints();
            this.offset = offset;
            this.speed = speed;
            // add drag to counter cars going out of turns
            this.drag = 0.9f;
        }
        
        /// <summary>Updates this Car</summary>        
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Vector2 target = waypoints.GetTarget(position); //read from 'Tiled' data
            target.Y += offset; //so cars don't follow same track

            // get direction by target minus car pos
            Vector2 vector2 = target - position;
            vector2.Normalize();

            // get direction and slowly transition to direction
            float direction = Vector2.Dot(Forward, vector2);
            Forward = Vector2.SmoothStep(Forward, vector2, SteeringFactor);

            // add the force to the npc car
            AddForce(Forward * speed * direction * SpeedMultiplier);

            if (velocity.LengthSquared() > maxSpeed * maxSpeed)
            {

                velocity.Normalize();
                velocity *= maxSpeed;

            }
        }
    }
}
