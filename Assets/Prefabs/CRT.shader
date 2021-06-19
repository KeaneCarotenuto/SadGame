Shader "Unlit/CRT"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 copyCol = col;

                i.uv.x *= 1080;
                i.uv.y *= 1920;

                int fixed = 10;
                float div = 0.3;


                if (int(i.vertex.x) % fixed == 0) {
                    col = fixed4(0, 0, 0, 1);
                }
                else if (int(i.vertex.x) % fixed <= 3) {
                    col = fixed4(col.x, 0, 0, 1);
                }
                else if (int(i.vertex.x) % fixed <= 6) {
                    col = fixed4(0, col.y, 0, 1);
                }
                else if (int(i.vertex.x) % fixed <= 9) {
                    col = fixed4(0, 0, col.z, 1);
                }
                

                if (int(i.vertex.y) % 10 == 3) {
                    col = fixed4(0, 0, 0, 1);
                }

                col += copyCol * 0.75;

                return col;
            }
            ENDCG
        }
    }
}
