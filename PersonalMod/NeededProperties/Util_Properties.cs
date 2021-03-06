﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;


namespace PersonalMod.NeededProperties
{
	public static class Util_Properties
	{
		public static float GetLerpValue(float from, float to, float t, bool clamped = false)
		{
			if (clamped)
			{
				if (from < to)
				{
					if (t < from)
					{
						return 0f;
					}
					if (t > to)
					{
						return 1f;
					}
				}
				else
				{
					if (t < to)
					{
						return 1f;
					}
					if (t > from)
					{
						return 0f;
					}
				}
			}
			return (t - from) / (to - from);
		}
		public static double GetLerpValue(double from, double to, double t, bool clamped = false)
		{
			if (clamped)
			{
				if (from < to)
				{
					if (t < from)
					{
						return 0.0;
					}
					if (t > to)
					{
						return 1.0;
					}
				}
				else
				{
					if (t < to)
					{
						return 1.0;
					}
					if (t > from)
					{
						return 0.0;
					}
				}
			}
			return (t - from) / (to - from);
		}
		public static bool FloatIntersect(float r1StartX, float r1StartY, float r1Width, float r1Height, float r2StartX, float r2StartY, float r2Width, float r2Height)
		{
			if (r1StartX > r2StartX + r2Width || r1StartY > r2StartY + r2Height || r1StartX + r1Width < r2StartX || r1StartY + r1Height < r2StartY)
			{
				return false;
			}
			return true;
		}
		public static float Distance(this Rectangle r, Vector2 point)
		{
			if (FloatIntersect(r.Left, r.Top, r.Width, r.Height, point.X, point.Y, 0f, 0f))
			{
				return 0f;
			}
			if (point.X >= (float)r.Left && point.X <= (float)r.Right)
			{
				if (point.Y < (float)r.Top)
				{
					return (float)r.Top - point.Y;
				}
				return point.Y - (float)r.Bottom;
			}
			if (point.Y >= (float)r.Top && point.Y <= (float)r.Bottom)
			{
				if (point.X < (float)r.Left)
				{
					return (float)r.Left - point.X;
				}
				return point.X - (float)r.Right;
			}
			if (point.X < (float)r.Left)
			{
				if (point.Y < (float)r.Top)
				{
					return Vector2.Distance(point, r.TopLeft());
				}
				return Vector2.Distance(point, r.BottomLeft());
			}
			if (point.Y < (float)r.Top)
			{
				return Vector2.Distance(point, r.TopRight());
			}
			return Vector2.Distance(point, r.BottomRight());
		}
		public static float Distance(this Vector2 Origin, Vector2 Target)
		{
			return Vector2.Distance(Origin, Target);
		}
		public static Vector2 DirectionTo(this Vector2 Origin, Vector2 Target)
		{
			return Vector2.Normalize(Target - Origin);
		}
		public static Vector2 RotTransform(Vector2 RotCenter, Vector2 slope, Vector2 position)
		{
			Vector2 PosDirections = RotCenter; // Basically the center's original position.
			Vector2 RotPos = position - PosDirections;

			double Rads = Math.Atan(slope.Y / slope.X) * (Math.PI / 180);  //These three steps combined are needed as Cosine and Sine use Radians.

			double CosValue = Math.Cos(Rads);
			double SinValue = Math.Sin(Rads);

			Vector2 RotAdjust = new Vector2((float)((RotPos.X * CosValue) + (RotPos.Y * -SinValue))
										   ,(float)((RotPos.X * SinValue) + (RotPos.Y *  CosValue)));

			position = RotAdjust + PosDirections;
			return position;
		}
		public static Vector2 RotPerc( Vector2 slope)
		{
			double SlopeValue = slope.Y / slope.X;   //Reversed so as to not confuse modders when defining slope
			double Degrees = Math.Atan(SlopeValue);
			double Rads = Degrees * (Math.PI / 180);  //These three steps are needed as Cosine and Sine use Radians.

			double CosValue = Math.Cos(Rads);
			double SinValue = Math.Sin(Rads);

			Vector2 RotAdjust = new Vector2(1,0);

			RotAdjust = new Vector2((float)((RotAdjust.X * CosValue) - (RotAdjust.Y * SinValue))
								   ,(float)((RotAdjust.X * SinValue) + (RotAdjust.Y * CosValue)));
			return RotAdjust;
		}
		public static float LinterpValue(Vector2 StartPoint, Vector2 EndPoint)
		{
			return EndPoint.Y - StartPoint.Y / EndPoint.X - StartPoint.X;
		}
		public static float DistanceFloat(Vector2 StartPoint, Vector2 EndPoint)
		{
			double XDistance = Math.Pow(EndPoint.X - StartPoint.X, 2);
			double YDistance = Math.Pow(EndPoint.Y - StartPoint.Y, 2);
			float Distance = (float)Math.Sqrt(XDistance + YDistance);
			return Distance;
		}
		public static Vector2 DistanceVector(Vector2 StartPoint, Vector2 EndPoint)
		{
			return new Vector2(EndPoint.X - StartPoint.X, EndPoint.Y - StartPoint.Y);
		}

	}
}