Shader "Pieka/MaskShader"
{
	Properties
	{ 
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		[PerRendererData] _Rotation ("_Rotation", Float) = 3.14
		_MaskTex ("Mask Texture", 2D) = "black" {}
		_Color ("Tint", Color) = (1,1,1,1)  
		_ShadowPower ("ShadowPower", Range( 0, 2 )) = 0.5
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
	}
	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 2.0
			#pragma multi_compile _ PIXELSNAP_ON
			#pragma multi_compile _ ETC1_EXTERNAL_ALPHA
			#include "UnityCG.cginc"
			
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				float2 texcoord  : TEXCOORD0;
				float2 maskcoord : TEXCOORD1;
				UNITY_VERTEX_OUTPUT_STEREO
			};
			
			float _ShadowPower;
			fixed4 _Color;
			float _Rotation;

			sampler2D _MainTex;
			sampler2D _MaskTex;
			sampler2D _AlphaTex;
			fixed4 _MainTex_ST;


			v2f vert(appdata_t IN)
			{
				v2f OUT;
				UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.maskcoord = IN.texcoord; 


				_Rotation = -_Rotation * 3.1415 / 180;

				float s = sin ( _Rotation );
                float c = cos ( _Rotation );
                float2x2 rotationMatrix = float2x2( c, -s, s, c);



				OUT.color = IN.color * _Color;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif

				float offsetX = .5; //_MainTex_ST.z +_MainTex_ST.x / 2;
                float offsetY = .5; //_MainTex_ST.w +_MainTex_ST.y / 2;
 
                float x = IN.texcoord.x - offsetX; //* _MainTex_ST.x + _MainTex_ST.z - offsetX;
                float y = IN.texcoord.y - offsetY; //* _MainTex_ST.y + _MainTex_ST.w - offsetY;
 
                OUT.texcoord = mul (float2(x, y), rotationMatrix ) + float2(offsetX, offsetY);
          //     	OUT.maskcoord = mul (float2(x, y), rotationMatrix ) + float2(offsetX, offsetY);
 

				return OUT;
			}



			fixed4 SampleSpriteTexture (float2 uv)
			{
				fixed4 color = tex2D (_MainTex, uv);

				#if ETC1_EXTERNAL_ALPHA
				// get the color from an external texture (usecase: Alpha support for ETC1 on android)
				color.a = tex2D (_AlphaTex, uv).r;
				#endif //ETC1_EXTERNAL_ALPHA

				return color;
			}

			fixed4 frag(v2f IN) : SV_Target
			{ 

				fixed4 mask = tex2D(_MaskTex, IN.texcoord);

				mask = mask - 0.5;

				fixed4 c = SampleSpriteTexture (IN.maskcoord) * IN.color;
 


			//	col.a = c.a;
			//	col.rgb *= c.a;

				fixed4 col = c + (mask*_ShadowPower);
				col.a = c.a;
				col.rgb *= c.a;
				return col;
			}
		ENDCG
		}
	}
}
