using UnityEngine;

namespace Code
{
    public class CollisionCore
    {
        public static bool HitSides(Collider2D col, Collision2D collision2D)
        {
            Side s = SideOfHit(collision2D);
            return (s == Side.Left || s ==  Side.Right);
        }

        private static ContactPoint2D[] _contactPoint2DsBuffer = new ContactPoint2D[10];
        public static Side SideOfHit(Collision2D collision2D)
        {
            Side retVal = Side.None;
            int count = collision2D.GetContacts(_contactPoint2DsBuffer);
            for (int i = 0; i < count; i++)
            {
                ContactPoint2D c2d = _contactPoint2DsBuffer[i];
                retVal |= SideOfHit(c2d.normal);
            }
            return retVal;
        }

        public static Side SideOfHit(Vector3 normal)
        {
            float angle = Vector3.Angle(normal, Vector3.up);
            if (MathUtilities.Approximately(angle, 0))
            {
                return Side.Bottom;
            }

            if (MathUtilities.Approximately(angle, 180))
            {
                return Side.Top;
            }

            if (MathUtilities.Approximately(angle, 90))
            {
                Vector3 cross = Vector3.Cross(Vector3.up, normal);
                if (cross.y > 0)
                {
                    return Side.Left;
                }
                else
                {
                    return Side.Right;
                }
            }

            return Side.None;
        }

    }
}