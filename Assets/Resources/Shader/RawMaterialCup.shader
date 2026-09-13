Shader "Custom/OutlineExtrude"
{
    Properties
    {
        _MainTex ("_MainTexture", 2D) = "white" {}
        _BaseColor ("_MainColor", Color) = (1,1,1,1)
        _OutlineColor ("_OutlineColor", Color) = (0,0,0,1)
        _OutlineWidth ("_OutlineWidth", Range(0, 0.1)) = 0.02
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry" }
        LOD 100

        // ========== Pass 1: 描边层 ==========
        Pass
        {
            Cull Front   // 剔除正面，只渲染背面
            ZWrite On

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            fixed4 _OutlineColor;
            float _OutlineWidth;

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                // 顶点沿法线方向向外挤出
                float3 expandedPos = v.vertex.xyz + v.normal * _OutlineWidth;
                o.pos = UnityObjectToClipPos(expandedPos);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return _OutlineColor;
            }
            ENDCG
        }

        // ========== Pass 2: 主体正常渲染 ==========
        Pass
        {
            Cull Back
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _BaseColor;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv) * _BaseColor;
                return col;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
