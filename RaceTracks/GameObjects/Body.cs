using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Racetracks
{
    class Body : RotatingSpriteGameObject
    {
        protected float radius;
        private Vector2 acceleration = Vector2.Zero;
        private float invMass = 1.0f; //set indirectly by setting 'mass'
        // add drag and angular velocity, acceleration
        public float drag = 0.99f;
        private float angularVelocity;
        private float angularAcceleration;
        private float angularDrag = 0.90f;
        private bool rotation = true;

        /// <summary>Creates a physics body</summary>
        public Body(Vector2 position, string assetName) : base(assetName)
        {
            Vector2 size = new Vector2(Width, Height); //circle collider will fit either width or height
            radius = Math.Max(Width, Height) / 2.0f;
            Mass = (radius * radius); //mass approx.
            origin = Center;
            this.position = position;
        }

        /// <summary>Updates this Body</summary>        
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            acceleration *= Mass;
            // add speed according to physics
            Velocity = Velocity + acceleration;
            acceleration = Vector2.Zero;
            // add the drag
            Velocity = Velocity * drag;
            // check if rotation is possible
            if (rotation == true)
            {   
                Angle += angularVelocity;
                angularVelocity += angularAcceleration;
                angularAcceleration = 0.0f;
                angularVelocity *= angularDrag;
            }     
        }

        public void AddAngularForce(float force)
        {
            angularAcceleration += MathHelper.ToRadians(force * 0.1f);
        }

        public void AddForce(Vector2 force)
        {
            acceleration += force;
        }

        public void CollisionHandling(Body otherBody)
        {
            // 
            Vector2 Direction = position - otherBody.position;
            Direction.Normalize();
            Direction *= (position - otherBody.position).Length() - radius - otherBody.radius;
            // change the objects accordingly
            velocity -= (Direction / 2) * (Mass * invMass);
            otherBody.velocity += (Direction / 2) * (Mass * invMass);
        }

        public bool CollisionCheck(Body otherBody)
        {
            // get the distance diffrence
            Vector2 positionDifference = position - otherBody.position;
            float Difference = positionDifference.LengthSquared() - ((radius + otherBody.radius) * (radius + otherBody.radius));
            positionDifference.Normalize();
            // if collide
            if (Difference < 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>Returns closest point on this shape</summary>        
        public Vector2 GetClosestPoint(Vector2 point)
        {
            Vector2 delta = point - position;
            if (delta.LengthSquared() > radius * radius)
            {
                delta.Normalize();
                delta *= radius;
            }
            return position + delta;
        }

        /// <summary>Sets the invMass to 0.0</summary>        
        protected void MakeStatic()
        {
            invMass = 0.0f; //making mass infinite
        }

        /// <summary>Get or set the invMass through setting the mass</summary>        
        private float _mass = 1.0f;
        public float Mass
        {
            get
            {
                return _mass;
            }
            set
            {
                _mass = value;
                invMass = 1.0f / value; //note: float div. by zero will give infinite invMass
            }
        }                
        
        /// <summary>Get or set the angle by getting/setting the forward Vec2</summary>
        public Vector2 Forward
        {
            get
            {
                return new Vector2((float)Math.Cos(Angle), (float)Math.Sin(Angle)); //angle to polar
            }

            set
            {
                Angle = (float)Math.Atan2(value.Y, value.X); //polar to angle
            }
        }

        /// <summary>Get or set the angle by getting/setting the right Vec2</summary>
        public Vector2 Right
        {
            get
            {
                return new Vector2((float)-Math.Sin(Angle), (float)Math.Cos(Angle)); //angle to polar
            }

            set
            {
                Angle = (float)Math.Atan2(-value.X, value.Y); //polar to angle
            }
        }
        
    }
}
