// using System;
// using System.IO;
// using System.Linq;

// namespace AOC22
// {
//     public class Vector2
//     {
//         public float x;
//         public float y;

//         public Vector2(float newX=0, float newY=0)
//         {
//             x = newX;
//             y = newY;
//         }

//         public static Vector2 operator +(Vector2 v1, Vector2 v2)
//         {
//             return new Vector2(v1.x + v2.x, v1.y + v2.y);
//         }

//         public static Vector2 operator -(Vector2 v1, Vector2 v2)
//         {
//             return new Vector2(v1.x - v2.x, v1.y - v2.y);
//         }

//         public static Vector2 operator *(Vector2 v1, Vector2 v2)
//         {
//             return new Vector2(v1.x * v2.x, v1.y * v2.y);
//         }

//         public static Vector2 operator /(Vector2 v1, Vector2 v2)
//         {
//             return new Vector2(v1.x / v2.x, v1.y / v2.y);
//         }

//         public static bool operator ==(Vector2 v1, Vector2 v2)
//         {
//             return (v1.x == v2.x && v1.y == v2.y);
//         }
        
//         public static bool operator !=(Vector2 v1, Vector2 v2)
//         {
//             return !(v1.x == v2.x && v1.y == v2.y);
//         }

//         // override object.Equals
//         public override bool Equals(object? obj)
//         {
//             if (obj == null || GetType() != obj.GetType())
//             {
//                 return false;
//             }
//             return this == (Vector2)obj;
//         }
        
//         // override object.GetHashCode
//         public override int GetHashCode()
//         {
//             return base.GetHashCode();
//         }

//         public override string ToString()
//         {
//             return $"{x.ToString()},{y.ToString()}";
//         }

//         public Vector2 Rotate(this Vector2 vec, float angle)
//         {
//             float ca = (float)Math.Cos(angle * (Math.PI/180));
//             float sa = (float)Math.Sin(angle * (Math.PI/180));
//             return new Vector2(ca*vec.x - sa*vec.y, sa*vec.x + ca*vec.y);
//         }
//     }
// }