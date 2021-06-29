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

                //Size of the CRT pixels (minimum: 4)
                int cellSize = 10;

                //Split into RGB cells and determine brightness by the pixel that should be there
                if (int(i.vertex.x) % cellSize <= (cellSize - 1) / 3) {
                    col = fixed4(col.x, 0, 0, 1);
                }
                else if (int(i.vertex.x) % cellSize <= 2 * (cellSize - 1) / 3) {
                    col = fixed4(0, col.y, 0, 1);
                }
                else if (int(i.vertex.x) % cellSize <= 3 * (cellSize - 1) / 3) {
                    col = fixed4(0, 0, col.z, 1);
                }
                
                //Add back some original colour
                col += copyCol * 0.75;

                //Clear black lines between cells (horiz and vert)
                if (int(i.vertex.x) % cellSize == 0) {
                    col = fixed4(0, 0, 0, 1);
                }
                if (int(i.vertex.y) % cellSize == 0) {
                    col = fixed4(0, 0, 0, 1);
                }

                //Scan line kind of thing
                int x = int(_Time.y * 1920) % 3000;
                int width = 2;
                if ( int(x/ width)* width == int(i.vertex.x/ width)* width) {
                    col = fixed4(0, 0, 0, 1);
                }

                return col;
            }
            ENDCG
        }
    }
}
