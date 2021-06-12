// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

 // Unlit alpha-blended shader.
 // - no lighting
 // - no lightmap support
 // - no per-material color
 
 Shader "Unlit/Transparent" {
 Properties {
     _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
     _MorphSpeedX ("Texture morph speed x", Range(1, 5)) = 1.3
     _MorphSpeedY ("Texture morph speed y", Range(1, 5)) = 2.7
     _MorphDistance("Texture morph distance", Range(0.005, 0.05)) = 0.025
 }
 
 SubShader {
     Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
     LOD 100
     
     ZWrite Off
     Blend SrcAlpha OneMinusSrcAlpha 
     
     Pass {  
         CGPROGRAM
             #pragma vertex vert
             #pragma fragment frag
             #pragma multi_compile_fog
             
             #include "UnityCG.cginc"
 
             struct appdata_t {
                 float4 vertex : POSITION;
                 float2 texcoord : TEXCOORD0;
             };
 
             struct v2f {
                 float4 vertex : SV_POSITION;
                 half2 texcoord : TEXCOORD0;
                 UNITY_FOG_COORDS(1)
             };
 
             sampler2D _MainTex;
             float4 _MainTex_ST;
             float _MorphDistance;
             float _MorphSpeedX;
             float _MorphSpeedY;
             
             v2f vert (appdata_t v)
             {
                 v2f o;
                //  half2 offset = half2(sin(v.texcoord.y) * 0.1, 0);
                 o.vertex = UnityObjectToClipPos(v.vertex);
                 o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);

                 o.texcoord.x += sin((o.texcoord.x+o.texcoord.y)*8 + _Time.g*_MorphSpeedX)*_MorphDistance;
                 o.texcoord.y += cos((o.texcoord.x-o.texcoord.y)*8 + _Time.g*_MorphSpeedY)*_MorphDistance;

                 UNITY_TRANSFER_FOG(o,o.vertex);
                 return o;
             }
             
             fixed4 frag (v2f i) : SV_Target
             {

                 fixed4 col = tex2D(_MainTex, i.texcoord);
                 col.a *= 0.5;
                 UNITY_APPLY_FOG(i.fogCoord, col);
                 return col;
             }
         ENDCG
     }
 }
 
 }