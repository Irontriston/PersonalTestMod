using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;
using static Terraria.ModLoader.ModContent;

namespace PersonalMod.NeededProperties
{
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct ZenithHelper
	{
		public delegate void SpawnDustMethod(Vector2 centerPosition, float rotation, Vector2 velocity);

		public struct ZenithProfileType
		{
			public float trailWidth;

			public Color trailColor;

			public SpawnDustMethod dustMethod;

			public VertexStrip.StripColorFunction colorMethod;

			public VertexStrip.StripHalfWidthFunction widthMethod;

			public ZenithProfileType(float fullBladeLength, Color color)
			{
				trailWidth = fullBladeLength / 2f;
				trailColor = color;
				widthMethod = null;
				colorMethod = null;
				dustMethod = null;
				widthMethod = StripWidth;
				colorMethod = StripColors;
				dustMethod = StripDust;
			}

			private void StripDust(Vector2 centerPosition, float rotation, Vector2 velocity)
			{
				if (Main.rand.Next(9) == 0)
				{
					int num = Main.rand.Next(1, 4);
					for (int i = 0; i < num; i++)
					{
						Dust dust = Dust.NewDustPerfect(centerPosition, 278, null, 100, Color.Lerp(trailColor, Color.White, Main.rand.NextFloat() * 0.3f));
						dust.scale = 0.4f;
						dust.fadeIn = 0.4f + Main.rand.NextFloat() * 0.3f;
						dust.noGravity = true;
						dust.velocity += rotation.ToRotationVector2() * (3f + Main.rand.NextFloat() * 4f);
					}
				}
			}

			private Color StripColors(float progressOnStrip)
			{
				Color result = trailColor * (1f - Util_Properties.GetLerpValue(0f, 0.98f, progressOnStrip));
				result.A /= 2;
				return result;
			}

			private float StripWidth(float progressOnStrip)
			{
				return trailWidth;
			}
		}

		public const int TotalIllusions = 4;

		public const int FramesPerImportantTrail = 15;

		private static VertexStrip _vertexStrip = new VertexStrip();

		private static Dictionary<int, ZenithProfileType> _ZenithProfiles = new Dictionary<int, ZenithProfileType>
		{
			{
				46,
				new ZenithProfileType(48f, new Color(122, 66, 191))
			},
			{
				65,
				new ZenithProfileType(48f, new Color(236, 62, 192))
			},
			{
				121,
				new ZenithProfileType(76f, new Color(254, 158, 35))
			},
			{
				155,
				new ZenithProfileType(70f, new Color(56, 78, 210))
			},
			{
				190,
				new ZenithProfileType(70f, new Color(107, 203, 0))
			},
			{
				273,
				new ZenithProfileType(70f, new Color(179, 54, 201))
			},
			{
				368,
				new ZenithProfileType(70f, new Color(236, 200, 19))
			},
			{
				674,
				new ZenithProfileType(70f, new Color(236, 200, 19))
			},
			{
				675,
				new ZenithProfileType(70f, new Color(179, 54, 201))
			},
			{
				757,
				new ZenithProfileType(70f, new Color(80, 222, 122))
			},
			{
				795,
				new ZenithProfileType(70f, new Color(237, 28, 36))
			},
			{
				989,
				new ZenithProfileType(48f, new Color(91, 158, 232))
			},
			{
				1123,
				new ZenithProfileType(48f, Main.OurFavoriteColor)
			},
			{
				1826,
				new ZenithProfileType(76f, new Color(252, 95, 4))
			},
			{
				2880,
				new ZenithProfileType(70f, new Color(84, 234, 245))
			},
			{
				3063,
				new ZenithProfileType(76f, new Color(254, 194, 250))
			},
			{
				3065,
				new ZenithProfileType(70f, new Color(237, 63, 133))
			},
			{
				3018,
				new ZenithProfileType(80f, new Color(143, 215, 29))
			},
			{
				3507,
				new ZenithProfileType(45f, new Color(235, 166, 135))
			},
		};

		private static ZenithProfileType _defaultProfile = new ZenithProfileType(50f, Color.White);


		public void Draw(Projectile proj)
		{
			ZenithProfileType zenithProfile = GetZenithProfile((int)proj.ai[1]);
			MiscShaderData miscShaderData = GameShaders.Misc["FinalFractal"];
			int num = 4;
			int num2 = 0;
			int num3 = 0;
			int num4 = 4;
			miscShaderData.Apply();
			_vertexStrip.PrepareStrip(proj.oldPos, proj.oldRot, zenithProfile.colorMethod, zenithProfile.widthMethod, -Main.screenPosition + proj.Size / 2f, proj.oldPos.Length, includeBacksides: true);
			_vertexStrip.DrawTrail();
			Main.pixelShader.CurrentTechnique.Passes[0].Apply();
		}

		public static ZenithProfileType GetZenithProfile(int usedSwordId)
		{
			if (!_ZenithProfiles.TryGetValue(usedSwordId, out ZenithProfileType value))
			{
				return _defaultProfile;
			}
			return value;
		}

		private Color StripColors(float progressOnStrip)
		{
			Color result = Color.Lerp(Color.White, Color.Violet, Util_Properties.GetLerpValue(0f, 0.7f, progressOnStrip, clamped: true)) * (1f - Util_Properties.GetLerpValue(0f, 0.98f, progressOnStrip));
			result.A /= 2;
			return result;
		}

		private float StripWidth(float progressOnStrip)
		{
			return 50f;
		}
	}
}
