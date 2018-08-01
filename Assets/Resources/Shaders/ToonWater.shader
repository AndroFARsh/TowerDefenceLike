Shader "Custom/ToonWater"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_TintColor ("Tint Color", Color) = (1,1,1,1)
		_Amplituda ("Amplituda", Range(0, 5)) = 1
		_FrequnceX ("Frequnce X", Range(0, 5)) = 1
		_FrequnceY ("Frequnce Y", Range(0, 5)) = 1
		
		_Transparency ("Transparency", Range(0, 1)) = 0.7
		_TexStrengs ("Texture Strengs", Range(0, 1)) = 0.5
		_TexSpeedU ("Texture Speed U", Range(-1, 1)) = 0.1
		_TexSpeedV ("Texture Speed V", Range(-1, 1)) = 0.1
	}
	SubShader
	{
	    Tags {"Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
	
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4    _TintColor;

			float     _Amplituda;
		    float     _FrequnceX;
		    float     _FrequnceY;
		    float     _Transparency;
		    float     _TexStrengs;
            float     _TexSpeedU;
            float     _TexSpeedV; 
    
			v2f vert (appdata v)
			{
				v2f o;
				v.vertex.y += cos(_Time.y + v.vertex.x * _FrequnceX) *
				              sin(_Time.y + v.vertex.z * _FrequnceY) *
				              _Amplituda;
                v.uv = float2(v.uv.x + _SinTime.y * _TexSpeedU, v.uv.y + _CosTime.y * _TexSpeedV);				                
				
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = _TintColor + tex2D(_MainTex, i.uv) * _TexStrengs;
				col.a = _Transparency; 
				return col;
			}
			ENDCG
		}
	}
}
