Shader "Custom/HealthBar"
{
    Properties
    {
        _MainTex("Sprite Texture", 2D) = "white" {}
        _FillAmount("Fill Amount", Range(0, 1)) = 1.0
    }
        SubShader
        {
            Tags {"Queue" = "Transparent" "RenderType" = "Transparent"}
            Pass
            {
                Blend SrcAlpha OneMinusSrcAlpha
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct appdata_t
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
                float _FillAmount; // 체력 비율

                v2f vert(appdata_t v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    fixed4 col = tex2D(_MainTex, i.uv);

                // 체력 비율보다 오른쪽이면 투명하게 만듦
                if (i.uv.x > _FillAmount)
                {
                    col.a = 0;
                }

                return col;
            }
            ENDCG
        }
        }
}
